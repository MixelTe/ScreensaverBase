using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace ScreenSaverBase
{
	class ScreensaverController
	{
		private readonly bool UseTimer = false;
		private readonly int Fps = 30;


		private Func<IController> _CreateController;
		private Func<IPainter> _CreatePainter;
		private object _lock = new object();
		private System.Threading.Timer _Timer;
		private bool _IsUpdating = false;
		private IController _game;
		private IPainter _painter;
		private bool _Paused;
		private IDrawingBuffer _DrawingBuffer;
		private readonly FPS _fps = new FPS();
		private long _time;

		internal Control TargetControl;
		private Thread _t;
		private bool _IsRunning;

		internal double GetFps() => _fps.Value;


		internal void TogglePaused()
		{
			lock (_lock)
			{
				_Paused = !_Paused;
			}
		}
		private bool Paused
		{
			get
			{
				lock (_lock) return _Paused;
			}
		}
		private bool IsRunning
		{
			get
			{
				lock (_lock) return _IsRunning;
			}
			set
			{
				lock (_lock) _IsRunning = value;
			}
		}
		public ScreensaverController(Func<IController> createController, Func<IPainter> createPainter)
		{
			_CreateController = createController;
			_CreatePainter = createPainter;
			_Timer = new System.Threading.Timer(OnTimer);
			_time = Stopwatch.GetTimestamp();
		}
		internal void RecreateGame(Rectangle rcClient)
		{
			var controller = _CreateController();
			var painter = _CreatePainter();

			controller.Init(rcClient.Width, rcClient.Height);
			painter.Init(controller, rcClient);

			AssignComponents(controller, painter, rcClient);
		}
		private void AssignComponents(IController game, IPainter painter, Rectangle rcClient)
		{
			lock (_lock)
			{
				if (_game is IDisposable d) d.Dispose();
				if (_painter is IDisposable d2) d2.Dispose();
				_game = game;
				_painter = painter;

				_DrawingBuffer?.Dispose();
				_DrawingBuffer = CreateDrawing1(rcClient);
			}
		}

		private IDrawingBuffer CreateDrawing1(Rectangle rcClient)
		{
			return new DrawingBuffer(TargetControl.CreateGraphics(), rcClient);
		}
		internal void Start()
		{
			if (UseTimer)
			{
				_Timer.Change(0, 1000 / Fps);
			}
			else
			{
				_t = new Thread(GameLoop)
				{
					IsBackground = true
				};
				IsRunning = true;
				_t.Start();
			}
		}
		internal void Stop()
		{
			if (UseTimer)
			{
				_Timer.Change(0, -1);

			}
			else
			{
				IsRunning = false;
				_t.Join(TimeSpan.FromSeconds(20)); // wait thread to finish
			}
		}
		private void OnTimer(object state)
		{
			GameUpdate();
		}
		private void GameLoop()
		{
			while (IsRunning)
			{
				var time = Stopwatch.GetTimestamp();
				if (time - _time > Stopwatch.Frequency / Fps)
				{
					_time = time;
					GameUpdate();
				}
				//Thread.Sleep(1000 / Fps);
			}
		}
		private void GameUpdate()
		{
			lock (_lock)
			{
				if (_IsUpdating) return;
				_IsUpdating = true;
			}
			if (!Paused)
			{
				lock (_lock)
				{
					_game.Update();
				}

				DrawGame();

				_fps.Increment();
			}
			lock (_lock)
			{
				_IsUpdating = false;
			}
		}
		internal void DrawGame()
		{
			lock (_lock)
			{
				using (var gTarget = TargetControl.CreateGraphics())
				{
					_DrawingBuffer.Draw(_painter);
					_DrawingBuffer.RenderTo(gTarget);
				}
			}
		}
	}
}
