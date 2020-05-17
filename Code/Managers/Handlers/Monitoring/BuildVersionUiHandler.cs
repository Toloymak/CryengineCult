using CryEngine.Game.Helpers;
using CryEngine.UI;

namespace CryEngine.Game.Logic.Monitoring
{
    public class BuildVersionUiHandler : IMonitoringUiHandler
    {
        private CommonUiHandler _commonUiHandler;
        
        private const string BuildVersionLabelName = "build";

        public BuildVersionUiHandler()
        {
            _commonUiHandler = new CommonUiHandler();
        }

        public void UpdateOnCanvas()
        {
            
        }

        public void CreateOnCanvas()
        {
            
        }

        public void DestroyOnCanvas()
        {
            
        }
    }
}