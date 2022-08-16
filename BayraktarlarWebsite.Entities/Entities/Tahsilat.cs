using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.Entities.Entities
{
    public class Tahsilat
    {
        
        public int Id { get; set; }
        //Tahsilatı yapan kullanıcı
        public int UserId { get; set; }
        public User User { get; set; }
        //Tahsilat yapılma tarihi
        public DateTime Date { get; set; }
        //Tahsilat tutarı
        public float Amount { get; set; }

    }
}
