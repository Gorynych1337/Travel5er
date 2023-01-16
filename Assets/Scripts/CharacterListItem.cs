using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterListItem: MonoBehaviour
{
    [SerializeField] protected GameObject token;
    [SerializeField] protected TMP_Text tmpName;

    [SerializeField] protected int tokenSize;
        
    protected Character _character;

    public void Initialize(Character character)
    {
        _character = character;
        
        tmpName.SetText(_character.Name);
        token.GetComponent<Image>().sprite = mImage.GetSprite(_character.Image, tokenSize, tokenSize);
    }
}