using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axiom.Core;
using System.Drawing;
using Axiom.Graphics;
using System.Drawing.Imaging;
using System.IO;

namespace Illisian.Niva.AxiomEngine
{
	public class TextureUtil
	{
		/// <summary>
		/// Bitmaps to texture.
		/// </summary>
		/// <param name="bmp">The BMP.</param>
		/// <param name="texture">The texture.</param>
		public static void BitmapToTexture(Bitmap bitmap, Texture texture)
		{

			var bmp = bitmap.Clone(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), PixelFormat.Format32bppArgb);

			var bitmapData = bmp.LockBits(
				new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height),
				ImageLockMode.ReadOnly,
				bmp.PixelFormat);

			var buffer = texture.GetBuffer();
			buffer.Lock(Axiom.Graphics.BufferLocking.Discard);


			buffer.CurrentLock.Data = bitmapData.Scan0;
			buffer.CurrentLock.Format = Axiom.Media.PixelFormat.A8R8G8B8;

			buffer.Unlock();
			bmp.UnlockBits(bitmapData);
			bmp.Dispose();

			//using (MemoryStream ms = new MemoryStream())
			//{
			//    //bmp.
			//    bmp.Save(ms,ImageFormat.MemoryBmp);
			//    //bmp.Save(@"E:\Temp\test.jpg", ImageFormat.Jpeg);

			//    texture.LoadRawData(ms, bmp.Width, bmp.Height, Axiom.Media.PixelFormat.A8R8G8B8);
			//    //texture.LoadImage(Axiom.Media.Image.FromStream(ms, "jpeg"));

			//    //texture.LoadImage(Axiom.Media.Image.FromRawStream(ms, bmp.Width, bmp.Height, Axiom.Media.PixelFormat.A8R8G8B8));
			//}


		}
		/// <summary>
		/// Creates the dynamic texture and material.
		/// </summary>
		/// <param name="TextureName">Name of the texture.</param>
		/// <param name="MaterialName">Name of the material.</param>
		/// <param name="textureHeight">Height of the texture.</param>
		/// <param name="textureWidth">Width of the texture.</param>
		/// <param name="texture">The texture.</param>
		/// <param name="material">The material.</param>
		public static void CreateDynamicTextureAndMaterial(string TextureName, string MaterialName,int textureHeight, int textureWidth, out Texture texture, out Material material)
		{
			texture = TextureManager.Instance.CreateManual(TextureName,
						ResourceGroupManager.DefaultResourceGroupName,
						TextureType.TwoD, textureWidth, textureHeight, 0,
						Axiom.Media.PixelFormat.A8R8G8B8, TextureUsage.Dynamic);


			material = (Material)MaterialManager.Instance.Create(MaterialName,
						ResourceGroupManager.DefaultResourceGroupName);

			material.GetTechnique(0).GetPass(0).CreateTextureUnitState(TextureName);
			

		}





	}
}
