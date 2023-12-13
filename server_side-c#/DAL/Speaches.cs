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
    }
}
