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

			Bitmap bmp = new Bitmap(100, 100, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			using (Graphics gImage = Graphics.FromImage(bmp))
			{
				gImage.FillRectangle(Brushes.Red, 0, 0, bmp.Width, bmp.Height);
				gImage.DrawRectangle(Pens.Black, 10, 10, bmp.Width - 20, bmp.Height - 20);
			}

			TextureUtil.CreateDynamicTextureAndMaterial(
				"DynamicTexture", 
				"DynamicMaterial", 
				100, 
				100, 
				out _texture, 
				out _material);

			bmp.Save(@"E:\Temp\test2.jpg", ImageFormat.Jpeg);

			TextureUtil.BitmapToTexture(bmp, _texture);
			
			bmp.Save(@"E:\Temp\test3.jpg", ImageFormat.Jpeg);


			_entity.MaterialName = "DynamicMaterial";

			//_entity.MaterialName = "Examples/TestImage";
			//_entity.CastShadows = true;

			//Core.BrowserManager.BrowserRenderEvent += new BrowserRenderEventHandler(BrowserManager_BrowserRenderEvent);
			//_browserId = Core.BrowserManager.CreateBrowser("http://www.google.com.au", _browserWidth, _browserHeight);
		}

		//void BrowserManager_BrowserRenderEvent(Guid id, System.Drawing.Bitmap screen)
		//{
		//    if (id == _browserId)
		//    {

		//    //	_entity.MaterialName = "DynamicMaterial";
		//        //TextureUtil.BitmapToTexture(screen, _texture);
		//        //_texture.Reload();
		//        //_material.Reload();



		//    }


		//}


		public void OnRenderFrame(long timeDelta)
		{
			_sceneNode.Roll(timeDelta * 0.02f);
		}


		public void OnUnload()
		{

		}

	}
}