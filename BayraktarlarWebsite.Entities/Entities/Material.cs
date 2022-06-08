using BayraktarlarWebsite.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.Entities.Entities
{
    public class Material : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }

    }
}
