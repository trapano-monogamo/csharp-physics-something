using OpenTK.Mathematics;
using renderable;

namespace lightsource
{
   public enum LightSourceType {
      Point = 0,
      Directional,
      Spotlight,
      Ambient
   }

   public class LightSource {
      public LightSourceType type;
      public Vector3 position;
      public Color4 color;

      public LightSource(LightSourceType t, Vector3 pos, Color4 col) {
         type = t;
         position = pos;
         color = col;
      }
   }
}