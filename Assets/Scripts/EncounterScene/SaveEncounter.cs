using UnityEngine;
using UnityEngine.SceneManagement;

namespace EncounterScene
{
    public class SaveEncounter : MonoBehaviour
    {
        public void Save()
        {
            EncounterManager.Instance.Save();
        }
    }
}
