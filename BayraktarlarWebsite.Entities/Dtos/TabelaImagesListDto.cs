using BayraktarlarWebsite.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.Entities.Dtos
{
    public class TabelaImagesListDto
    {
        public IList<TabelaImages> TabelaImages { get; set; }

        public static implicit operator TabelaImagesListDto(List<TabelaImages> v)
        {
            throw new NotImplementedException();
        }
    }
}
