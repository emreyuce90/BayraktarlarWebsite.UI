using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.DAL.Interfaces
{
    public interface IUnitOfWork: IAsyncDisposable
    {
        ILetRepository Lets { get; }
        IStatusRepository Status { get; }
        IMaterialRepository Materials { get; }
        IBrandRepository Brands { get; }
        ICustomerRepository Customer { get; }
        ITabelaImagesRepository TabelaImages { get; }
        ITabelaRepository Tabelas { get; }
        Task<int> SaveAsync();
    }
}
