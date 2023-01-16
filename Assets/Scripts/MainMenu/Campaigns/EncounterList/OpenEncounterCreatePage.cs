using UnityEngine;

namespace MainMenu.Campaigns.EncounterList
{
    public class OpenEncounterCreatePage: MonoBehaviour
    {
        [SerializeField] public GameObject CreateEncounterPage;
        
        public void Open()
        {
            gameObject.SetActive(false);
            CreateEncounterPage.SetActive(true);
        }
    }
}