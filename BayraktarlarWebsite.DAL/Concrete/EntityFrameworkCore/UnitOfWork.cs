using BayraktarlarWebsite.DAL.Concrete.EntityFrameworkCore.Repositories;
using BayraktarlarWebsite.DAL.Context;
using BayraktarlarWebsite.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.DAL.Concrete.EntityFrameworkCore
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EfUrgencyRepository _urgencyRepository;
        private readonly EfAttachmentRepository _attachmentRepository;
        private readonly EfTicketRepositoryBase _ticketRepository;
        private readonly EfNotificationRepository _notificationRepository;
        private readonly EfStatusRepository _efStatusRepository;
        private readonly EfMaterialRepository _efMaterialRepository;
        private readonly DatabaseConnection _context;
        private readonly EfTabelaRepository _tabelaRepository;
        private readonly EfTabelaImagesRepository _tabelaImagesRepository;
        private readonly EfCustomerRepository _customerRepository;
        private readonly EfBrandRepository _brandRepository;
        private readonly EfLetRepository _efLetRepository;
        public UnitOfWork(DatabaseConnection context)
        {
            _context = context;
            
        }

        public IBrandRepository Brands => _brandRepository ?? new EfBrandRepository(_context);
        public ITabelaRepository Tabelas => _tabelaRepository ?? new EfTabelaRepository(_context);
        public ITabelaImagesRepository TabelaImages => _tabelaImagesRepository ?? new EfTabelaImagesRepository(_context);

        public ICustomerRepository Customer => _customerRepository ?? new EfCustomerRepository(_context);

        public IMaterialRepository Materials => _efMaterialRepository ?? new EfMaterialRepository(_context);

        public IStatusRepository Status => _efStatusRepository ?? new EfStatusRepository(_context);

        public ILetRepository Lets => _efLetRepository ?? new EfLetRepository(_context);

        public INotificationRepository Notifications => _notificationRepository ?? new EfNotificationRepository(_context);

        public ITicketRepository Tickets => _ticketRepository ?? new EfTicketRepositoryBase(_context);

        public IAttachmentRepository Attachments => _attachmentRepository ?? new EfAttachmentRepository(_context);

        public IUrgencyRepository Urgencies => _urgencyRepository ?? new EfUrgencyRepository(_context);

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
