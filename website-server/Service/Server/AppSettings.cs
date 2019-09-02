using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public string LedIp { get; set; }
        public int LedPort { get; set; }
        public string LedFormat { get; set; }
        public string ImageDir { get; set; }
    }
}
