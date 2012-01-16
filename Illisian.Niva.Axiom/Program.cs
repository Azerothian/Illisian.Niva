using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Axiom.Core;
using Axiom.Graphics;
using Axiom.Math;
using Axiom.Framework.Configuration;

namespace Illisian.Niva.AxiomEngine
{
	class Program : IWindowEventListener
	{
		static Program _startup;
		static void Main(string[] args)
		{
			_startup = new Program();
			_startup.Initialise();
		}



		RenderWindow _renderWindow;
		Root _root;
		public void Initialise()
		{
			IConfigurationManager ConfigurationManager = ConfigurationManagerFactory.CreateDefault();
			using (_root = new Root("game.log"))
			{
				ConfigurationManager.RestoreConfiguration(_root);
				if (ConfigurationManager.ShowConfigDialog(_root))
				{
					ConfigurationManager.SaveConfiguration(_root);

					using (_renderWindow = _root.Initialize(true, "Illisian.Niva"))
					{
						WindowEventMonitor.Instance.RegisterListener(_renderWindow, this);


						var game = new Game(_root, _renderWindow);
						game.OnLoad();
						game.CreateScene();
						_root.FrameRenderingQueued += game.OnRenderFrame;
						_root.StartRendering();

						game.OnUnload();
					}
				}

			}
		}

		public void WindowClosed(RenderWindow rw)
		{
			_root.Shutdown();
		}

		public void WindowFocusChange(RenderWindow rw)
		{
		}

		public void WindowMoved(RenderWindow rw)
		{
		}

		public void WindowResized(RenderWindow rw)
		{
		}



	}
}
