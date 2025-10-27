using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XapCheck.Models
{
    public class ActionLog
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
        public string PerformedBy { get; set; }
        public string Details { get; set; }
    }
}
