using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Nlog.Parser.Libs.Interface
{
    internal class NlogParserInterface
    {
        private NlogParser _parser;

        public int LogCount => _parser.Logs.Count;
        private NlogParserInterface(NlogParser parser) 
        {
            _parser = parser;
        }

        
    }
}
