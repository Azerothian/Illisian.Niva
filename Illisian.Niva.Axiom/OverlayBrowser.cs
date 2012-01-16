using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axiom.Core;
using Axiom.Graphics;
using Axiom.Overlays;

namespace Illisian.Niva.AxiomEngine
{
	public class OverlayBrowser
	{
		private Root _root;

		//private Entity _entity;
		//private Texture _texture;
		//private Material _material;
		private Guid _browserId = Guid.Empty;
		//private SceneNode _sceneNode;

		//private Overlay _overlay;

		public OverlayBrowser(Root root)
		{
			_root = root;
		}

		public void CreateScene()
		{
			//_overlay = OverlayManager.Instance.Create("OverlayBrowser");

			

			//OverlayElementContainer oec
			
			Core.BrowserManager.BrowserRenderEvent += BrowserManager_BrowserRenderEvent;
			_browserId = Core.BrowserManager.CreateBrowser("http://www.google.com.au", 640, 480);
		}

		void BrowserManager_BrowserRenderEvent(Guid id, System.Drawing.Bitmap screen)
		{
			if (id == _browserId)
			{
				
			}
		}

		internal void OnUnload()
		{

		}

		internal void OnRenderFrame(long timeDelta)
		{
			
		}

	}
}
