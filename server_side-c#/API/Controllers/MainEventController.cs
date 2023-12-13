using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DTO;
using BLL;
using System.Web.Http.Cors;
using System.Web;
using Newtonsoft.Json;

namespace API.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("API/mainEvents")]
    public class MainEventController : ApiController
    {
        //הצגת כל האירועים
        [HttpGet]
        [Route("AllEvent")]
        public List<DTO.MainEvent> AllEvent()
        {
            return MainEventBl.AllEvent();
        }

        //הצגת כל הטיולים
        [HttpGet]
        [Route("AllTrip")]
        public List<DTO.Trip> AllTrip()
        {
            return MainEventBl.AllTrip();
        }

        //הצגת כל ההרצאות / הופעות
        [HttpGet]
        [Route("AllSpeache")]
        public List<DTO.Speaches> AllSpeache()
        {
            return MainEventBl.AllSpeache();
        }

        //בדיקת כמות הכרטיסים הנותרים
        [HttpGet]
        [Route("RemainingTickets/{idEvent}/{amount}")]
        public int RemainingTickets(int idEvent, int amount)
        {
            return MainEventBl.RemainingTickets(idEvent, amount);
        }

        //בדיקת כמות הכרטיסים של לקוח מסוים מאירוע מסוים 
        [HttpGet]
        [Route("CountCard/{gmail}/{idEvent}")]
        public int CountCard(string gmail, int idEvent)
        {
            return MainEventBl.CountCard(gmail, idEvent);
        }

        //קניית כרטיסים
        [HttpGet]
        [Route("BuyCard/{idCustomer}/{x}")]
        public bool BuyCard(string idCustomer, int x)
        {
            return MainEventBl.BuyCard(idCustomer);
        }

        //ביטול כרטיס
        [HttpGet]
        [Route("CardCancellation/{idCustomer}/{idEvent}")]
        public bool CardCancellation(string idCustomer, int idEvent)
        {
            return MainEventBl.CardCancellation(idEvent, idCustomer);
        }

        //החזרת כל האירועים מאותו סוג
        [HttpGet]
        [Route("AllEventSameType/{type}")]
        public List<DTO.MainEvent> AllEventSameType(string type)
        {
            return MainEventBl.AllEventSameType(type);
        }

        //בדיקת תאריך של אירוע
        [HttpGet]
        [Route("checkDate")]
        public void checkDate()
        {
            MainEventBl.checkDate();
        }

        //הוספת אירוע חדש
        [HttpPost]
        [Route("addEvent")]
        public bool AddEvent()
        {
           //MainEventBl.addEvent();
           //שליפת התמונה מתוך הג'יסון
            HttpPostedFile img = HttpContext.Current.Request.Files[0];
            //שליפת האירוע מתוך הג'יסון
            DTO.MainEvent mainEvent =
                 JsonConvert.DeserializeObject<DTO.MainEvent>
                 (HttpContext.Current.Request["mainEvent"]);
            return MainEventBl.addEvent(mainEvent, img);
        }

        //הוספת טיול חדש
        [HttpPost]
        [Route("AddTrip")]
        public bool AddTrip()
        {
            //MainEventBl.addEvent();
            //שליפת התמונה מתוך הג'יסון
            HttpPostedFile img = HttpContext.Current.Request.Files[0];
            //שליפת האירוע מתוך הג'יסון
            DTO.Trip trip =
                 JsonConvert.DeserializeObject<DTO.Trip>
                 (HttpContext.Current.Request["trip"]);
            return MainEventBl.addTrip(trip, img);
        }

        //הוספת הרצאה / הופעה חדשה
        [HttpPost]
        [Route("AddSpeach")]
        public bool AddSpeach()
        {
            //MainEventBl.addEvent();
            //שליפת התמונה מתוך הג'יסון
            HttpPostedFile img = HttpContext.Current.Request.Files[0];
            //שליפת האירוע מתוך הג'יסון
            DTO.Speaches speaches =
                 JsonConvert.DeserializeObject<DTO.Speaches>
                 (HttpContext.Current.Request["speaches"]);
            return MainEventBl.addSpeaches(speaches, img);
        }

        //עדכון אירוע קיים
        [HttpPost]
        [Route("updatingEvent")]
        public bool updatingEvent()
        {
            //MainEventBl.addEvent();
            //שליפת התמונה מתוך הג'יסון
            HttpPostedFile img = HttpContext.Current.Request.Files[0];
            //שליפת האירוע מתוך הג'יסון
            DTO.MainEvent mainEvent =
                 JsonConvert.DeserializeObject<DTO.MainEvent>
                 (HttpContext.Current.Request["mainEvent"]);
            deletEvent(mainEvent.Id);
            return MainEventBl.addEvent(mainEvent, img);
        }

        //עדכון טיול קיים
        [HttpPost]
        [Route("updatingTrip")]
        public bool updatingTrip()
        
        {
            //MainEventBl.addEvent();
            //שליפת התמונה מתוך הג'יסון
            HttpPostedFile img = null;
            if (HttpContext.Current.Request.Files.Count>0)
               img = HttpContext.Current.Request.Files[0];
            //שליפת האירוע מתוך הג'יסון
            DTO.Trip trip =
                 JsonConvert.DeserializeObject<DTO.Trip>
                 (HttpContext.Current.Request["trip"]);
            //deletTrip(trip.Id);
            return MainEventBl.updatingTrip(trip, img);
        }

        //עדכון הרצאה קיימת
        [HttpPost]
        [Route("updatingSpeache")]
        public bool updatingSpeache()
        {
            //MainEventBl.addEvent();
            //שליפת התמונה מתוך הג'יסון
            HttpPostedFile img = null;
            if (HttpContext.Current.Request.Files.Count > 0)
                img = HttpContext.Current.Request.Files[0];
            //שליפת האירוע מתוך הג'יסון
            DTO.Speaches speache =
                 JsonConvert.DeserializeObject<DTO.Speaches>
                 (HttpContext.Current.Request["speache"]);
            //deletTrip(trip.Id);
            return MainEventBl.updatingSpeache(speache, img);
        }

        //מחיקת אירוע
        [HttpGet]
        [Route("deletEvent/{id}")]
        public bool deletEvent(int id)
        {
            return MainEventBl.deletEvent(id);
        }

        //מחיקת טיול 
        [HttpGet]
        [Route("deletTrip/{id}")]
        public bool deletTrip(int id)
        {
            return MainEventBl.deletTrip(id);
        }

        //מחיקת הרצאה
        [HttpGet]
        [Route("deletSpeache/{id}")]
        public bool deletSpeache(int id)
        {
            return MainEventBl.deletSpeache(id);
        }

        //החזרת סוג האירוע
        [HttpGet]
        [Route("typeEvent/{id}")]
        public string typeEvent(int id)
        {
            return MainEventBl.typeEvent(id);
        }

        //הצגת אירוע 
        [HttpGet]
        [Route("EventProphil/{id}")]
        public MainEvent EventProphil(int id)
        {
            return MainEventBl.EventProphil(id);
        }

        //הצגת טיול קיים
        [HttpGet]
        [Route("tripProphil/{id}")]
        public Trip tripProphil(int id)
        {
            return MainEventBl.tripProphil(id);
        }

        //הצגת הרצאה 
        [HttpGet]
        [Route("SpeacheProphil/{id}")]
        public Speaches SpeacheProphil(int id)
        {
            return MainEventBl.SpeacheProphil(id);
        }
    }
}