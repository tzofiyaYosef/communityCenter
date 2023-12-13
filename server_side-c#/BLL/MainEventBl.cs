using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using DAL;
using DTO;
using System.Web;

namespace BLL
{
    public class MainEventBl
    {

        public static bool BuyCard(string gmail)
        {
            return CustomerDal.BuyCard(gmail);
        }

        //החזרת כל האירועיים
        public static List<DTO.MainEvent> AllEvent()
        {
            List <DAL.MainEvent> eventDal = DAL.MainEventDal.AllEvent();
            List <DTO.MainEvent> eventDto = new List<DTO.MainEvent>();
            foreach (DAL.MainEvent ev in eventDal)
            {
                if (ev.Speach != null)
                {
                    eventDto.Add((DTO.Speaches.ConvertDalSpeachToDto(ev.Speach)));
                    return eventDto;
                }
                    
                else { eventDto.Add(DTO.Trip.ConvertDalTripToDto(ev.Trip));return eventDto; }
            }
            return null;
        }


        //החזרת כל הטיולים
        public static List<DTO.Trip> AllTrip()
        {
            List<DAL.Trip> eventDal = DAL.MainEventDal.AllTrip();
            List<DTO.Trip> eventDto = new List<DTO.Trip>();
            foreach (DAL.Trip t in eventDal)
                eventDto.Add(DTO.Trip.ConvertDalTripToDto(t));
            return eventDto;
        }

        //החזרת כל הרצאות
        public static List<DTO.Speaches> AllSpeache()
        {
            List<DAL.Speach> eventDal = DAL.MainEventDal.AllSpeach();
            List<DTO.Speaches> eventDto = new List<DTO.Speaches>();
            foreach (DAL.Speach s in eventDal)
                eventDto.Add(DTO.Speaches.ConvertDalSpeachToDto(s));
            return eventDto;
        }

