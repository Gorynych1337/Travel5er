using System.IO;
using UnityEngine;

public class Character
{
    private short _id;
    public short Id => _id;
    private string _name;
    public string Name => _name;

    private byte[] _image;
    public byte[] Image => _image;

    private short _owner;
    public short Owner => _owner;
        
    public Character(short id, string name, byte[] image, short ownerId)
    {
        _id = id;
        _name = name;
        _image = image;
        _owner = ownerId;
    }
}