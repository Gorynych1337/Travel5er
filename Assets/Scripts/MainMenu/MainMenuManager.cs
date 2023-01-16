using UnityEngine;

namespace MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject AuthPage;
        [SerializeField] private GameObject CampaignPage;
        [SerializeField] private GameObject CampaignList;
        [SerializeField] private GameObject EncounterList;
        [SerializeField] private GameObject ErrorMessageObj;

        private void Start()
        {
            WWDB.ConnectionBreak += (error) =>
            {
                ErrorMessageObj.SetActive(true);
                ErrorMessageObj.GetComponent<ErrorMessageUI>().Init(error);
            };
        
            if (SessionInfo.Instance.UserId is null)
            {
                AuthPage.SetActive(true);
            }
            else if (SessionInfo.Instance.CampInfo is null)
            {
                CampaignPage.SetActive(true);
                CampaignList.SetActive(true);
            }
            else
            {
                CampaignPage.SetActive(true);
                EncounterList.SetActive(true);
                CampaignList.SetActive(false);
            }
        }
    }
}
