namespace CryEngine.Projects.Game.Logic.MouseLogic
{
    public class MouseVisibleHandler
    {
        private static bool _isShowedCoursor = false;

        public void ChangeVisible()
        {
            if (_isShowedCoursor)
            {
                Mouse.HideCursor();
            }
            else
            {
                Mouse.ShowCursor();
            }

            _isShowedCoursor = !_isShowedCoursor;
        }
    }
}