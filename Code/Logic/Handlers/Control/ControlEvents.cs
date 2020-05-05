using CryEngine.Game.Helpers;
using CryEngine.Projects.Game.Logic.MouseLogic;
using CryEngine.Projects.Game.Storage;

namespace CryEngine.Projects.Game.Handlers.Control
{
    public class ControlEvents
    {
        private UiHelper _uiHelper;
        private MouseVisibleHandler _mouseVisibleHandler;
        private CameraHandler _cameraHandler;

        public ControlEvents()
        {
            _mouseVisibleHandler = new MouseVisibleHandler();
            _uiHelper = new UiHelper();
            _cameraHandler = new CameraHandler();
        }

        public void ChangeUiActive(InputEvent e)
        {
            if(e.KeyPressed(ControlMap.ShowInterfaceButton))
            {
                _uiHelper.ChangeActiveForAll();
            }
        }

        public void ChangeMouseVisible(InputEvent e)
        {
            if(e.KeyPressed(ControlMap.ShowMouseButton))
            {
                _mouseVisibleHandler.ChangeVisible();
            }
        }
        public void CameraMovement(InputEvent e)
        {
            if(e.KeyPressed(ControlMap.MoveForward) || e.KeyDown(ControlMap.MoveForward))
            {
                _cameraHandler.MoveForward();
            }
            
            if(e.KeyPressed(ControlMap.MoveBack) || e.KeyDown(ControlMap.MoveBack))
            {
                _cameraHandler.MoveBack();
            }
            
            if(e.KeyPressed(ControlMap.MoveLeft) || e.KeyDown(ControlMap.MoveLeft))
            {
                _cameraHandler.MoveLeft();
            }
            
            if(e.KeyPressed(ControlMap.MoveRight) || e.KeyDown(ControlMap.MoveRight))
            {
                _cameraHandler.MoveRight();
            }
        }
        
    }
}