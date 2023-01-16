using System;
using MainMenu.Campaigns.EncounterList;
using UnityEngine;

namespace MainMenu.Campaigns.CampaignList
{
    public class OpenCampaign: MonoBehaviour
    {
        public void Open()
        {
            GameObject.Find("Campaigns").transform.Find("CampaignList").gameObject.SetActive(false);
            var campaignPage =  GameObject.Find("Campaigns").transform.Find("EncounterList");
            
            campaignPage.gameObject.SetActive(true);
            campaignPage.GetComponent<OpenEncounterList>().Init();
        }
    }
}