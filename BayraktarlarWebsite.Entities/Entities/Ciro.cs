using BayraktarlarWebsite.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.Entities.Entities
{
    public class Ciro:IEntity
    {
        public int Id { get; set; }
        //Kullanıcı cirosu
        public int UserId { get; set; }
        public User User { get; set; }
        public float Amount { get; set; }
        public DateTime Date { get; set; }

    }
}
