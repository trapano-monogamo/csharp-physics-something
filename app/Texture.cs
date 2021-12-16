using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace texture
{
   public class Texture
   {
      private int handle;

      public Texture(string path)
      {
         handle = GL.GenTexture();
         UseTexture();

         Image<Rgba32> image = Image.Load<Rgba32>(path);
         image.Mutate(x => x.Flip(FlipMode.Vertical));
         var pixels = new List<byte>(4 * image.Height * image.Width);
         for (int y = 0; y < image.Height; y++) {
            var row = image.GetPixelRowSpan(y);

            for (int x = 0; x < image.Width; x++) {
               pixels.Add(row[x].R);
               pixels.Add(row[x].G);
               pixels.Add(row[x].B);
               pixels.Add(row[x].A);
            }
         }

         // please for god's sake DO NOT FORGET to set the texture's parameters... you dumbass
         GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
         GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
         GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
         GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
         GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels.ToArray());
      }

      public void UseTexture(TextureUnit unit = TextureUnit.Texture0)
      {
         GL.ActiveTexture(unit);
         GL.BindTexture(TextureTarget.Texture2D, handle);
      }
   }
}