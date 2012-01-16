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
	class Program
	{

		static void Main(string[] args)
		{
			IConfigurationManager ConfigurationManager = ConfigurationManagerFactory.CreateDefault();
			using (Root _root = new Root("game.log"))
			{
				ConfigurationManager.RestoreConfiguration(_root);
				if (ConfigurationManager.ShowConfigDialog(_root))
				{
					ConfigurationManager.SaveConfiguration(_root);

					using (RenderWindow _renderWindow = _root.Initialize(true, "Illisian.Niva"))
					{

						var game = new Game(_root, _renderWindow);
						WindowEventMonitor.Instance.RegisterListener(_renderWindow, game);
						game.OnLoad();
						game.CreateScene();
						_root.FrameRenderingQueued += game.OnRenderFrame;
						_root.FrameStarted += game.UpdateInput;
						_root.FrameStarted += game.UpdateOverlay;
						_root.FrameEnded += game.OnRenderFrameEnd;
						_root.StartRendering();

						game.OnUnload();
					}
				}

			}
		}

		public void Initialise()
		{
			

		}



	}
}
