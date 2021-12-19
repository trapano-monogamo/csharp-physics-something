using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using scene;

namespace game
{
   public class Game : GameWindow
   {
      protected bool wireframeMode;
      protected float speed;
      protected float sensitivity;
      protected Vector2 lastPos;
      protected bool mouseFocused;
      protected bool shouldClose = false;
      protected Vector2 windowSize;

      protected Scene scene;

      public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
         : base(gameWindowSettings, nativeWindowSettings)
      {
         wireframeMode = false;
         speed = 2.1f;
         sensitivity = 1.1f;
         windowSize = nativeWindowSettings.Size;
         mouseFocused = false;
         lastPos = new Vector2(0.0f);
         this.scene = new Scene();
      }

      public void Start()
      {
         Run();
      }

      protected override void OnLoad()
      {
         UserLoad();
         base.OnLoad();
      }

      protected override void OnUnload()
      {
         UserUnload();
         base.OnUnload();
      }

      protected override void OnRenderFrame(FrameEventArgs args)
      {
         UserRender(args);
         base.OnRenderFrame(args);
         // finish rendering before exiting
         if (this.shouldClose) { base.Close(); }
      }

      protected override void OnUpdateFrame(FrameEventArgs args)
      {
         if (!IsFocused) {
            return;
         }

         this.UserInput(args);
         this.UserUpdate(args);

         base.OnUpdateFrame(args);
      }

      protected override void OnResize(ResizeEventArgs e)
      {
         GL.Viewport(0,0,e.Width,e.Height);
         base.OnResize(e);
      }

      protected override void OnMouseMove(MouseMoveEventArgs e)
      {
         base.OnMouseMove(e);
      }


      protected virtual void UserLoad() {}
      protected virtual void UserUnload() {}
      protected virtual void UserInput(FrameEventArgs args) {}
      protected virtual void UserUpdate(FrameEventArgs args) {}
      protected virtual void UserRender(FrameEventArgs args) {}
   }
}