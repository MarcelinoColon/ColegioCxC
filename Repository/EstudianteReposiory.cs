using Aplicacion.Interfaces.Repository;
using Data;
using Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class EstudianteReposiory : IReadRepository<EstudianteEntity>, ICreateRepository<EstudianteEntity>, IUpdateRepository<EstudianteEntity>
    {
        private readonly ColegioDbContext _context;

        public EstudianteReposiory(ColegioDbContext context)
        {
            _context = context;
        }

        public async Task Create(EstudianteEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var estudiante = MapToModel(entity);

            await _context.Estudiantes.AddAsync(estudiante);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EstudianteEntity>> GetAllAsync()
        {
            var estudiantes = await _context.Estudiantes.Include(e => e.Tutores)
                .AsNoTracking()
                .ToListAsync();

            return estudiantes.Select(e =>MapToEntity(e));
        }

        public async Task<EstudianteEntity> GetByIdAsync(int id)
        {
            if(id<=0)
                throw new ArgumentException(nameof(id));

            var estudiante = await _context.Estudiantes.Include(e => e.Tutores)
                .FirstOrDefaultAsync(e => e.Id == id);

            if(estudiante == null)
                throw new KeyNotFoundException($"Estudiante con id: {id} no existe.");

            return MapToEntity(estudiante);
        }

        public async Task Update(EstudianteEntity entity)
        {
            if(entity == null)
                throw new ArgumentNullException(nameof(entity));

            var estudiante = await _context.Estudiantes.Include(e => e.Tutores)
                .FirstOrDefaultAsync(e => e.Id == entity.Id);

            estudiante.Nombre = entity.Nombre;
            estudiante.Apellido = entity.Apellido;
            estudiante.Matricula = entity.Matricula;

            //Actualizar tutores
        }

        #region Mappers

        public static EstudianteEntity MapToEntity(Estudiante estudiante)
        {
            var estudianteEntity = new EstudianteEntity(
                estudiante.Id,
                estudiante.Nombre,
                estudiante.Apellido,
                estudiante.Matricula
            );

            if (estudiante.Tutores != null)
            {
                foreach (var tutor in estudiante.Tutores)
                {
                    estudianteEntity.AgregarTutor(new TutorEntity(tutor.Id, tutor.Nombre, tutor.Apellido,
                        tutor.Cedula, tutor.Telefono));
                }
            }

            return estudianteEntity;
        }

        public static Estudiante MapToModel(EstudianteEntity estudianteEntity)
        {
            var estudiante = new Estudiante
            {
                Id = estudianteEntity.Id,
                Nombre = estudianteEntity.Nombre,
                Apellido = estudianteEntity.Apellido,
                Matricula = estudianteEntity.Matricula
            };

            foreach (var tutorEntity in estudianteEntity.Tutores)
            {
                estudiante.Tutores.Add(new Tutor
                {
                    Id = (int)tutorEntity.Id,
                    Nombre = tutorEntity.Nombre,
                    Apellido = tutorEntity.Apellido,
                    Cedula = tutorEntity.Cedula,
                    Telefono = tutorEntity.Telefono
                });
            }

            return estudiante;
        }

        #endregion
    }
}
