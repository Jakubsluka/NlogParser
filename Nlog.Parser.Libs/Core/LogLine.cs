using Nlog.Parser.Libs.Core;
using System.Reflection;

namespace Nlog.Parser.Libs.Classes
{
    public class LogLine
    {
        public string LineText {  get; private set; } = string.Empty;
        public DateTime LineDate { get; private set; }
        public string LineLevel { get; private set; } = string.Empty;
        public string LineMessage { get; private set; } = string.Empty;
        public string Logger { get; private set; } = string.Empty;

        public LogLine(string lineText)
        {
            LineText = lineText;
        }

        public void ParseLineTextDynamicaly(Dictionary<string, int> placeholdersPositions, string separator) 
        {
            var parts = LineText.Split(separator);

            var placeholders = SettingsLoader.LoadedPlaceholders;

            foreach (var placehoder in placeholders) 
            {
                foreach (var prop in this.GetType().GetProperties()) 
                {
                    if (prop.Name == placehoder.LinePropertyName) 
                    {
                        var value = placeholdersPositions.FirstOrDefault(x => x.Key.ToLower().Contains(placehoder.PlaceHolderName));

                        try
                        {
                            if (prop.PropertyType.Name == nameof(DateTime))
                            {
                                prop.SetValue(this, DateTime.Parse(parts[value.Value]));
                            }
                            else
                            {
                                prop.SetValue(this, parts[value.Value]);
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
        }
    }
}
