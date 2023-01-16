using System;
using UnityEngine.PlayerLoop;

namespace MainMenu.Campaigns.EncounterList
{
    public class EncounterListItemPrefab: TextListItemPrefab
    {
        public override void Click()
        {
            Encounter encounter = WWDB.Instance.GetEncounter(_id);
            
            EncounterData.Instance.SetData(_id, encounter.MapImage, encounter.Width, encounter.Height);

            GetComponent<OpenEncounter>().Open();
        }
    }
}