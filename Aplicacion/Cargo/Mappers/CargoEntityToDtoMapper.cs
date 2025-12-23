using Aplicacion.Cargo.DTOs;
using Aplicacion.Concepto.DTOs;
using Aplicacion.Estudiante.DTOs;
using Aplicacion.Interfaces.Mapper;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Cargo.Mappers
{
    public class CargoEntityToDtoMapper : IMapper<CargoEntity, CargoDto>
    {
        public CargoDto Map(CargoEntity cargoEntity)
        {
            var CargoDto = new CargoDto
            {
                Id = cargoEntity.Id,
                TotalCargo = cargoEntity.TotalCargo,
                SaldoPendiente = cargoEntity.SaldoPendiente,
                TotalPagado = cargoEntity.TotalPagado,
                Estado = cargoEntity.Estado,
                FechaGeneracion = cargoEntity.FechaGeneracion,
                FechaVencimiento = cargoEntity.FechaVencimiento,
                CargoId = cargoEntity.Id,
                Estudiante = new EstudianteDto
                {
                    Id = cargoEntity.Estudiante.Id,
                    Nombre = cargoEntity.Estudiante.Nombre,
                    Apellido = cargoEntity.Estudiante.Apellido,
                    Matricula = cargoEntity.Estudiante.Matricula
                },
                Concepto = new ConceptoDto
                {
                    Id = cargoEntity.Concepto.Id,
                    Nombre = cargoEntity.Concepto.Nombre,
                    Monto = cargoEntity.Concepto.Monto,
                    EsMora = cargoEntity.Concepto.EsMora
                }
            };

            return CargoDto;
        }
    }
}
