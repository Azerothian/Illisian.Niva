using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axiom.Core;
using System.Drawing;
using Axiom.Graphics;
using System.Drawing.Imaging;
using System.IO;
using Axiom.Media;

namespace Illisian.Niva.AxiomEngine
{
	public class TextureUtil
	{
		/// <summary>
		/// Bitmaps to texture.
		/// </summary>
		/// <param name="bmp">The BMP.</param>
		/// <param name="texture">The texture.</param>
		public unsafe static void BitmapToTexture(Bitmap bitmap, Texture texture)
		{

			var bmp = bitmap.Clone(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			var bitmapData = bmp.LockBits(
				new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height),
				ImageLockMode.ReadOnly,
				bmp.PixelFormat);

			HardwarePixelBuffer buffer = texture.GetBuffer();
			buffer.Lock(Axiom.Graphics.BufferLocking.Discard);
			PixelBox pixelBox = buffer.CurrentLock;

			UInt32* texData = (UInt32*)pixelBox.Data;
			UInt32* mapData = (UInt32*)bitmapData.Scan0;

			var height = Math.Min(pixelBox.Height, bmp.Height);
			var width = Math.Min(pixelBox.Width, bmp.Width);

			for (var y = 0; y < height; ++y)
				for (var x = 0; x < width; ++x)
					texData[pixelBox.RowPitch * y + x] = mapData[bitmapData.Stride / 4 * y + x];

			buffer.Unlock();
			bmp.UnlockBits(bitmapData);

		}

		/*
		 * System::Void JointPositionOverlay::copyBitmapToTexture()
{
  // Lock the texture buffer so we can write to it

  Ogre::HardwarePixelBufferSharedPtr buffer = mTexture->getBuffer ();
  buffer->lock(Ogre::HardwareBuffer::HBL_DISCARD);
  const Ogre::PixelBox &pb = buffer->getCurrentLock();
  Ogre::uint32 *texData = static_cast<Ogre::uint32*>(pb.data);


  // Lock the bitmap buffer so we can read from it

  System::Drawing::Imaging::BitmapData^ bmd = 
    mBitmap->LockBits(
      System::Drawing::Rectangle(0, 0, mBitmap->Width, mBitmap->Height),
      System::Drawing::Imaging::ImageLockMode::ReadOnly, 
      System::Drawing::Imaging::PixelFormat::Format32bppArgb);
  Ogre::uint32* mapData = static_cast<Ogre::uint32*>(bmd->Scan0.ToPointer());


  // Now copy the data between buffers

  size_t height = std::min (pb.getHeight(), (size_t)bmd->Height);
  size_t width = std::min(pb.getWidth(), (size_t)bmd->Width);
  for(size_t y=0; y<height; ++y) 
    for(size_t x=0; x<width; ++x) 
      texData[pb.rowPitch*y + x] = mapData[bmd->Stride/4 * y + x];


  // Unlock the buffers again

  mBitmap->UnlockBits(bmd);
  buffer->unlock();
}
		 * 
		 * 
		 * 
		 */

		/// <summary>
		/// Creates the dynamic texture and material.
		/// </summary>
		/// <param name="TextureName">Name of the texture.</param>
		/// <param name="MaterialName">Name of the material.</param>
		/// <param name="textureHeight">Height of the texture.</param>
		/// <param name="textureWidth">Width of the texture.</param>
		/// <param name="texture">The texture.</param>
		/// <param name="material">The material.</param>
		public static void CreateDynamicTextureAndMaterial(string TextureName, string MaterialName, int textureHeight, int textureWidth, out Texture texture, out Material material)
		{
			texture = TextureManager.Instance.CreateManual(TextureName,
						ResourceGroupManager.DefaultResourceGroupName,
						TextureType.TwoD, textureWidth, textureHeight, 0,
						Axiom.Media.PixelFormat.A8R8G8B8, TextureUsage.DynamicWriteOnlyDiscardable);


			material = (Material)MaterialManager.Instance.Create(MaterialName,
						ResourceGroupManager.DefaultResourceGroupName);

			material.GetTechnique(0).GetPass(0).CreateTextureUnitState(TextureName);


		}





	}
}
