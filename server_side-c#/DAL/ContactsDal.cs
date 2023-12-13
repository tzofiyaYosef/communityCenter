using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ContactsDal
    {
        public static List<DAL.Contact> GetContacts()
        {
            List<DAL.Contact> contact = new List<DAL.Contact>();
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                foreach (DAL.Contact c in DB.Contacts)
                    contact.Add(c);
            }
            return contact;
        }

        public static List<DAL.Contact> GetContactsWithAnswer()
        {
            List<DAL.Contact> contact = new List<DAL.Contact>();
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                foreach (DAL.Contact c in DB.Contacts)
                    if (c.Answer != null)
                        contact.Add(c);
            }
            return contact;
        }

        public static List<DAL.Contact> GetContactsWithoutAnswer()
        {
            List<DAL.Contact> contact = new List<DAL.Contact>();
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                foreach (DAL.Contact c in DB.Contacts)
                    if (c.Answer == null)
                       contact.Add(c);
            }
            return contact;
        }

        public static List<DAL.Contact> questionsCustomer(string gmail)
        {
            List<DAL.Contact> contact = new List<DAL.Contact>();
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                foreach (DAL.Contact c in DB.Contacts)
                    if (c.GmailId.Equals(gmail))
                    {
                        contact.Add(c);
                    }
            }
            return contact;
        }


        public static bool GetAnswer(int contactId, string answer)
        {
            bool flag = false;
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                foreach (DAL.Contact AllContacts in DB.Contacts)
                {

                    if (AllContacts.contactId == contactId)
                    {
                        AllContacts.Answer = answer;
                        flag= true;
                    }
                    
                }
                DB.SaveChanges();
            }
                return flag;
        }

        public static string AddQuestion(DAL.Contact c)
        {
            using(CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                //c.Customer = DB.Customers.Where(i => i.Gmail.Equals(c.GmailId)).First();
                DB.Contacts.Add(c);
                DB.SaveChanges();
            }
            return "נוסף בהצלחה!!";
        }

        public static bool updatingQuestion(int id, string question)
        {
            //עדכון פניה של משתמש
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                DAL.Contact c = DB.Contacts.Where(i => i.contactId == id).FirstOrDefault();
                c.Question = question;
                DB.SaveChanges();
                return true;
            }
        }

        public static bool deletQuestion(int id)
        {
            //מחיקת פניה של משתמש
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                DB.Contacts.Remove(DB.Contacts.Where(i => i.contactId == id).First());
                DB.SaveChanges();
                return true;
            }
        }
    }



}
