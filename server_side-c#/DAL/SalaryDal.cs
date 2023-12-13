//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DAL
//{
//  public  class SalaryDal
//    {
//        public int Hour { get; set; }
//        public double Bonus { get; set; }
//        public double Price { get; set; }

//        //בנאי מלא
//        public SalaryDal(int hour, int bonus, double price)
//        {
//            Hour = hour;
//            Bonus = bonus;
//            Price = price;
//        }

//        //בנאי ריק
//        public SalaryDal()
//        {
//            Hour = 4 * (8 * 5);
//            Bonus = (150.0 * Price) / 100.0;
//            Price = 29.12;
//        }

//        public override string ToString()
//        {
//            return "the price to hour: " + Price + " the bonus to extra hour: " + Bonus;
//        }
//    }
//}
