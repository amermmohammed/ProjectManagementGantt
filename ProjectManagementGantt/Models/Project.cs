using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string StartingDate { get; set; }
        public string EndingDate { get; set; }
        //public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual ICollection<Phase> Phases { get; set; }// = new List<Phase>();

    }

}
