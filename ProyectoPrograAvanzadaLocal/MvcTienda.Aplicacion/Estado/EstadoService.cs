using MvcTienda.Domain.Entities;
using MvcTienda.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace MvcTienda.Aplicacion.Estados
{
    public class EstadoService : IEstadoService
    {
        private readonly IEstadoRepository _repo;

        public EstadoService(IEstadoRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<EstadoDto> GetAll()
        {
            return _repo.GetAll().Select(e => new EstadoDto
            {
                EstadoId = e.EstadoId,
                Nombre = e.Nombre
            });
        }

        public EstadoDto GetById(int id)
        {
            var e = _repo.GetById(id);
            if (e == null) return null;

            return new EstadoDto
            {
                EstadoId = e.EstadoId,
                Nombre = e.Nombre
            };
        }

        public void Create(EstadoDto dto)
        {
            var entity = new Estado
            {
                Nombre = dto.Nombre
            };

            _repo.Add(entity);
            _repo.Save();
        }

        public void Update(EstadoDto dto)
        {
            var entity = _repo.GetById(dto.EstadoId);
            if (entity == null) return;

            entity.Nombre = dto.Nombre;

            _repo.Update(entity);
            _repo.Save();
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
            _repo.Save();
        }
    }
}
