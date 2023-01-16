using UnityEngine;

namespace MainMenu.Campaigns.CampaignList
{
    public class OpenCreateCampaignPage : MonoBehaviour
    {
        [SerializeField] private GameObject CreateCampaignPage;
        public void Open()
        {
            gameObject.SetActive(false);
            CreateCampaignPage.SetActive(true);
        }
    }
}
