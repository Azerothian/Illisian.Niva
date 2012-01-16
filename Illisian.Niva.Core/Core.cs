using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Awesomium.Core;
using System.Diagnostics;
using System.Threading;

namespace Illisian.Niva
{
	public class Core
	{
		//static bool _running;
		//static Thread _updateThread;
		private static BrowserManager _browserManager = null;
		public static BrowserManager BrowserManager
		{
			get
			{
				if (_browserManager == null)
					_browserManager = new BrowserManager();
				return _browserManager;
			}

		}

		public static void Startup()
		{

			if (!Process.GetCurrentProcess().ProcessName.EndsWith("vshost"))
			{
				WebCore.Initialize(new WebCoreConfig() { ChildProcessPath = WebCoreConfig.CHILD_PROCESS_SELF, });
			}
			
			//_running = true;
			//_updateThread = new Thread(UpdateWorker);
		//_updateThread.Start();


		}
		//private static void UpdateWorker()
		//{
		//    do
		//    {
		//        Thread.Sleep(100);
		//        // WebCore provides an Auto-Update feature
		//        // for UI applications. A console application
		//        // has no UI and no synchronization context
		//        // so we need to manually call Update here.
		//        if (WebCore.IsRunning)
		//        {
		//            WebCore.Update();
		//        }
		//    } while (_running);
		//}



		public static void Shutdown()
		{
			WebCore.Shutdown();
			//_running = false;
		}

	}
}
