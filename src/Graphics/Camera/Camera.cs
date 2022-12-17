using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace BroEngine.Graphics.Camera
{
    public class Camera
    {
        public Vector3 CameraPosition { get; set; } = new Vector3(0.0f, 0.0f, 3.0f);
        private Vector3 CameraDirection { get; set; } = -Vector3.UnitZ;
        private Vector3 CameraRight { get; set; } = Vector3.UnitX;
        private Vector3 CameraUp { get; set; } = Vector3.UnitY;
        // Rotation around the X axis (radians)
        private float _pitch { get; set; }
        // Rotation around the Y axis (radians)
        private float _yaw { get; set; } = -MathHelper.PiOver2;
        public float FieldOfView { get; set; } = 45f;
        public float AspectRatio { get; set; } = 1980 / 1080;
        public Camera() {
        }

        public float Pitch
        {
            get => MathHelper.RadiansToDegrees(_pitch);
            set
            {
                var angle = MathHelper.Clamp(value, -89f, 89f);
                _pitch = MathHelper.DegreesToRadians(angle);
            }
        }

        public float Yaw
        {
            get => MathHelper.RadiansToDegrees(_yaw);
            set
            {
                _yaw = MathHelper.DegreesToRadians(value);
            }
        }

        public Matrix4 ViewMatrix { get; set; } = Matrix4.Identity;
        public float CameraSpeed { get; set; } = 1.0f;
        public Vector2 Sensitivity { get; set; } = new Vector2(0.01f, 0.01f);

        public Matrix4 GetViewMatrix() {
            return Matrix4.LookAt(CameraPosition, CameraPosition + CameraDirection, CameraUp);
        }

        public Matrix4 GetProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(FieldOfView), AspectRatio, 0.1f, 100.0f);
        }


        // Camera Movement
        private void MoveForward(float distance)
        {
            Vector3 direction = Vector3.Normalize(CameraDirection);
            CameraPosition += direction * distance;
        }

        private void MoveBackwards(float distance)
        {
            Vector3 direction = Vector3.Normalize(CameraDirection);
            CameraPosition -= direction * distance;
        }

        private void MoveRight(float distance)
        {
            Vector3 direction = Vector3.Normalize(Vector3.Cross(CameraDirection, CameraUp));
            CameraPosition += direction * distance;
        }

        private void MoveLeft(float distance)
        {
            Vector3 direction = Vector3.Normalize(Vector3.Cross(CameraDirection, CameraUp));
            CameraPosition -= direction * distance;
        }

        private void MoveUp(float distance)
        {
            Vector3 direction = Vector3.Normalize(CameraUp);
            CameraPosition += direction * distance;
        }

        private void MoveDown(float distance)
        {
            Vector3 direction = Vector3.Normalize(CameraUp);
            CameraPosition -= direction * distance;
        }

        public void RotateCamera(float deltaX, float deltaY, float deltaTime)
        {
            _yaw += deltaX * Sensitivity.X * deltaTime;
            _pitch -= deltaY * Sensitivity.Y * deltaTime;

            if (_pitch > 89.0f)
            {
                _pitch = 89.0f;
            }
            else if (_pitch < -89.0f)
            {
                _pitch = -89.0f;
            }
            Vector3 newDirection;

            newDirection.Y = (float)Math.Sin(Pitch);
            newDirection.X = (float)Math.Cos(Pitch) * (float)Math.Cos(Yaw);
            newDirection.Z = (float)Math.Cos(Pitch) * (float)Math.Sin(Yaw);
            CameraDirection = Vector3.Normalize(newDirection);

            CameraRight = Vector3.Normalize(Vector3.Cross(CameraDirection, Vector3.UnitY));
            CameraUp = Vector3.Normalize(Vector3.Cross(CameraRight, CameraDirection));
        }

        public void CameraMovement(KeyboardState state, float speed)
        {
            if(state.IsKeyDown(Keys.W))
            {
                MoveForward(speed);
            }

            if (state.IsKeyDown(Keys.S))
            {
                MoveBackwards(speed);
            }

            if (state.IsKeyDown(Keys.A))
            {
                MoveLeft(speed);
            }

            if (state.IsKeyDown(Keys.D))
            {
                MoveRight(speed);
            }

            if (state.IsKeyDown(Keys.Q))
            {
                MoveDown(speed);
            }

            if (state.IsKeyDown(Keys.E))
            {
                MoveUp(speed);
            }

        }
    }
}
