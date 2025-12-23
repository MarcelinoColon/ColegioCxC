using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class ConceptoEntity
    {
        public int? Id { get; private set; }
        public string Nombre { get; private set; }
        public decimal Monto { get; private set; }
        public bool EsMora { get; private set; }

        public ConceptoEntity(string nombre,decimal monto, bool esMora)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentNullException("El nombre no puede estar vacio");
            if (monto < 0)
                throw new ArgumentException("El monto no puede ser negativo.", nameof(monto));


            Nombre = nombre;
            Monto = monto;
            EsMora = esMora;
        }

        public ConceptoEntity(int id, string nombre,decimal monto, bool esMora)
        {
            if(id<= 0)
                throw new ArgumentOutOfRangeException("El id no puede ser menor o igual a 0");
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentNullException("El nombre no puede estar vacio");
            if (monto < 0)
                throw new ArgumentException("El monto no puede ser negativo.", nameof(monto));

            Id = id;
            Nombre = nombre;
            Monto = monto;
            EsMora = esMora;
        }
    }
}
