using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPS.WF.Model
{
    public class Transition
    {
        public int ElementId { get; set; }
        public string SourceState { get; set; }
        public string DestnationState { get; set; }
        public string TriggeredBy { get; set; }
        public DateTime TriggeredDate { get; set; }
        public string Comment { get; set; }
    }
}
