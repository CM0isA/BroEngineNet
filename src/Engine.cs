using BroEngine.Graphics.Shaders;
using BroEngine.Graphics.Buffers;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Drawing;
using OpenTK.Mathematics;

namespace broEngine
{
    public class Engine : GameWindow
    {
        private VBO Vbo { get; set; }
        private VAO Vao { get; set; }

        public float[] vertices { get; set; }

        Shader shader { get; set; }

        public Engine(string title) : base(GameWindowSettings.Default, new NativeWindowSettings())
        {
            Title = title;
            this.CenterWindow(new Vector2i(1980, 1080));
        }



        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            KeyboardState keyboardState = KeyboardState.GetSnapshot();

            if (keyboardState.IsKeyDown(Keys.H))
            {
            }
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            shader = new Shader("VertexShader.glsl", "FragmentShader.glsl");

            Vao = new VAO();
            Vbo = new VBO();


            Vbo.UploadData(vertices);

            Vao.BindVAO();

            Vao.LinkVBO(Vbo, 0, 3, VertexAttribPointerType.Float, 3, 0); 

            Vao.UnbindVAO();

        }


        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            base.OnRenderFrame(args);

            shader.Use();

            Vao.BindVAO();

            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            Vao.UnbindVAO();

            SwapBuffers();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
        }

        protected override void OnUnload()
        {
            base.OnUnload();
            shader.Dispose();
        }

    }


}
