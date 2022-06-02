using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.Entities.Entities
{
    public class TabelaImages
    {
        public int Id { get; set; }
        public string PictureUrl { get; set; }
        public int TabelaId { get; set; }
        public Tabela Tabela { get; set; }

    }
}
