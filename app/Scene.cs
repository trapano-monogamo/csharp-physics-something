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

      public Scene(
         Camera _camera,
         List<Renderable> _renderableObjects,
         List<LightSource> _lightSources
      ) {
         camera = _camera;
         renderableObjects = _renderableObjects;
         lightSources = _lightSources;
      }

      public void Render(bool wireframe)
      {
         // render lightSources and perform calculations
         // -- snippet --

         // render renderables
         foreach (var r in renderableObjects) {
            camera.Render(r, wireframe);
         }
      }

      public void Delete()
      {
         // TODO : delete scene.lightSources
         foreach (var r in renderableObjects) {
            r.Delete();
         }
      }
   }
}