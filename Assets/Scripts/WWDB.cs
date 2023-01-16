using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using MainMenu.Campaigns.EncounterList;
using Npgsql;
using UnityEngine;

public class WWDB
{
    private NpgsqlConnection con;

    private static Lazy<WWDB> _instance = new Lazy<WWDB>(() => new WWDB());
    public static WWDB Instance => _instance.Value;

    public delegate void DbChanged();
    [CanBeNull] public static event DbChanged CharactersChanged;

    public delegate void DbError(string error);
    [CanBeNull]public static event DbError ConnectionBreak;

    private WWDB()
    {
        string connectionString =
            "Host=localhost;" +
            "Port=5432;" +
            "Database=UnityDB;" +
            "User ID=postgres;" +
            "Password=admin;";

        con = new NpgsqlConnection(connectionString);
    }

    private object Connection( Func<object> action)
    {
        object res = null;
        try
        {
            con.Open();
            res = action();
        }
        catch (Exception e)
        {
            ConnectionBreak?.Invoke(e is NpgsqlException ? "Ошибка, перепроверьте данные и повторите попытку" : e.Message);
        }
        finally
        {
            con.Close();
        }

        return res;
    }
    
    private object Transaction( Func<object> action)
    {
        con.Open();
        var trunc = con.BeginTransaction();
        object res = null;
        try
        {
            res = action();
            trunc.Commit();
        }
        catch (Exception e)
        {
            trunc.Rollback();
            if (e is NpgsqlException)
            {
                    Debug.Log("Ошибка БД, повторите попытку");
            }
        }
        finally
        {
            con.Close();
        }

        return res;
    }
        
    public Tuple<short, bool?> Login(string login, string password)
    {
        return (Tuple<short, bool?>)Connection(() =>
        {
            NpgsqlCommand cmd = con.CreateCommand();

            string sql =
                $"SELECT id, \"IsAdmin\" " +
                "FROM \"TokenMap\".\"Users\" " +
                $"WHERE \"Nickname\" = '{login}' AND \"Password\" = '{password}'";
            cmd.CommandText = sql;

            var reader = cmd.ExecuteReader();
            if (!reader.Read())
                throw new NpgsqlException();

            return new Tuple<short, bool?>(reader.GetInt16(0), reader.GetBoolean(1));
        });
    }

