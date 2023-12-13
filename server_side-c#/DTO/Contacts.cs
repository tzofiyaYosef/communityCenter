using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace DTO
{
    public class Contacts
    {
        public int contactId;
        public string Question { get; set; }
        public string Answer { get; set; }

        public string GmailId { get; set; }


        public Contacts()
        {
        }



        public Contacts( string question, string gmail):this()
        {
            contactId = -1;
            Question = question;
            Answer = null;
            this.GmailId = gmail;
        }


        public Contacts(string question, string answer, string gmailId) : this()
        {
            contactId = -1;
            Question = question;
            Answer = answer;
            GmailId = gmailId;
        }


        // convert from DAL=>DTO
        public static DAL.Contact ConvertContactsToDAL(Contacts c)
        {
            return new DAL.Contact()
            {
                contactId = c.contactId,
                Answer = c.Answer,
                GmailId = c.GmailId,
                Question = c.Question
            };
        }

        // convert from DTO=>DAL
        public static Contacts ConvertDalUserToDto(DAL.Contact c)
        {
            return new Contacts()
            {
                contactId = c.contactId,
                Answer =c.Answer,
                GmailId = c.GmailId,
                Question = c.Question
            };
        }

    }
}
