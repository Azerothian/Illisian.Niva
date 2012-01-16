using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Awesomium.Core;

namespace Illisian.Niva
{
	public class BrowserManager : IDisposable
	{
		public event BrowserRenderEventHandler BrowserRenderEvent;
		Dictionary<Guid, Browser> _browsers = new Dictionary<Guid, Browser>();
		public BrowserManager()
		{
			//WebCore.SuppressPrinterDialog(true);
		}


		public void Dispose()
		{
			WebCore.Shutdown();
		}

		public Browser this[Guid g]
		{
			get
			{
				return _browsers[g];
			}
		}
		public Guid CreateBrowser(string url, int Height, int Width)
		{
			var g = Guid.NewGuid();
			var newBrowser = new Browser(g, url, Height, Width);
			newBrowser.BrowserRenderEvent += newBrowser_BrowserRenderEvent;
			_browsers.Add(g, newBrowser);
			return g;
		}

		void newBrowser_BrowserRenderEvent(Guid id, System.Drawing.Bitmap screen)
		{
			if (BrowserRenderEvent != null)
			{
				BrowserRenderEvent(id, screen);
			}
		}

	}
}
