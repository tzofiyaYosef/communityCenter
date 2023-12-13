using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DTO;
using BLL;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using System.Web;

namespace API.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("API/customer")]
    public class CustomerController : ApiController
    {

        //פונקציה שמחברת משתמשים ועובדים
        [Route("GetLogin/{gmail}/{name}/{pass}")]
        public int GetLogin(string gmail ,string name, string pass)
        {
         return CustomerBl.Login(gmail, name, pass);
        }

        //צפייה בשאלות והכנסת תשובות
        //הדפסת השאלות על המסך
        [Route("GetQuestionWithoutAnswers")]
        public List<Contacts> GetQuestionWithoutAnswers()
        {
            return CustomerBl.GetListContactsWithoutAnswer();
        }

        //שינוי סיסמא
        [HttpGet]
        [Route("examinationGmail/{gmail}/{userName}/{pass}")]
        public bool examinationGmail(string gmail, string userName, string pass)
        {
            return CustomerBl.examination(gmail, userName, pass);
        }

        //הוספת חשבון
        [HttpPost]
        [Route("AddNewCustomer")]
        public bool AddNewCustomer()
        {
            DTO.Customer customer =
               JsonConvert.DeserializeObject<DTO.Customer>
               (HttpContext.Current.Request["customer"]);
            return CustomerBl.AddUser(customer);
        }

        //עדכון פרטי חשבון
        [HttpPost]
        [Route("updatingCustomer")]
        public bool updatingCustomer()
        {
            DTO.Customer customer =
                 JsonConvert.DeserializeObject<DTO.Customer>
                 (HttpContext.Current.Request["customer"]);
            return CustomerBl.updatingCustomer(customer);
        }

        [HttpGet]
        [Route("deletCustomer/{gmail}/{x}")]
        public bool deletCustomer(string gmail, int x)
        {
            //מחיקת חשבון של משתמש
            return CustomerBl.deletCustomer(gmail);
        }

        [HttpGet]
        [Route("deletQuestion/{id}")]
        public bool deletQuestion(int id)
        {
            //מחיקת פניה של משתמש
            return CustomerBl.deletQuestion(id);
        }

        //הדפסת רשימת המשתמשים
        [Route("GetCustomerList")]
        public List<DTO.Customer> GetCustomerList()
        {
           return CustomerBl.CustomerList();
        }

        //החזרת משתמש לפי המייל - שזה בעצם הת.ז
        [Route("GetCustomer/{gmail}/{x}")]
        public DTO.Customer GetCustomer(string gmail, int x)
        {
            return CustomerBl.GetCustomer(gmail);
        }

        //החזרת כל האירועים של המשתמש 
        [Route("GetAllEventForCust/{gmail}/{x}")]
        public List<int> GetAllEventForCust(string gmail, int x)
        {
            return CustomerBl.GetAllEventForCust(gmail);
        }

        //החזרת כל האירועים של המשתמש בסל הקניות 
        [Route("GetAllEventInCart/{gmail}/{x}")]
        public List<int> GetAllEventInCart(string gmail, int x)
        {
            return CustomerBl.GetAllEventInCart(gmail);
        }

        //הוספת אירוע לסל הקניות או קניה שלו
        [HttpGet]
        [Route("AddEventToCart/{idEvent}/{gmail}/{s}")]
        public bool AddEventToCart(int idEvent, string gmail, int s)
        {
            bool status = s == 1;
            MainEventToCustomer mainEvent = new MainEventToCustomer(-1, idEvent, gmail, status);
            return CustomerBl.AddEventToCart(mainEvent);
        }



        //   //לוקח מהטופס 
        //   [Route("GetDeserilazeFromXML")]
        //public void GetDeserilazeFromXML()
        //{
        //    CustomerBl.DeserilazeFromXML();
        //}
    }
}
