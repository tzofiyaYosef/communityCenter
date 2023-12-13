using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace DTO
{
    public class Maneger:Person
    {
        public string Id { get; set; }//ת.ז
        public double Salary { get; set; }
        public string Password { get; set; }

        public Maneger(string id, string firstName, string lastName, string phone, string gmail, double salary, string password) : base(firstName, lastName, phone, gmail)
        {
            Id = id;
            Salary = salary;
            Password = password;
        }

        public Maneger()
        {
        }

        public override string ToString()
        {
            return base.ToString() + "this salary: " + Salary + " this password: " + Password;
        }


        // convert from DAL=>DTO
        public static DAL.Maneger ConvertManagerToDAL(Maneger maneger)
        {
            DAL.Person user = new DAL.Person()
            {
                FirstName = maneger.FirstName,
                LastName = maneger.LastName,
                Phone = maneger.Phone,
                Gmail = maneger.Gmail,

            };

            DAL.Maneger maneger1 = new DAL.Maneger()
            {
                Id = maneger.Id,
                Salary = maneger.Salary,
                Password = maneger.Password,
                Person = user,
                Gmail = maneger.Gmail
            };
            return maneger1;
        }

        //convert from DTO=>DAL
        public static Maneger ConvertDalManagerToDto(DAL.Maneger maneger)
        {
            return new DTO.Maneger
            {
                Id = maneger.Id,
                FirstName = maneger.Person.FirstName,
                LastName = maneger.Person.LastName,
                Phone = maneger.Person.Phone,
                Gmail = maneger.Gmail,
                Salary = maneger.Salary,
                Password = maneger.Password
            };
        }
    }

}


