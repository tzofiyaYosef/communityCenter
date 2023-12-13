using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Worker:Person
    {
        public Salary Salary { get; set; }

        //public Worker(string id, string firstName, string lastName, string phone, string gmail, double salary) : base(id, firstName, lastName, phone, gmail)
        //{

        //}
    }
}
