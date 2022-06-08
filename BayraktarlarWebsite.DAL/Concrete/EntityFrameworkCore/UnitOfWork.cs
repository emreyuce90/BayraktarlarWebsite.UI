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
        public UnitOfWork(DatabaseConnection context, EfTabelaRepository tabelaRepository)
        {
            _context = context;
            
        }
      
        public ITabelaRepository Tabelas => _tabelaRepository ?? new EfTabelaRepository(_context);

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
