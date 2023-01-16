using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MainMenu.Campaigns.EncounterList
{
    public class OpenEncounterList: MonoBehaviour
    {
        [SerializeField] private TMP_Text nameTMPText;
        
        [SerializeField] private Transform container;
        [SerializeField] private GameObject encounterPrefab;

        private void OnEnable()
        {
            Init();
        }

        public void Init()
        {
            nameTMPText.text = SessionInfo.Instance.CampInfo.Value.Name;

            foreach (Transform item in container)
            {
                Destroy(item.gameObject);
            }
            
            List<Tuple<short, string>> encounters = WWDB.Instance.GetEncounterList(SessionInfo.Instance.CampInfo.Value.Id);
            foreach ((short encounterId, string encounterName) in encounters)
            {
                GameObject encounterListItem = Instantiate(encounterPrefab, container);
                encounterListItem.GetComponent<EncounterListItemPrefab>()?.Init(encounterId, encounterName);
            }
        }
    }
}