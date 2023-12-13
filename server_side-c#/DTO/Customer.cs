using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace DTO
{
    public class Customer:Person
    {
        public string UserName { get; set; }//שם משתמש
        public string Password { get; set; }//סיסמא

        

        //public string Password
        //{
        //    get { return Password; }
        //    set
        //    {
        //        if (value.Length > 8)
        //            if (value.IndexOf('!') + value.IndexOf('?') + value.IndexOf('@') + value.IndexOf('#') > 0)
        //                password = value;
        //            else Console.WriteLine("The password must include one of the following characters:!,@,#,?");
        //        else Console.WriteLine("The password must be at least 8 characters long");
        //    }
        //}

        //פעולה בונה מלאה
        public Customer(string firstName, string lastName, string phone, string gmail, string userName, string password) : base(firstName, lastName, phone, gmail)
        {
            UserName = userName;
            Password = password;
        }

        public Customer(string gmail, string userName, string password):base(gmail)
        {
            UserName = userName;
            Password = password;
        }

        public Customer(string userName, string password) 
        {
            UserName = userName;
            Password = password;
        }

        //פעולה בונה ריקה
        public Customer() : base()
        {
        }

        public override string ToString()
        {
            return "this userName: " + UserName + " " + "this password: " + " " + Password + " " + base.ToString();
        }

        // convert from DAL=>DTO
        public static DAL.Customer ConvertUserToDAL(Customer customer)
        {
            DAL.Person user = new DAL.Person()
            {
               FirstName = customer.FirstName,
               LastName=customer.LastName,
               Phone=customer.Phone,
               Gmail=customer.Gmail,
              
            };

            DAL.Customer customer1 = new DAL.Customer()
            {
                UserName = customer.UserName,
                Password = customer.Password,
                Gmail = customer.Gmail,
                Person=user,
            };
            return customer1;
        }

        // convert from DTO=>DAL
        public static Customer ConvertDalUserToDto(DAL.Customer user)
        {
            return new DTO.Customer
            {
                FirstName = user.Person.FirstName,
                LastName = user.Person.LastName,
                Phone = user.Person.Phone,
                Gmail = user.Gmail,
                UserName = user.UserName,
                Password = user.Password
            };
        }
    }
}

