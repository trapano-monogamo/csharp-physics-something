using OpenTK.Mathematics;
using System.Collections.Generic;
using camera;
using renderable;
using lightsource;

namespace scene
{
   public class Scene{
      public Camera camera;
      public List<Renderable> renderableObjects;
      public List<LightSource> lightSources;

      public Scene() {
         // default scene
         camera = new Camera(90f);
         renderableObjects = new List<Renderable>();
         lightSources = new List<LightSource>();
      }

      public void Render(bool wireframe)
      {
         Vector3 light_direction = new Vector3();
         Vector3 light_color = new Vector3();

         // render lightSources and perform calculations
         foreach (var l in lightSources) {
            camera.RenderLight(l, wireframe);
            light_direction = l.direction;
            light_color = l.color;
         }

         // render renderables
         foreach (var r in renderableObjects) {
            camera.Render(r, light_direction, light_color, wireframe);
         }
      }

      public void Delete()
      {
         foreach (var l in lightSources) {
            l.Delete();
         }
         foreach (var r in renderableObjects) {
            r.Delete();
         }
      }
   }
}