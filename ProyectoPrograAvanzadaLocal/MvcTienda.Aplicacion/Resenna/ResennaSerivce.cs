using MvcTienda.Domain.Entities;
using MvcTienda.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace MvcTienda.Aplicacion.Resennas
{
    public class ResennaService : IResennaService
    {
        private readonly IResennaRepository _repo;

        public ResennaService(IResennaRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<ResennaDto> GetAll()
        {
            return _repo.GetAll().Select(r => new ResennaDto
            {
                ID_Resenna = r.ID_Reseña,
                Comentario = r.Comentario,
                Calificacion = r.Calificación,
                Fecha_Resenna = r.Fecha_Reseña,
                ID_Producto = r.ID_Producto,
                ID_Estado = r.ID_Estado,
                ID_Usuario = r.ID_Usuario
            });
        }

        public ResennaDto GetById(int id)
        {
            var r = _repo.GetById(id);
            if (r == null) return null;

            return new ResennaDto
            {
                ID_Resenna = r.ID_Reseña,
                Comentario = r.Comentario,
                Calificacion = r.Calificación,
                Fecha_Resenna = r.Fecha_Reseña,
                ID_Producto = r.ID_Producto,
                ID_Estado = r.ID_Estado,
                ID_Usuario = r.ID_Usuario
            };
        }

        public void Create(ResennaDto dto)
        {
            var entity = new Resenna
            {
                Comentario = dto.Comentario,
                Calificación = dto.Calificacion,
                Fecha_Reseña = dto.Fecha_Resenna,
                ID_Producto = dto.ID_Producto,
                ID_Estado = dto.ID_Estado,
                ID_Usuario = dto.ID_Usuario
            };

            _repo.Add(entity);
            _repo.Save();
        }

        public void Update(ResennaDto dto)
        {
            var entity = _repo.GetById(dto.ID_Resenna);
            if (entity == null) return;

            entity.Comentario = dto.Comentario;
            entity.Calificación = dto.Calificacion;
            entity.Fecha_Reseña = dto.Fecha_Resenna;
            entity.ID_Producto = dto.ID_Producto;
            entity.ID_Estado = dto.ID_Estado;
            entity.ID_Usuario = dto.ID_Usuario;

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
