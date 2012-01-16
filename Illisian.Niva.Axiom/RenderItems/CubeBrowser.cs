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

namespace Illisian.Niva.AxiomEngine
{
	public class CubeBrowser : IRenderItem
	{
		int _browserWidth = 640;
		int _browserHeight = 480;

		private Root _root;

		private Entity _entity;
		private Texture _texture;
		private Material _material;
		private Guid _browserId = Guid.Empty;
		private SceneNode _sceneNode;

		public void Initialise(Root item)
		{
			_root = item;

		}

		public void CreateScene()
		{
			_entity = _root.SceneManager.CreateEntity("CubeBrowser", PrefabEntity.Cube);

			_sceneNode = Root.Instance.SceneManager.RootSceneNode.CreateChildSceneNode();
			_sceneNode.AttachObject(_entity);
			_sceneNode.Yaw(45);
			_sceneNode.Position = new Vector3(0, 0, -300);

			TextureUtil.CreateDynamicTextureAndMaterial(
				"DynamicTexture", 
				"DynamicMaterial", 
				_browserHeight, 
				_browserWidth, 
				out _texture, 
				out _material);


			_entity.MaterialName = "Examples/TestImage";
			_entity.CastShadows = true;

			Core.BrowserManager.BrowserRenderEvent += new BrowserRenderEventHandler(BrowserManager_BrowserRenderEvent);
			_browserId = Core.BrowserManager.CreateBrowser("http://www.google.com.au", _browserWidth, _browserHeight);
		}

		void BrowserManager_BrowserRenderEvent(Guid id, System.Drawing.Bitmap screen)
		{
			if (id == _browserId)
			{

				_entity.MaterialName = "DynamicMaterial";
				TextureUtil.BitmapToTexture(screen, _texture);
				//_texture.Reload();
				//_material.Reload();



			}


		}


		public void OnRenderFrame(long timeDelta)
		{
			_sceneNode.Roll(timeDelta * 0.02f);
		}


		public void OnUnload()
		{

		}

	}
}