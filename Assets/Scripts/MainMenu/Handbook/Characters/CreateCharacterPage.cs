using UnityEngine;
using static MainMenu.Handbook.Characters.HandbookClItem;

namespace MainMenu.Handbook.Characters
{
    public class CreateCharacterPage : MonoBehaviour
    {
        [SerializeField] private GameObject EditCharacterPagePrefab;

        private GameObject _page;
    
        public static event OpenPage Create;
    
        private void Start()
        {
            Open += DestroyPage;
        }

        private void DestroyPage()
        {
            Destroy(_page);
            _page = null;
        }

        public void CreatePage()
        {
            Create?.Invoke();
            if (_page is not null)
            {
                _page.SetActive(true);
                return;
            }

            _page = Instantiate(EditCharacterPagePrefab, GameObject.Find("Body").transform);
        }
    }
}
