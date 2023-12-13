using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SpeachesDal
    {
        public static int addSpeaches(DAL.Speach speaches)
        {
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                DB.MainEvents.Add(speaches.MainEvent);
                speaches.EventId = speaches.MainEvent.Id;
                DB.Speaches.Add(speaches);
                DB.SaveChanges();
                return speaches.EventId;
            }
        }

        public static bool savePath(string path, int id)
        {
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                DAL.MainEvent mainEvent = DB.MainEvents.Where(i => i.Id == id).FirstOrDefault();
                if (mainEvent != null) {
                    mainEvent.ImagePath = path;
                    DB.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public static bool updatingSpeache(Speach s)
        {
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                Speach updateS = DB.Speaches.Where(i => i.EventId == s.EventId).FirstOrDefault();
                updateS.MainEvent = DB.MainEvents.Where(i => i.Id == s.EventId).FirstOrDefault();
                updateS.NameLecturer = s.NameLecturer;
                updateS.MainEvent.Name = s.MainEvent.Name;
                updateS.MainEvent.Description = s.MainEvent.Description;
                updateS.MainEvent.EventDate = s.MainEvent.EventDate;
                updateS.MainEvent.NumOfParticipance = s.MainEvent.NumOfParticipance;
                updateS.MainEvent.CostPerParticipance = s.MainEvent.CostPerParticipance;
                updateS.MainEvent.EventCost = s.MainEvent.EventCost;
                updateS.MainEvent.MinAge = s.MainEvent.MinAge;
                updateS.MainEvent.MaxAge = s.MainEvent.MaxAge;
                updateS.MainEvent.Gender = s.MainEvent.Gender;
                DB.SaveChanges();
                return true;
            }
        }
    }
}
