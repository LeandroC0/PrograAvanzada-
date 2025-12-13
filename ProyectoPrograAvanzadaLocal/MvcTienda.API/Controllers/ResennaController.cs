using MvcTienda.Aplicacion.Common;
using MvcTienda.Aplicacion.Estados;
using MvcTienda.Aplicacion.Resennas;
using System;
using System.Net;
using System.Web.Http;

namespace MvcTienda.API.Controllers
{
    [RoutePrefix("api/resennas")]
    public class ResennasController : ApiController
    {
        private readonly IResennaService _service;
        public class EstadoDto
        {
            public int NuevoEstadoId { get; set; }
        }

        public ResennasController(IResennaService service)
        {
            _service = service;
        }

        // GET api/resennas
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            try
            {
                var resennas = _service.GetAll();
                return Ok(resennas);
            }
            catch (Exception)
            {
                // Aquí conviene loguear
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("producto/{productoId:int}")]
        public IHttpActionResult GetByProducto(int productoId)
        {
            try
            {
                var resennas = _service.GetAllByProductoId(productoId);
                return Ok(resennas);
            }
            catch (Exception)
            {
                // Aquí conviene loguear
                return InternalServerError();
            }
        }

        // PUT api/resennas/5
        [HttpPut]
        [Route("{id:int}/moderar")]
        public IHttpActionResult Moderar(int id, [FromBody] EstadoDto dto)
        {
            try
            {
                if(dto == null || (dto.NuevoEstadoId != 4 && dto.NuevoEstadoId != 5))
                {
                    return BadRequest("El estado proporcionado no es válido para moderar la reseña.");
                }
                _service.CambiarEstado(id, dto.NuevoEstadoId);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(
                    $"Error interno: {ex.Message} - Stack: {ex.StackTrace}"
                ));
            }
        }
    }
}