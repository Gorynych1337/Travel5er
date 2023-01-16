using System;
using UnityEngine;

namespace MainMenu.Handbook.Characters
{
    public class HandbookCL : CharacterList
    {
        void Start()
        {
            WWDB.CharactersChanged += RefillList;

            FillList();
        }

        private void RefillList()
        {
            foreach (Transform child in container)
            {
                Destroy(child.gameObject);
            }
            
            FillList();
        }
    }
}
