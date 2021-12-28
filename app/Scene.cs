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
         // render lightSources and perform calculations
         foreach (var l in lightSources) {
            camera.Render(l, wireframe);
         }

         // render renderables
         foreach (var r in renderableObjects) {
            camera.Render(r, wireframe);
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