using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System;
using TRB.Resources;

namespace TRB.Utils
{
	public static class Dialog
	{
		/// <summary>
		/// Displays an error message in a message box with details from the provided exception.
		/// </summary>
		/// <param name="ex">The exception to display information about.</param>
		public static void ErrorMessage(Exception ex)
		{
			try
			{
				StackTrace stackTrace = new StackTrace(ex);
				MethodBase method = stackTrace.GetFrame(stackTrace.FrameCount - 1).GetMethod();
				string titleText = method.Name;
				string message = string.Format("{0}\n\n{1}\n\n{2}", ex.Message, ex.StackTrace, TRBLocalization.Get(MessageKey.SendScreenShot));
				MessageBox.Show(message, titleText, MessageBoxButton.OK, MessageBoxImage.Error);
			}
			catch (Exception ex2)
			{
				MessageBox.Show(TRBLocalization.Get(MessageKey.AnErrorOccured) + ex2.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		/// <summary>
		/// Displays an information message in a message box.
		/// </summary>
		/// <param name="message">The message to display.</param>
		/// <param name="title">Optional title for the message box. Defaults to the application name.</param>
		public static void InformationMessage(string message, string title = "Information")
		{
			try
			{
				string titleText = title;
				MessageBox.Show(message, titleText, MessageBoxButton.OK, MessageBoxImage.Information);
			}
			catch (Exception ex)
			{
				ErrorMessage(ex);
			}
		}

		public static void WarningMessage(string message, string title)
		{
			try
			{
				MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
			}
			catch (Exception ex2)
			{
				ErrorMessage(ex2);
			}
		}

		public static void WarningMessage(string message)
		{
			try
			{
				WarningMessage(message, "Warning");
			}
			catch (Exception ex2)
			{
				ErrorMessage(ex2);
			}
		}

		public static MessageBoxResult QuestionMessage(string message, string title)
		{
			try
			{
				return MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question);
			}
			catch (Exception ex2)
			{
				ErrorMessage(ex2);
			}

			return MessageBoxResult.No;
		}
	}
}
