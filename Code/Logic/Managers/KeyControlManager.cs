using System;
using CryEngine.Game.Helpers;
using CryEngine.Game.Logic.KeyControl;

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
        }
    }
}