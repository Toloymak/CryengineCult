using System.Collections;
using System.Collections.Generic;
using CryEngine.UI;
using CryEngine.UI.Components;

namespace CryEngine.Game.Helpers
{
    public class UiHelper
    {
        private static readonly IDictionary<string, Text> LabelList = new Dictionary<string, Text>();

        private const int DefaultFontSize = 28;
        private const int DefaultXPosition = 10;
        private const int DefaultYPosition = 10;
        private const int DefaultYPositionDelta = 30;
        private const Alignment DefaultAlignment = Alignment.TopLeft;
        
        private static Canvas GetCanvas => SceneObject.Instantiate<Canvas>(SceneManager.RootObject);
        private static bool IsActive = true;

        private Point GetPoint => 
            new Point(
                DefaultXPosition,
                DefaultYPosition + DefaultYPositionDelta * LabelList.Count);

        public void AddText(string label, string value = null)
        {
            var text = GetCanvas.AddComponent<Text>();
            text.Alignment = DefaultAlignment;
            text.Height = DefaultFontSize;
            text.Offset = GetPoint;
            text.Color = Color.Yellow.WithAlpha(0.5f);
            
            if (value != null)
                text.Content = value;
            
            LabelList.Add(label, text);
        }

        public void UpdateTextValue(string labelName, string value)
        {
            LabelList[labelName].Content = value;
        }
        
        public void Destroy(string labelName)
        {
            LabelList[labelName].Owner.Destroy();
            LabelList.Remove(labelName);
        }

        // todo : move to handler
        public void ChangeActive(string labelName)
        {
            LabelList[labelName].Active = !LabelList[labelName].Active;
        }

        public void ChangeActiveForAll(bool? isActive = null)
        {
            IsActive = isActive ?? !IsActive;
            
            foreach (var label in LabelList)
            {
                label.Value.Active = IsActive;
            }
        }
    }
}