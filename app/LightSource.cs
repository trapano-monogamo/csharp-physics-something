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
      public Vector3 color;

      public LightSource() {}

      public LightSource(float[] vertexRawData, uint[] indexData, Shader _shaderProgram, Texture[] _textures, Vector3 col, string name = "unknown")
         : base(vertexRawData, indexData, _shaderProgram, _textures, name)
      {
         this.color = col;
      }

      public LightSource(float[] vertexRawData, uint[] indexData, Shader _shaderProgram, Texture[] _textures, Vector3 col, Vector3 dir, string name = "unknown")
         : base(vertexRawData, indexData, _shaderProgram, _textures, name)
      {
         this.color = col;
         this.direction = dir;
      }
   }
}