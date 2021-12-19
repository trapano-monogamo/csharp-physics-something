using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using game;

namespace physics_goes_brr
{
    public class MyGame : Game {
        public MyGame(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {}

        protected override void UserLoad() {}

        protected override void UserUnload() {}

        protected override void UserInput() {}

        protected override void UserRender() {}

        protected override void UserUpdate() {}
    }

    class Launcher
    {
        static void Main(string[] args)
        {
            GameWindowSettings gameWindowSettings = GameWindowSettings.Default;
            NativeWindowSettings nativeWindowSettings = NativeWindowSettings.Default;
            nativeWindowSettings.Title = "Hello OpenTK";
            nativeWindowSettings.Size = new Vector2i(840,840);
            MyGame myApp = new MyGame(gameWindowSettings, nativeWindowSettings);
            myApp.Start();
        }
    }
}
