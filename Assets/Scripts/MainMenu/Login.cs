using System;
using TMPro;
using UnityEngine;

namespace MainMenu
{
    public class Login : MonoBehaviour
    {
        [SerializeField] private TMP_InputField nickname;
        [SerializeField] private TMP_InputField password;
        [SerializeField] private GameObject mainMenu;
    
        public void LoginClick()
        {
            string nicknameText = nickname.text;
            string passwordText = password.text;

            Tuple<short, bool?> userInfo = WWDB.Instance.Login(nicknameText, passwordText);
            if (userInfo == null)
                return;

            (short id, bool? isAdmin) = userInfo;
            SessionInfo.Instance.SetUser(id, (bool)isAdmin);
        
            transform.parent.gameObject.SetActive(false);
            mainMenu.SetActive(true);
        }
    }
}
