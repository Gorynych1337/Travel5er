using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace FileExplorer.Scripts {

	public static class Utilities {
		public static string GetUserRoot () {
			System.Environment.SpecialFolder rootFolder;

			switch (Application.platform) {
			case RuntimePlatform.OSXPlayer:
			case RuntimePlatform.OSXEditor:
			case RuntimePlatform.WindowsPlayer:
			case RuntimePlatform.WindowsEditor:
			case RuntimePlatform.LinuxPlayer:
			default:
				rootFolder = System.Environment.SpecialFolder.MyPictures;
				break;
			}

			return System.Environment.GetFolderPath(rootFolder);
		}

		public static List<FileSystemInfo> GetFilesInDirectory (string directoryPath, bool includeHidden = false) {
			List<FileSystemInfo> filesAndDirectories = new List<FileSystemInfo>();

			// IMPORTANT: A null or empty string path results in the root directories of all logical drives be returned.
			if (directoryPath == null || directoryPath.Equals("")) {
				string[] drives = Directory.GetLogicalDrives ();
				foreach (string drive in drives) {
					DirectoryInfo info = new DirectoryInfo (drive);
					if ((info.Attributes & FileAttributes.Directory) == FileAttributes.Directory) {
						filesAndDirectories.Add (info);
					}
				}
			} 
			else {
				DirectoryInfo directory = new DirectoryInfo (directoryPath);
				
				FileSystemInfo[] array = directory.GetFileSystemInfos ();
				
				if (!includeHidden) {
					foreach (FileSystemInfo fileOrDirectory in array) {
						if ((fileOrDirectory.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden) {
							filesAndDirectories.Add (fileOrDirectory);
						}
					}
				}
			}
			
			return filesAndDirectories;
		}
	}
	
}