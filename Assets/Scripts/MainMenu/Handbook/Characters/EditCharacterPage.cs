using System.IO;
using FileExplorer.Scripts;
using SFB;
using TMPro;

namespace MainMenu.Handbook.Characters
{
    public class EditCharacterPage : CharacterPage

    {
    private byte[] _image;
    private Character _character;

    private void Start()
    {
        //mWindowController.OpenImage += LoadFile;
    }

    public override void Initialize(Character character)
    {
        base.Initialize(character);
        _character = character;
        _image = character.Image;
    }

    protected override void setName(string name)
    {
        tmpName.GetComponent<TMP_InputField>().text = name;
    }

    public void OpenToken()
    {
        var extensions = new[]
        {
            new ExtensionFilter("Image Files", "png", "jpg", "jpeg"),
        };
        var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, true);
            
        if (paths.Length == 0) return;
        _image = File.ReadAllBytes(paths[0]);
        ShowToken(_image);
        
        /*mWindowController controller = new mWindowController();
        FileExplorerEx.Open(controller, WindowStyle.List);*/
    }

    /*private void LoadFile(string path)
    {
        if (!gameObject.activeInHierarchy) return;

        _image = File.ReadAllBytes(path);
        ShowToken(_image);
    }*/

    public void SaveCharacter()
    {
        if (_character is null)
        {
            short id = WWDB.Instance.SaveCharacter(tmpName.GetComponent<TMP_InputField>().text, _image,
                (short)SessionInfo.Instance.UserId);

            _character = new Character(
                id, tmpName.GetComponent<TMP_InputField>().text,
                _image, (short)SessionInfo.Instance.UserId);
        }
        else
        {
            WWDB.Instance.UpdateCharacter(_character.Id, tmpName.GetComponent<TMP_InputField>().text, _image);
        }
    }

    public void DeleteCharacter()
    {
        if (_character is null) return;
        WWDB.Instance.DeleteCharacter(_character.Id);
        Destroy(gameObject);
    }
    }
}
