using CollegeSystem.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeSystem.Models.Classes
{
    public abstract class Person : IPerson, IGeneral
    {
        string _name { get; set; }
        DateTime _birthday { get; set; }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public DateTime Birthday
        {
            get
            {
                return _birthday;
            }
            set
            {
                _birthday = value;
            }
        }

        public Person()
        {

        }

        public Person(Person p)
        {
            _name = p.Name;
            _birthday = p.Birthday;
        }

        public abstract bool Save();

        public abstract bool Remove();

        public abstract object GetData();
    }
}