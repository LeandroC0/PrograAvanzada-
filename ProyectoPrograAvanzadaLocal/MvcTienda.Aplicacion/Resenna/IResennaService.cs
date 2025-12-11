using System.Collections.Generic;

namespace MvcTienda.Aplicacion.Resennas
{
    public interface IResennaService
    {
        IEnumerable<ResennaDto> GetAll();
        IEnumerable<ResennaDto> GetAllPublic();
        IEnumerable<ResennaDto> GetAllPendiente();
        IEnumerable<ResennaDto> GetAllByUsuarioId(int usuarioId);
        ResennaDto GetById(int id);
        void Create(ResennaDto dto);
        void Update(ResennaDto dto);
        void Delete(int id);
        void CambiarEstado(int id, int nuevoEstadoId);
    }
}