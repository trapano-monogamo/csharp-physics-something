using System.Collections.Generic;
using System.Linq;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using renderable;
using shader;
using texture;
using camera;
using objectloader;

// TODO : refactor naming of everything
// TODO : put renderable mesh data into mesh class
// TODO : put it all together and load .obj and .mtl files

namespace game
{
   public class Game : GameWindow
   {
      List<Renderable> renderableObjects;
      Camera camera;
      int wireframeMode;
      float speed;
      float sensitivity;
      Vector2 lastPos;
      bool mouseFocused;

      Vector2 windowSize;

      public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
         : base(gameWindowSettings, nativeWindowSettings)
      {
         renderableObjects = new List<Renderable>();
         wireframeMode = -1;
         camera = new Camera(90f);
         speed = 2.1f;
         sensitivity = 1.1f;
         windowSize = nativeWindowSettings.Size;
         mouseFocused = false;
         lastPos = new Vector2(0.0f);
      }

      public void Start()
      {
         Run();
      }

      protected override void OnResize(ResizeEventArgs e)
      {
         GL.Viewport(0,0,e.Width,e.Height);
         base.OnResize(e);
      }

      protected override void OnLoad()
      {
         GL.ClearColor(0.2f, 0.2f, 0.2f, 1.0f);
         GL.Enable(EnableCap.DepthTest);

         // -- resource loading code --

         var temp = ObjectLoader.LoadObjFile("./res/models/untitled.obj");
         temp.Print();

         camera.Move(new Vector3(.0f, .0f, 3.0f));
         
         renderableObjects.Add(new Renderable(
            new float[]{
            // position            color                     texture coords
            -0.5f, -0.5f,  0.5f,   1.0f, 0.0f, 0.0f, 0.5f,   0.0f, 0.0f,   // near top    right
             0.5f, -0.5f,  0.5f,   0.0f, 0.0f, 1.0f, 0.5f,   1.0f, 0.0f,   // near bottom right
             0.5f,  0.5f,  0.5f,   0.0f, 1.0f, 0.0f, 0.5f,   1.0f, 1.0f,   // near bottom left
            -0.5f,  0.5f,  0.5f,   1.0f, 1.0f, 1.0f, 0.5f,   0.0f, 1.0f,   // near top    left
            -0.5f, -0.5f, -0.5f,   1.0f, 0.0f, 1.0f, 0.5f,   0.0f, 0.0f,   // far  top    right
             0.5f, -0.5f, -0.5f,   0.0f, 1.0f, 0.0f, 0.5f,   1.0f, 0.0f,   // far  bottom right
             0.5f,  0.5f, -0.5f,   1.0f, 0.0f, 0.0f, 0.5f,   1.0f, 1.0f,   // far  bottom left
            -0.5f,  0.5f, -0.5f,   0.0f, 1.0f, 1.0f, 0.5f,   0.0f, 1.0f    // far  top    left
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
            new Texture[]{ new Texture("./res/textures/container.jpg"), new Texture("./res/textures/awesomeface.jpg")/*, new Texture("./res/trollface.jpg")*/ }
         ));
         
         renderableObjects.Add(new Renderable(
            new float[]{
            // position            color                     texture coords
            -0.5f, -0.5f,  0.5f,   0.0f, 1.0f, 1.0f, 0.5f,   0.0f, 0.0f,   // near top    right
             0.5f, -0.5f,  0.5f,   1.0f, 1.0f, 0.0f, 0.5f,   1.0f, 0.0f,   // near bottom right
             0.5f,  0.5f,  0.5f,   0.0f, 0.0f, 1.0f, 0.5f,   1.0f, 1.0f,   // near bottom left
            -0.5f,  0.5f,  0.5f,   1.0f, 0.0f, 0.0f, 0.5f,   0.0f, 1.0f,   // near top    left
            -0.5f, -0.5f, -0.5f,   1.0f, 1.0f, 1.0f, 0.5f,   0.0f, 0.0f,   // far  top    right
             0.5f, -0.5f, -0.5f,   1.0f, 0.0f, 1.0f, 0.5f,   1.0f, 0.0f,   // far  bottom right
             0.5f,  0.5f, -0.5f,   1.0f, 1.0f, 1.0f, 0.5f,   1.0f, 1.0f,   // far  bottom left
            -0.5f,  0.5f, -0.5f,   0.0f, 1.0f, 0.0f, 0.5f,   0.0f, 1.0f    // far  top    left
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
            //new Texture[]{ new Texture("./res/textures/container.jpg"), new Texture("./res/textures/awesomeface.jpg")/*, new Texture("./res/trollface.jpg")*/ }
            new Texture[]{}
         ));

         renderableObjects[0].Translate(new Vector3(2.5f, -1.3f, -0.2f));

         base.OnLoad();
      }

      protected override void OnUnload()
      {
         foreach (var r in this.renderableObjects) {
            r.Delete();
         }
         base.OnUnload();
      }

      protected override void OnRenderFrame(FrameEventArgs args)
      {
         GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

         // -- RENDERING CODE --

         foreach (var r in this.renderableObjects)
         {
            camera.Render(r, wireframeMode);
         }

         Context.SwapBuffers();
         base.OnRenderFrame(args);
      }

      protected override void OnUpdateFrame(FrameEventArgs args)
      {
         if (!IsFocused) {
            return;
         }

         var keyboard = KeyboardState;

         if (keyboard.IsKeyDown(Keys.Escape))
         {
            base.Close();
         }
         if (keyboard.IsKeyPressed(Keys.M))
         {
            this.wireframeMode *= -1;
         }
         if (keyboard.IsKeyPressed(Keys.N))
         {
            mouseFocused = !mouseFocused;
            CursorGrabbed = !CursorGrabbed;
            if (!CursorGrabbed) { CursorVisible = true; }
         }

         Vector3 horizontalDir = Vector3.Normalize(new Vector3(camera.dir.X, 0.0f, camera.dir.Z));
         Vector3 horizontalUp = new Vector3(0.0f, 1.0f, 0.0f);

         if (keyboard.IsKeyDown(Keys.W)) {
            camera.Move(horizontalDir * speed * (float)args.Time);
         }
         if (keyboard.IsKeyDown(Keys.A)) {
            camera.Move((-camera.right) * speed * (float)args.Time);
         }
         if (keyboard.IsKeyDown(Keys.S)) {
            camera.Move((-horizontalDir) * speed * (float)args.Time);
         }
         if (keyboard.IsKeyDown(Keys.D)) {
            camera.Move(camera.right * speed * (float)args.Time);
         }
         if (keyboard.IsKeyDown(Keys.Space)) {
            camera.Move(horizontalUp * speed * (float)args.Time);
         }
         if (keyboard.IsKeyDown(Keys.LeftShift)) {
            camera.Move((-horizontalUp) * speed * (float)args.Time);
         }

         var mouse = MouseState;

         if (mouseFocused) {
            float dx = mouse.X - lastPos.X;
            float dy = mouse.Y - lastPos.Y;
            camera.pitch -= dy * sensitivity;
            camera.yaw += dx * sensitivity;
            lastPos = new Vector2(mouse.X, mouse.Y);
            //System.Console.WriteLine(System.String.Format("x: {0}, y: {1}\t dx: {2}, dy: {3}", mouse.X, mouse.Y, dx, dy));
            camera.UpdateDirection();
         }

         renderableObjects[0].Rotate(new Vector3(.0f, .1f, .2f));

         base.OnUpdateFrame(args);
      }

      protected override void OnMouseMove(MouseMoveEventArgs e)
      {
         base.OnMouseMove(e);
      }
   }
}