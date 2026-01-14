using Aplicacion.Interfaces.Repository;
using Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ColegioDbContext _context;
        public UnitOfWork(ColegioDbContext context)
        {
            _context = context;
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
