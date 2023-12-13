using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class ManegerDal
    {
        public static bool AddManager(Maneger maneger)
        {
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                foreach (DAL.Maneger m in DB.Manegers)
                {
                    //בדיקה אם המשתמש קיים במערכת אז הוא לא יוכל להירשם אלא להתחבר
                    if (m.Id.Equals(maneger.Id))
                    {
                        return false;
                    }
                }
                DB.People.Add(maneger.Person);
                maneger.Gmail = maneger.Person.Gmail;
                DB.Manegers.Add(maneger);
                DB.SaveChanges();
            }
            return true;
         }

        public static DAL.Maneger GetManager(string gmail)
        {
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                DAL.Maneger m = DB.Manegers.Where(i => i.Gmail.Equals(gmail)).FirstOrDefault();
                m.Person = CustomerDal.getPerson(gmail);
                return m;
            }
        }

        public static bool deletManager(string gmail)
        {
            //מחיקת חשבון של משתמש
            using (CommunityCenterDBEntities DB = new CommunityCenterDBEntities())
            {
                DB.People.Remove(DB.People.Where(i => i.Gmail.Equals(gmail)).FirstOrDefault());
                DB.Manegers.Remove(DB.Manegers.Where(i => i.Gmail.Equals(gmail)).FirstOrDefault());
                DB.SaveChanges();
                return true;
            }
        }
    }
}
