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

      public Scene(
         Camera _camera,
         List<Renderable> _renderableObjects,
         List<LightSource> _lightSources
      ) {
         camera = _camera;
         renderableObjects = _renderableObjects;
         lightSources = _lightSources;
      }
   }
}