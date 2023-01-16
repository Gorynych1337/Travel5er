using UnityEngine;

namespace EncounterScene
{
    public class EncounterCLItem : CharacterListItem
    {
        public void SpawnToken()
        {
            GetComponent<SpawnToken>().InstantiateToken(_character);
        }
    }
}
