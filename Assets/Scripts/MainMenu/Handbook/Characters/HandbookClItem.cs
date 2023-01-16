using UnityEngine;
using static MainMenu.Handbook.Characters.CreateCharacterPage;

namespace MainMenu.Handbook.Characters
{
    public class HandbookClItem : CharacterListItem
    {

        [SerializeField] private GameObject BestiaryPagePrefab;
        [SerializeField] private GameObject EditBestiaryPagePrefab;

        private GameObject _page;
        
        public delegate void OpenPage();
        public static event OpenPage Open;

        private void Start()
        {
            Open += ClosePage;
            Create += ClosePage;
        }

        private void ClosePage()
        {
            _page?.SetActive(false);
        }

        private void OnDestroy()
        {
            Destroy(_page);
            _page = null;
        }

        public void OpenBestiaryPage()
        {
            Open?.Invoke();

            if (_page != null)
            {
                _page.SetActive(true);
                return;
            }
            
            _page = _character.Owner == SessionInfo.Instance.UserId ?
                Instantiate(EditBestiaryPagePrefab, GameObject.Find("Body").transform) :
                Instantiate(BestiaryPagePrefab, GameObject.Find("Body").transform);
                
            _page.GetComponent<IInitializeCharacterPage>().Initialize(_character);
        }
    }
}
