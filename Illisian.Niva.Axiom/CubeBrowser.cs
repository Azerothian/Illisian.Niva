using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axiom.Graphics;
using Axiom.Core;
using Axiom.Media;
using System.Drawing.Imaging;
using System.IO;

namespace Illisian.Niva.AxiomEngine
{
	public class CubeBrowser
	{
		private Root _root;

		private Entity _entity;
		private Texture _texture;
		private Material _material;
		private Guid _browserId = Guid.Empty;
		private SceneNode _sceneNode;

		public CubeBrowser(Root root)
		{
			_root = root;
		}
		public void CreateScene()
		{
			_entity = _root.SceneManager.CreateEntity("CubeBrowser", PrefabEntity.Cube);

			_sceneNode = Root.Instance.SceneManager.RootSceneNode.CreateChildSceneNode();
			_sceneNode.AttachObject(_entity);
			_sceneNode.Yaw(45);

			Core.BrowserManager.BrowserRenderEvent += new BrowserRenderEventHandler(BrowserManager_BrowserRenderEvent);
			_browserId = Core.BrowserManager.CreateBrowser("http://www.google.com.au", 640, 480);
		}

		void BrowserManager_BrowserRenderEvent(Guid id, System.Drawing.Bitmap screen)
		{
			if (id == _browserId)
			{
				if (_texture == null)
				{
					_texture = TextureManager.Instance.CreateManual("DynamicTexture",
						ResourceGroupManager.DefaultResourceGroupName,
						TextureType.TwoD, screen.Width, screen.Height, 0,
						Axiom.Media.PixelFormat.A8R8G8B8, TextureUsage.DynamicWriteOnlyDiscardable);

					_material = (Material)MaterialManager.Instance.Create("DynamicMaterial",
						ResourceGroupManager.DefaultResourceGroupName);

					var unitState = _material.GetTechnique(0).GetPass(0).CreateTextureUnitState("DynamicTexture");

					_entity.MaterialName = "DynamicMaterial";
					_entity.CastShadows = true;
					
				}
				var bitmapData = screen.LockBits(
					new System.Drawing.Rectangle(0, 0, screen.Width, screen.Height),
					ImageLockMode.ReadOnly,
					screen.PixelFormat);

				var buffer = _texture.GetBuffer();
				buffer.Lock(Axiom.Graphics.BufferLocking.Discard);


				buffer.CurrentLock.Data = bitmapData.Scan0;
				buffer.CurrentLock.Format = Axiom.Media.PixelFormat.A8R8G8B8;

				buffer.Unlock();
				screen.UnlockBits(bitmapData);

				//using (MemoryStream ms = new MemoryStream())
				//{
				//    screen.Save(ms, ImageFormat.Bmp);
				//    screen.Save(@"E:\Temp\test.jpg", ImageFormat.Jpeg);
				//    _texture.LoadImage(Image.FromRawStream(ms, screen.Width, screen.Height, Axiom.Media.PixelFormat.A8R8G8B8));
				//}


			}


		}

		internal void OnUnload()
		{

		}

		internal void OnRenderFrame(long timeDelta)
		{
			_sceneNode.Roll(timeDelta * 0.02f);
		}
	}
}