using BayraktarlarWebsite.Shared.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.Entities.Entities
{
    public class Urgency:IEntity
    {
        public int Id { get; set; }
        public string UrgencyName { get; set; }
    }
}
