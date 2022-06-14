using BayraktarlarWebsite.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.BLL.Interfaces
{
    public interface ITabelaImagesService
    {
        Task AddAsync(TabelaImageAddDto tabelaImageAddDto);
    }
}
