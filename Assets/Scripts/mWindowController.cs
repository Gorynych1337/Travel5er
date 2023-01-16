using FileExplorer.Scripts;
using FileExplorer.Scripts.UIWidgets;
using JetBrains.Annotations;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class mWindowController: WindowController
{
        public delegate void OpenFile(string path);
        public static event OpenFile OpenImage;

        [CanBeNull] private string _path;
        
        protected override void OnOtherButtonPressed(Button button)
        {
                ListColumn col = (window as ListWindow).highlightColumn;
                if (col.Type != "file") return;
                
                OpenImage?.Invoke((window as ListWindow)?.highlightColumn.path);
                
                base.OnOtherButtonPressed(button);
        }
}