using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axiom.Graphics;
using Axiom.Core;
using Axiom.Media;
using System.Drawing.Imaging;
using System.IO;
using Axiom.Math;
using System.Drawing;
using Awesomium.Core;
using Axiom.Input;
using Axiom.Overlays;
using Axiom.Overlays.Elements;
using Axiom.Fonts;

namespace Illisian.Niva.AxiomEngine
{
	public class CubeBrowser : IRenderItem
	{
		int _browserWidth = 1024;
		int _browserHeight = 768;

		private Root _root;

		private Entity _entity;
		private Texture _texture;
		private Material _material;
		private Guid _browserId = Guid.Empty;
		private SceneNode _sceneNode;

		public void Initialise(Root item)
		{
			_root = item;

			Game.Context.Input.KeyDown += Input_KeyDown;
			Game.Context.Input.KeyUp += Input_KeyUp;

		}

		void Input_KeyUp(object sender, Axiom.Input.KeyEventArgs e)
		{
			Core.BrowserManager[_browserId].KeyUp(CreateWebKeyboardEvent(e, WebKeyType.KeyUp));
			Core.BrowserManager[_browserId].KeyPress(CreateWebKeyboardEvent(e));

            if (e.Key == KeyCodes.A)
            {
                _sceneNode.Rotate(new Vector3(0, 1, 0), 10f);
            }
            if ( e.Key == KeyCodes.D)
            {
                _sceneNode.Rotate(new Vector3(0, 1, 0), -1f);
            }

		}

		void Input_KeyDown(object sender, Axiom.Input.KeyEventArgs e)
		{
			if (e.Key == KeyCodes.Escape)
			{
				Environment.Exit(Environment.ExitCode);
			}


			Core.BrowserManager[_browserId].KeyDown(CreateWebKeyboardEvent(e, WebKeyType.KeyDown));
		}

		WebKeyboardEvent CreateWebKeyboardEvent(Axiom.Input.KeyEventArgs e)
		{
			return new WebKeyboardEvent()
			{
				IsSystemKey = false,
				Text = new ushort[] { (ushort)e.KeyChar, 0, 0, 0 },
				Type = WebKeyType.Char,
				Modifiers = AwesomiumUtil.ConvertModifierKeys(e.Modifiers),
				VirtualKeyCode = VirtualKey.UNKNOWN
			};

		}

		WebKeyboardEvent CreateWebKeyboardEvent(Axiom.Input.KeyEventArgs e, WebKeyType type)
		{
			return new WebKeyboardEvent()
			{
				IsSystemKey = false,
				Type = type,
				Modifiers = AwesomiumUtil.ConvertModifierKeys(e.Modifiers),
				VirtualKeyCode = AwesomiumUtil.ConvertKeyCodesToVirtualKey(e.Key)
			};

		}

		public void CreateScene()
		{

			//ManualObject manual = _root.SceneManager.CreateManualObject("TEST");
			
			
			_entity = _root.SceneManager.CreateEntity("CubeBrowser", PrefabEntity.Cube);
			
			_sceneNode = Root.Instance.SceneManager.RootSceneNode.CreateChildSceneNode();
			_sceneNode.AttachObject(_entity);
			//_sceneNode.Yaw(45);
			_sceneNode.Position = new Vector3(0, 0, -300);
			_sceneNode.Scale = new Vector3(5, 4, 4);
			TextureUtil.CreateDynamicTextureAndMaterial(
				"CBDynamicTexture",
				"CBDynamicMaterial",
				_browserWidth,
				_browserHeight,
				out _texture,
				out _material);

			_entity.MaterialName = "CBDynamicMaterial";


			Core.BrowserManager.BrowserRenderEvent += new BrowserRenderEventHandler(BrowserManager_BrowserRenderEvent);
			_browserId = Core.BrowserManager.CreateBrowser("http://www.google.com.au", _browserWidth, _browserHeight);

			CreateOverlay();
		}

		void BrowserManager_BrowserRenderEvent(Guid id, System.Drawing.Bitmap screen)
		{
			if (id == _browserId)
			{
				TextureUtil.BitmapToTexture(screen, _texture);
			}
		}


