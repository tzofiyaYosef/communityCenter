using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class ManagerBl
    {
        public static bool AddManager(DTO.Maneger maneger)
        {
            return ManegerDal.AddManager(DTO.Maneger.ConvertManagerToDAL(maneger));
        }

        public static bool updatingManager(DTO.Maneger maneger)
        {
            string [] id = maneger.Id.Split(' ');
            if (!id[0].Equals(id[1]))
                return false;
            string[] gmail = maneger.Gmail.Split(' ');
            maneger.Gmail = gmail[0];
            maneger.Id = id[1];
            if (deletManager(gmail[1]))
                return AddManager(maneger);
            return false;
        }

        public static bool deletManager(string gmail)
        {
            //מחיקת חשבון של משתמש
            return ManegerDal.deletManager(gmail);
        }

        public static DTO.Maneger GetManager(string gmail)
        {
            return DTO.Maneger.ConvertDalManagerToDto(ManegerDal.GetManager(gmail));
        }
    }
}
