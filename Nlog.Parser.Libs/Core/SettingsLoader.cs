using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Nlog.Parser.Libs.Core
{
    internal static class SettingsLoader
    {
        internal static List<SettingsObject> LoadedPlaceholders { get; }
        static SettingsLoader() 
        {
            LoadedPlaceholders = LoadPlaceholders();
        }

        private static List<SettingsObject> LoadPlaceholders() 
        {
            var path = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "NlogParserSettings.json");

            var content = File.ReadAllText(path);

            return JsonSerializer.Deserialize<List<SettingsObject>>(content);
        }
    }



    internal class SettingsObject 
    {
        public string PlaceHolderName { get; set; }
        public string LinePropertyName { get; set; }
    }
}
