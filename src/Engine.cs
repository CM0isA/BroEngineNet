using BroEngine.Graphics.Shaders;
using BroEngine.Graphics.Buffers;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using BroEngine.Graphics.Camera;

namespace broEngine
{
    public class Engine : GameWindow
    {
        private VBO vbo { get; set; }
        private VAO vao { get; set; }
        private EBO ebo { get; set; }

        public float[] vertices { get; set; }
        public uint[] indices { get; set; }
        public Matrix4 ModelMatrix { get; set; }
        public Matrix4 ModelView { get; set; }
        public Matrix4 ModelProjection { get; set; }
        public Camera camera { get; set; } = new Camera();


        Shader shader { get; set; }

        public Engine(string title, int width = 1980, int height = 1080) :
            base(GameWindowSettings.Default,
                new NativeWindowSettings()
                {
                    Title = title,
                    Size = new Vector2i(width, height),
                    WindowBorder = WindowBorder.Fixed,
                    StartVisible = false,
                    StartFocused = true,
                    API = ContextAPI.OpenGL,
                    Profile = ContextProfile.Core,
                    APIVersion = new Version(4, 6)
                })
        {
            this.CenterWindow(new Vector2i(width, height));
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            KeyboardState keyboardState = KeyboardState.GetSnapshot();

            if (IsFocused)
            {
                camera.CameraMovement(keyboardState);
            }
        }

        protected override void OnLoad()
        {
            this.IsVisible = true;
            base.OnLoad();
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            shader = new Shader("VertexShader.glsl", "FragmentShader.glsl");

            vao = new VAO();
            vbo = new VBO();
            ebo = new EBO();

            ebo.UploadData(indices);

            vbo.UploadData(vertices);

            vao.BindVAO();

            vao.LinkVBO(vbo, 0, 3, VertexAttribPointerType.Float, 7 * sizeof(float), 0);
            vao.LinkVBO(vbo, 1, 4, VertexAttribPointerType.Float, 7 * sizeof(float), 3 * sizeof(float));

            vao.UnbindVAO();

            shader.Use();

            ModelProjection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), (float)1980 / (float)1080, 0.1f, 100.0f);
            ModelMatrix = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(-55.0f));

            camera = new Camera();
        }


        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            ModelView = camera.GetViewMatrix();

            shader.Use();
            shader.SetMatrix4("model", ModelView);
            shader.SetMatrix4("view", ModelView);
            shader.SetMatrix4("projection", ModelProjection);

            vao.BindVAO();
            ebo.BindBuffer();

            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

            vao.UnbindVAO();

            SwapBuffers();
            base.OnRenderFrame(args);
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
            vbo.Delete();
            ebo.Delete();
            vao.Delete();
        }

    }


}
