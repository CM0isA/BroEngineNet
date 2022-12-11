using BroEngine.Graphics.Shaders;
using OpenTK.Windowing.GraphicsLibraryFramework;

class Program
{
    private static void Main(string[] args)
    {
        float[] vertices =
        {
            -0.5f, -0.5f, 0.0f, //Bottom-left vertex
            0.5f, -0.5f, 0.0f, //Bottom-right vertex
            0.0f,  0.5f, 0.0f  //Top vertex
        };

        using (broEngine.Engine engine = new broEngine.Engine("BroEngine"))
        {
            engine.vertices = vertices;
            engine.Run();
        }

    }
}