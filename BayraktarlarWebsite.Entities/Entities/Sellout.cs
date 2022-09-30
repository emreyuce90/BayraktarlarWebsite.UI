using BayraktarlarWebsite.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.Entities.Entities
{
    public class Sellout:IEntity
    {
        //TOLGA VARTA VARTA 300
        //SERTAN MOBIL CVL PREMIUM 300 Temmuz 2022 
        public int Id { get; set; }
        //Kullanıcı Sertan
        public int UserId { get; set; }
        public User User { get; set; }

        //Marka Mobil

        //Altmarka PVL
        public int AltMarkaId { get; set; }
        public AltMarka AltMarka { get; set; }
        //100LT
        public int Sell { get; set; }
        //Satış tarihi
        public DateTime SellDate { get; set; }

    }
}
