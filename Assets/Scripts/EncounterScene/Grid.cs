using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EncounterScene
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private int plateSize;
        public static int PlateSize { get; private set; }
        void Start()
        {
            PlateSize = plateSize;

            GetComponentInChildren<Image>().rectTransform.sizeDelta = new Vector2(EncounterData.Instance.Size.x * plateSize,
                EncounterData.Instance.Size.y * plateSize);
            GetComponentInChildren<Image>().sprite = mImage.GetSprite(EncounterData.Instance.Image,
                (int)EncounterData.Instance.Size.y * plateSize, (int)EncounterData.Instance.Size.x * plateSize);

            List<Tuple<Character, Vector2>> encounterCharacters =
                WWDB.Instance.GetEncounterCharacters(EncounterData.Instance.Id);

            foreach ((Character character, Vector2 position) in encounterCharacters)
            {
                 GetComponent<SpawnToken>().InstantiateToken(character, position);
            }
        }
    }
}
