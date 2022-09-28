using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.DAL.Interfaces
{
    public interface IUnitOfWork: IAsyncDisposable
    {
        ICiroRepository Ciro { get; }
        IUrgencyRepository Urgencies { get; }
        IAttachmentRepository Attachments { get; }
        ITicketRepository Tickets { get; }
        INotificationRepository Notifications { get; }
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
