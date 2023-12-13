using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using DTO;
using BLL;
using Newtonsoft.Json;

namespace API.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("API/manager")]
    public class ManagerController:ApiController
    {
        [HttpPost]
        [Route("AddManager")]
        public bool AddManager()
        {
            DTO.Maneger manager =
                JsonConvert.DeserializeObject<DTO.Maneger>
                (HttpContext.Current.Request["manager"]);
            return ManagerBl.AddManager(manager);
        }

        [HttpPost]
        [Route("updatingManager")]
        public bool updatingManager()
        {
            DTO.Maneger manager =
                JsonConvert.DeserializeObject<DTO.Maneger>
                (HttpContext.Current.Request["manager"]);
            return ManagerBl.updatingManager(manager);
        }

        [HttpGet]
        [Route("deletManager/{gmail}/{x}")]
        public bool deletManager(string gmail, int x)
        {
            //מחיקת חשבון של משתמש
            return ManagerBl.deletManager(gmail);
        }

        //החזרת מנהל לפי המייל 
        [Route("GetManager/{gmail}/{x}")]
        public DTO.Maneger GetManager(string gmail, int x)
        {
            return ManagerBl.GetManager(gmail);
        }
    } 
}