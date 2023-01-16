using System;
using System.Collections.Generic;
using UnityEngine;

namespace MainMenu.Campaigns.CampaignList
{
    public class LoadCampaigns : MonoBehaviour
    {
        [SerializeField] private Transform container;
        [SerializeField] private GameObject campaignPrefab;

        private void OnEnable()
        {
            foreach (Transform item in container)
            {
                Destroy(item.gameObject);
            }
            
            List<Campaign> campaigns = WWDB.Instance.GetCampaignList((short)SessionInfo.Instance.UserId);
            
            foreach (Campaign campaign in campaigns)
            {
                GameObject campaignListItem = Instantiate(campaignPrefab, container);
                campaignListItem.GetComponent<CampaignListItemPrefab>()?.Init(campaign.Id, campaign.Name);
            }
        }
    }
}
