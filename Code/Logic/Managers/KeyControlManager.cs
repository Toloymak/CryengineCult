using System;
using CryEngine.Game.Helpers;
using CryEngine.Projects.Game.Handlers.Control;

namespace CryEngine.Projects.Game.Managers
{
    public class KeyControlManager : IManager
    {
        private static KeyControlManager _instance;

        private ControlEvents _controlEvents;
        
        public void Initialize()
        {
            if(_instance != null)
                return;

            _instance = this;
            
            _controlEvents = new ControlEvents();
            
            Input.OnKey += _controlEvents.ChangeUiActive;
            Input.OnKey += _controlEvents.ChangeMouseVisible;
            Input.OnKey += _controlEvents.CameraMovement;
        }

        public void Shutdown()
        {
            _instance?.Dispose();
            _instance = null;
        }

        public void Dispose()
        {
            if(Engine.IsDedicatedServer)
                return;

            Input.OnKey -= _instance._controlEvents.ChangeUiActive;
            Input.OnKey -= _instance._controlEvents.ChangeMouseVisible;
            Input.OnKey -= _instance._controlEvents.CameraMovement;
        }
    }
}