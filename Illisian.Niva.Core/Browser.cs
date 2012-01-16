using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Awesomium.Core;
using System.Drawing;
using Awesomium.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;

namespace Illisian.Niva
{

	public class Browser
	{
		WebView webView;
		object fbLockObject = new object();
		Bitmap frameBuffer;

		public Guid Id { get; set; }

		private int _width = 0;
		private int _height = 0;

		public event BrowserRenderEventHandler BrowserRenderEvent;

		private string _url;
		public string Url
		{
			get
			{
				return _url;
			}
			set
			{
				webView.LoadURL(value);
				//webView.Focus();
			}
		}

		void webView_BeginNavigation(object sender, BeginNavigationEventArgs e)
		{
			_url = e.Url;
		}




		public Browser(Guid id, string url, int width, int height)
		{
			Id = id;
			_width = width;
			_height = height;
			webView = WebCore.CreateWebView(width, height);
			webView.ResizeComplete += webView_ResizeComplete;
			webView.IsDirtyChanged += webView_IsDirtyChanged;
			//webView.SelectLocalFiles += webView_SelectLocalFiles;
			webView.CursorChanged += webView_CursorChanged;
			webView.LoadCompleted += new EventHandler(webView_LoadCompleted);
			webView.LoadURL(url);
			webView.Focus();
		}

		void webView_LoadCompleted(object sender, EventArgs e)
		{

		}


		void webView_CursorChanged(object sender, ChangeCursorEventArgs e)
		{

		}

		//void webView_SelectLocalFiles(object sender, SelectLocalFilesEventArgs e)
		//{
		//    
		//}

		void webView_IsDirtyChanged(object sender, EventArgs e)
		{
			Render();
		}

		void webView_ResizeComplete(object sender, ResizeEventArgs e)
		{

		}


		void Render()
		{
			if ((webView == null) || !webView.IsEnabled)
				return;
			if (!webView.IsDirty)
				return;

			try
			{
				RenderBuffer rBuffer = webView.Render();
				if (rBuffer != null)
				{
					lock (fbLockObject)
					{
						// Only recreate the bitmap if the size of the buffer has changed.
						if (frameBuffer == null || frameBuffer.Width != rBuffer.Width || frameBuffer.Height != rBuffer.Height)
						{
							if (frameBuffer != null)
								frameBuffer.Dispose();

							frameBuffer = new Bitmap(rBuffer.Width, rBuffer.Height, PixelFormat.Format32bppPArgb);
						}

						BitmapData bits = frameBuffer.LockBits(
							new System.Drawing.Rectangle(0, 0, rBuffer.Width, rBuffer.Height),
							ImageLockMode.ReadWrite, frameBuffer.PixelFormat);

						// Copy to Bitmap specifying ConvertToRGBA = true.
						rBuffer.CopyTo(bits.Scan0, bits.Stride, 4, false);

						frameBuffer.UnlockBits(bits);


						//frameBuffer.Save(@"E:\Temp\test.jpg", ImageFormat.Jpeg);
						if (BrowserRenderEvent != null)
						{
							BrowserRenderEvent(Id, frameBuffer);
						}
					}
				}

			}
			catch { /* */ }
			finally
			{
				GC.Collect();
			}


		}
		public void Resize(int height, int width)
		{
			if (webView != null && webView.IsEnabled)
				webView.Resize(width, height);
		}
		public void MouseDown(MouseButton button)
		{
			if (webView != null && webView.IsEnabled)
				webView.InjectMouseDown(button);
		}
		public void MouseUp(MouseButton button)
		{
			if (webView != null && webView.IsEnabled)
				webView.InjectMouseUp(button);
		}
		public void MouseMove(int x, int y)
		{
			if (webView != null && webView.IsEnabled)
				webView.InjectMouseMove(x, y);
		}
		public void MouseWheel(int delta)
		{
			if (webView != null && webView.IsEnabled)
				webView.InjectMouseWheel(delta);
		}

		public void KeyPress(WebKeyboardEvent webKeyboardEvent)
		{
			if (webView != null && webView.IsEnabled)
				webView.InjectKeyboardEvent(webKeyboardEvent);
		}

		public void KeyDown(WebKeyboardEvent webKeyboardEvent)
		{
			if (webView != null && webView.IsEnabled)
			{
				webView.InjectKeyboardEvent(webKeyboardEvent);
			}
		}

		public void KeyUp(WebKeyboardEvent webKeyboardEvent)
		{
			if (webView != null && webView.IsEnabled)
				webView.InjectKeyboardEvent(webKeyboardEvent);
		}
	}
}
