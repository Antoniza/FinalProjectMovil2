using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Models
{
    class Sale
    {
        public String ClientName { get; set; }
        public String ClientPhone { get; set; }
        public String ClientMail { get; set; }
        public String ClientLatitude { get; set; }
        public String ClientLongitude { get; set; }
        public String Detail { get; set; }
        public String PayFormat { get; set; }
        public String TotalToPay { get; set; }
        public String ShoppingCar { get; set; }
    }
}
