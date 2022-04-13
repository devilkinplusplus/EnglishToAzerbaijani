using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngTur
{
    internal class UserControl
    {
        public static string setUsername { get; set; }
        public static string getUsername { get { return setUsername; } }
        public static string setPassword { get; set; }
        public static string getPassword { get { return setPassword; } }

    }
}
