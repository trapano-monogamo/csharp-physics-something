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
      public Renderable boundObject;
      public LightSourceType type;
      public Vector3 position;
      public Vector3 direction;
      public Color4 color;

      public LightSource() {}

      public LightSource(LightSourceType t, Vector3 pos, Color4 col) {
         type = t;
         position = pos;
         color = col;
         boundObject = new Renderable();
      }

      public LightSource(LightSourceType t, Color4 col, ref Renderable r) {
         type = t;
         color = col;
         boundObject = r;
      }

      public void AttachRenderable(ref Renderable r) {
         this.boundObject = r;
      }
   }
}