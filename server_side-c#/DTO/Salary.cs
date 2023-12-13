using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Salary
    {
        public double Hour { get; set; }
        public double Bonus { get; set; }
        public double Price { get; set; }

        //בנאי מלא
        public Salary(int hour, int bonus, double price)
        {
            Hour = hour;
            Bonus = bonus;
            Price = price;
        }

        public Salary(Salary other)
        {
            this.Hour = other.Hour;
            this.Price = other.Price;
            this.Bonus = other.Bonus;
        }

        //בנאי ריק
        public Salary()
        {
            Hour = 4 * (8 * 5);
            Bonus = (150.0 * Price) / 100.0;
            Price = 29.12;
        }

        public override string ToString()
        {
            return "the price to hour: " + Price + " the bonus to extra hour: " + Bonus;
        }


        // convert from DAL=>DTO
        public static DAL.Salary ConvertSalaryToDAL(Salary salary)
        {
            return new DAL.Salary()
            {
                Hour = salary.Hour,
                Price = salary.Price,
                Bonus = salary.Bonus,
            };
        }

        // convert from DTO=>DAL
        public static Salary ConvertDalSalaryToDto(DAL.Salary salary)
        {
            return new DTO.Salary
            {
                Hour = (double)salary.Hour,
                Price = (double)salary.Price,
                Bonus = (double)salary.Bonus,
            };
        }
    }
}


