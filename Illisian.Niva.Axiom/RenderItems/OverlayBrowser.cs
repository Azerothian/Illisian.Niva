using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axiom.Core;
using Axiom.Graphics;
using Axiom.Overlays;

namespace Illisian.Niva.AxiomEngine
{
	public class OverlayBrowser : IRenderItem
	{
		private Root _root;

		//private Entity _entity;
		private Texture _texture;
		private Material _material;
		private Guid _browserId = Guid.Empty;
		private OverlayElementContainer _panel;
		//private SceneNode _sceneNode;

		private Overlay _overlay;
		int _browserWidth = 640;
		int _browserHeight = 480;

		public void CreateScene()
		{
			TextureUtil.CreateDynamicTextureAndMaterial(
				"OBDynamicTexture",
				"OBDynamicMaterial",
				_browserWidth,
				_browserHeight,
				out _texture,
				out _material);



			
			_panel = (OverlayElementContainer)OverlayManager.Instance.Elements.CreateElement("Panel", "Panels");
			_panel.SetPosition(1, 1);
			_panel.SetDimensions(_browserWidth, _browserHeight);
			_panel.MaterialName = "OBDynamicMaterial";


			_overlay = OverlayManager.Instance.Create("OverlayBrowser");
			_overlay.AddElement(_panel);
			_overlay.Show();

			Core.BrowserManager.BrowserRenderEvent += BrowserManager_BrowserRenderEvent;
			_browserId = Core.BrowserManager.CreateBrowser("http://www.google.com.au", _browserWidth, _browserHeight);
		}

		public void Initialise(Root item)
		{
			_root = item;
		}



		public void OnRenderFrame(long timeDelta)
		{
		}

		public void OnUnload()
		{
		}

		void BrowserManager_BrowserRenderEvent(Guid id, System.Drawing.Bitmap screen)
		{
			if (id == _browserId)
			{
				
				TextureUtil.BitmapToTexture(screen, _texture);
			}
		}



		public void OnUpdateInput(Axiom.Input.InputReader inputReader)
		{

		}
	}
}
