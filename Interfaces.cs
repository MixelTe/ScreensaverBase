using System;
using System.Drawing;

namespace ScreenSaverBase
{
	interface IDrawingBuffer : IDisposable
	{
		void Draw(IPainter painter);
		void RenderTo(Graphics target);
	}
	public interface IPainter
	{
		void Init(IController controller, Rectangle rcScreen);
		void Draw(Graphics g);
	}

	public interface IController
	{
		void Init(int screenWidth, int screenHeight);
		void Update();
	}
}