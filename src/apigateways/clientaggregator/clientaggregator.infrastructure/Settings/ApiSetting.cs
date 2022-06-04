using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientaggregator.infrastructure.Settings
{
    public class ApiSetting
    {
        public string CustomerApi { get; set; }        
        public string UserAgent { get; set; }
        public int HandlerLifetimeMinutes { get; set; }
    }
}
