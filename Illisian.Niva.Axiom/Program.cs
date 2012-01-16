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
			using (var root = new Root("Game.log"))
			{
				if (ConfigurationManager.ShowConfigDialog(root))
				{
					using (var renderWindow = root.Initialize(true))
					{
						var game = new Game(root, renderWindow);
						game.OnLoad();
						game.CreateScene();
						root.FrameRenderingQueued += game.OnRenderFrame;
						root.StartRendering();
						
						game.OnUnload();
					}
				}

			}

		}
	}
}
