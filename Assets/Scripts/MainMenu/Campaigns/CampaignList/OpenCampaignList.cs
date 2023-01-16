using UnityEngine;

namespace MainMenu.Campaigns.CampaignList
{
    public class OpenCampaignList : MonoBehaviour
    {
        [SerializeField] private GameObject campaignsPage;

        public void OpenCampaignsClick()
        {
            transform.parent.gameObject.SetActive(false);
            campaignsPage.SetActive(true);
        }
    }
}
