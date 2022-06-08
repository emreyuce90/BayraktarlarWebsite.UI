using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.Shared.Interface
{
    public interface IEntityRepository<T> where T:class,IEntity,new()
    {
        //repository.GetAsync(I=>I.Name == "XXX",I=>I.Comment)
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        //repository.GetAllAsync();
        //repository.GetAllAsync(I=>I.CategoryName == C#,I=>I.Comments)
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        //Frontend tarafında Ajax işlemlerinden sonra kullanıcıya toast mesajı göndermek için entityyi return ederiz
        Task AddAsync(T t);
        //Frontend tarafında Ajax işlemlerinden sonra kullanıcıya toast mesajı göndermek için entityyi return ederiz
        Task UpdateAsync(T t);
        Task DeleteAsync(T t);
        //Geriye liste döner,liste olarak predice alır ve include alır
        Task<List<T>> SearchAsync(IList<Expression<Func<T, bool>>> predicates, params Expression<Func<T, object>>[] includeProperties);

        //_userRepository.Any(I=>I.Name =="Emre");
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
    }
}