    public List<Character> GetCharactersList()
    {
        return (List<Character>) Connection(() =>
        {
            List<Character> CharacterList = new List<Character>();
                
            NpgsqlCommand cmd = con.CreateCommand();
            string sql =
                $"SELECT id, \"Name\", \"Token\", \"Owner\" " +
                "FROM \"TokenMap\".\"Characters\" ";
            cmd.CommandText = sql;

            var rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                CharacterList.Add(new Character(rdr.GetInt16(0),
                                                    rdr.GetString(1),
                                                    rdr.GetFieldValue<byte[]>(2),
                                                    rdr.GetInt16(3)));
            }
                
            return CharacterList;
        });
    }

    public short SaveCharacter(string name, byte[] image, short ownerId)
    {
        short result = (short) Connection(() =>
        {
            NpgsqlCommand cmd = con.CreateCommand();
            string sql =
                "INSERT INTO \"TokenMap\".\"Characters\" (\"Name\", \"Token\", \"Owner\") " +
                $"VALUES ('{name}', @Image, {ownerId}) " +
                $"RETURNING id";
            cmd.CommandText = sql;
                
            NpgsqlParameter par = cmd.CreateParameter();
            par.ParameterName = "@Image";
            par.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bytea;
            par.Value = image;
            cmd.Parameters.Add(par);

            return cmd.ExecuteScalar();
        });
        
        CharactersChanged?.Invoke();

        return result;
    }

    public void UpdateCharacter(short id, string name, byte[] image)
    {
        Connection(() =>
        {
            NpgsqlCommand cmd = con.CreateCommand();
            string sql =
                "UPDATE \"TokenMap\".\"Characters\" " +
                $"SET \"Name\" = '{name}', \"Token\" = @Image " +
                $"WHERE id = {id}";
            cmd.CommandText = sql;
                
            NpgsqlParameter par = cmd.CreateParameter();
            par.ParameterName = "@Image";
            par.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bytea;
            par.Value = image;
            cmd.Parameters.Add(par);

            cmd.ExecuteNonQuery();
            
            return true;
        });
        
        CharactersChanged?.Invoke();
    }

    public void DeleteCharacter(short id)
    {
        Connection(() =>
        {
            NpgsqlCommand cmd = con.CreateCommand();
            string sql =
                "DELETE FROM \"TokenMap\".\"Characters\" " +
                $"WHERE id = {id}";
            cmd.CommandText = sql;

            cmd.ExecuteNonQuery();
            
            return true;
        });
        
        CharactersChanged?.Invoke();
    }

    public List<Campaign> GetCampaignList(short ownerId)
    {
        return (List<Campaign>) Connection(() =>
        {
            List<Campaign> campaignsList = new List<Campaign>();
                
            NpgsqlCommand cmd = con.CreateCommand();
            string sql =
                "SELECT id, \"Name\", \"OwnerId\" " +
                "FROM \"TokenMap\".\"Campaigns\" " +
                $"WHERE \"OwnerId\" = {ownerId}";
            cmd.CommandText = sql;

            var rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                campaignsList.Add(new Campaign(rdr.GetInt16(0),
                    rdr.GetString(1),
                    rdr.GetInt16(2)));
            }
                
            return campaignsList;
        });
    }

    public short AddCampaign(string name, short ownerId)
    {
        short result = (short) Connection(() =>
        {
            NpgsqlCommand cmd = con.CreateCommand();
            string sql =
                "INSERT INTO \"TokenMap\".\"Campaigns\" (\"Name\", \"OwnerId\") " +
                $"VALUES ('{name}', {ownerId}) " +
                $"RETURNING id";
            cmd.CommandText = sql;

            return cmd.ExecuteScalar();
        });

        return result;
    }
    
    /// <summary>
    /// Save encounter and characters on map
    /// </summary>
    /// <param name="name">name of encounter</param>
    /// <param name="campaignId">id campaign of encounter</param>
    /// <param name="map">map image source</param>
    /// <param name="characters">characterId, plate from left, plate from top</param>
    /// <param name="height">count of plates by y</param>
    /// <param name="width">count of plates by x</param>
    public void SaveEncounter(short encounterId, List<Tuple<short, Vector2>> characters)
    {
        Transaction(() =>
        {
            DeleteEncounterCharacters(encounterId);
            
            foreach (var item in characters)
            {
                (short id, Vector2 position) = item;
                
                NpgsqlCommand cmd = con.CreateCommand();
                string sql = $"INSERT INTO \"TokenMap\".\"Encounter-Character\" " +
                             $"(\"CharacterId\", \"EncounterId\", \"Height\", \"Width\") " +
                             $"VALUES ('{id}', {encounterId}, '{position.y}', '{position.x}') ";
                cmd.CommandText = sql;
                
                cmd.ExecuteNonQuery();
            }

            return true;
        });
    }

    public void DeleteEncounter(short id)
    {
        Connection(() =>
        {
            NpgsqlCommand cmd = con.CreateCommand();
            string sql =
                "DELETE FROM \"TokenMap\".\"Encounters\" " +
                $"WHERE id = {id}";
            cmd.CommandText = sql;

            cmd.ExecuteNonQuery();
            
            return true;
        });
    }

    private void DeleteEncounterCharacters(int encounterId)
    {
        NpgsqlCommand cmd = con.CreateCommand();
        string sqlDel = $"DELETE FROM \"TokenMap\".\"Encounter-Character\" " +
                        $"WHERE \"EncounterId\" = {encounterId}";
        cmd.CommandText = sqlDel;
                
        cmd.ExecuteNonQuery();
    }
    
    public short AddEncounter(string name, short campaignId, byte[] map, Vector2 size)
    {
        return (short)Connection(() =>
        {
            NpgsqlCommand cmd = con.CreateCommand();
            string sql =
                "INSERT INTO \"TokenMap\".\"Encounters\" " +
                "(\"Name\", \"CampaignId\", \"BackgroundImage\", \"Height\", \"Width\") " +
                $"VALUES ('{name}', {campaignId}, @Image, '{size.y}', '{size.x}') " +
                $"RETURNING id";
            cmd.CommandText = sql;
                
            NpgsqlParameter par = cmd.CreateParameter();
            par.ParameterName = "@Image";
            par.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bytea;
            par.Value = map;
            cmd.Parameters.Add(par);

            return (short)cmd.ExecuteScalar();
        });
    }
    
    public List<Tuple<short, string>> GetEncounterList(short campaignId)
    {
        return (List<Tuple<short, string>>) Connection(() =>
        {
            List<Tuple<short, string>> campaignsList = new List<Tuple<short, string>>();
                
            NpgsqlCommand cmd = con.CreateCommand();
            string sql =
                "SELECT id, \"Name\" " +
                "FROM \"TokenMap\".\"Encounters\" " +
                $"WHERE \"CampaignId\" = {campaignId}";
            cmd.CommandText = sql;

            var rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                campaignsList.Add(new Tuple<short, string>(rdr.GetInt16(0), rdr.GetString(1)));
            }
                
            return campaignsList;
        });
    }

    public Encounter GetEncounter(short id)
    {
        return (Encounter)Connection(() =>
        {
            NpgsqlCommand cmd = con.CreateCommand();
            string sql =
                "SELECT \"Name\", \"CampaignId\", \"BackgroundImage\", \"Height\", \"Width\" " +
                "FROM \"TokenMap\".\"Encounters\" " +
                $"WHERE id = {id}";
            cmd.CommandText = sql;

            var reader = cmd.ExecuteReader();
            if (!reader.Read())
                throw new NpgsqlException();

            return new Encounter(id,
                reader.GetString(0),
                reader.GetInt16(1),
                reader.GetFieldValue<byte[]>(2),
                reader.GetInt16(3),
                reader.GetInt16(4));
        });
    }

    public List<Tuple<Character, Vector2>> GetEncounterCharacters(short encounterId)
    {
        return (List<Tuple<Character, Vector2>>)Connection(() =>
        {
            List<Tuple<Character, Vector2>> charactersTuple = new List<Tuple<Character, Vector2>>();
            
            NpgsqlCommand cmd = con.CreateCommand();
            string sql =
                "SELECT \"Char\".\"id\", \"Char\".\"Name\", \"Char\".\"Token\", \"Char\".\"Owner\", \"E-C\".\"Width\", \"E-C\".\"Height\" " +
                "FROM \"TokenMap\".\"Characters\" as \"Char\" inner join \"TokenMap\".\"Encounter-Character\" as \"E-C\" " +
                "ON \"Char\".id = \"E-C\".\"CharacterId\" " +
                $"WHERE \"E-C\".\"EncounterId\" = {encounterId}";
            cmd.CommandText = sql;

            var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                charactersTuple.Add(
                    new Tuple<Character, Vector2>(
                        new Character(
                            rdr.GetInt16(0),
                            rdr.GetString(1),
                            rdr.GetFieldValue<byte[]>(2),
                            rdr.GetInt16(3)),
                        new Vector2(
                            rdr.GetInt16(4),
                            rdr.GetInt16(5))));
            }
            return charactersTuple;
        });
    }
}