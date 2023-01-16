using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu.Handbook.Characters
{
    public class CharacterPage : MonoBehaviour, IInitializeCharacterPage
    {
        [SerializeField] protected GameObject tmpName;
        [SerializeField] protected GameObject token;

        [SerializeField] protected int tokenSize;

        public virtual void Initialize(Character character)
        {
            gameObject.SetActive(true);
            
            setName(character.Name);
            
            ShowToken(character.Image);
        }

        protected virtual void setName(string name)
        {
            tmpName.GetComponentInChildren<TMP_Text>().text = name;
        }

        protected void ShowToken(byte[] image)
        {
            token.GetComponent<Image>().sprite = mImage.GetSprite(image, tokenSize, tokenSize);
        }
    }
}
