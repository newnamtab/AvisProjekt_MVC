using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCAvis.Models
{
    public class Orders
    {
        public string reservationsnummer { get; set; }
        public string carCategory { get; set; }
        public string destination { get; set; }
        public string city { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public int phonenumber { get; set; }
        public double totalPrize { get; set; } = 0;
        private double dailyPrize { get; set; } = 0;
    }
}
