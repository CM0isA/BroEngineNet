using OpenTK.Mathematics;

namespace BroEngine.Graphics.Model
{
    public readonly struct VertexAttribute
    {
        public readonly string Name;
        public readonly int Index;
        public readonly int ComponentCount;
        public readonly int Offset;

        public VertexAttribute(string name, int index, int count, int offset)
        {
            Name = name;
            Index = index;
            ComponentCount = count;
            Offset = offset;
        }
    }


    public sealed class VertexInfo
    {
        public readonly Type Type;
        public readonly int SizeInBytes;
        public readonly VertexAttribute[] VertexAttributes;

        public VertexInfo(Type type, params VertexAttribute[] attributes)
        {
            this.Type = type;
            this.SizeInBytes = 0;
            this.VertexAttributes = attributes;

            for (int i = 0; i < this.VertexAttributes.Length; i++)
            {
                VertexAttribute vertexAttribute = this.VertexAttributes[i];
                this.SizeInBytes += vertexAttribute.ComponentCount * sizeof(float);
            }
        }

    }


    public readonly struct Vertex
    {
        public readonly Vector3 Position;
        public readonly Vector4 Color;
        public readonly Vector3 Normal;

        public static readonly VertexInfo VertexInfo = new VertexInfo(
            typeof(Vertex),
            new VertexAttribute("Position", 0, 3, 0),
            new VertexAttribute("Color", 1, 4, 2 * sizeof(float)),
            new VertexAttribute("Normal", 2, 3, 7 * sizeof(float))
            );

        public Vertex(Vector3 position, Vector4 color, Vector3 normal)
        {
            Position = position;
            Color = color;
            Normal = normal;
        }



    }
}
