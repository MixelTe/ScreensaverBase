using Microsoft.Win32;
using System.Windows.Forms;

namespace ScreensaverBase
{
	class Settings
	{
		public int Speed = 1;


		public override string ToString()
		{
			return "Speed: " + Speed;
		}
	}
}