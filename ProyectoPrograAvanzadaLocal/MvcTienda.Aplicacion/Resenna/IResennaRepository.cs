using System.Collections.Generic;

namespace MvcTienda.Aplicacion.Resennas
{
    public interface IResennaService
    {
        IEnumerable<ResennaDto> GetAll();
        ResennaDto GetById(int id);
        void Create(ResennaDto dto);
        void Update(ResennaDto dto);
        void Delete(int id);
    }
}