using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Damato_API.Settings
{
    public class OutSettings
    {
        public OutSettings() { FileOut = new Dictionary<string, int>();}
        public Dictionary<string, int> FileOut { get; set; }
		
    }
}