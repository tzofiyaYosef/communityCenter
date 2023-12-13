using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MainEventDal
    {
        public static List<DAL.MainEvent> AllEvent()
        {
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                List<DAL.MainEvent> allMainEvent = DB.MainEvents.ToList();
                foreach(DAL.MainEvent ev in allMainEvent)
                {
                    Speach s = DB.Speaches.Where(i => i.EventId == ev.Id).FirstOrDefault();
                    Trip t = DB.Trips.Where(i => i.EventId == ev.Id).FirstOrDefault();
                    if (s != null)
                        ev.Speach = s;
                    else ev.Trip = t;
                }
                return allMainEvent;
            }
        }

        public static List<DAL.Trip> AllTrip()
        {
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                List<DAL.Trip> allTrip = DB.Trips.ToList();
                foreach (DAL.Trip trip in allTrip)
                {
                    trip.MainEvent = DB.MainEvents.Where(t => t.Id.Equals(trip.EventId)).FirstOrDefault();
                }
                return allTrip;
            }
        }

        public static List<DAL.Speach> AllSpeach()
        {
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                List<DAL.Speach> allSpeach = DB.Speaches.ToList();
                foreach (DAL.Speach speach in allSpeach)
                {
                    speach.MainEvent = DB.MainEvents.Where(t => t.Id.Equals(speach.EventId)).FirstOrDefault();
                }
                return allSpeach;
            }
        }

        public static MainEvent eventProphil(int id)
        {
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                MainEvent mainEvent = null;
                foreach (MainEvent m in DB.MainEvents)
                    if (m.Id == id)
                        mainEvent = m;
                Trip trip = DB.Trips.Where(t => t.EventId == id).FirstOrDefault();
                Speach speach = DB.Speaches.Where(s => s.EventId == id).FirstOrDefault();
                mainEvent.Trip = trip;
                mainEvent.Speach = speach;
                return mainEvent;
            }
        }

        public static MainEvent EventProphil(int id)
        {
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                return DB.MainEvents.Where(m => m.Id == id).FirstOrDefault();
            }
        }

        public static Trip tripProphil(int id)
        {
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                MainEvent mainEvent = DB.MainEvents.Where(m => m.Id == id).FirstOrDefault();
                return DB.Trips.Where(t => t.EventId == id).FirstOrDefault();
            }
        }

        public static Speach SpeacheProphil(int id)
        {
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                MainEvent mainEvent = DB.MainEvents.Where(m => m.Id == id).FirstOrDefault();
                return DB.Speaches.Where(t => t.EventId == id).FirstOrDefault();
            }
        }

        public static string typeEvent(int id)
        {
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                if (DB.Trips.Where(t => t.EventId == id).FirstOrDefault() != null)
                    return "trip";
                return "speache";
            }
        }

        public static int CountCard(string gmail, int idEvent)
        {
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                int count = 0;
                foreach (MainEventToCustomer eventToCustomer in DB.MainEventToCustomers)
                    if (eventToCustomer.IdEvent == idEvent && eventToCustomer.IdCustomer.Equals(gmail) &&!eventToCustomer.Status)
                        count++;
                return count;
            }
        }

        public static int RemainingTickets(int idEvent, int amount)
        {
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                int count = 0;
                foreach(DAL.MainEventToCustomer ev in DB.MainEventToCustomers)
                    if (idEvent == ev.IdEvent && ev.Status)
                        count++;
                foreach (DAL.MainEvent ev in DB.MainEvents)
                    if (idEvent == ev.Id)
                        if (count + amount <= ev.NumOfParticipance)
                            //if(amount != 0)
                            //return amount;
                        /*else*/ return ev.NumOfParticipance - count;
            }
            return -1;//מסיבה כלשהי לא נמצא האירוע הנבחר
        }

        public static bool CardCancellation(int idEvent, string idCustomer)
        {
            //if (m.EventDate - DateTime.Now)
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                var b = (DB.MainEventToCustomers.FirstOrDefault(m => m.IdEvent == idEvent && m.IdCustomer == idCustomer && !m.Status));
                DB.MainEventToCustomers.Remove(b);
                DB.SaveChanges();
                return true;
            }
            return false;
        }

        public static List<DAL.MainEvent> AllEventSameType(string type)
        {
            List<DAL.MainEvent> events = new List<DAL.MainEvent>(), allEvent;
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                allEvent = DB.MainEvents.ToList();
                foreach(DAL.MainEvent ev in allEvent)
                {
                    if (ev.GetType().Equals(type))
                        events.Add(ev);
                }
            }
            return events;
        }

        public static void checkDate()
        {
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                foreach(MainEvent ev in DB.MainEvents)
                {
                    if (ev.EventDate.ToShortDateString().CompareTo(DateTime.Now.ToShortDateString()) > 0)
                        DB.MainEvents.Remove(ev);
                    DB.SaveChanges();
                }
            }
        }

        public static bool deletEvent(int id)
        {
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                DB.MainEvents.Remove(DB.MainEvents.Where(i => i.Id == id).FirstOrDefault());
                DB.SaveChanges();
                return true;
            }
        }

        public static bool deletTrip(int id)
        {
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                DB.Trips.Remove(DB.Trips.Where(i => i.EventId == id).FirstOrDefault());
                DB.SaveChanges();
                deletEvent(id);
                return true;
            }
        }

        public static bool deletSpeache(int id)
        {
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                DB.Speaches.Remove(DB.Speaches.Where(i => i.EventId == id).FirstOrDefault());
                DB.SaveChanges();
                deletEvent(id);
                return true;
            }
        }
    }
}
