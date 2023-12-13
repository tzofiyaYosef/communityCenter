using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TripDal
    {
        public static int addTrip(DAL.Trip trip)
        {
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                DB.MainEvents.Add(trip.MainEvent);
                //trip.EventId = trip.MainEvent.Id;
                DB.Trips.Add(trip);
                DB.SaveChanges();
                return trip.EventId;
            }
        }

        public static bool savePath(string path, int id)
        {
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                DAL.MainEvent mainEvent = DB.MainEvents.Where(i => i.Id == id).FirstOrDefault();
                if (mainEvent != null)
                {
                    mainEvent.ImagePath = path;
                    DB.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public static bool updatingTrip(Trip t)
        {
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                Trip updateT = DB.Trips.Where(i => i.EventId == t.EventId).FirstOrDefault();
                updateT.MainEvent = DB.MainEvents.Where(i => i.Id == t.EventId).FirstOrDefault();
                updateT.LeavingTime = t.LeavingTime;
                updateT.ReturnTime = t.ReturnTime;
                updateT.Location = t.Location;
                updateT.MainEvent.Name = t.MainEvent.Name;
                updateT.MainEvent.Description = t.MainEvent.Description;
                updateT.MainEvent.EventDate = t.MainEvent.EventDate;
                updateT.MainEvent.NumOfParticipance = t.MainEvent.NumOfParticipance;
                updateT.MainEvent.CostPerParticipance = t.MainEvent.CostPerParticipance;
                updateT.MainEvent.EventCost = t.MainEvent.EventCost;
                updateT.MainEvent.MinAge = t.MainEvent.MinAge;
                updateT.MainEvent.MaxAge = t.MainEvent.MaxAge;
                updateT.MainEvent.Gender = t.MainEvent.Gender;
                DB.SaveChanges();
                return true;
            }
        }
    }
}
