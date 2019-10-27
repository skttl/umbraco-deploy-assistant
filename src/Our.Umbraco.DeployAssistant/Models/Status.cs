using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecreo.DeployAssistant.Models
{
    public class Status
    {
        public bool Failed { get; set; }
        public bool InProgress { get; set; }
        public bool Complete { get; set; }
        public bool Export { get; set; }
        public string Content { get; set; }
        public DateTime LastEdit { get; set; }
    }
}
