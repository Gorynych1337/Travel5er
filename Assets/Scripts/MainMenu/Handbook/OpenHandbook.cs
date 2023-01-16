using UnityEngine;

namespace MainMenu.Handbook
{
    public class OpenHandbook : MonoBehaviour
    {
        [SerializeField] private GameObject handbookPage;
        public void OpenHandbookClick()
        {
            transform.parent.gameObject.SetActive(false);
            handbookPage.SetActive(true);
        }
    }
}
