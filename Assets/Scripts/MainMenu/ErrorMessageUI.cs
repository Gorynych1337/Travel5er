using TMPro;
using UnityEngine;

namespace MainMenu
{
    public class ErrorMessageUI: MonoBehaviour
    {
        [SerializeField] private TMP_Text tmpMessage;

        public void Init(string message)
        {
            tmpMessage.text = message;
        }

        public void Click()
        {
            gameObject.SetActive(false);
        }
    }
}