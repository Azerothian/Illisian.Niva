using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axiom.Core;
using Axiom.Graphics;
using Axiom.Math;
using Axiom.Media;
using Axiom.Input;

namespace Illisian.Niva.AxiomEngine
{
	public sealed class Game : IWindowEventListener
	{
		static Game _context;
		public static Game Context
		{
			get
			{
				return _context;
			}
		}

		private readonly Root _root;
		private readonly RenderWindow _renderWindow;
		private SceneManager _sceneManager;

		private InputReader _inputReader;
		public InputReader Input
		{
			get
			{
				return _inputReader;
			}
		}
		public RenderWindow RenderWindow
		{
			get
			{
				return _renderWindow;
			}
		}
		public Camera Camera
		{
			get
			{
				return _camera;
			}
		}
		private Camera _camera;
		private Viewport _viewport;
		private Light _light;

		List<IRenderItem> _renderItems;

		long timeLast, timeNow, timeDelta;

		public Game(Root root, RenderWindow renderWindow)
		{
			_root = root;
			_renderWindow = renderWindow;
			_renderItems = new List<IRenderItem>();
			_context = this;
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
			_camera.FieldOfView = 0.70f;
			_viewport = _renderWindow.AddViewport(_camera, 0, 0, 1.0f, 1.0f, 100);
			_viewport.BackgroundColor = ColorEx.Black; ;

			_light = _sceneManager.CreateLight("light1");
			_light.Type = LightType.Directional;
			_light.Position = new Vector3(0, 150, 300);
			_light.Diffuse = ColorEx.Blue;
			_light.Specular = ColorEx.Blue;
			//_light.Direction = new Vector3(0, 0, -300);
			_sceneManager.AmbientLight = ColorEx.White;// new ColorEx(0.2f, 0.2f, 0.2f);

			ResourceGroupManager.Instance.InitializeAllResourceGroups();


			_inputReader = PlatformManager.Instance.CreateInputReader();
			_inputReader.Initialize(_renderWindow, true, true, false, false);

			_inputReader.UseKeyboardEvents = true;
			_inputReader.UseMouseEvents = false;

			_renderItems.Add(new BasicCube());
			_renderItems.Add(new CubeBrowser());
			foreach (var i in _renderItems)
			{
				i.Initialise(_root);
			}
		}

		public void CreateScene()
		{
			foreach (var i in _renderItems)
			{
				i.CreateScene();
			}

		}

		public void OnUnload()
		{
			foreach (var i in _renderItems)
			{
				i.OnUnload();
			}

		}
		public void UpdateInput(object sender, FrameEventArgs e)
		{
			_inputReader.Capture();

			foreach (var i in _renderItems)
			{
				i.OnUpdateInput(_inputReader);
			}

		}


		public void UpdateOverlay(object sender, FrameEventArgs e)
		{
			foreach (var i in _renderItems)
			{
				//i.OnUnload();
			}

		}
		public void OnRenderFrameEnd(object sender, FrameEventArgs e)
		{

			foreach (var i in _renderItems)
			{
				//i.OnUnload();
			}

		}
		public void OnRenderFrame(object s, FrameEventArgs e)
		{
			timeLast = timeNow;
			timeNow = _root.Timer.Milliseconds;
			timeDelta = timeNow - timeLast;

			foreach (var i in _renderItems)
			{
				i.OnRenderFrame(timeDelta);
			}


		}

		public void WindowClosed(RenderWindow rw)
		{
			Root.Instance.QueueEndRendering();
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
