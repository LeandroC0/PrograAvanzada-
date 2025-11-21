using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcTienda.Aplicacion.Productos
{
    public interface IProductoService
    {
        IEnumerable<ProductoDto> GetAll();
        ProductoDto GetById(int id);
        void Create(ProductoDto producto);
        void Update(ProductoDto producto);
        void Delete(int id);
    }
}
