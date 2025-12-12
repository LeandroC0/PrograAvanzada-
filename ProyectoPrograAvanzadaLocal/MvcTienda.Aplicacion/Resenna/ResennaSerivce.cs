using Microsoft.AspNet.Identity;
using MvcTienda.Domain.Entities;
using MvcTienda.Domain.Repositories;
using MvcTienda.Infrastructura.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcTienda.Aplicacion.Resennas
{
    public class ResennaService : IResennaService
    {
        private readonly IResennaRepository _repo;
        private readonly UserManager<ApplicationUser, int> _userManager;

        public ResennaService(IResennaRepository repo,
            UserManager<ApplicationUser, int> userManager)
        {
            _repo = repo;
            _userManager = userManager;
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
                ProductoNombre = r.Producto?.Nombre,
                EstadoId = r.EstadoId,
                EstadoNombre = r.Estado?.Nombre,
                UsuarioId = r.UsuarioId,
                UsuarioNombre = _userManager.FindByIdAsync(r.UsuarioId).Result?.UserName ?? "Desconocido"
            });
        }
        public IEnumerable<ResennaDto> GetAllPendiente()
        {
            return _repo.GetAll()
                .Where(r => r.EstadoId != 4)
                .Select(r => new ResennaDto
                {
                    ResennaId = r.ResennaId,
                    Comentario = r.Comentario,
                    Calificacion = r.Calificación,
                    Fecha_Resenna = r.Fecha_Reseña,
                    ProductoId = r.ProductoId,
                    ProductoNombre = r.Producto?.Nombre,
                    EstadoId = r.EstadoId,
                    EstadoNombre = r.Estado?.Nombre,
                    UsuarioId = r.UsuarioId,
                    UsuarioNombre = _userManager.FindByIdAsync(r.UsuarioId).Result?.UserName ?? "Desconocido"
                });
        }

        public IEnumerable<ResennaDto> GetAllPublic()
        {
            return _repo.GetAll()
                .Where(r => r.EstadoId == 4)
                .Select(r => new ResennaDto
                {
                    ResennaId = r.ResennaId,
                    Comentario = r.Comentario,
                    Calificacion = r.Calificación,
                    Fecha_Resenna = r.Fecha_Reseña,
                    ProductoId = r.ProductoId,
                    ProductoNombre = r.Producto?.Nombre,
                    EstadoId = r.EstadoId,
                    EstadoNombre = r.Estado?.Nombre,
                    UsuarioId = r.UsuarioId,
                    UsuarioNombre = _userManager.FindByIdAsync(r.UsuarioId).Result?.UserName ?? "Desconocido"
                });
        }

        public IEnumerable<ResennaDto> GetAllByUsuarioId(int usuarioId)
        {
            return _repo.GetAll()
                .Where(r => r.UsuarioId == usuarioId)
                .Select(r => new ResennaDto
                {
                    ResennaId = r.ResennaId,
                    Comentario = r.Comentario,
                    Calificacion = r.Calificación,
                    Fecha_Resenna = r.Fecha_Reseña,
                    ProductoId = r.ProductoId,
                    ProductoNombre = r.Producto?.Nombre,
                    EstadoId = r.EstadoId,
                    EstadoNombre = r.Estado?.Nombre,
                    UsuarioId = r.UsuarioId,
                    UsuarioNombre = _userManager.FindByIdAsync(r.UsuarioId).Result?.UserName ?? "Desconocido"
                });
        }
        public IEnumerable<ResennaDto> GetAllByProductoId(int productoId)
        {
            return _repo.GetAll()
                .Where(r => r.ProductoId == productoId && r.EstadoId == 4)
                .Select(r => new ResennaDto
                {
                    ResennaId = r.ResennaId,
                    Comentario = r.Comentario,
                    Calificacion = r.Calificación,
                    Fecha_Resenna = r.Fecha_Reseña,
                    ProductoId = r.ProductoId,
                    ProductoNombre = r.Producto?.Nombre,
                    EstadoId = r.EstadoId,
                    EstadoNombre = r.Estado?.Nombre,
                    UsuarioId = r.UsuarioId,
                    UsuarioNombre = _userManager.FindByIdAsync(r.UsuarioId).Result?.UserName ?? "Desconocido"

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
                ProductoNombre = r.Producto?.Nombre,
                EstadoId = r.EstadoId,
                EstadoNombre = r.Estado?.Nombre,
                UsuarioId = r.UsuarioId,
                UsuarioNombre = _userManager.FindByIdAsync(r.UsuarioId).Result?.UserName ?? "Desconocido"
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

        public void CambiarEstado(int id, int nuevoEstadoId)
        {
            if (nuevoEstadoId != 4 && nuevoEstadoId != 5)
                throw new Exception("Estado Invalido");

            var entity = _repo.GetById(id);
            if (entity == null) return;
            entity.EstadoId = nuevoEstadoId;
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
