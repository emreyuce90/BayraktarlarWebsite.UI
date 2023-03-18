using BayraktarlarWebsite.DAL.Interfaces;
using BayraktarlarWebsite.Entities.Entities;
using BayraktarlarWebsite.Shared.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BayraktarlarWebsite.DAL.Concrete.EntityFrameworkCore.Repositories
{
    public class EfTownRepository : EfEntityRepositoryBase<Town>, ITownRepository
    {
        public EfTownRepository(DbContext context) : base(context)
        {
        }
    }
}
