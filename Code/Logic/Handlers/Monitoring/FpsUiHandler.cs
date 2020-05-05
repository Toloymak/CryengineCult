using System;
using CryEngine.Game.Helpers;

namespace CryEngine.Game.Logic.Monitoring
{
    public class FpsUiHandler : IMonitoringUiHandler
    {
        private UiHelper _uiHelper;
        
        private const string FpsLabelName = "fps";

        private DateTime _updateFpsTime = DateTime.MinValue;
        private int _frameCount = 0;

        public FpsUiHandler()
        {
            _uiHelper = new UiHelper();
        }
        
        public void UpdateOnCanvas()
        {
            // Update FPS Label.
            if(DateTime.Now > _updateFpsTime)
            {
                _uiHelper.UpdateTextValue(FpsLabelName, _frameCount.ToString());
                _frameCount = 0;
                _updateFpsTime = DateTime.Now.AddSeconds(1);
            }
			
            _frameCount++;
        }

        public void CreateOnCanvas()
        {
            _uiHelper.AddText(FpsLabelName, "0");

        }

        public void DestroyOnCanvas()
        {
            _uiHelper.Destroy(FpsLabelName);
        }
    }
}