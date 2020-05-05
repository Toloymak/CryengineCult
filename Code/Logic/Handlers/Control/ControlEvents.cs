using CryEngine.Game.Helpers;
using CryEngine.Projects.Game.Logic.MouseLogic;
using CryEngine.Projects.Game.Storage;

namespace CryEngine.Projects.Game.Handlers.Control
{
    public class ControlEvents
    {
        private UiHelper _uiHelper;
        private MouseVisibleHandler _mouseVisibleHandler;

        public ControlEvents()
        {
            _mouseVisibleHandler = new MouseVisibleHandler();
            _uiHelper = new UiHelper();
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
    }
}