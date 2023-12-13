using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserDal
    {
        public string Name { get; set; }
        public string Pass { get; set; }
        public double Age { get; set; }

        public UserDal(string name, string pass, double age)
        {
            Name = name;
            Pass = pass;
            Age = age;
        }

        public UserDal()
        {
        }
    }
}
