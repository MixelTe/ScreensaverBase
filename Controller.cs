using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ScreensaverBase
{
	class Controller: IController
	{
		private readonly Settings _Settings;
		private readonly int _Height;
		private readonly int _Width;
		public Rectangle Rect;


		public Controller(int width, int height)
		{
			_Settings = Program.Settings;
			_Width = width;
			_Height = height;
			Rect = new Rectangle(width / 2, height / 2, 0, 0);
		}
		public void Update()
		{
			Rect.Inflate(_Settings.Speed, _Settings.Speed);
			if (Rect.Width > _Width && Rect.Height > _Height)
			{
				Rect = new Rectangle(_Width / 2, _Height / 2, 0, 0);
			}
		}

		public void Dispose()
		{

		}
	}
}