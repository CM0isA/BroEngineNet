using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace BroEngine.Graphics.Camera
{
    public class Camera
    {
        public Vector3 CameraPosition { get; set; } = new Vector3(0.0f, 0.0f, 3.0f);
        public Vector3 CameraDirection { get; set; } = new Vector3(0.0f, 0.0f, -1.0f);
        public Vector3 CameraRight { get; set; }
        public Vector3 CameraUp { get; set; } = new Vector3(0.0f, 1.0f, 0.0f);
        public Matrix4 ViewMatrix { get; set; } = Matrix4.Identity;
        public float CameraSpeed { get; set; } = 1.0f;
        public Camera() {
            CameraRight = Vector3.Cross(CameraDirection, CameraUp);

        }

        public Matrix4 GetViewMatrix() {
            return Matrix4.LookAt(CameraPosition, CameraPosition + CameraDirection, CameraUp);
        }

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

        private void RotateOX(float angle)
        {
            
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
