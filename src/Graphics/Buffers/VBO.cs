using OpenTK.Graphics.OpenGL4;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace BroEngine.Graphics.Buffers
{
    public class VBO
    {
        public int ID;
        public VBO()
        {
            ID = GL.GenBuffer();
        }

        public void UploadData(float[] vertices)
        {
            BindBuffer();
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
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

        public void Delete()
        {
            GL.DeleteBuffer(ID);
        }
    }
}
