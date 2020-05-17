
using System;
using CryEngine.Game.Helpers;

namespace CryEngine.Projects.Game.Handlers.Control
{
    public class CameraHandler
    {
        private const int CameraSpeed = 10;
        
        public void MoveForward()
        {
            Camera.Position += GetForwardSpeed();
        }
        
        public void MoveBack()
        {
            Camera.Position -= GetForwardSpeed();
        }

        public void MoveRight()
        {
            Camera.Position += GetSideSpeed();
        }

        public void MoveLeft()
        {
            Camera.Position -= GetSideSpeed();
        }

        private static Vector3 GetForwardSpeed()
        {
           var cameraDirection = Camera.ForwardDirection * CameraSpeed * FrameTime.Delta;
           cameraDirection.Z = 0;
           return cameraDirection;
        }
        
        private static Vector3 GetSideSpeed()
        {
            return Quaternion.CreateRotationZ(-_halfOfPi) * GetForwardSpeed();
        }

        private static float _halfOfPi = (float) (Math.PI / 2);
    }
}