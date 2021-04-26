using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeSystem.Models.Interfaces
{
    interface IGeneral
    {
        bool Save();

        bool Remove();

        object GetData();
    }
}
