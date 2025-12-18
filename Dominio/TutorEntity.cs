using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class TutorEntity
    {
        public int? Id { get; private set; }
        public string Nombre { get; private set; }
        public string? Apellido { get; private set; }
        public string? Cedula { get; private set; }
        public string? Telefono { get; private set; }
        public List<EstudianteEntity> Estudiantes { get; private set; }

        public TutorEntity(int id, string nombre, string? apellido, string? cedula, string? telefono)
        {
            if (id <= 0)
                throw new ArgumentException("El Id debe ser un entero positivo.", nameof(id));
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombre));

            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Cedula = cedula;
            Telefono = telefono;
            Estudiantes = new();
        }

        public TutorEntity(string nombre, string? apellido, string? cedula, string? telefono)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombre));

            Nombre = nombre;
            Apellido = apellido;
            Cedula = cedula;
            Telefono = telefono;
            Estudiantes = new();
        }

        public void AgregarEstudiante(EstudianteEntity estudiante)
        {
            if (estudiante == null)
                throw new ArgumentNullException(nameof(estudiante), "El estudiante no puede ser nulo.");
            Estudiantes.Add(estudiante);
        }

        public void QuitarEstudiante(int estudianteId)
        {
            var estudiante = Estudiantes.FirstOrDefault(e => e.Id == estudianteId);
            if (estudiante == null)
                throw new ArgumentNullException(nameof(estudiante), "El estudiante no puede ser nulo.");
            Estudiantes.Remove(estudiante);
        }

    }
}