		public void OnRenderFrame(long timeDelta)
		{
			//_sceneNode.Roll(timeDelta * 0.02f);
		}


		public void OnUnload()
		{

		}

		bool IsPressed = false;
		Random r = new Random();
        int mposx = 0;
        int mposy = 0;
		public void OnUpdateInput(Axiom.Input.InputReader inputReader)
		{
            if (mposx != inputReader.AbsoluteMouseX || mposy != inputReader.AbsoluteMouseY)
            {
                mposx = inputReader.AbsoluteMouseX;
                mposy = inputReader.AbsoluteMouseY;

                Core.BrowserManager[_browserId].MouseMove(mposx, mposy);
            }
            
            
            string status = "off";

            if (inputReader.IsMousePressed(MouseButtons.Left))
            {
                status = "on";
                IsPressed = true;

                Core.BrowserManager[_browserId].MouseDown(MouseButton.Left);

            }
            if (!inputReader.IsMousePressed(MouseButtons.Left) && IsPressed)
            {
                Core.BrowserManager[_browserId].MouseUp(MouseButton.Left);

                //var _camera = Game.Context.Camera;
                //var _renderWindow = Game.Context.RenderWindow;
                //var _ray = _camera.GetCameraToViewportRay(
                //        inputReader.AbsoluteMouseX / (float)_renderWindow.Width,
                //        inputReader.AbsoluteMouseY / (float)_renderWindow.Height);
                //RaySceneQuery _sceneQuery = _root.SceneManager.CreateRayQuery(
                //    _ray, (uint)SceneQueryTypeMask.WorldGeometry);
                //_sceneQuery.AddWorldFragmentType(WorldFragmentType.SingleIntersection);
                //_sceneQuery.AddWorldFragmentType(WorldFragmentType.CustomGeometry);
                //var results = _sceneQuery.Execute();


                //foreach (RaySceneQueryResultEntry result in results)
                //{

                //    if (result.SceneObject != null)
                //    {
                        

                       // result.SceneObject.ShowBoundingBox = true;
                        //var woot = _ray.GetPoint(result.Distance);
                        //Vector3[] corners = result.SceneObject.GetWorldBoundingBox().Corners;

                        //var res = (from v in corners
                        //           orderby v.Distance(woot)
                        //           select v).Take(4);
                        ////var i = 1;
                        ////i++;

                        //foreach (var v in res)
                        //{
                        //    var e = _root.SceneManager.CreateEntity("1BasicCube" + r.Next().ToString(), PrefabEntity.Cube);
                        //    e.MaterialName = "CBDynamicMaterial";

                        //    var s = Root.Instance.SceneManager.RootSceneNode.CreateChildSceneNode();
                        //    s.Position = v;
                        //    s.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                        //    s.AttachObject(e);
                        //    s.Yaw(45);
                        //}

                //    }
                //}



                IsPressed = false;

            }


            //_element.Text = String.Format("X: {0} Y: {1} Status: {2}", 
            //    inputReader.AbsoluteMouseX,
            //    inputReader.AbsoluteMouseY, 
            //    status);


		}
		
		public void CreateOverlay()
		{



            //_panel = (OverlayElementContainer)OverlayManager.Instance.Elements.CreateElementFromFactory("Panel", "DebugPanel");
            //_panel.SetPosition(1, 1);
            //_panel.SetDimensions(1, 1);
            //_panel.ScreenLeft(0);
            //_panel.ScreenTop(0);

            //_element = (TextArea)OverlayManager.Instance.Elements.CreateElementFromFactory("TextArea", "DebugTexy");
            //_element.FontName = "Arial";
            //_element.Text = "Click ON";
            //_element.MetricsMode = MetricsMode.Pixels;
            //_element.SetDimensions(1, 1);
            //_element.SetPosition(1, 1);
            //_element.CharHeight = 48;
            //_element.Color = ColorEx.Blue;
            //_element.Show();
            //_panel.AddChild(_element);
            //_panel.Show();

            //_overlay = OverlayManager.Instance.Create("DebugOverlay");
            //_overlay.ZOrder = 600;
            //_overlay.AddElement(_panel);
            //_overlay.Show();

		}

		private OverlayElementContainer _panel;
		private TextArea _element;
		private Overlay _overlay;
	}
}