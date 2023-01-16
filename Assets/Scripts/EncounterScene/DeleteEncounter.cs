using UnityEngine;

namespace EncounterScene
{
    public class DeleteEncounter : MonoBehaviour
    {
        public void Delete()
        {
            EncounterManager.Instance.Delete();
        }
    }
}
