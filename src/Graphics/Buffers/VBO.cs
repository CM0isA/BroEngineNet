using BroEngine.Graphics.Model;
using OpenTK.Graphics.OpenGL4;

namespace BroEngine.Graphics.Buffers
{
    public class VBO : IDisposable
    {
        public static readonly int MinVertexCount = 1;
        public static readonly int MaxVertexCount = 100_000;


        public bool disposed;

        public readonly int ID;
        public readonly VertexInfo vertexInfo;
        private readonly BufferUsageHint usageHint;
        public readonly int VertexCount;

        public VBO(VertexInfo VertexInfo, int vertexCount, BufferUsageHint usageHint)
        {
            this.disposed = false;

            if(vertexCount < VBO.MinVertexCount ||
                vertexCount > VBO.MaxVertexCount)
            {
                throw new ArgumentOutOfRangeException(nameof(vertexCount));
            }

            this.usageHint = usageHint;
            this.VertexCount = vertexCount;
            this.vertexInfo = VertexInfo;

            int vertexSizeInBytes = Vertex.VertexInfo.SizeInBytes;
            ID = GL.GenBuffer();
            BindBuffer();
            GL.BufferData(BufferTarget.ArrayBuffer, vertexSizeInBytes, IntPtr.Zero, usageHint);
            UnbindBuffer();
        }

        ~VBO()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (disposed) return;

            disposed = true;
            GC.SuppressFinalize(this);
            GL.DeleteBuffer(ID);
        }

        public void UploadData(Vertex[] vertices)
        {
            BindBuffer();
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * Vertex.VertexInfo.SizeInBytes, vertices, usageHint);
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


        public void SetData<T>(T[] data, int count) where T : struct
        {
            if (typeof(T) != this.vertexInfo.Type) throw new ArgumentException("Generic type 'T' does not match the vertex type of the vertex buffer.");

            if (data is null) throw new ArgumentNullException(nameof(data));

            if (data.Length < 0) throw new ArgumentOutOfRangeException(nameof(data));

            if (count < 0 || count > data.Length || count > this.VertexCount) throw new ArgumentOutOfRangeException(nameof(count));

            BindBuffer();
            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, count * this.vertexInfo.SizeInBytes, data);
            UnbindBuffer();
        }


    }
}
