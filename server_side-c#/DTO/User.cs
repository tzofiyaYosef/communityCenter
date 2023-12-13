using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace DTO
{
   public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public double MyAge { get; set; }

        public User(string userName, string password, double myAge)
        {
            UserName = userName;
            Password = password;
            MyAge = myAge;
        }

        public User()
        {
        }

        // convert from DAL=>DTO
        public static UserDal ConvertUserToDal(User user)
        {
            return new UserDal()
            {
                Name = user.UserName,
                Pass = user.Password,
                Age = user.MyAge
            };
        }

        // convert from DTO=>DAL
        public static User ConvertDalUserToDto(UserDal user)
        {
            return new User()
            {
              UserName = user. Name,
              Password  = user.Pass,
              MyAge = user.Age
            };
        }
    }
}
