using BayraktarlarWebsite.DAL.Interfaces;
using BayraktarlarWebsite.Entities.Entities;
using BayraktarlarWebsite.Shared.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BayraktarlarWebsite.DAL.Concrete.EntityFrameworkCore.Repositories
{
    public class EfHunterRepository : EfEntityRepositoryBase<Hunter>,IHunterRepository
    {
        public EfHunterRepository(DbContext context) : base(context)
        {
        }
    }
}
