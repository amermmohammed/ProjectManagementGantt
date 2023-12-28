using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Models
{
    public class Phase
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        //public int ProjectId { get; set; }
        //public int ParentPhaseId { get; set; }

        public virtual Project Project { get; set; }
        public virtual Phase ParentPhase { get; set; }
        
    }

}
