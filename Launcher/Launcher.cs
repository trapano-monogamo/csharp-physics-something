using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using game;

namespace physics_goes_brr
{
    class Launcher
    {
        static void Main(string[] args)
        {
            GameWindowSettings gameWindowSettings = GameWindowSettings.Default;
            NativeWindowSettings nativeWindowSettings = NativeWindowSettings.Default;
            nativeWindowSettings.Title = "Hello OpenTK";
            nativeWindowSettings.Size = new Vector2i(840,840);
            Game myApp = new Game(gameWindowSettings, nativeWindowSettings);
            myApp.Start();
        }
    }
}
