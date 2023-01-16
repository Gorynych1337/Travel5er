using System;
using TMPro;
using UnityEngine;

namespace MainMenu.Campaigns.CampaignList
{
    public class CreateCampaign : MonoBehaviour
    {
        [SerializeField] private TMP_InputField nameInputField;
    
        public void Save()
        {
            short campaignId = WWDB.Instance.AddCampaign(nameInputField.text, (short)SessionInfo.Instance.UserId);
            SessionInfo.Instance.SetCampaignInfo(campaignId, nameInputField.text);
            
            GetComponent<OpenCampaign>().Open();
        }
    }
}
