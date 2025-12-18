using Aplicacion.Interfaces.Repository;
using Data;
using Dominio;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class TutorRepository : ICreateRepository<TutorEntity>, IReadRepository<TutorEntity>, 
        IUpdateRepository<TutorEntity>, ISearchRepository<TutorEntity>
    {
        private readonly ColegioDbContext _context;
        public TutorRepository(ColegioDbContext context)
        {
            _context = context;
        }
        public async Task Create(TutorEntity entity)
        {
            if(entity == null)
                throw new ArgumentNullException(nameof(entity), "El tutor no puede ser nulo.");

            var tutorModel = new Tutor
            {
                Nombre = entity.Nombre,
                Apellido = entity.Apellido,
                Cedula = entity.Cedula,
                Telefono = entity.Telefono,
                Estudiantes = entity.Estudiantes?.Select(e => new Estudiante
                {
                    Nombre = e.Nombre,
                    Apellido = e.Apellido,
                    Matricula = e.Matricula
                }).ToList() ?? new List<Estudiante>()
            };
           await _context.Tutores.AddAsync(tutorModel);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TutorEntity>> GetAllAsync()
        {
            var tutores = await _context.Tutores.Include(t => t.Estudiantes).AsNoTracking().ToListAsync();

            return tutores.Select(t => MapToEntity(t));
        }

        public async Task<TutorEntity> GetByIdAsync(int id)
        {
            if(id <= 0)
                throw new ArgumentException("El ID debe ser un número positivo.", nameof(id));

            var tutor = await _context.Tutores.Include(t => t.Estudiantes).FirstOrDefaultAsync(t => t.Id == id);

            if(tutor == null)
                throw new KeyNotFoundException($"No se encontró un tutor con ID {id}.");

            return MapToEntity(tutor);
        }

        public async Task Update(TutorEntity entity)
        {
            var tutorModel = await _context.Tutores.Include(t => t.Estudiantes).FirstOrDefaultAsync(t => t.Id == entity.Id);

            if (tutorModel == null)
                throw new KeyNotFoundException($"No se encontró un tutor con ID {entity.Id}.");

            tutorModel.Nombre = entity.Nombre;
            tutorModel.Apellido = entity.Apellido;
            tutorModel.Cedula = entity.Cedula;
            tutorModel.Telefono = entity.Telefono;

            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<TutorEntity>> SearchAsync(string searchTerm)
        {
            IQueryable<Tutor> query = _context.Tutores;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query
                    .OrderBy(t => t.Nombre);
            }
            else
            {
                searchTerm = searchTerm.Trim();

                query = query.Where(t =>
                    t.Nombre.Contains(searchTerm) ||
                    (t.Apellido != null && t.Apellido.Contains(searchTerm)) ||
                    (t.Cedula != null && t.Cedula.Contains(searchTerm)) ||
                    (t.Telefono != null && t.Telefono.Contains(searchTerm))
                );
            }

            var tutores = await query
                .Take(10)
                .ToListAsync();

            return tutores.Select(t => MapToEntity(t));

        }

        #region
        private static TutorEntity MapToEntity(Tutor tutor)
        {
            var tutorEntity = new TutorEntity(
                tutor.Id,
                tutor.Nombre,
                tutor.Apellido,
                tutor.Cedula,
                tutor.Telefono
            );
            if (tutor.Estudiantes != null)
            {
                foreach (var estudiante in tutor.Estudiantes)
                {
                    var estudianteEntity = new EstudianteEntity(
                        estudiante.Id,
                        estudiante.Nombre,
                        estudiante.Apellido,
                        estudiante.Matricula
                    );
                    tutorEntity.AgregarEstudiante(estudianteEntity);
                }
            }
            return tutorEntity;
        }

        #endregion
    }
}
