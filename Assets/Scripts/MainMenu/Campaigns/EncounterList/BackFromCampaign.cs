namespace MainMenu.Campaigns.EncounterList
{
    public class BackFromCampaign: Back
    {
        public override void Click()
        {
            base.Click();
            SessionInfo.Instance.RemoveCampaignInfo();
        }
    }
}