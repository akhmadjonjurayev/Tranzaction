using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transaction.Models
{
    public class StudentViewModel
    {
        public string FullName { get; set; }
        public bool IsStudent { get; set; }
        public DateTime Birthday { get; set; }
        public string PochtaIndex { get; set; }
    }
}
