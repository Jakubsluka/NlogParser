namespace Nlog.Parser.Libs.Classes
{
    internal class NlogTarget
    {
        internal string TargetLineSeparator { get; }
        internal string TargetPath { get; private set; } = string.Empty;
        internal string TargetLayout { get; private set; } = string.Empty;
        internal string TargetDirectory { get; private set; } = string.Empty;
        internal string TargetFileNamePrefix { get; private set; } = string.Empty;
        internal DirectoryInfo TargetDi { get; private set; }
        internal Dictionary<string, int> PlaceHoldersPositions { get; private set; }

        internal NlogTarget(string path, string layout, string separator)
        {
            TargetPath = path;
            TargetLayout = layout;
            TargetLineSeparator = separator;
            InitStrings();
            InitDirectoryInfo();
            ParseLayout();
        }

        private void InitStrings()
        {
            var parts = TargetPath.Split('/');
            TargetDirectory = Path.Combine(parts.Take(parts.Length - 1).ToArray());


            var last = parts.Last();
            var index = last.IndexOf("$");
            TargetFileNamePrefix = Path.Combine(Path.Combine(parts.Take(parts.Length - 1).ToArray()), last.Substring(0, index));
        }

        private void InitDirectoryInfo()
        {
            TargetDi = new DirectoryInfo(TargetDirectory);
        }

        private void ParseLayout()
        {
            PlaceHoldersPositions = new Dictionary<string, int>();
            var parts = TargetLayout.Split(TargetLineSeparator);

            for (int i = 0; i < parts.Length; i++)
            {
                PlaceHoldersPositions.Add(parts[i], i);
            }
        }
    }
}
