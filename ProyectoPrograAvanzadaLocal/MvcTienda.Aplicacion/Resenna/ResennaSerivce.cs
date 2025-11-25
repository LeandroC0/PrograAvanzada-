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
                ResennaId = r.ResennaId,
                Comentario = r.Comentario,
                Calificacion = r.Calificación,
                Fecha_Resenna = r.Fecha_Reseña,
                ProductoId = r.ProductoId,
                EstadoId = r.EstadoId,
                UsuarioId = r.UsuarioId
            });
        }

        public ResennaDto GetById(int id)
        {
            var r = _repo.GetById(id);
            if (r == null) return null;

            return new ResennaDto
            {
                ResennaId = r.ResennaId,
                Comentario = r.Comentario,
                Calificacion = r.Calificación,
                Fecha_Resenna = r.Fecha_Reseña,
                ProductoId = r.ProductoId,
                EstadoId = r.EstadoId,
                UsuarioId = r.UsuarioId
            };
        }

        public void Create(ResennaDto dto)
        {
            var entity = new Resenna
            {
                Comentario = dto.Comentario,
                Calificación = dto.Calificacion,
                Fecha_Reseña = dto.Fecha_Resenna,
                ProductoId = dto.ProductoId,
                EstadoId = dto.EstadoId,
                UsuarioId = dto.UsuarioId
            };

            _repo.Add(entity);
            _repo.Save();
        }

        public void Update(ResennaDto dto)
        {
            var entity = _repo.GetById(dto.ResennaId);
            if (entity == null) return;

            entity.Comentario = dto.Comentario;
            entity.Calificación = dto.Calificacion;
            entity.Fecha_Reseña = dto.Fecha_Resenna;
            entity.ProductoId = dto.ProductoId;
            entity.EstadoId = dto.EstadoId;
            entity.UsuarioId = dto.UsuarioId;

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
