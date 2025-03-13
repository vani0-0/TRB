using System;
using System.Windows;
using TRB.Resources;
using TRB.Utils;

namespace Sample_App
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			TRBLocalization.SetLanguage(TRB.Resources.Language.Tagalog);
		}

		private void ToIntButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				string roman = RomanInput.Text.Trim();
				int result = RomanNumeral.ToInt(roman);
				ResultTextBlockInt.Text = $"Integer: {result}";
			}
			catch (ArgumentException ex)
			{
				ResultTextBlockInt.Text = ex.Message;
			}
		}

		private void ToRomanButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (int.TryParse(IntInput.Text.Trim(), out int number))
				{
					string result = RomanNumeral.ToRoman(number);
					ResultTextBlockRoman.Text = $"Roman: {result}";
				}
				else
				{
					ResultTextBlockRoman.Text = "Invalid Integer Input.";
				}
			}
			catch (ArgumentException ex)
			{
				ResultTextBlockRoman.Text = ex.Message;
			}
		}

		// Clear placeholder text on focus
		private void ClearText(object sender, RoutedEventArgs e)
		{
			var tb = sender as System.Windows.Controls.TextBox;
			if (tb.Text == tb.Tag.ToString()) tb.Text = "";
		}

		// Reset placeholder text if empty
		private void ResetText(object sender, RoutedEventArgs e)
		{
			var tb = sender as System.Windows.Controls.TextBox;
			if (string.IsNullOrWhiteSpace(tb.Text)) tb.Text = tb.Tag.ToString();
		}
	}
}
