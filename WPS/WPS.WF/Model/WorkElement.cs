using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPS.WF.Model
{
    public class WorkElement
    {
        public int Id { get; set; }
        public string OrderTitle { get; set; }
        public string OrderDescription { get; set; }
        public string RequestedBy { get; set; }
        public string EmailAddress { get; set; }
        public string LastState { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime TerminationDate { get; set; }
        public List<Transition> Transition { get; set; }
    }
}
