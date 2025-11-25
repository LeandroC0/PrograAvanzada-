using MvcTienda.Domain.Entities;
using MvcTienda.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace MvcTienda.Aplicacion.Ordenes
{
    public class OrdenService : IOrdenService
    {
        private readonly IOrdenRepository _repo;

        public OrdenService(IOrdenRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<OrdenDto> GetAll()
        {
            return _repo.GetAll().Select(o => new OrdenDto
            {
                OrdenId = o.OrdenId,
                Fecha_Orden = o.Fecha_Orden,
                Total = o.Total,
                UsuarioId = o.UsuarioId,
                EstadoId = o.EstadoId
            });
        }

        public OrdenDto GetById(int id)
        {
            var o = _repo.GetById(id);
            if (o == null) return null;

            return new OrdenDto
            {
                OrdenId = o.OrdenId,
                Fecha_Orden = o.Fecha_Orden,
                Total = o.Total,
                UsuarioId = o.UsuarioId,
                EstadoId = o.EstadoId
            };
        }

        public void Create(OrdenDto dto)
        {
            var entity = new Orden
            {
                Fecha_Orden = dto.Fecha_Orden,
                Total = dto.Total,
                UsuarioId = dto.UsuarioId,
                EstadoId = dto.EstadoId
            };

            _repo.Add(entity);
            _repo.Save();
        }

        public void Update(OrdenDto dto)
        {
            var entity = _repo.GetById(dto.OrdenId);
            if (entity == null) return;

            entity.Fecha_Orden = dto.Fecha_Orden;
            entity.Total = dto.Total;
            entity.UsuarioId = dto.UsuarioId;
            entity.EstadoId = dto.EstadoId;

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
