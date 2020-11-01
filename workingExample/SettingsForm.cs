using ScreenSaverBase;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;

namespace Screensaver
{
	public partial class SettingsForm : Form
	{
		private Settings settings = (Settings)ScreenSaver.Settings;
		public SettingsForm()
		{
			InitializeComponent();
			
			ChangeLanguage();

			SetFields();
		}
		private void ChangeLanguage()
		{
			if (CultureInfo.CurrentUICulture.Name == "ru-RU")
			{
				Text = "ScreenSaver";

				SpeedLbl.Text = "Скорость";

				ResetBtn.Text = "Сбросить";
				OkBtn.Text = "ОК";
			}
		}
		private void SetFields()
		{
			SpeedUnD.Value = settings.Speed;
		}
		private void SpeedUnD_ValueChanged(object sender, EventArgs e)
		{
			settings.Speed = (int)SpeedUnD.Value;
		}




		private void ResetBtn_Click(object sender, EventArgs e)
		{
			ScreenSaver.Settings = new Settings();
			settings = (Settings)ScreenSaver.Settings;
			SetFields();
		}
		private void OkBtn_Click(object sender, EventArgs e)
		{
			ScreenSaver.SaveSettingsToReg();
			Close();
		}

		private void PictureBoxGitHub_Click(object sender, EventArgs e)
		{
			var link = "https://github.com/MixelTe/ScreensaverBase";
			try
			{
				Process.Start(link);
			}
			catch (Exception)
			{
				MessageBox.Show(link + "\n\nCopied to clipboard", "ScreenSaver: Source code", MessageBoxButtons.OK, MessageBoxIcon.Information);
				Clipboard.SetText(link);
			}
		}
	}
}
