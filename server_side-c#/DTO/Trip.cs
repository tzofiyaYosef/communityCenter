using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Trip:MainEvent
    {
        public string LeavingTime { get; set; }//שעת יציאה
        public string ReturnTime { get; set; }//שעת חזרה
        public string Location { get; set; }//מיקום

        public Trip(string leavingTime, string returnTime, string location, string name, string description, DateTime eventDate, int numOfParticipance, int costPerParticipance, double eventCost, int minAge, int maxAge, string gender) : base(name, description, eventDate, numOfParticipance, costPerParticipance, eventCost, minAge, maxAge, gender)
        {
            LeavingTime = leavingTime;
            ReturnTime = returnTime;
            Location = location;
        }

        public Trip() { }


        // convert from DAL=>DTO
        public static DAL.Trip ConvertTripToDAL(Trip t)
        {
            DAL.MainEvent ev = new DAL.MainEvent()
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                EventDate = t.EventDate,
                NumOfParticipance = t.NumOfParticipance,
                CostPerParticipance = t.CostPerParticipance,
                EventCost = t.EventCost,
                MinAge = t.MinAge,
                MaxAge = t.MaxAge,
                Gender = t.Gender,
                ImagePath = t.ImagePath
            };

            DAL.Trip trip = new DAL.Trip()
            {
                LeavingTime = t.LeavingTime,
                ReturnTime = t.ReturnTime,
                Location = t.Location,
                EventId = t.Id,
                MainEvent = ev
            };
            return trip;
        }

        // convert from DTO=>DAL
        public static Trip ConvertDalTripToDto(DAL.Trip trip)
        {
            return new DTO.Trip
            {
                LeavingTime = trip.LeavingTime.ToString(),
                ReturnTime = trip.ReturnTime.ToString(),
                Location = trip.Location,
                EventDate = trip.MainEvent.EventDate,
                Id = trip.MainEvent.Id,
                Name = trip.MainEvent.Name,
                Description = trip.MainEvent.Description,
                NumOfParticipance = trip.MainEvent.NumOfParticipance,
                CostPerParticipance = trip.MainEvent.CostPerParticipance,
                EventCost = trip.MainEvent.EventCost,
                MinAge = (int)trip.MainEvent.MinAge,
                MaxAge = (int)trip.MainEvent.MaxAge,
                Gender = trip.MainEvent.Gender,
                ImagePath = trip.MainEvent.ImagePath
            };
        }
    }
}
