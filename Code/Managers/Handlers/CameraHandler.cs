using CryEngine.Common;

namespace CryEngine.Game.Handlers
{
    public class CameraHandler
    {
        private static readonly Quaternion DefaultCameraRotation =
            new Quaternion(new Vector3(0, 1, -0.5f));

        public void SetDefaultPosition()
        {
            Camera.Rotation = DefaultCameraRotation;
        }
    }
}