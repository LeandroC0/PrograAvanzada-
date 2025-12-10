using Microsoft.AspNet.Identity;
using MvcTienda.Aplicacion.Carrito;
using MvcTienda.Domain.Entities;
using MvcTienda.Domain.Repositories;
using MvcTienda.Infrastructura.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTienda.Aplicacion.Ordenes
{
    public class OrdenService : IOrdenService
    {
        private readonly IOrdenRepository _ordenRepo;
        private readonly IDetalleOrdenRepository _detalleRepo;
        private readonly IProductoRepository _productoRepo;
        private readonly UserManager<ApplicationUser, int> _userManager;

        public OrdenService(
            IOrdenRepository ordenRepo,
            IDetalleOrdenRepository detalleRepo,
            IProductoRepository productoRepo,
            UserManager<ApplicationUser, int> userManager)
        {
            _ordenRepo = ordenRepo;
            _detalleRepo = detalleRepo;
            _productoRepo = productoRepo;
            _userManager = userManager;
        }

        public IEnumerable<OrdenDto> GetAll()
        {
            return _ordenRepo.GetAll().Select(o => new OrdenDto
            {
                OrdenId = o.OrdenId,
                Fecha_Orden = o.Fecha_Orden,
                Total = o.Total,
                UsuarioId = o.UsuarioId,
                UsuarioNombre = _userManager.FindById(o.UsuarioId)?.UserName ?? "Desconocido",
                EstadoId = o.EstadoId,
                EstadoNombre = o.Estado != null ? o.Estado.Nombre : "Sin Estado"
            });
        }

        public OrdenDto GetById(int id)
        {
            var o = _ordenRepo.GetById(id);
            if (o == null) return null;

            return new OrdenDto
            {
                OrdenId = o.OrdenId,
                Fecha_Orden = o.Fecha_Orden,
                Total = o.Total,
                UsuarioId = o.UsuarioId,
                UsuarioNombre = _userManager.FindById(o.UsuarioId)?.UserName,
                EstadoId = o.EstadoId,
                EstadoNombre = o.Estado?.Nombre

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

            _ordenRepo.Add(entity);
            _ordenRepo.Save();
        }

        public void Update(OrdenDto dto)
        {
            var entity = _ordenRepo.GetById(dto.OrdenId);
            if (entity == null) return;

            entity.Fecha_Orden = dto.Fecha_Orden;
            entity.Total = dto.Total;
            entity.UsuarioId = dto.UsuarioId;
            entity.EstadoId = dto.EstadoId;

            _ordenRepo.Update(entity);
            _ordenRepo.Save();
        }

        public void Delete(int id)
        {
            _ordenRepo.Delete(id);
            _ordenRepo.Save();
        }

        public async Task<int> CrearOrdenDesdeCarritoAsync(int usuarioId, IList<ItemCarritoDto> items)
        {
            if (items == null || !items.Any())
                throw new InvalidOperationException("El carrito está vacío.");

            // Validar usuario
            var user = await _userManager.FindByIdAsync(usuarioId);
            if (user == null)
                throw new Exception("Usuario no válido. Debe iniciar sesión.");

            // Obtener productos
            var idsProductos = items.Select(i => i.ProductoId).ToList();
            var productos = _productoRepo.GetAll().Where(p => idsProductos.Contains(p.ProductoId)).ToList();

            foreach (var item in items)
            {
                var prod = productos.FirstOrDefault(p => p.ProductoId == item.ProductoId);

                if (prod == null)
                    throw new Exception($"El producto {item.ProductoId} no existe.");

                if (prod.Inventario < item.Cantidad)
                    throw new Exception($"No hay inventario suficiente para {prod.Nombre}.");
            }

            var orden = new Orden
            {
                Fecha_Orden = DateTime.Now,
                Total = items.Sum(i => i.Subtotal),
                UsuarioId = usuarioId,
                EstadoId = 1,
                Detalles = new List<DetalleOrden>()
            };

            _ordenRepo.Add(orden);

            foreach (var item in items)
            {
                var prod = productos.First(p => p.ProductoId == item.ProductoId);

                orden.Detalles.Add(new DetalleOrden
                {
                    ProductoId = prod.ProductoId,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = prod.Precio,
                    EstadoId = 1
                });

                prod.Inventario -= item.Cantidad;
                _productoRepo.Update(prod);
            }

            _ordenRepo.Save();
            _productoRepo.Save();

            return orden.OrdenId;
        }


    }
}