        public static bool updatingTrip(DTO.Trip mainEvent, HttpPostedFile img)
        {
            if (img != null)
            {
                //string basePath = "C:\\Users\\Comp10\\Desktop\\צופיהההה בהצלחההה\\server";
                string basePath = "C:\\Users\\User\\OneDrive\\שולחן העבודה\\צופיהההה בהצלחההה\\server";
                string pathToGetExtension = string.Format(@"c:\" + img.FileName);
                // string EventImageNewPath =  "/DAL/Images/" + mainEvent.Id + Path.GetExtension(pathToGetExtension);
                //שמירת התמונה בשם של הניתוב של האירוע 
                DAL.Trip evDal = DTO.Trip.ConvertTripToDAL(mainEvent);
                string EventImageNewPath = "/DAL/Images/" + evDal.EventId + Path.GetExtension(pathToGetExtension);
                String path = mainEvent.ImagePath;
                if (System.IO.File.Exists(path)) { System.IO.File.Delete(path); }
                img.SaveAs(basePath + EventImageNewPath);
                mainEvent.ImagePath = EventImageNewPath;
                SpeachesDal.savePath(basePath + EventImageNewPath, evDal.EventId);
            }
            return TripDal.updatingTrip(DTO.Trip.ConvertTripToDAL(mainEvent));
        }

        public static bool updatingSpeache(DTO.Speaches mainEvent, HttpPostedFile img)
        {
            if (img != null)
            {
                //string basePath = "C:\\Users\\Comp10\\Desktop\\צופיהההה בהצלחההה\\server";
                string basePath = "C:\\Users\\User\\OneDrive\\שולחן העבודה\\צופיהההה בהצלחההה\\server";
                string pathToGetExtension = string.Format(@"c:\" + img.FileName);
                // string EventImageNewPath =  "/DAL/Images/" + mainEvent.Id + Path.GetExtension(pathToGetExtension);
                //שמירת התמונה בשם של הניתוב של האירוע 
                DAL.Speach evDal = DTO.Speaches.ConvertSpeachToDAL(mainEvent);
                string EventImageNewPath = "/DAL/Images/" + evDal.EventId + Path.GetExtension(pathToGetExtension);
                String path = mainEvent.ImagePath;
                if (System.IO.File.Exists(path)) { System.IO.File.Delete(path); }
                img.SaveAs(basePath + EventImageNewPath);
                mainEvent.ImagePath = EventImageNewPath;
                SpeachesDal.savePath(basePath + EventImageNewPath, evDal.EventId);
            }
            return SpeachesDal.updatingSpeache(DTO.Speaches.ConvertSpeachToDAL(mainEvent));
        }

        public static DTO.MainEvent EventProphil(int id)
        {
            return DTO.MainEvent.ConvertDalEventToDto(MainEventDal.EventProphil(id));
        }

        public static DTO.Trip tripProphil(int id)
        {
            return DTO.Trip.ConvertDalTripToDto(MainEventDal.tripProphil(id));
        }

        public static DTO.Speaches SpeacheProphil(int id)
        {
            return DTO.Speaches.ConvertDalSpeachToDto(MainEventDal.SpeacheProphil(id));
        }

        public static string typeEvent(int id)
        {
            return DAL.MainEventDal.typeEvent(id);
        }

        public static int RemainingTickets(int idEvent, int amount)
        {
            return DAL.MainEventDal.RemainingTickets(idEvent, amount);
        }

        public static bool CardCancellation(int idEvent, string idCustomer)
        {
            return MainEventDal.CardCancellation(idEvent, idCustomer);
        }

        public static List<DTO.MainEvent> AllEventSameType(string type)
        {
            List<DAL.MainEvent> eventDal = DAL.MainEventDal.AllEventSameType(type);
            List<DTO.MainEvent> eventDto = new List<DTO.MainEvent>();
            foreach (DAL.MainEvent ev in eventDal)
            {
                eventDto.Add(DTO.MainEvent.ConvertDalEventToDto(ev));
            }
            return eventDto;
        }

        public static int CountCard(string gmail, int idEvent)
        {
            return MainEventDal.CountCard(gmail, idEvent);
        }

        public static void checkDate()
        {
            DAL.MainEventDal.checkDate();
        }

        public static bool addEvent(DTO.MainEvent mainEvent, HttpPostedFile img)
        {
            string basePath = "C:\\Users\\Comp10\\Desktop\\צופיהההה בהצלחההה\\server";
            string pathToGetExtension = string.Format(@"c:\" + img.FileName);
            // string EventImageNewPath =  "/DAL/Images/" + mainEvent.Id + Path.GetExtension(pathToGetExtension);
            //שמירת התמונה בשם של הניתוב של האירוע 
            string EventImageNewPath = null;
            if (mainEvent is DTO.Trip)
            {
                DAL.Trip evDal = DTO.Trip.ConvertTripToDAL((DTO.Trip)mainEvent);
                EventImageNewPath = "/DAL/Images/" + TripDal.addTrip(evDal) + Path.GetExtension(pathToGetExtension);
            }
            else if(mainEvent is DTO.Speaches)
            {
                DAL.Speach evDal = DTO.Speaches.ConvertSpeachToDAL((DTO.Speaches)mainEvent);
                EventImageNewPath = "/DAL/Images/" + SpeachesDal.addSpeaches(evDal) + Path.GetExtension(pathToGetExtension);
            }
            img.SaveAs(basePath + EventImageNewPath);
            mainEvent.ImagePath = EventImageNewPath;
            return false;
        }

        public static bool addTrip(DTO.Trip mainEvent, HttpPostedFile img)
        {
            //string basePath = "C:\\Users\\Comp10\\Desktop\\צופיהההה בהצלחההה\\server";
            string basePath = "C:\\Users\\User\\OneDrive\\שולחן העבודה\\צופיהההה בהצלחההה\\server";
            string pathToGetExtension = string.Format(@"c:\" + img.FileName);
            // string EventImageNewPath =  "/DAL/Images/" + mainEvent.Id + Path.GetExtension(pathToGetExtension);
            //שמירת התמונה בשם של הניתוב של האירוע 
            DAL.Trip evDal = DTO.Trip.ConvertTripToDAL(mainEvent);
            string EventImageNewPath = "/DAL/Images/" + TripDal.addTrip(evDal) + Path.GetExtension(pathToGetExtension);
            img.SaveAs(basePath + EventImageNewPath);
            mainEvent.ImagePath = EventImageNewPath;
            return TripDal.savePath(basePath + EventImageNewPath, evDal.EventId);
        }

        public static bool addSpeaches(DTO.Speaches mainEvent, HttpPostedFile img)
        {
            //string basePath = "C:\\Users\\Comp10\\Desktop\\צופיהההה בהצלחההה\\server";
            string basePath = "C:\\Users\\User\\OneDrive\\שולחן העבודה\\צופיהההה בהצלחההה\\server";
            string pathToGetExtension = string.Format(@"c:\" + img.FileName);
            // string EventImageNewPath =  "/DAL/Images/" + mainEvent.Id + Path.GetExtension(pathToGetExtension);
            //שמירת התמונה בשם של הניתוב של האירוע 
            DAL.Speach evDal = DTO.Speaches.ConvertSpeachToDAL(mainEvent);
            string EventImageNewPath = "/DAL/Images/" + SpeachesDal.addSpeaches(evDal) + Path.GetExtension(pathToGetExtension);
            img.SaveAs(basePath + EventImageNewPath);
            mainEvent.ImagePath = EventImageNewPath;
            return SpeachesDal.savePath(basePath+EventImageNewPath, evDal.EventId);
        }

        public static bool deletEvent(int id)
        {
            return MainEventDal.deletEvent(id);
        }

        public static bool deletTrip(int id)
        {
            return MainEventDal.deletTrip(id);
        }

        public static bool deletSpeache(int id)
        {
            return MainEventDal.deletSpeache(id);
        }
    }
}
