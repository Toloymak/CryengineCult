using System.Collections.Generic;
using CryEngine.UI;
using CryEngine.UI.Components;
using Resources.Settings;

namespace CryEngine.Game.Helpers
{
    public class CommonUiHandler
    {
        private static bool IsActive = true;

        private Point GetTextPosition(IDictionary<string, Text> LabelList) => 
            new Point(DefaultUiSettings.DefaultXPosition, DefaultUiSettings.DefaultYPosition + DefaultUiSettings.DefaultYPositionDelta * LabelList.Count);

        public void AddText(Canvas canvas, IDictionary<string, Text> LabelList, string label, string value = null)
        {
            var text = canvas.AddComponent<Text>();
            text.Alignment = DefaultUiSettings.DefaultAlignment;
            text.Height = DefaultUiSettings.DefaultFontSize;
            text.Offset = GetTextPosition(LabelList);
            text.Color = Color.Yellow.WithAlpha(0.5f);
            
            if (value != null)
                text.Content = value;

            LabelList.Add(label, text);
        }

        public void UpdateTextValue(IDictionary<string, Text> LabelList, string labelName, string value)
        {
            LabelList[labelName].Content = value;
        }
        
        public void Destroy(IDictionary<string, Text> LabelList, string labelName)
        {
            LabelList[labelName].Owner.Destroy();
            LabelList.Remove(labelName);
        }

        // todo : move to handler
        public void ChangeActive(IDictionary<string, Text> LabelList, string labelName)
        {
            LabelList[labelName].Active = !LabelList[labelName].Active;
        }

        public void ChangeActiveForAll(IDictionary<string, Text> LabelList, bool? isActive = null)
        {
            IsActive = isActive ?? !IsActive;
            
            foreach (var label in LabelList)
            {
                label.Value.Active = IsActive;
            }
        }
    }
}