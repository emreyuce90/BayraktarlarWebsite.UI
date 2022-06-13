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
        private readonly DatabaseConnection _context;
        private readonly EfTabelaRepository _tabelaRepository;
        private readonly EfTabelaImagesRepository _tabelaImagesRepository;
        private readonly EfCustomerRepository _customerRepository;
        public UnitOfWork(DatabaseConnection context)
        {
            _context = context;
            
        }
      
        public ITabelaRepository Tabelas => _tabelaRepository ?? new EfTabelaRepository(_context);
        public ITabelaImagesRepository TabelaImages => _tabelaImagesRepository ?? new EfTabelaImagesRepository(_context);

        public ICustomerRepository Customer => _customerRepository ?? new EfCustomerRepository(_context);

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
