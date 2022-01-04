using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novus.Classes
{
    public class Item
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string LaunchPara { get; set; }
        public string IconLocation { get; set; }

        // TODO :
        // public string IconStrech { get; set; }
        // public string Category { get; set; }
    }

    public class Preference
    {
        public string Version { get; set; }
        public string EnableLog { get; set; }
    }
}
