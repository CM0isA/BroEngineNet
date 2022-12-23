using BroEngine.Graphics.Buffers;
using BroEngine.Graphics.Camera;
using BroEngine.Graphics.Model;
using BroEngine.Graphics.Shaders;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace broEngine
{
    public class Engine : GameWindow
    {
        #region variables
        private VBO vbo { get; set; }
        private VAO vao { get; set; }
        private EBO ebo { get; set; }

        public Vertex[] vertices { get; set; }
        public uint[] indices { get; set; }
        public Matrix4 ModelMatrix { get; set; } = Matrix4.Identity;
        public Matrix4 ViewMatrix { get; set; } = Matrix4.Identity;
        public Matrix4 ProjectionMatrix { get; set; } = Matrix4.Identity;
        public Camera camera { get; set; } = new Camera();
        public float DeltaTime { get; set; }
        private Vector2 LastMousePosition { get; set; } = Vector2.Zero;
        private bool CameraTrigger { get; set; } = false;
        private bool MouseFirstMove { get; set; } = true;

        private Shader shader { get; set; }
        #endregion

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
            DeltaTime = (float)args.Time;

            KeyboardState keyboardState = KeyboardState.GetSnapshot();
            if (IsFocused)
            {
                float cameraSpeedNormalized = camera.CameraSpeed * DeltaTime;
                camera.CameraMovement(keyboardState, speed: cameraSpeedNormalized);
            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButton.Right)
            {
                CameraTrigger = true;
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButton.Right)
            {
                CameraTrigger = false;
            }
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            float deltaX = e.DeltaX;
            float deltaY = e.DeltaY;


            if (MouseFirstMove)
            {
                deltaX = 0; deltaY = 0;
                MouseFirstMove = false;
            }

            if (CameraTrigger)
            {
                camera.RotateCamera(deltaX, deltaY, DeltaTime);
            }

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
            vbo = new VBO(Vertex.VertexInfo, vertexCount: vertices.Length, BufferUsageHint.StaticDraw);
            ebo = new EBO();

            ebo.UploadData(indices);

            //vbo.UploadData(vertices);

            vbo.SetData(vertices, vertices.Length);

            int vertexSizeInBytes = Vertex.VertexInfo.SizeInBytes;

            vao.BindVAO();

            VertexAttribute attrib0 = Vertex.VertexInfo.VertexAttributes[0];
            VertexAttribute attrib1 = Vertex.VertexInfo.VertexAttributes[1];
            VertexAttribute attrib2 = Vertex.VertexInfo.VertexAttributes[2];

            vao.LinkVBO(vbo, layout: attrib0.Index, numberOfComponents: attrib0.ComponentCount, type: VertexAttribPointerType.Float, stride: vertexSizeInBytes, offset: attrib0.Offset);
            vao.LinkVBO(vbo, layout: attrib1.Index, numberOfComponents: attrib1.ComponentCount, type: VertexAttribPointerType.Float, stride: vertexSizeInBytes, offset: attrib1.Offset);
            vao.LinkVBO(vbo, layout: attrib2.Index, numberOfComponents: attrib2.ComponentCount, type: VertexAttribPointerType.Float, stride: vertexSizeInBytes, offset: attrib2.Offset);



            vao.UnbindVAO();

            shader.Use();

            camera = new Camera();
            ProjectionMatrix = camera.GetProjectionMatrix();

            ModelMatrix = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(-55.0f));
            var scale = Matrix4.CreateScale(5);
            ModelMatrix *= scale;

        }


        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            ViewMatrix = camera.GetViewMatrix();
            ModelMatrix *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(DeltaTime * 5f));

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
            ebo.Delete();
            vao.Delete();
        }

    }


}
