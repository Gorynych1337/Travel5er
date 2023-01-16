using TMPro;
using UnityEngine;

namespace MainMenu.Campaigns
{
    public class TextListItemPrefab: MonoBehaviour
    {
        [SerializeField] private TMP_Text nameplate;
        
        protected short _id;
        protected string _name;
        public virtual void Init(short id, string name)
        {
            nameplate.SetText(name);
            _name = name;
            _id = id;
        }

        public virtual void Click(){}
    }
}