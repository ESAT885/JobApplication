using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationLibrary.Models
{
    public class JobApplication
    {
        public Application Application { get; set; }
        public int YearsOfExperience { get; set; }
        public List<string> TechStackList { get; set; }
       public Validation ValidationMode { get; set; }

    }
    public enum Validation
    {
       Detailed,
       Quick
    }
}
