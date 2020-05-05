using CryEngine.Game.Helpers;

namespace CryEngine.Game.Logic.Monitoring
{
    public class BuildVersionUiHandler : IMonitoringUiHandler
    {
        private UiHelper _uiHelper;
        
        private const string BuildVersionLabelName = "build";

        public BuildVersionUiHandler()
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