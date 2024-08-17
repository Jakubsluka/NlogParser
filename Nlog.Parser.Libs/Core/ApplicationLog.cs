namespace Nlog.Parser.Libs.Classes
{
    internal class ApplicationLog
    {
        internal NlogTarget Target { get; private set; }
        internal Dictionary<string, List<LogLine>> LogLinesByFile { get; private set; } = new Dictionary<string, List<LogLine>>();
        internal string Name { get; private set; } = string.Empty;
        private string _blankLinePlaceHolder = "-BLANK-";

        internal ApplicationLog(NlogTarget target)
        {
            Target = target;
            Name = target.TargetPath.Split("/").Last();
            ReadAllLogFiles();
        }

        private void ReadAllLogFiles()
        {
            var files = GetRelevantFiles().ToList();

            foreach (var file in files)
            {
                var fileLogLines = new List<LogLine>();

                using (var sr = new StreamReader(file.FullName))
                {
                    while (sr.Peek() != -1)
                    {
                        var lineText = sr.ReadLine() ?? _blankLinePlaceHolder;
                        var logLine = new LogLine(lineText);
                        logLine.ParseLineTextDynamicaly(Target.PlaceHoldersPositions, Target.TargetLineSeparator);
                        fileLogLines.Add(logLine);
                    }
                }

                LogLinesByFile.Add(file.Name, fileLogLines);
            }
        }

        private IEnumerable<FileInfo> GetRelevantFiles()
        {
            var files = Target.TargetDi.GetFiles();

            foreach (var file in files)
            {
                if (file.FullName.StartsWith(Target.TargetFileNamePrefix))
                {
                    yield return file;
                }
            }
        }
    }
}
