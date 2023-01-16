using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu.Campaigns.EncounterList
{
    public class OpenEncounter: MonoBehaviour
    {
        public void Open()
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }
}