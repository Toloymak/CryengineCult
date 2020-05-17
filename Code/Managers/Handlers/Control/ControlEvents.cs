using CryEngine.Game;
using CryEngine.Game.Helpers;
using CryEngine.Projects.Game.Logic.MouseLogic;
using Resources;

namespace CryEngine.Projects.Game.Handlers.Control
{
    public class ControlEvents
    {
        private CommonUiHandler _commonUiHandler;
        private MouseVisibleHandler _mouseVisibleHandler;
        private CameraHandler _cameraHandler;

        public ControlEvents()
        {
            _mouseVisibleHandler = new MouseVisibleHandler();
            _commonUiHandler = new CommonUiHandler();
            _cameraHandler = new CameraHandler();
        }

        public void ChangeUiActive(InputEvent e)
        {
            if(e.KeyPressed(ControlMap.ShowInterfaceButton))
            {
                _commonUiHandler.ChangeActiveForAll(Storage.LabelList);
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