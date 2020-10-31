using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenSaverBase
{
	public partial class Form1 : Form
	{
		private Point _PastMousePos;
		private bool _FirstMouseMove = true;
		private Size _OldSize;
		private readonly ScreensaverController _Controller;

		public Form1(Func<IController> createController, Func<IPainter> createPainter)
		{
			InitializeComponent();
			Cursor.Hide();
			_Controller = new ScreensaverController(createController, createPainter);
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			_Controller.TargetControl = this;

			RecreateGame();

			_Controller.Start();
		}
		private void Form1_Click(object sender, EventArgs e)
		{
			_Controller.TogglePaused();
		}
		private void Timer1_Tick(object sender, EventArgs e)
		{
			if (_OldSize != ClientRectangle.Size)
			{
				RecreateGame();

				_Controller.DrawGame();
			}

			Text = $"fps={_Controller.GetFps():#,##0}";
		}
		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			_Controller.Stop();
		}
		private void RecreateGame()
		{
			var rcClient = ClientRectangle;

			_Controller.RecreateGame(rcClient);

			_OldSize = rcClient.Size;
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			Close();
		}

		private void Form1_MouseMove(object sender, MouseEventArgs e)
		{
			var mousePos = e.Location;
			if (_FirstMouseMove)
			{
				_PastMousePos = mousePos;
				_FirstMouseMove = false;
			}
			else
			{
				if (_PastMousePos != mousePos)
				{
					Close();
					_PastMousePos = mousePos;
				}
			}
		}
	}
}
