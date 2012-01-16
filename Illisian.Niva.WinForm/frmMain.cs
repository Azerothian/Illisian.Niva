using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Awesomium.Core;

using Awesomium.Windows.Forms;

namespace Illisian.Niva.WinForm
{
	public partial class frmMain : Form
	{
		public frmMain()
		{
			InitializeComponent();
		}
		Guid id = Guid.Empty;
		private void frmMain_Load(object sender, EventArgs e)
		{

			Core.BrowserManager.BrowserRenderEvent += BrowserManager_BrowserRenderEvent;
			id = Core.BrowserManager.CreateBrowser("http://www.google.com.au", pictureBox1.Height, pictureBox1.Width);
		}



		void BrowserManager_BrowserRenderEvent(Guid id, Bitmap screen)
		{
			pictureBox1.Image = screen;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Core.BrowserManager[id].Url = textBox1.Text;
		}

		private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			Core.Shutdown();
		}

		private void pictureBox1_Resize(object sender, EventArgs e)
		{
			Core.BrowserManager[id].Resize(pictureBox1.Height, pictureBox1.Width);
		}


		private void pictureBox1_MouseDown(object sender, MouseEventArgs me)
		{
			Core.BrowserManager[id].MouseDown(ConvertWFtoAwe(me.Button));
		}



		private void pictureBox1_MouseMove(object sender, MouseEventArgs me)
		{

			Core.BrowserManager[id].MouseMove(me.X,me.Y);

		}

		private void pictureBox1_MouseUp(object sender, MouseEventArgs me)
		{
			Core.BrowserManager[id].MouseUp(ConvertWFtoAwe(me.Button));
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);
			Core.BrowserManager[id].MouseWheel(e.Delta);
		}

		MouseButton ConvertWFtoAwe(MouseButtons buttons)
		{
			switch (buttons)
			{
				case System.Windows.Forms.MouseButtons.Left:
					return MouseButton.Left;
				case System.Windows.Forms.MouseButtons.Middle:
					return MouseButton.Middle;
				case System.Windows.Forms.MouseButtons.Right:
					return MouseButton.Right;
				default:
					return MouseButton.Left;
			}
		}

		private void frmMain_KeyDown(object sender, KeyEventArgs e)
		{
			Core.BrowserManager[id].KeyDown(e.GetKeyboardEvent(WebKeyType.KeyDown));
		}

		private void frmMain_KeyPress(object sender, KeyPressEventArgs e)
		{
			Core.BrowserManager[id].KeyPress(e.GetKeyboardEvent());
		}

		private void frmMain_KeyUp(object sender, KeyEventArgs e)
		{
			Core.BrowserManager[id].KeyUp(e.GetKeyboardEvent(WebKeyType.KeyUp));
		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			Core.BrowserManager[id].KeyPress(e.GetKeyboardEvent());
			base.OnKeyPress(e);
			

		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			Core.BrowserManager[id].KeyDown(e.GetKeyboardEvent(WebKeyType.KeyDown));
			base.OnKeyDown(e);
			
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			Core.BrowserManager[id].KeyUp(e.GetKeyboardEvent(WebKeyType.KeyUp));
			base.OnKeyUp(e);

			
		}

		private void textBox1_KeyDown(object sender, KeyEventArgs e)
		{
			Core.BrowserManager[id].KeyDown(e.GetKeyboardEvent(WebKeyType.KeyDown));
		}

		private void textBox1_KeyUp(object sender, KeyEventArgs e)
		{
			Core.BrowserManager[id].KeyUp(e.GetKeyboardEvent(WebKeyType.KeyUp));
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{

		}

		private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
		{
			Core.BrowserManager[id].KeyPress(e.GetKeyboardEvent());

		}
		





	}
}
