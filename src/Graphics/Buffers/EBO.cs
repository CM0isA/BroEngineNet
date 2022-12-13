using OpenTK.Graphics.OpenGL4;

namespace BroEngine.Graphics.Buffers
{
    public class EBO
    {
        public int ID;
        public EBO()
        {
            ID = GL.GenBuffer();
        }

        public void UploadData(uint[] indices)
        {
            BindBuffer();
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
        }

        public void BindBuffer()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ID);
        }

        public void UnbindBuffer()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }
        public void Delete()
        {
            GL.DeleteBuffer(ID);
        }
    }
}
