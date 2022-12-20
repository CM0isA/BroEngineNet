using BroEngine.Graphics.Model;
using OpenTK.Graphics.OpenGL4;

namespace BroEngine.Graphics.Buffers
{
    public class VBO : IDisposable
    {
        public int ID;
        public VBO()
        {
            ID = GL.GenBuffer();
        }

        ~VBO()
        {
            Dispose();
        }

        public void Dispose()
        {
            GL.DeleteBuffer(ID);
        }

        public void UploadData(Vertex[] vertices)
        {
            BindBuffer();
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * Vertex.VertexInfo.SizeInBytes, vertices, BufferUsageHint.StaticDraw);
            UnbindBuffer();
        }

        public void BindBuffer()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, ID);
        }

        public void UnbindBuffer()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }
}
