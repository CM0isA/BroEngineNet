using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace BroEngine.Graphics.Shaders
{
    public class Shader
    {
        public int shaderProgramID;
        private bool disposedValue = false;

        public Shader(string vertexSource, string fragmentSource)
        {
            string vertexCode = GetFileContent(vertexSource);
            string fragmentCode = GetFileContent(fragmentSource);


            int VertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(VertexShader, vertexCode);

            int FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(FragmentShader, fragmentCode);

            GL.CompileShader(VertexShader); GL.CompileShader(FragmentShader);

            TestShader(VertexShader);
            TestShader(FragmentShader);


            shaderProgramID = GL.CreateProgram();
            GL.AttachShader(shaderProgramID, VertexShader);
            GL.AttachShader(shaderProgramID, FragmentShader);

            GL.LinkProgram(shaderProgramID);

            GL.GetProgram(shaderProgramID, GetProgramParameterName.LinkStatus, out int programSucces);
            if (programSucces == 0)
            {
                string infoLog = GL.GetProgramInfoLog(programSucces);
                Console.WriteLine(infoLog);
            }


            // Clear
            GL.DetachShader(shaderProgramID, VertexShader);
            GL.DetachShader(shaderProgramID, FragmentShader);

            GL.DeleteShader(VertexShader);
            GL.DeleteShader(FragmentShader);
        }

        protected void TestShader(int shader)
        {
            GL.GetShader(shader, ShaderParameter.CompileStatus, out int success);
            if (success == 0)
            {
                string infoLog = GL.GetShaderInfoLog(shader);
                Console.WriteLine(infoLog);
            }
        }

        ~Shader()
        {
            GL.DeleteProgram(shaderProgramID);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Use()
        {
            GL.UseProgram(shaderProgramID);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(shaderProgramID);
                disposedValue = true;
            }
        }

        private string GetFileContent(string filename)
        {
            string _ = Directory.GetCurrentDirectory() +
                "\\src\\Graphics\\Shaders\\" + filename;

            return System.IO.File.ReadAllText(_, System.Text.Encoding.UTF8);
        }

        public void SetMatrix4(string location, Matrix4 matrix)
        {
            int uniformLocation = GL.GetUniformLocation(shaderProgramID, location);
            GL.UniformMatrix4(uniformLocation, false, ref matrix);
        }
    }
}
