using BroEngine.Graphics.Shaders;
using OpenTK.Windowing.GraphicsLibraryFramework;

class Program
{
    private static void Main(string[] args)
    {
        float[] vertices = {
            0.5f,  0.5f, 0.0f, 1f, 0f, 0f, 1f, // top right
            0.5f, -0.5f, 0.0f, 1.0f, 0f, 0f, 1f, // bottom right
            -0.5f, -0.5f, 0.0f, 1.0f, 1.0f, 0f, 0f,  // bottom left
            -0.5f,  0.5f, 0.0f, 0f, 0f, 1.0f, 1f   // top left
        };

        uint[] indices = {
            0, 1, 3,   // first triangle
            1, 2, 3    // second triangle
            };

        using (broEngine.Engine engine = new broEngine.Engine("BroEngine", 1980, 1080))
        { 
            engine.vertices = vertices;
            engine.indices = indices;
            engine.Run();
        }

    }
}