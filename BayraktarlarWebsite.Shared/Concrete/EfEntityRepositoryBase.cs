using BayraktarlarWebsite.Shared.Interface;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.Shared.Concrete
{
    public class EfEntityRepositoryBase<Tentity> : IEntityRepository<Tentity> where Tentity : class, IEntity, new()
    {
        //Ef entitybase den türeyen tüm sınıfların erişebilmesi için protected olarak işaretledik   
        protected readonly DbContext _context;
        public EfEntityRepositoryBase(DbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Tentity t)
        {
            await _context.Set<Tentity>().AddAsync(t);
            
        }

        public async Task<bool> AnyAsync(Expression<Func<Tentity, bool>> predicate)
        {
            return await _context.Set<Tentity>().AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<Tentity, bool>> predicate = null)
        {
            return (predicate == null ? await _context.Set<Tentity>().CountAsync() : await _context.Set<Tentity>().CountAsync(predicate));
        }

        public async Task DeleteAsync(Tentity t)
        {
            await Task.Run(() => { _context.Remove(t); });
        }

        public async Task<List<Tentity>> GetAllAsync(Expression<Func<Tentity, bool>> predicate = null, params Expression<Func<Tentity, object>>[] includeProperties)
        {
            IQueryable<Tentity> queryable = _context.Set<Tentity>();
            if (predicate != null)
            {
                queryable = queryable.Where(predicate);
            }

            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    queryable = queryable.Include(includeProperty);
                }
            }

            return await queryable.ToListAsync();
        }

        public async Task<Tentity> GetAsync(Expression<Func<Tentity, bool>> predicate, params Expression<Func<Tentity, object>>[] includeProperties)
        {
            IQueryable<Tentity> queryable = _context.Set<Tentity>();
            if (predicate != null)
            {
                queryable = queryable.Where(predicate);
            }
            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    queryable = queryable.Include(includeProperty);
                }
            }

            return await queryable.SingleOrDefaultAsync();
        }

        public async Task<List<Tentity>> SearchAsync(IList<Expression<Func<Tentity, bool>>> predicates, params Expression<Func<Tentity, object>>[] includeProperties)
        {
            IQueryable<Tentity> query = _context.Set<Tentity>();
            if (predicates.Any())
            {
                //bir predicate zinciri oluşturalım
                var predicateChain = PredicateBuilder.New<Tentity>();

                //query1||query2||query3
                foreach (var p in predicates)
                {
                    predicateChain.Or(p);
                }

                query = query.Where(predicateChain);

            }

            if (includeProperties.Any())
            {
                foreach (var i in includeProperties)
                {
                    query = query.Include(i);
                }
            }

            return await query.ToListAsync();
        }

        public async Task UpdateAsync(Tentity t)
        {
            await Task.Run(() => { _context.Set<Tentity>().Update(t); });

        }
    }
}
