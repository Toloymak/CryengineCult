using System;
using System.Collections.Generic;
using CryEngine.Game.Helpers;
using CryEngine.UI;
using CryEngine.UI.Components;

namespace CryEngine.Game.Logic.Monitoring
{
    public class FpsUiHandler : IMonitoringUiHandler
    {
        private CommonUiHandler _commonUiHandler;
        
        private const string FpsLabelName = "fps";

        private DateTime _updateFpsTime = DateTime.MinValue;
        private int _frameCount = 0;


        public FpsUiHandler()
        {
            _commonUiHandler = new CommonUiHandler();
        }
        
        public void UpdateOnCanvas()
        {
            // Update FPS Label.
            if(DateTime.Now > _updateFpsTime)
            {
                _commonUiHandler.UpdateTextValue(Storage.LabelList, FpsLabelName, _frameCount.ToString());
                _frameCount = 0;
                _updateFpsTime = DateTime.Now.AddSeconds(1);
            }
			
            _frameCount++;
        }

        public void CreateOnCanvas()
        {
            _commonUiHandler.AddText(SceneObject.Instantiate<Canvas>(SceneManager.RootObject), Storage.LabelList, FpsLabelName, "0");
        }

        public void DestroyOnCanvas()
        {
            _commonUiHandler.Destroy(Storage.LabelList, FpsLabelName);
        }
    }
}