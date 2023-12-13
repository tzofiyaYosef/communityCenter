using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace DTO
{
   public class Person
    {
        //public string Id { get; }//ת.ז
        public string FirstName { get; set; }//שם פרטי
        public string LastName { get; set; }//שם משפחה
        public string Phone { get; set; }//פלאפון
        public string Gmail { get; set; }//אימייל

        //פעולה בונה מלאה - מעתיקה
        public Person(string firstName, string lastName, string phone, string gmail)
        {
            if (gmail.IndexOf('@') != -1)
                this.Gmail = gmail;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
        }
        //פעולה בונה חסרה 
        //public Person(string id, string firstName, string lastName, string phone)
        //{
        //    Id = id;
        //    FirstName = firstName;
        //    LastName = lastName;
        //    Phone = phone;
        //    Gmail = null;
        //}


        public Person(string str)
        {
            if (str.IndexOf('@') != -1)
                this.Gmail = str;
            else
            {
                string[] FirstLast = str.Split(' ');
                this.FirstName = FirstLast[0];
                this.LastName = FirstLast[1];
            }
        }
        public Person()
        {
        }

        public override string ToString()
        {
            return "the name: " + this.LastName + " " + this.FirstName + "\ngmail: " + this.Gmail + "\nthe phon: " + this.Phone;
        }
    }
}

