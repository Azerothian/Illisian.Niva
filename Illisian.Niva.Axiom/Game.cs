using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axiom.Core;
using Axiom.Graphics;
using Axiom.Math;
using Axiom.Media;

namespace Illisian.Niva.AxiomEngine
{
	public sealed class Game
	{
		private readonly Root _root;
		private readonly RenderWindow _window;
		private SceneManager _sceneManager;

		private Camera _camera;
		private Viewport _viewport;
		private Light _light;

		List<IRenderItem> _renderItem;



		long timeLast, timeNow, timeDelta;

		public Game(Root root, RenderWindow window)
		{
			_renderItem = new List<IRenderItem>();
			_root = root;
			_window = window;
			_renderItem = new List<IRenderItem>();
		}

		public void OnLoad()
		{
			//ResourceGroupManager.Instance.AddResourceLocation("media", "Folder", true);

			_root.SceneManager = _sceneManager = _root.CreateSceneManager(SceneType.ExteriorClose);
			_sceneManager.ClearScene();

			_camera = _sceneManager.CreateCamera("MainCamera");

			_camera.Position = new Vector3(0, 0, 500);
			_camera.LookAt(new Vector3(0, 0, -300));
			_camera.Near = 5;
			_camera.AutoAspectRatio = true;

			_viewport = _window.AddViewport(_camera, 0, 0, 1.0f, 1.0f, 100);
			_viewport.BackgroundColor = ColorEx.DarkBlue;

			_light = _sceneManager.CreateLight("light1");
			_light.Type = LightType.Directional;
			_light.Position = new Vector3(0, 150, 300);
			_light.Diffuse = ColorEx.Blue;
			_light.Specular = ColorEx.Blue;
			//_light.Direction = new Vector3(0, 0, -300);
			_sceneManager.AmbientLight = ColorEx.White;// new ColorEx(0.2f, 0.2f, 0.2f);
			
			ResourceGroupManager.Instance.InitializeAllResourceGroups();


			_renderItem.Add(new BasicCube());
			_renderItem.Add(new CubeBrowser());
			lock (_renderItem)
			{
				foreach (var i in _renderItem)
				{
					i.Initialise(_root);
				}
			}
		}

		public void CreateScene()
		{
			lock (_renderItem)
			{
				foreach (var i in _renderItem)
				{
					i.CreateScene();
				}
			}
		}

		public void OnUnload()
		{
			lock (_renderItem)
			{
				foreach (var i in _renderItem)
				{
					i.OnUnload();
				}
			}
		}

		public void OnRenderFrame(object s, FrameEventArgs e)
		{
			timeLast = timeNow;
			timeNow = _root.Timer.Milliseconds;
			timeDelta = timeNow - timeLast;
			//_cubeBrowser.OnRenderFrame(timeDelta);
			lock (_renderItem)
			{
				foreach (var i in _renderItem)
				{
					i.OnRenderFrame(timeDelta);
				}
			}

		}



	}
}
