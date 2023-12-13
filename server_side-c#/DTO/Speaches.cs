using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Speaches:MainEvent
    {
        public string NameLecturer { get; set; }//שם המרצה / מפעיל ההופעה

        public Speaches(string nameLecturer, string name, string description, DateTime eventDate, int numOfParticipance, int costPerParticipance, double eventCost, int minAge, int maxAge, string gender) : base(name, description, eventDate, numOfParticipance, costPerParticipance, eventCost, minAge, maxAge, gender)
        {
            NameLecturer = nameLecturer;
        }

        public Speaches(string name, string description, DateTime eventDate, int numOfParticipance, double eventCost, string gender) : base(name, description, eventDate, numOfParticipance, eventCost, gender)
        {
            NameLecturer = null;
        }

        public Speaches() { }

        // convert from DAL=>DTO
        public static DAL.Speach ConvertSpeachToDAL(Speaches s)
        {
            DAL.MainEvent ev = new DAL.MainEvent()
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                EventDate = s.EventDate,
                NumOfParticipance = s.NumOfParticipance,
                CostPerParticipance = s.CostPerParticipance,
                EventCost = s.EventCost,
                MinAge = s.MinAge,
                MaxAge = s.MaxAge,
                Gender = s.Gender,
                ImagePath = s.ImagePath
            };

            DAL.Speach speach = new DAL.Speach()
            {
                EventId = s.Id,
                NameLecturer = s.NameLecturer,
                MainEvent = ev
            };
            return speach;
        }

        // convert from DTO=>DAL
        public static Speaches ConvertDalSpeachToDto(DAL.Speach speach)
        {
            return new DTO.Speaches
            {
                NameLecturer = speach.NameLecturer,
                EventDate = speach.MainEvent.EventDate,
                Id = speach.MainEvent.Id,
                Name = speach.MainEvent.Name,
                Description = speach.MainEvent.Description,
                NumOfParticipance = speach.MainEvent.NumOfParticipance,
                CostPerParticipance = speach.MainEvent.CostPerParticipance,
                EventCost = speach.MainEvent.EventCost,
                MinAge = (int)speach.MainEvent.MinAge,
                MaxAge = (int)speach.MainEvent.MaxAge,
                Gender = speach.MainEvent.Gender,
                ImagePath = speach.MainEvent.ImagePath
            };
        }
    }
}

