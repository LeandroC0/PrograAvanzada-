using MvcTienda.Aplicacion.Carrito;
using MvcTienda.Domain.Entities;
using MvcTienda.Domain.Repositories;
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

        public OrdenService(
            IOrdenRepository ordenRepo,
            IDetalleOrdenRepository detalleRepo,
            IProductoRepository productoRepo)
        {
            _ordenRepo = ordenRepo;
            _detalleRepo = detalleRepo;
            _productoRepo = productoRepo;
        }

        public IEnumerable<OrdenDto> GetAll()
        {
            return _ordenRepo.GetAll().Select(o => new OrdenDto
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
            var o = _ordenRepo.GetById(id);
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

        public Task<int> CrearOrdenDesdeCarritoAsync(int usuarioId, IList<ItemCarritoDto> items)
        {
            if (items == null || !items.Any())
                throw new InvalidOperationException("El carrito está vacío.");

            // Obtener productos que están en el carrito
            var idsProductos = items.Select(i => i.ProductoId).ToList();

            var productos = _productoRepo
                .GetAll()
                .Where(p => idsProductos.Contains(p.ProductoId))
                .ToList();

            // Validar inventario
            foreach (var item in items)
            {
                var prod = productos.FirstOrDefault(p => p.ProductoId == item.ProductoId);

                if (prod == null)
                    throw new Exception($"El producto {item.ProductoId} no existe.");

                if (prod.Inventario < item.Cantidad)
                    throw new Exception($"No hay inventario suficiente para el producto {prod.Nombre}.");
            }

            // Calcular total de la orden
            decimal total = items.Sum(i => i.Subtotal);

            var orden = new Orden
            {
                Fecha_Orden = DateTime.Now,
                Total = total,
                UsuarioId = usuarioId,
                EstadoId = 1 // Ej: 1 = 'Activa' o 'Procesada'
            };

            _ordenRepo.Add(orden);
            _ordenRepo.Save(); 

            // Crear detalles y actualizar inventario
            foreach (var item in items)
            {
                var prod = productos.First(p => p.ProductoId == item.ProductoId);

                var detalle = new DetalleOrden
                {
                    OrdenId = orden.OrdenId,
                    ProductoId = prod.ProductoId,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = prod.Precio,
                    EstadoId = 1 
                };

                _detalleRepo.Add(detalle);

                // Descontar inventario
                prod.Inventario -= item.Cantidad;
                _productoRepo.Update(prod);
            }

            // Guardar cambios de detalles e inventario
            _detalleRepo.Save();
            _productoRepo.Save();

 
            return Task.FromResult(orden.OrdenId);
        }
    }
}
