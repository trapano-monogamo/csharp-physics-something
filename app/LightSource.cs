using OpenTK.Mathematics;
using renderable;
using texture;
using shader;

namespace lightsource
{
   public enum LightSourceType {
      Point = 0,
      Directional,
      Spotlight,
      Ambient
   }

   public class LightSource : Renderable {
      public LightSourceType type;
      public Vector3 direction;
      public Color4 color;

      public LightSource() {}

      public LightSource(float[] vertexRawData, uint[] indexData, Shader _shaderProgram, Texture[] _textures, Color4 col, string name = "unknown")
         : base(vertexRawData, indexData, _shaderProgram, _textures, name)
      {
         this.color = col;
      }
   }
}