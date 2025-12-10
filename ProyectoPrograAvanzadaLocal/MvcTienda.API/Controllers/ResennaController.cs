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

        // GET api/resennas/5
        [HttpGet]
        [Route("{id:int}", Name = "GetResennaById")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var resenna = _service.GetById(id);
                return Ok(resenna);
            }
            catch (NegocioException ex)
            {
                // Reglas de negocio violadas → 400
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        // POST api/resennas
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] ResennaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _service.Create(dto);

                // Devolver 201 Created con Location header
                return CreatedAtRoute(
                    "GetResennaById",
                    new { id = dto.ResennaId },
                    dto
                );
            }
            catch (NegocioException ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception)
            {
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

        // DELETE api/resennas/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (NegocioException ex)
            {
                // Podrías usar 404 si el mensaje es "no existe", o 400 si es regla de negocio
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}