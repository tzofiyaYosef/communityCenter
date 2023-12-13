using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PersonDal { 
    //{
    //    private string firstName;
    //    private string lastName;
    //    private string phone;
    //    private string gmail;
    //    private string name;

    //    public PersonDal(string name)
    //    {
    //        this.name = name;
    //    }

    //    public PersonDal(string firstName, string lastName, string phone, string gmail)
    //    {
    //        this.firstName = firstName;
    //        this.lastName = lastName;
    //        this.phone = phone;
    //        this.gmail = gmail;
    //    }

        public static int Login(string gmail, string name, string pass)
        {
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                string[] FirstLast = name.Split(' ');
                //במידה ואין משתמש כזה כלל, לא לקוח ולא מנהל
                Person p = DB.People.Where(i => i.Gmail.Equals(gmail)).FirstOrDefault();
                if ( p == null)
                    return 0;
                foreach (DAL.Customer customer in DB.Customers)
                    {
                    //בדיקה אם הסיסמה והשם תקינים
                    if (customer.UserName.Equals(name) && customer.Gmail.Equals(gmail))
                        if (customer.Password.Equals(pass))
                            return 1;//אם הוא לקוח
                        else return 3;
                    }
                foreach (DAL.Maneger maneger in DB.Manegers)
                {
                    maneger.Person = p;
                    //בדיקה אם הסיסמה והשם תקינים
                    //לבדוקקקקקקקקק
                    if (maneger.Person.FirstName.Equals(FirstLast[0]) && maneger.Person.LastName.Equals(FirstLast[1]) && maneger.Password.Equals(pass) && maneger.Gmail.Equals(gmail))
                        return 2;
                }
            }
            return 3;//אם המשתמש קיים אך השם או הסיסמא אינם נכונים
        }
    }
}
