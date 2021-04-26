using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeSystem.Models.Interfaces
{
    interface IPerson
    {
        string Name { get; set; }
        DateTime Birthday { get; set; }
    }
}
