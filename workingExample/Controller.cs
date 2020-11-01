using ScreenSaverBase;

namespace Screensaver
{
	internal class Controller : IController
	{
		private Settings _Settings;
		private int _Width;
		private int _Height;
		public int _ObjX;
		public int _ObjY;
		private int _SpeedXMul = 1;
		private int _SpeedYMul = 1;
		public void Init(int screenWidth, int screenHeight)
		{
			_Settings = (Settings)ScreenSaver.Settings;
			_Width = screenWidth;
			_Height = screenHeight;
			_ObjX = _Width / 2;
			_ObjY = _Height / 2;
		}

		public void Update()
		{
			_ObjX += _Settings.Speed * _SpeedXMul;
			_ObjY += _Settings.Speed * _SpeedYMul;
			if (_ObjX > _Width) _SpeedXMul = -1;
			if (_ObjX < 0) _SpeedXMul = 1;
			if (_ObjY > _Height) _SpeedYMul = -1;
			if (_ObjY < 0) _SpeedYMul = 1;
		}
	}
}