using System.IO;
using FileExplorer.Scripts;
using SFB;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu.Campaigns.EncounterList
{
    public class CreateEncounter: MonoBehaviour
    {
        [SerializeField] private TMP_InputField nameInputField;

        [SerializeField] private Image map;
        
        [SerializeField] private TMP_InputField width;
        [SerializeField] private TMP_InputField height;

        private byte[] _map;

        private void Start()
        {
            //mWindowController.OpenImage += LoadFile;
        }

        public void OpenImage()
        {
            var extensions = new[]
            {
                new ExtensionFilter("Image Files", "png", "jpg", "jpeg"),
            };
            var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, true);
            
            if (paths.Length == 0) return;
            _map = File.ReadAllBytes(paths[0]);
            map.sprite = mImage.GetSprite(_map, 150, 150);
            
            /*mWindowController controller = new mWindowController();
            FileExplorerEx.Open(controller, WindowStyle.List);*/
        }

        /*private void LoadFile(string path)
        {
            if (!gameObject.activeInHierarchy) return;

            _map = File.ReadAllBytes(path);
            map.sprite = mImage.GetSprite(_map, 150, 150);
        }*/

        public void SaveEncounter()
        {
            if (nameInputField.text == "" || height.text == "" || width.text == "" || _map is null) return;

            short id = WWDB.Instance.AddEncounter(nameInputField.text, SessionInfo.Instance.CampInfo.Value.Id,
                _map, new Vector2(short.Parse(width.text), short.Parse(height.text)));
            
            EncounterData.Instance.SetData(id, _map, short.Parse(width.text), short.Parse(height.text));
            
            GetComponent<OpenEncounter>().Open();
        }
    }
}