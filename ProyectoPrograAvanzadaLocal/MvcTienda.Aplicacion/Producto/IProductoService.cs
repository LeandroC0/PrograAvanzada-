using System.Collections.Generic;
using System.Threading.Tasks;

namespace MvcTienda.Aplicacion.Productos
{
    public interface IProductoService
    {
        IEnumerable<ProductoDto> GetAll();
        IEnumerable<ProductoDto> Search(string searchTerm, int? estadoId);
        ProductoDto GetById(int id);
        void Create(ProductoDto producto);
        void Update(ProductoDto producto);
        void ChangeStatus(int id, int estadoId);

        Task<ProductoDto> GetByIdAsync(int id);

    }
}