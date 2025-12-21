using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class EstudianteEntity
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public string Apellido { get; private set; }
        public string Matricula { get; private set; }
        public List<TutorEntity> Tutores { get; private set; }

        public EstudianteEntity(int id, string nombre, string apellido, string matricula)
        {
            if (id <= 0)
                throw new ArgumentException("El Id debe ser un entero positivo.", nameof(id));
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombre));
            if (string.IsNullOrWhiteSpace(apellido))
                throw new ArgumentException("El apellido no puede estar vacío.", nameof(apellido));
            if (string.IsNullOrWhiteSpace(matricula))
                throw new ArgumentException("La matrícula no puede estar vacía.", nameof(matricula));
            if (matricula.Length > 9)
                throw new ArgumentException("La matrícula no puede exceder los 8 caracteres.", nameof(matricula));

            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Matricula = matricula;
            Tutores = new();
        }
        public EstudianteEntity(string nombre, string apellido, string matricula)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombre));
            if (string.IsNullOrWhiteSpace(apellido))
                throw new ArgumentException("El apellido no puede estar vacío.", nameof(apellido));
            if (string.IsNullOrWhiteSpace(matricula))
                throw new ArgumentException("La matrícula no puede estar vacía.", nameof(matricula));
            if (matricula.Length > 9)
                throw new ArgumentException("La matrícula no puede exceder los 8 caracteres.", nameof(matricula));

            Nombre = nombre;
            Apellido = apellido;
            Matricula = matricula;
            Tutores = new();
        }

        public void AgregarTutor(TutorEntity tutor)
        {
            if (tutor == null)
                throw new ArgumentNullException(nameof(tutor), "El tutor no puede ser nulo.");
            Tutores.Add(tutor);
        }
        public void QuitarTutor(int tutorId)
        {
            var tutor = Tutores.FirstOrDefault(t => t.Id == tutorId);
            if (tutor == null)
                throw new ArgumentNullException(nameof(tutor), "El tutor no puede ser nulo.");
            Tutores.Remove(tutor);
        }
        public void Validar()
        {
            if (!Tutores.Any())
                throw new InvalidOperationException("El estudiante debe tener al menos un tutor asignado.");
        }

    }
}
