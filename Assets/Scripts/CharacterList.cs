using UnityEngine;

public class CharacterList: MonoBehaviour
{
    [SerializeField] protected GameObject characterListItemPrefab;
    [SerializeField] protected Transform container;

    protected void FillList()
    {
        var charactersList = WWDB.Instance.GetCharactersList();
        
        charactersList.ForEach((x) =>
        {
            var characterListItem = Instantiate(characterListItemPrefab, container);
            characterListItem.GetComponent<CharacterListItem>().Initialize(x);
        });
    }
}