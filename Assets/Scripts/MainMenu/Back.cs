using System;
using UnityEngine;

namespace MainMenu
{
    public class Back: MonoBehaviour
    {
        [SerializeField] private GameObject previousPage;

        public virtual void Click()
        {
            gameObject.SetActive(false);
            previousPage.SetActive(true);
        }
    }
}