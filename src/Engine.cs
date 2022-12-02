using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace broEngine {
    public class Engine : GameWindow
    {
        public Engine(int width, int height, string title) : base (GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title }) { }
    }


}
