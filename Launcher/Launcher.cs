using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using System;
using renderable;
using lightsource;
using shader;
using texture;
using game;

// TODO: refactor naming of everything
// --- classes:
// TODO: Material class
// TODO: Mesh class
// TODO: ControlHelper/InputHelper helper class
// DONE: LightSource
//          -> consider making a lightSources list in the game (in the future scene class) and render them first, then the renderableObjects
// DONE: Scene (renderables + light sources + camera)
// DONE: abstract the game class (use the default OnLoad etc, and call there the OnLoad defined by the user their derived class)
// --- features:
// TODO: integrate .obj and .mtl files with the renderable creation process

namespace physics_goes_brr
{
   public class MyGame : Game
   {
		 float r = 0.0f;
		 float s = 0.0f;
		 float t = 0.0f;

		 public MyGame(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
				: base(gameWindowSettings, nativeWindowSettings)
      {
			 // -- snippet --
      }

      protected override void UserLoad()
      {
         GL.ClearColor(0.2f, 0.2f, 0.2f, 1.0f);
         GL.Enable(EnableCap.DepthTest);

         // -- resource loading code --

         scene.camera.Move(new Vector3(.0f, .0f, 3.0f));

         // normal cube with texture
         scene.renderableObjects.Add(new Renderable(
            new float[]{
            // position            normal              color                     texture coords
            -0.5f, -0.5f,  0.5f,   1.0f, 1.0f, 1.0f,   1.0f, 0.0f, 0.0f, 1.0f,   0.0f, 0.0f,   // near top    right
             0.5f, -0.5f,  0.5f,   1.0f, 1.0f, 1.0f,   1.0f, 0.0f, 0.0f, 1.0f,   1.0f, 0.0f,   // near bottom right
             0.5f,  0.5f,  0.5f,   1.0f, 1.0f, 1.0f,   1.0f, 0.0f, 0.0f, 1.0f,   1.0f, 1.0f,   // near bottom left
            -0.5f,  0.5f,  0.5f,   1.0f, 1.0f, 1.0f,   1.0f, 0.0f, 0.0f, 1.0f,   0.0f, 1.0f,   // near top    left
            -0.5f, -0.5f, -0.5f,   1.0f, 1.0f, 1.0f,   1.0f, 0.0f, 0.0f, 1.0f,   0.0f, 0.0f,   // far  top    right
             0.5f, -0.5f, -0.5f,   1.0f, 1.0f, 1.0f,   1.0f, 0.0f, 0.0f, 1.0f,   1.0f, 0.0f,   // far  bottom right
             0.5f,  0.5f, -0.5f,   1.0f, 1.0f, 1.0f,   1.0f, 0.0f, 0.0f, 1.0f,   1.0f, 1.0f,   // far  bottom left
            -0.5f,  0.5f, -0.5f,   1.0f, 1.0f, 1.0f,   1.0f, 0.0f, 0.0f, 1.0f,   0.0f, 1.0f    // far  top    left
            },
            new uint[]{
               // front face
               0, 1, 2,
               2, 3, 0,
               // back face
               7, 6, 5,
               5, 4, 7,
               // left face
               4, 5, 1,
               1, 0, 4,
               // right face
               3, 2, 6,
               6, 7, 3,
               // top face
               1, 5, 6,
               6, 2, 1,
               // bottom face
               4, 0, 3,
               3, 7, 4,
            },
            new Shader("./res/shaders/vert_shader.vert", "./res/shaders/frag_shader.frag"),
            new Texture[] { new Texture("./res/textures/container.jpg"), new Texture("./res/textures/awesomeface.jpg")/*, new Texture("./res/trollface.jpg")*/ }
         ));

         scene.renderableObjects.Add(new Renderable(
            new float[]{
            // position            normal              color                     texture coords
            -0.5f, -0.5f,  0.5f,   1.0f, 1.0f, 1.0f,   0.1f, 0.2f, 0.5f, 1.0f,   0.0f, 0.0f,   // near top    right
             0.5f, -0.5f,  0.5f,   1.0f, 1.0f, 1.0f,   0.1f, 0.2f, 0.5f, 1.0f,   1.0f, 0.0f,   // near bottom right
             0.5f,  0.5f,  0.5f,   1.0f, 1.0f, 1.0f,   0.1f, 0.2f, 0.5f, 1.0f,   1.0f, 1.0f,   // near bottom left
            -0.5f,  0.5f,  0.5f,   1.0f, 1.0f, 1.0f,   0.1f, 0.2f, 0.5f, 1.0f,   0.0f, 1.0f,   // near top    left
            -0.5f, -0.5f, -0.5f,   1.0f, 1.0f, 1.0f,   0.1f, 0.2f, 0.5f, 1.0f,   0.0f, 0.0f,   // far  top    right
             0.5f, -0.5f, -0.5f,   1.0f, 1.0f, 1.0f,   0.1f, 0.2f, 0.5f, 1.0f,   1.0f, 0.0f,   // far  bottom right
             0.5f,  0.5f, -0.5f,   1.0f, 1.0f, 1.0f,   0.1f, 0.2f, 0.5f, 1.0f,   1.0f, 1.0f,   // far  bottom left
            -0.5f,  0.5f, -0.5f,   1.0f, 1.0f, 1.0f,   0.1f, 0.2f, 0.5f, 1.0f,   0.0f, 1.0f    // far  top    left
            },
            new uint[]{
               // front face
               0, 1, 2,
               2, 3, 0,
               // back face
               7, 6, 5,
               5, 4, 7,
               // left face
               4, 5, 1,
               1, 0, 4,
               // right face
               3, 2, 6,
               6, 7, 3,
               // top face
               1, 5, 6,
               6, 2, 1,
               // bottom face
               4, 0, 3,
               3, 7, 4,
            },
            new Shader("./res/shaders/vert_shader.vert", "./res/shaders/frag_shader.frag"),
            new Texture[] {}
         ));
         scene.renderableObjects[1]
            .Translate(new Vector3(-1.5f, 0.4f, 1.8f))
            .Rotate(new Vector3(30.0f, 90.0f, 20.0f));

         // light source
         scene.lightSources.Add(new LightSource(
            new float[]{
            // position            normal              color                     texture coords
            -0.5f, -0.5f,  0.5f,   1.0f, 1.0f, 1.0f,   0.3f, 1.0f, 0.8f, 1.0f,   0.0f, 0.0f,   // near top    right
             0.5f, -0.5f,  0.5f,   1.0f, 1.0f, 1.0f,   0.3f, 1.0f, 0.8f, 1.0f,   1.0f, 0.0f,   // near bottom right
             0.5f,  0.5f,  0.5f,   1.0f, 1.0f, 1.0f,   0.3f, 1.0f, 0.8f, 1.0f,   1.0f, 1.0f,   // near bottom left
            -0.5f,  0.5f,  0.5f,   1.0f, 1.0f, 1.0f,   0.3f, 1.0f, 0.8f, 1.0f,   0.0f, 1.0f,   // near top    left
            -0.5f, -0.5f, -0.5f,   1.0f, 1.0f, 1.0f,   0.3f, 1.0f, 0.8f, 1.0f,   0.0f, 0.0f,   // far  top    right
             0.5f, -0.5f, -0.5f,   1.0f, 1.0f, 1.0f,   0.3f, 1.0f, 0.8f, 1.0f,   1.0f, 0.0f,   // far  bottom right
             0.5f,  0.5f, -0.5f,   1.0f, 1.0f, 1.0f,   0.3f, 1.0f, 0.8f, 1.0f,   1.0f, 1.0f,   // far  bottom left
            -0.5f,  0.5f, -0.5f,   1.0f, 1.0f, 1.0f,   0.3f, 1.0f, 0.8f, 1.0f,   0.0f, 1.0f    // far  top    left
            },
            new uint[]{
               // front face
               0, 1, 2,
               2, 3, 0,
               // back face
               7, 6, 5,
               5, 4, 7,
               // left face
               4, 5, 1,
               1, 0, 4,
               // right face
               3, 2, 6,
               6, 7, 3,
               // top face
               1, 5, 6,
               6, 2, 1,
               // bottom face
               4, 0, 3,
               3, 7, 4,
            },
            new Shader("./res/shaders/lamp/vert_shader.vert", "./res/shaders/lamp/frag_shader.frag"),
            new Texture[] { /*new Texture("./res/textures/container.jpg"), new Texture("./res/textures/awesomeface.jpg")*/ },
            new Vector3(0.3f, 1.0f, 0.8f),
            new Vector3(1.0f)
         ));
         scene.lightSources[0]
            .Translate(new Vector3(2.0f, 1.4f, -2.1f))
            .Scale(new Vector3(0.5f));
      }

      protected override void UserUnload()
      {
         scene.Delete();
      }

      protected override void UserInput(FrameEventArgs args)
      {
         var keyboard = KeyboardState;

         if (keyboard.IsKeyDown(Keys.Escape))
         {
            base.shouldClose = true;
         }
         if (keyboard.IsKeyPressed(Keys.M))
         {
            base.wireframeMode = !wireframeMode;
         }
         if (keyboard.IsKeyPressed(Keys.N))
         {
            base.mouseFocused = !base.mouseFocused;
            CursorGrabbed = !CursorGrabbed;
            if (!CursorGrabbed) { CursorVisible = true; }
         }

         Vector3 horizontalDir = Vector3.Normalize(new Vector3(scene.camera.dir.X, 0.0f, scene.camera.dir.Z));
         Vector3 horizontalUp = new Vector3(0.0f, 1.0f, 0.0f);

         if (keyboard.IsKeyDown(Keys.W))
         {
            scene.camera.Move(horizontalDir * speed * (float)args.Time);
         }
         if (keyboard.IsKeyDown(Keys.A))
         {
            scene.camera.Move((-scene.camera.right) * speed * (float)args.Time);
         }
         if (keyboard.IsKeyDown(Keys.S))
         {
            scene.camera.Move((-horizontalDir) * speed * (float)args.Time);
         }
         if (keyboard.IsKeyDown(Keys.D))
         {
            scene.camera.Move(scene.camera.right * speed * (float)args.Time);
         }
         if (keyboard.IsKeyDown(Keys.Space))
         {
            scene.camera.Move(horizontalUp * speed * (float)args.Time);
         }
         if (keyboard.IsKeyDown(Keys.LeftShift))
         {
            scene.camera.Move((-horizontalUp) * speed * (float)args.Time);
         }
         if (keyboard.IsKeyPressed(Keys.Z))
         {
            scene.camera.ZoomIn(1.8f);
         }
         if (keyboard.IsKeyPressed(Keys.X))
         {
            scene.camera.ZoomOut(1.8f);
         }

         var mouse = MouseState;

         if (mouseFocused)
         {
            float dx = mouse.X - lastPos.X;
            float dy = mouse.Y - lastPos.Y;
            scene.camera.pitch -= dy * sensitivity;
				scene.camera.yaw += dx * sensitivity;
            lastPos = new Vector2(mouse.X, mouse.Y);
            //System.Console.WriteLine(System.String.Format("x: {0}, y: {1}\t dx: {2}, dy: {3}", mouse.X, mouse.Y, dx, dy));
            scene.camera.UpdateDirection();
         }
      }

      protected override void UserUpdate(FrameEventArgs args)
      {
         r += 0.1f;
         s += 0.1f;
         t += 0.1f;
         scene.renderableObjects[0].Rotate(new Vector3(.0f, .1f, .2f));
         scene.lightSources[0].color = new Vector3(MathF.Abs(MathF.Sin(MathHelper.DegreesToRadians(2 * r))), MathF.Abs(MathF.Sin(MathHelper.DegreesToRadians(4 * s))), MathF.Abs(MathF.Sin(MathHelper.DegreesToRadians(t))));
      }

      protected override void UserRender(FrameEventArgs args)
      {
         GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

         // -- RENDERING CODE --
         scene.Render(wireframeMode);

         Context.SwapBuffers();
      }
   }

   class Launcher
   {
      static void Main(string[] args)
      {
         GameWindowSettings gameWindowSettings = GameWindowSettings.Default;
         NativeWindowSettings nativeWindowSettings = NativeWindowSettings.Default;
         nativeWindowSettings.Title = "Hello OpenTK";
         nativeWindowSettings.Size = new Vector2i(840, 840);
         MyGame myApp = new MyGame(gameWindowSettings, nativeWindowSettings);
         myApp.Start();
      }
   }
}
