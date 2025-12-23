using Aplicacion.Estudiante.DTOs;
using Aplicacion.Interfaces.Repository;
using Data;
using Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class EstudianteReposiory : IReadRepository<EstudianteEntity>, ICreateRepository<EstudianteEntity>,
        IUpdateRepository<EstudianteEntity>, IRangeValidateRepository<EstudianteEntity>
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
            var exists = await _context.Estudiantes
                .AnyAsync(e => e.Matricula == entity.Matricula);

            if (exists)
                throw new InvalidOperationException($"Ya existe un estudiante con la matrícula: {entity.Matricula}");

            var estudiante = MapToModel(entity);

            await _context.Estudiantes.AddAsync(estudiante);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EstudianteEntity>> GetAllAsync()
        {
            var estudiantes = await _context.Estudiantes.Include(e => e.Tutores)
                .AsNoTracking()
                .ToListAsync();

            return estudiantes.Select(e => MapToEntity(e));
        }

        public async Task<EstudianteEntity> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException(nameof(id));

            var estudiante = await _context.Estudiantes.Include(e => e.Tutores)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (estudiante == null)
                throw new KeyNotFoundException($"Estudiante con id: {id} no existe.");

            return MapToEntity(estudiante);
        }

        public async Task Update(EstudianteEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var estudiante = await _context.Estudiantes.Include(e => e.Tutores)
                .FirstOrDefaultAsync(e => e.Id == entity.Id);

            if (estudiante == null)
                throw new ArgumentNullException(nameof(entity));

            estudiante.Nombre = entity.Nombre;
            estudiante.Apellido = entity.Apellido;
            estudiante.Matricula = entity.Matricula;

            var idsTutores = entity.Tutores.Select(t => t.Id).ToList();

            var tutoresDb = await _context.Tutores
                .Where(t => idsTutores.Contains(t.Id))
                .ToListAsync();

            estudiante.Tutores = tutoresDb;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> Validate(IEnumerable<int> ids)
        {
            return await _context.Estudiantes
                         .CountAsync(e => ids.Contains(e.Id)) == ids.Distinct().Count();
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
