using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static EncounterScene.Grid;
using static EncounterScene.GridControl;

namespace EncounterScene
{
    public class EncounterManager
    {
        private static Lazy<EncounterManager> _instance = new Lazy<EncounterManager>(() => new EncounterManager());
        public static EncounterManager Instance => _instance.Value;

        private GameObject _lastToken;
        private Stack<GameObject> _deletedObjects = new Stack<GameObject>();
        private List<GameObject> _characters = new List<GameObject>();

        public void SetLastToken(GameObject token)
        {
            _lastToken = token;
        }

        public void AddToken(GameObject token)
        {
            _characters.Add(token);
        }

        public void Save()
        {
            List<Tuple<short, Vector2>> characters = new List<Tuple<short, Vector2>>();
            
            _characters.ForEach((item) =>
            {
                Vector2 coord = GetPlateByCoordinates(item.transform.position,
                    EncounterData.Instance.Size, PlateSize);
                characters.Add(new Tuple<short, Vector2>(item.GetComponent<Token>().Character.Id, coord));
            });

            WWDB.Instance.SaveEncounter(EncounterData.Instance.Id, characters);

            EndWork();
        }

        public void Delete()
        {
            WWDB.Instance.DeleteEncounter(EncounterData.Instance.Id);
            EndWork();
        }

        private void EndWork()
        {
            _lastToken = null;
            _characters.Clear();
            _deletedObjects.Clear();
            
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                _deletedObjects.Push(_lastToken);
                _characters.Remove(_lastToken);
                _lastToken.SetActive(false);
                _lastToken = null;
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                _lastToken = _deletedObjects.Pop();
                _lastToken.SetActive(true);
            }
        }
    }
}
