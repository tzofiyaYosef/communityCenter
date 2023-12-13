using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace DTO
{
    public class MainEventToCustomer
    {
        public int Id { get; set; }
        public int IdEvent { get; set; }
        public string IdCustomer { get; set; }
        public bool Status { get; set; }

        public MainEventToCustomer(int id, int idEvent, string idCustomer, bool status)
        {
            Id = id;
            IdEvent = idEvent;
            IdCustomer = idCustomer;
            Status = status;
        }

        public MainEventToCustomer()
        {
        }

        // convert from DAL=>DTO
        public static DAL.MainEventToCustomer ConverDtotEvToDAL(MainEventToCustomer ev)
        {
            DAL.MainEventToCustomer ev1 = new DAL.MainEventToCustomer()
            {
               Id = ev.Id,
               IdCustomer = ev.IdCustomer,
               IdEvent = ev.IdEvent,
               Status = ev.Status,
            };
            return ev1;
        }

        // convert from DTO=>DAL
        public static MainEventToCustomer ConvertDalEvToDto(DAL.MainEventToCustomer ev)
        {
            return new DTO.MainEventToCustomer
            {
                Id = ev.Id,
                IdEvent = ev.IdEvent,
                IdCustomer = ev.IdCustomer,
                Status = ev.Status
            };
        }
    }
}
