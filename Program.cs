using BroEngine.Graphics.Model;
using OpenTK.Mathematics;

class Program
{
    private static void Main(string[] args)
    {
        Vertex[] vertices =
        {
            new Vertex(new Vector3(0.5f,  0.5f, 0.0f), new Vector4(1f, 0f, 0f, 1f), new Vector3(0.5f,  0.5f, 0.0f)),
            new Vertex(new Vector3(0.5f, -0.5f, 0.0f), new Vector4(1f, 0f, 0f, 1f), new Vector3(0.5f,  0.5f, 0.0f)),
            new Vertex(new Vector3(-0.5f, -0.5f, 0.0f), new Vector4(0f, 1f, 0f, 1f), new Vector3(0.5f,  0.5f, 0.0f)),
            new Vertex(new Vector3(-0.5f,  0.5f, 0.0f), new Vector4(0f, 0f, 1f, 1f), new Vector3(0.5f,  0.5f, 0.0f))
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