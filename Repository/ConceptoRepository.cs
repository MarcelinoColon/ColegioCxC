using Aplicacion.Interfaces.Repository;
using Data;
using Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class ConceptoRepository : IReadRepository<ConceptoEntity>, ICreateRepository<ConceptoEntity>
    {
        private readonly ColegioDbContext _context;
        public ConceptoRepository(ColegioDbContext context)
        {
            _context = context;
        }

        public async Task Create(ConceptoEntity entity)
        {
            if(entity == null)
                throw new ArgumentNullException(nameof(entity));

            var concepto = new Concepto
            {
                Nombre = entity.Nombre,
                EsMora = entity.EsMora
            };

            await _context.Conceptos.AddAsync(concepto);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ConceptoEntity>> GetAllAsync()
        {
            var conceptos = await _context.Conceptos.AsNoTracking().ToListAsync();

            var conceptosEntities = conceptos.Select(c => new ConceptoEntity(c.Id, c.Nombre, c.EsMora));

            return conceptosEntities;
        }

        public async Task<ConceptoEntity> GetByIdAsync(int id)
        {
            var concepto = await _context.Conceptos.FindAsync(id);

            if (concepto == null)
                throw new ArgumentNullException(nameof(concepto));

            return new ConceptoEntity(concepto.Id, concepto.Nombre, concepto.EsMora);
        }
    }
}
