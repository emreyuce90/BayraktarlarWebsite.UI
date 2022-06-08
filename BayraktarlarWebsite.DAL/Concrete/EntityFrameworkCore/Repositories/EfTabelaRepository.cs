using BayraktarlarWebsite.DAL.Interfaces;
using BayraktarlarWebsite.Entities.Entities;
using BayraktarlarWebsite.Shared.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.DAL.Concrete.EntityFrameworkCore.Repositories
{
    //Concrete tarafı da concrete den inherit ettik ve bu sınıfa özgü ekstra metot ihtiyacı için de yine bu concrete sınıfın interfaceini kalıttık
    public class EfTabelaRepository : EfEntityRepositoryBase<Tabela>, ITabelaRepository
    {
        public EfTabelaRepository(DbContext context) : base(context)
        {
        }
    }
}
