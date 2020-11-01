using ScreenSaverBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Screensaver
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			ScreenSaver.Regkey = @"HKEY_CURRENT_USER\Software\MixelTe\ScreenSaver";
			ScreenSaver.Settings = new Settings();
			ScreenSaver.Run(args, () => new SettingsForm(), () => new Controller(), () => new Painter());
		}
	}
}
