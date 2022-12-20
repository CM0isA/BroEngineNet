using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroEngine.Graphics.Buffers
{
    public class VAO
    {
        public int ID;
        public VAO()
        {
            ID = GL.GenVertexArray();
        }

        public void LinkVBO(VBO vBO, int layout, int numberOfComponents, VertexAttribPointerType type, int stride, int offset)
        {
            vBO.BindBuffer();
            GL.VertexAttribPointer(layout, numberOfComponents, type, false, stride, offset);
            GL.EnableVertexAttribArray(layout);
            vBO.UnbindBuffer();
        }

        public void BindVAO()
        {
            GL.BindVertexArray(ID);
        }

        public void UnbindVAO()
        {
            GL.BindVertexArray(0);
        }

        public void Delete()
        {
            GL.DeleteVertexArray(ID);
        }

    }
}
