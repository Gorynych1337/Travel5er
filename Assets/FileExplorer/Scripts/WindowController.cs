using FileExplorer.Scripts.UIWidgets;
using UnityEngine.UI;

namespace FileExplorer.Scripts {

	// WindowController is the base class for customized controller.
	// It is used to respond to window UI interactions.
	public class WindowController {

		public WindowBase window {get;set;}


		public void RegisterCancelButton (Button cancelButton) {
			if (cancelButton == null) return ;

			cancelButton.onClick.AddListener(delegate {
				OnCancelButtonPressed(cancelButton);
			});
		}

		public void RegisterOtherButton (Button otherButton) {
			if (otherButton == null) return ;

			otherButton.onClick.AddListener(delegate {
				OnOtherButtonPressed(otherButton);
			});
		}

		// Override these functions to customize.
		public virtual void OnStart () {}
		public virtual void OnFileHighlighted (string path) {}
		protected virtual void OnCancelButtonPressed (Button Button)
		{
			FileExplorerEx.Close();
		}

		protected virtual void OnOtherButtonPressed(Button Button)
		{
			FileExplorerEx.Close();
		}
	}

}