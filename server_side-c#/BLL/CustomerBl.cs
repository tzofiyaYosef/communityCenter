using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using DAL;
using DTO;

namespace BLL
{
    //פונקציות שקשורות למשתמש
    public class CustomerBl
    {
        //רישום
        public static bool AddUser(DTO.Customer newUser)
        {
            //התחברות לדאטה בייס שנמצא בדאל
            DAL.Customer uDAL = DTO.Customer.ConvertUserToDAL(newUser);
            return DAL.CustomerDal.AddUser(uDAL);
        }

        public static int Login(string gmail, string name, string pass)
        {
            return DAL.PersonDal.Login(gmail, name, pass);
        }

        public static bool GetAnswer(int contactId, string answer)
        {
            return DAL.ContactsDal.GetAnswer(contactId, answer);
        }

        //הדפסת השאלות של כל המשתמשים
        public static List<Contacts> GetListContacts()
        {
            List<DAL.Contact> contacts = ContactsDal.GetContactsWithAnswer();
            List<Contacts> contact = new List<Contacts>();
            foreach (DAL.Contact c in contacts)
                contact.Add(Contacts.ConvertDalUserToDto(c));
            return contact;
        }

        //שאלות בלי תשובות
        public static List<Contacts> GetListContactsWithoutAnswer()
        {
            List<DAL.Contact> contacts = ContactsDal.GetContactsWithoutAnswer();
            List<Contacts> contact = new List<Contacts>();
            foreach (DAL.Contact c in contacts)
                contact.Add(Contacts.ConvertDalUserToDto(c));
            return contact;
        }

        public static List<int> GetAllEventForCust(string gmail)
        {
            return CustomerDal.GetAllEventForCust(gmail);
        }

        public static List<int> GetAllEventInCart(string gmail)
        {
            return CustomerDal.GetAllEventInCart(gmail);
        }

        public static List<Contacts> questionCustomer(string gmail)
        {
            List<DAL.Contact> contacts = ContactsDal.questionsCustomer(gmail);
            List<Contacts> contact = new List<Contacts>();
            foreach (DAL.Contact c in contacts)
                contact.Add(Contacts.ConvertDalUserToDto(c));
            return contact;
        }

        //שינוי סיסמה למשתמש
        public static bool examination(string gmail, string userName, string pass)
        {
            return DAL.CustomerDal.examination(gmail, userName, pass);
        }

        //עדכון פניה של משתמש
        public static bool updatingQuestion(int id, string question)
        {
            //עדכון פניה של משתמש
            return ContactsDal.updatingQuestion(id, question);
        }

        //מחיקה פניה של משתמש
        public static bool deletQuestion(int id)
        {
            //מחיקת פניה של משתמש
            return ContactsDal.deletQuestion(id);
        }

        //עדכון פרטי חשבון
        public static bool updatingCustomer(DTO.Customer c)
        {
            string [] gmail = c.Gmail.Split(' ');
            if (!gmail[0].Equals(gmail[1]))
                return false;
            else return CustomerDal.updatingCustomer(DTO.Customer.ConvertUserToDAL(c), gmail[1]);
        }

        public static bool deletCustomer(string gmail)
        {
            //מחיקת חשבון של משתמש
            return CustomerDal.deletCustomer(gmail);
        }

        public static DTO.Customer GetCustomer(string gmail)
        {
            return DTO.Customer.ConvertDalUserToDto(CustomerDal.GetCustomer(gmail));
        }

        public static bool AddEventToCart(DTO.MainEventToCustomer ev)
        {
            return CustomerDal.AddEventToCart(DTO.MainEventToCustomer.ConverDtotEvToDAL(ev));
        }

        //
        //public static string changeGmailToUser(string gmail, string pass)
        //{
        //    foreach (CustomerDal c in DB.Customers)
        //        if (c.Gmail.Equals(gmail))
        //        {
        //            string temp = c.Password;
        //            c.Password = pass;
        //            return "after"+pass+"-- before"+temp;
        //        }

        //    return "failed";
        //}


        //הכנסת השאלה מהמשתמש לרשימת הצור קשר

        public static string AddQuestion(Contacts c)
        {
            //int index = DB.Customers.FindIndex(i => i.Gmail == c.GmailId);
            DAL.Contact cDAL = Contacts.ConvertContactsToDAL(c);
            return DAL.ContactsDal.AddQuestion(cDAL);
        }

        public static List<DTO.Customer> CustomerList()
        {
            List <DAL.Customer> customersDal = DAL.CustomerDal.CustomerList();
            List <DTO.Customer> customersDto = new List<DTO.Customer>();
            foreach (DAL.Customer c in customersDal)
                customersDto.Add(DTO.Customer.ConvertDalUserToDto(c));
            return customersDto;
        }

        //לקריאה
        //    public static void DeserilazeFromXML()
        //    {//W:\\tzofiya yosef\\server\\DAL\\XMLFile.xml

        //        const string PATH = "C:F:\\tzofiya yosef\\server\\DAL\\XMLFile.xml";

        //        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<CustomerDal>));

        //        List<CustomerDal> newListOfUsers;
        //        using (StreamReader reader = new StreamReader(PATH))
        //        {
        //            newListOfUsers = (List<CustomerDal>)xmlSerializer.Deserialize(reader);

        //        }
        //    }

        //    //לכתיבה
        //    public static void SerilazeToXML()
        //    {
        //        SerilazeToXML(DB.Customers);
        //    }
        //    public static void SerilazeToXML(List<CustomerDal> allUsers) 
        //{

        //    const string PATH = "C:\\Users\\User\\OneDrive\\שולחן העבודה\\tzofiya yosef\\server\\BLL\\CustomerBl.cs";

        //    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<CustomerDal>));

        //    using (StreamWriter writer = File.AppendText(PATH))
        //    {
        //        xmlSerializer.Serialize(writer, allUsers);
        //    }
    }

    }


