using CryEngine.Game.Helpers;

namespace CryEngine.Game.Logic.Monitoring
{
    public class BuildVersionUiLogic : IMonitoringUiLogic
    {
        private UiHelper _uiHelper;
        
        private const string BuildVersionLabelName = "build";

        public BuildVersionUiLogic()
        {
            _uiHelper = new UiHelper();
        }

        public void UpdateOnCanvas()
        {
        }

        public void CreateOnCanvas()
        {
            _uiHelper.AddText(BuildVersionLabelName, GetType().Assembly.GetName().Version.ToString());
        }

        public void DestroyOnCanvas()
        {
            _uiHelper.Destroy(BuildVersionLabelName);
        }
    }
}