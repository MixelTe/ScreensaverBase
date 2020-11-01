using System;
using System.Windows.Forms;

namespace ScreenSaverBase
{
	static class ScreenSaver
	{
		static public object Settings = new object();
		static public string Regkey = @"HKEY_CURRENT_USER\Software\MixelTe\ScreenSaver";
		public static void Run(string[] args, Func<Form> createSettingsForm, Func<IController> createController, Func<IPainter> createPainter)
		{
			//var message = "";
			//foreach (var item in args)
			//{
			//	message += item + " | ";
			//}
			//MessageBox.Show(message);
			if (args.Length > 0 && args[0].Substring(0, 2).Equals("/s", StringComparison.InvariantCultureIgnoreCase))
			{
				RegSerializer.Load(Regkey, Settings);
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new ScreenSaverForm(createController, createPainter));
			}
			else if (args.Length == 0 || args.Length > 0 && args[0].Substring(0, 2).Equals("/c", StringComparison.InvariantCultureIgnoreCase))
			{
				RegSerializer.Load(Regkey, Settings);
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(createSettingsForm());
			}
		}
		static public void SaveSettingsToReg()
		{
			RegSerializer.Save(Regkey, Settings);
		}
	}
}
