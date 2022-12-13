using BroEngine.Graphics.Buffers;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace BroEngine.Graphics
{
    public readonly struct Vertex
    {
        public readonly Vector3 positions;
        public readonly Vector3 normals;
        public readonly Vector2 textureCoord;
        public readonly Vector3 colors;

        public Vertex()
        {

        }
    }

    public class Model
    {
        public float[] m_vertices { get; set; }
        public uint[] m_indices { get; set; }
        private int num_Vertices { get; set; } = 0;
        private int num_Indices { get; set; } = 0;
        public int BaseVertex { get; set; }
        public int BaseIndex { get; set; }
        public int MaterialIndex { get; set; }
        private VAO meshVAO;
        private EBO meshEBO;
        private VBO meshVBO;

        public Model(float[] vertices, uint[] indices)
        {
            m_vertices = vertices;
            m_indices = indices;
            BaseVertex = 0;
            BaseIndex = 0;
            num_Indices = indices.Length;
            num_Vertices = vertices.Length;
            meshVAO = new VAO();
            meshVBO = new VBO();
            meshEBO = new EBO();
        }


        public void LoadModel()
        {
            meshEBO.UploadData(m_indices);
            meshVBO.UploadData(m_vertices);

            meshVAO.BindVAO();
            //meshVAO.LinkVBO(meshVBO, 0, 3, VertexAttribPointerType.Float, num_Vertices * sizeof(Vertex), 0);
            //meshVAO.LinkVBO(meshVBO, 1, 3, VertexAttribPointerType.Float, num_Vertices * sizeof(Vertex), 0);
            //meshVAO.LinkVBO(meshVBO, 2, 3, VertexAttribPointerType.Float, num_Vertices * sizeof(Vertex), 0);
            //meshVAO.LinkVBO(meshVBO, 3, 3, VertexAttribPointerType.Float, num_Vertices * sizeof(Vertex), 0);

            meshVAO.UnbindVAO();
        }

        public void LoadFromFile(string filename)
        {
        }

        public void RenderModel()
        {
            GL.DrawElements(PrimitiveType.Triangles, m_indices.Length, DrawElementsType.UnsignedInt, 0);

        }

    }
}
