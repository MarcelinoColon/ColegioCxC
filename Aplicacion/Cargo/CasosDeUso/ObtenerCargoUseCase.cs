using Aplicacion.Cargo.DTOs;
using Aplicacion.Interfaces.Mapper;
using Aplicacion.Interfaces.Repository;
using Aplicacion.Interfaces.UseCase;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Cargo.CasosDeUso
{
    public class ObtenerCargoUseCase : IReadUseCase<CargoDto, CargoEntity>
    {
        private readonly IReadRepository<CargoEntity> _repository;
        private readonly IMapper<CargoEntity, CargoDto> _mapper;
        public ObtenerCargoUseCase(IReadRepository<CargoEntity> repository, IMapper<CargoEntity, CargoDto> mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CargoDto>> GetAll()
        {
            var cargos = await _repository.GetAllAsync();

            return cargos.Select(c => _mapper.Map(c));
        }

        public async Task<CargoDto> GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El id debe ser un numero entero positivo");

            var cargo = await _repository.GetByIdAsync(id);

            if (cargo == null)
                throw new KeyNotFoundException("Cargo no existente");

            return _mapper.Map(cargo);
        }
    }
}
