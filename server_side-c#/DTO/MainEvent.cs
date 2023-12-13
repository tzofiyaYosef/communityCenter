using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public class MainEvent
    {
        public int Id { get; set; }//מס' זיהוי של אירוע
        public string Name { get; set; }//שם של האירוע - פירוט מהו
        public string Description { get; set; }//תיאור האירוע
        public DateTime EventDate { get; set; }//תאריך האירוע
        public int NumOfParticipance { get; set; }//מס' משתתפים
        public int CostPerParticipance { get; set; }//עלות למשתתף
        public double EventCost { get; set; }//סך עלות האירוע
        public int MinAge { get; set; }//הגיל המינימלי למשתתף
        public int MaxAge { get; set; }//הגיל המקסימלי למשתתף
        public string Gender { get; set; }//למי מיועד??? בנים/ בנות
        public string ImagePath { get; set; }//כתובת של תמונה

        //פעולה בונה מלאה
        public MainEvent(string name, string description, DateTime eventDate, int numOfParticipance, int costPerParticipance, double eventCost, int minAge, int maxAge, string gender) : this(name, description, eventDate, numOfParticipance, eventCost)
        {
            CostPerParticipance = costPerParticipance;
            MinAge = minAge;
            MaxAge = maxAge;
            Gender = gender;
        }


        //פעולה בונה חסרה
        public MainEvent(string name, string description, DateTime eventDate, int numOfParticipance, double eventCost, string gender) : this(name, description, eventDate, numOfParticipance, eventCost)
        {
            CostPerParticipance = 0;
            MinAge = 0;
            MaxAge = 0;
            Gender = gender;
        }

        //פעולה בונה מעתיקה
        public MainEvent(string name, string description, DateTime eventDate, int numOfParticipance, double eventCost)
        {
            Id =  -1;
            EventDate = eventDate;
            NumOfParticipance = numOfParticipance;
            EventCost = eventCost;
            Name = name;
            Description = description;
        }

        //פעולה בונה חסרה
        public MainEvent() { }
        // convert from DAL=>DTO
        public static DAL.MainEvent ConvertEventToDAL(DTO.MainEvent events)
        {
            return new DAL.MainEvent()
            {
                Id = events.Id,
                Name = events.Name,
                Description = events.Description,
                EventDate = events.EventDate,
                NumOfParticipance = events.NumOfParticipance,
                CostPerParticipance = events.CostPerParticipance,
                EventCost = events.EventCost,
                MinAge = events.MinAge,
                MaxAge = events.MaxAge,
                Gender = events.Gender,
                ImagePath = events.ImagePath
            };
        }

        // convert from DTO=>DAL
        public static DTO.MainEvent ConvertDalEventToDto(DAL.MainEvent events)
        {
            return new DTO.MainEvent
            {
                Id = events.Id,
                Name = events.Name,
                Description = events.Description,
                EventDate = events.EventDate,
                NumOfParticipance = events.NumOfParticipance,
                CostPerParticipance = events.CostPerParticipance,
                EventCost = events.EventCost,
                MinAge = (int)events.MinAge,
                MaxAge = (int)events.MaxAge,
                Gender = events.Gender,
                ImagePath = events.ImagePath
            };
        }
    }
}
