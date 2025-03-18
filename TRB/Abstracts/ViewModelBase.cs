using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Windows;
using TRB.Utils;

namespace TRB.Abstracts
{
	public abstract class ViewModelBase : DevExpress.Mvvm.ViewModelBase
	{
		#region Version & Title

		private string _applicationName;

		public string ApplicationName
		{
			get { return _applicationName; }
			set { SetProperty(ref _applicationName, value, nameof(ApplicationName)); }
		}

		private Version _version;

		public Version Version
		{
			get { return _version; }
			set { SetProperty(ref _version, value, nameof(Version)); }
		}

		public string Title
		{
			get { return $"{ApplicationName} v{Version}"; }
		}

		#endregion

		#region Dialog Functions

		public void ShowError(Exception ex) => Dialog.ErrorMessage(ex);

		public void ShowInfo(string message, string title = null) => Dialog.InformationMessage(message, title ?? ApplicationName);

		public void ShowWarning(string message, string title = "Warning") => Dialog.WarningMessage(message, title);

		public MessageBoxResult ShowQuestion(string message, string title = "Question") => Dialog.QuestionMessage(message, title);

		#endregion

		#region Shared Function

		public string GetFolderPath(string title)
		{
			string path = string.Empty;
			try
			{
				using (CommonOpenFileDialog dialog = new CommonOpenFileDialog
				{
					IsFolderPicker = true,
					AllowNonFileSystemItems = false,
					Multiselect = false,
					Title = title
				})
				{
					if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
					{
						path = dialog.FileName;
					}
				}
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
			return path;
		}

		#endregion

	}
}
