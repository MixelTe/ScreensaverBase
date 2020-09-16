using Microsoft.Win32;
using System.Windows.Forms;

namespace ScreensaverBase
{
	class ScreensaverSettings
	{
		private static readonly string _KeyName = @"HKEY_CURRENT_USER\Software\MixelTe\ScreenSaver";
		public static int D_Speed = 1;

		public int Speed = D_Speed;


		public void Save()
		{
			Registry.SetValue(_KeyName, "Speed", Speed);
		}
		public void Load()
		{
			var speed = Registry.GetValue(_KeyName, "Speed", Speed);

			if (speed != null) Speed = (int)speed;
		}

		public override string ToString()
		{
			return "Speed: " + Speed;
		}
	}
}