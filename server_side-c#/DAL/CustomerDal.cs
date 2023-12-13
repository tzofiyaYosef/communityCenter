using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CustomerDal
    {
        public static bool BuyCard(string gmail)
        {
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                foreach (MainEventToCustomer m in DB.MainEventToCustomers)
                    if (m.IdCustomer.Equals(gmail))
                        m.Status = true;
                DB.SaveChanges();
                return true;
            }
        }

        public static bool examination(string gmail, string userName, string pass)
        {
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                bool flag = false;
               foreach (DAL.Customer c in DB.Customers)
                  if (c.Gmail.Equals(gmail) && c.UserName.Equals(userName))
                  {
                      c.Password = pass;
                      flag = true;
                  }
                DB.SaveChanges();
                return flag;
            }  
        }

        public static bool AddUser(DAL.Customer uDAL)
        {
            //התחברות לדאטה בייס שנמצא בדאל
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                foreach (DAL.Person c in DB.People)
                {
                    //בדיקה אם המשתמש קיים במערכת אז הוא לא יוכל להירשם אלא להתחבר
                    if (c.Gmail.Equals(uDAL.Gmail))
                        return false;
                }
                DB.People.Add(uDAL.Person);
                DB.Customers.Add(uDAL);
                //DAL.Customer cNew = new DAL.Customer();
                //cNew.Person = uDAL.Person;
                //cNew = uDAL;
                //DB.Customers.Add(cNew);
                //DB.People.Add(cNew.Person);
                DB.SaveChanges();
            }
            return true;
        }

        public static List<DAL.Customer> CustomerList()
        {
            List<DAL.Customer> customers = new List<DAL.Customer>();
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                DAL.Customer customer = new DAL.Customer();
                foreach (DAL.Customer p in DB.Customers)
                {
                    customer = p;
                    customer.Person = getPerson(p.Gmail);
                    customers.Add(customer);
                } 
            }
            return customers;
        }

        public static DAL.Person getPerson(string gmail)
        {
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                return DB.People.Where(i => i.Gmail.Equals(gmail)).FirstOrDefault();
            }
        }

        //לא עובד!!!!
        public static bool updatingCustomer(Customer c, string gmail)
        {
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                DB.Customers.Include("Person").FirstOrDefault(i => i.Gmail.Equals(gmail)).Person.FirstName = c.Person.FirstName;
                DB.Customers.Include("Person").FirstOrDefault(i => i.Gmail.Equals(gmail)).Person.LastName = c.Person.LastName;
                DB.Customers.Include("Person").FirstOrDefault(i => i.Gmail.Equals(gmail)).Person.Phone = c.Person.Phone;
                DB.Customers.Include("Person").FirstOrDefault(i => i.Gmail.Equals(gmail)).Password = c.Password;
                DB.Customers.Include("Person").FirstOrDefault(i => i.Gmail.Equals(gmail)).UserName = c.UserName;
                //DB.Customers.Include("Person").FirstOrDefault(i => i.Gmail.Equals(gmail)).Gmail = c.Gmail;
                //DB.Customers.Include("Person").FirstOrDefault(i => i.Gmail.Equals(gmail)).Person = c.Person;
                DB.SaveChanges();
                return true;
            }
        }

        public static bool deletCustomer(string gmail)
        {
            //מחיקת חשבון של משתמש
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                foreach (DAL.Contact c in DB.Contacts)
                    if (c.GmailId.Equals(gmail))
                        DB.Contacts.Remove(c);
                foreach (DAL.MainEventToCustomer m in DB.MainEventToCustomers)
                    if (m.IdCustomer.Equals(gmail))
                        DB.MainEventToCustomers.Remove(m);

                DB.People.Remove(DB.People.Where(i => i.Gmail.Equals(gmail)).First());
                DB.Customers.Remove(DB.Customers.Where(i => i.Gmail.Equals(gmail)).First());
                DB.SaveChanges();
                return true;
            }
        }

        public static DAL.Customer GetCustomer(string gmail)
        {
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                DAL.Customer c = DB.Customers.Where(i => i.Gmail.Equals(gmail)).First();
                c.Person = getPerson(gmail);
                return c;
            }
        }

        public static List<int> GetAllEventForCust(string gmail)
        {
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                List<int> str = new List<int>();
                foreach (MainEventToCustomer ev in DB.MainEventToCustomers)
                    if (ev.IdCustomer.Equals(gmail) && ev.Status)
                        str.Add(ev.IdEvent);
                return str;
            }
        }

        public static List<int> GetAllEventInCart(string gmail)
        {
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                List<int> str = new List<int>();
                foreach (MainEventToCustomer ev in DB.MainEventToCustomers)
                    if (ev.IdCustomer.Equals(gmail) && !ev.Status)
                        str.Add(ev.IdEvent);
                return str;
            }
        }

        public static bool AddEventToCart(MainEventToCustomer ev)
        {
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                DB.MainEventToCustomers.Add(ev);
                DB.SaveChanges();
                return true;
            }
        }
    }
}
