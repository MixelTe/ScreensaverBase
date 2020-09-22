using System.Drawing;

namespace ScreensaverBase
{
	class Painter : IPainter
	{
		private readonly Settings _Settings;
		private readonly Controller _game;
		private readonly Rectangle _rcClient;

		public Painter(Controller game, Rectangle rcClient)
		{
			_Settings = Program.Settings;
			_game = game;
			_rcClient = rcClient;
		}

		public void Draw(Graphics g)
		{
			g.FillRectangle(Brushes.Black, _rcClient);
			g.FillRectangle(Brushes.Blue, _game.Rect);
		}


		public void Dispose()
		{

		}
	}
}