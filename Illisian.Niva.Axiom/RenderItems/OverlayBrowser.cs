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
		private OverlayElement _panel;
		//private SceneNode _sceneNode;

		private Overlay _overlay;


		public void CreateScene()
		{
			_overlay = OverlayManager.Instance.Create("OverlayBrowser");
			_panel = OverlayManager.Instance.Elements.CreateElement("Panel", "Panels");
			_panel.SetPosition(1, 1);
			_panel.SetDimensions(640, 480);
			_overlay.Show();

			Core.BrowserManager.BrowserRenderEvent += BrowserManager_BrowserRenderEvent;
			_browserId = Core.BrowserManager.CreateBrowser("http://www.google.com.au", 640, 480);
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
				if (_texture == null)
				{
					TextureUtil.CreateDynamicTextureAndMaterial("DynamicTexture", "DynamicMaterial", screen.Height, screen.Width, out _texture, out _material);

					_panel.MaterialName = "DynamicMaterial";
					//_entity.CastShadows = true;

				}
				TextureUtil.BitmapToTexture(screen, _texture);

			}
		}

	}
}
