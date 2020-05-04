using CryEngine.Game.Helpers;

namespace CryEngine.Game.Logic.KeyControl
{
    public class ControlEvents
    {
        private UiHelper _uiHelper;

        public ControlEvents()
        {
            _uiHelper = new UiHelper();
        }

        public void ChangeUiActive(InputEvent e)
        {
            if(e.KeyPressed(KeyId.F4))
            {
                _uiHelper.ChangeActiveForAll();
            }
        }
    }
}