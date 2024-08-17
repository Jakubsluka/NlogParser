
using System.Xml;
using Nlog.Parser.Libs.Classes;
using Nlog.Parser.Libs.Core;

namespace Nlog.Parser.Libs
{
    public class NlogParser
    {
        private string _configPath;
        internal Dictionary<string, ApplicationLog> Logs { get; }

        public NlogParser(string configPath)
        {
            _configPath = configPath;
            Logs = new Dictionary<string, ApplicationLog>();
            this.CreateLogs();
        }

        public NlogParser()
        {
            //TODO: Sort out the root path code
            _configPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "nlog.config");
            Logs = new Dictionary<string, ApplicationLog>();
            this.CreateLogs();
        }

        private bool VerifyLogFileExist(string logPath) => File.Exists(Path.Combine("", logPath));

        private bool VerifyConfigFileExist() => File.Exists(_configPath);

        private void CreateLogs() 
        {

            if (VerifyConfigFileExist())
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(File.ReadAllText(_configPath));

                foreach (XmlNode node in xDoc.DocumentElement.ChildNodes) 
                {
                    if (node.Name == "targets") 
                    {
                        foreach (XmlNode targetNode in node.ChildNodes) 
                        {
                            if (targetNode.Name == "target") 
                            {
                                try
                                {
                                    var pathAttr = targetNode.Attributes.Item(2) ?? null;
                                    var layoutAttr = targetNode.Attributes.Item(3) ?? null;

                                    if (pathAttr is not null && layoutAttr is not null)
                                    {
                                        var target = new NlogTarget(pathAttr.InnerText, layoutAttr.InnerText,"|");
                                        var log = new ApplicationLog(target);
                                        Logs.Add(log.Name,log);
                                    }
                                }
                                catch (Exception e) 
                                {
                                    throw e;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
