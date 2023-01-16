using System;

namespace MainMenu.Campaigns.CampaignList
{
    public class CampaignListItemPrefab : TextListItemPrefab
    {
        public override void Click()
        {
            SessionInfo.Instance.SetCampaignInfo(_id, _name);
            GetComponent<OpenCampaign>().Open();
        }
    }
}
