using BayraktarlarWebsite.DAL.Interfaces;
using BayraktarlarWebsite.Entities.Entities;
using BayraktarlarWebsite.Shared.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BayraktarlarWebsite.DAL.Concrete.EntityFrameworkCore.Repositories
{
    public class EfDistrictRepository : EfEntityRepositoryBase<District>,IDistrictRepository
    {
        public EfDistrictRepository(DbContext context) : base(context)
        {
        }
    }
}
