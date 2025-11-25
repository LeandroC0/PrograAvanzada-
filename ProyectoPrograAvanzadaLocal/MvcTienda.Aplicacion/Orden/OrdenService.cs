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
                ID_Orden = o.ID_Orden,
                Fecha_Orden = o.Fecha_Orden,
                Total = o.Total,
                ID_Usuario = o.ID_Usuario,
                ID_Estado = o.ID_Estado
            });
        }

        public OrdenDto GetById(int id)
        {
            var o = _repo.GetById(id);
            if (o == null) return null;

            return new OrdenDto
            {
                ID_Orden = o.ID_Orden,
                Fecha_Orden = o.Fecha_Orden,
                Total = o.Total,
                ID_Usuario = o.ID_Usuario,
                ID_Estado = o.ID_Estado
            };
        }

        public void Create(OrdenDto dto)
        {
            var entity = new Orden
            {
                Fecha_Orden = dto.Fecha_Orden,
                Total = dto.Total,
                ID_Usuario = dto.ID_Usuario,
                ID_Estado = dto.ID_Estado
            };

            _repo.Add(entity);
            _repo.Save();
        }

        public void Update(OrdenDto dto)
        {
            var entity = _repo.GetById(dto.ID_Orden);
            if (entity == null) return;

            entity.Fecha_Orden = dto.Fecha_Orden;
            entity.Total = dto.Total;
            entity.ID_Usuario = dto.ID_Usuario;
            entity.ID_Estado = dto.ID_Estado;

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
