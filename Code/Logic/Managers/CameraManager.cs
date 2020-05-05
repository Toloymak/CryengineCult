using CryEngine.Game.Handlers;

namespace CryEngine.Projects.Game.Managers
{
    public class CameraManager : IManager, IGameRenderReceiver
    {
        private static CameraManager _instance;

        private CameraHandler _cameraHandler;
        
        public void Initialize()
        {
            if(_instance != null)
                return;
            
            _instance = this;
            
            _cameraHandler = new CameraHandler();

            GameFramework.RegisterForRender(this);
        }

        public void OnRender()
        {
            _cameraHandler.SetDefaultPosition();
        }

        public void Shutdown()
        {
            _instance.Dispose();
            _instance = null;
        }

        public void Dispose()
        {
            GameFramework.UnregisterFromRender(this);
        }
    }
}