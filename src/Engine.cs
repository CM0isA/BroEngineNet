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
        public Matrix4 ModelMatrix { get; set; } = Matrix4.Identity;
        public Matrix4 ViewMatrix { get; set; } = Matrix4.Identity;
        public Matrix4 ProjectionMatrix { get; set; } = Matrix4.Identity;
        public Camera camera { get; set; } = new Camera();
        public float deltaTime { get; set; }
        private Vector2 LastMousePosition { get; set; } = Vector2.Zero;
        private bool MouseFirstMove { get; set; } = true;

        private Shader shader { get; set; }

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
            deltaTime = (float)args.Time;

            KeyboardState keyboardState = KeyboardState.GetSnapshot();
            if (IsFocused)
            {
                float cameraSpeedNormalized = camera.CameraSpeed * deltaTime;
                camera.CameraMovement(keyboardState, speed: cameraSpeedNormalized);
            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            float deltaX =  e.DeltaX;
            float deltaY = e.DeltaY;

            if(MouseFirstMove)
            {
                deltaX = 0; deltaY = 0;
                MouseFirstMove = false;
            }

            camera.RotateCamera(deltaX, deltaY, deltaTime);
            base.OnMouseMove(e);
        }

        protected override void OnMouseEnter()
        {
            base.OnMouseEnter();
            LastMousePosition = new Vector2(MousePosition.X, MousePosition.Y);
            MouseFirstMove = true;
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

            ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), (float)1980 / (float)1080, 0.1f, 100.0f);
            ModelMatrix = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(-55.0f));

            camera = new Camera();
        }


        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            ViewMatrix = camera.GetViewMatrix();
            ModelMatrix *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(deltaTime * 5f));

            shader.Use();
            shader.SetMatrix4("model", ModelMatrix);
            shader.SetMatrix4("view", ViewMatrix);
            shader.SetMatrix4("projection", ProjectionMatrix);

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
