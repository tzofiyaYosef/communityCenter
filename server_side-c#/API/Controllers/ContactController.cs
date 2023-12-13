using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DTO;
using BLL;
using System.Web.Http.Cors;

namespace API.Controllers
{
    //העברתי הכל לדטה בייס שבטבלאות והכל עובד!!!
    [EnableCors("*", "*", "*")]
    [RoutePrefix("API/contacs")]
    public class ContactController : ApiController
    {
        [HttpGet]
        [Route("GetAllQuetions")]
        public List<Contacts> GetAllQuetions()
        {
            //למנהל- הכל
            return CustomerBl.GetListContacts();
        }

        [Route("GetEmptyQuetions")]
        public List<Contacts> GetEmptyQuetions()
        {
            //למנהל- שאלות בלי תשובה
            return CustomerBl.GetListContactsWithoutAnswer();
        }

        [Route("GetUserQuetions/{gmail}/{x}")]
        public List<Contacts> GetUserQuetions(string gmail, int x)
        {
            //למשתמש- כל שלו
            return CustomerBl.questionCustomer(gmail);
        }

        //לשאול את המורה
        [HttpGet]
        [Route("AddAnswerToQuestion/{contactId}/{answer}")]
        public bool AddAnswerToQuestion(int contactId, string answer)
        {
            //int contactId, string Answer
            //עדכון תשובה של המנהל
            return CustomerBl.GetAnswer(contactId, answer);
        }

        [HttpGet]
        [Route("AddQuestion/{gmail}/{question}")]
        public string AddQuestion(string gmail, string question)
        {
            //הוספת פניה של משתמש
            return CustomerBl.AddQuestion(new Contacts(question, gmail));
        }

        [HttpGet]
        [Route("updatingQuestion/{id}/{question}")]
        public bool updatingQuestion(int id, string question)
        {
            //עדכון פניה של משתמש
            return CustomerBl.updatingQuestion(id, question);
        }

        [HttpGet]
        [Route("deletQuestion/{id}")]
        public bool deletQuestion(int id)
        {
            //מחיקת פניה של משתמש
            return CustomerBl.deletQuestion(id);
        }
    }
}
