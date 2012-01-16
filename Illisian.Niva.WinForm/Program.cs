using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using Awesomium.Core;

namespace Illisian.Niva.WinForm
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{

			if (WebCore.IsChildProcess)
			{
				WebCore.ChildProcessMain();
				return;
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Core.Startup();

			Application.Run(new frmMain());
		}
	}
}
