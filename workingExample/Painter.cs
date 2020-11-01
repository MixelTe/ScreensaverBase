using ScreenSaverBase;
using System.Drawing;

namespace Screensaver
{
	internal class Painter : IPainter
	{
		private Controller _Controller;
		private Rectangle _Screen;
		public void Init(IController controller, Rectangle rcScreen)
		{
			_Controller = (Controller)controller;
			_Screen = rcScreen;
		}
		public void Draw(Graphics g)
		{
			using (var b = new SolidBrush(Color.FromArgb(50, 0, 0, 0)))
			{
				g.FillRectangle(b, _Screen);
			}
			var size = 50;
			g.FillEllipse(Brushes.Blue, _Controller._ObjX - size / 2, _Controller._ObjY - size / 2, size, size);
		}

	}
}