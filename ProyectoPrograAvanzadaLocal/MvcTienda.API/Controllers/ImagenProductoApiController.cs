using MvcTienda.Aplicacion.Imagenes;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MvcTienda.API.Controllers
{
    [RoutePrefix("api/imagenes")]
    public class ImagenProductoApiController : ApiController
    {
        private readonly IImagenProductoService _service;

        public ImagenProductoApiController(IImagenProductoService service)
        {
            _service = service;
        }

        // GET api/imagenes
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var imagenes = _service.GetAll()
                .Select(i => new
                {
                    i.ImagenProductoId,
                    ImagenBase64 = Convert.ToBase64String(i.RutaImagen),
                    i.ProductoId,
                    i.EstadoId
                });

            return Ok(imagenes);
        }

        // GET api/imagenes/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var img = _service.GetById(id);
            if (img == null)
                return NotFound();

            return Ok(new
            {
                img.ImagenProductoId,
                ImagenBase64 = Convert.ToBase64String(img.RutaImagen),
                img.ProductoId,
                img.EstadoId
            });
        }

        // POST api/imagenes (subir imagen)
        [HttpPost]
        [Route("crear")]
        public IHttpActionResult Create()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                var file = httpRequest.Files["Archivo"];
                int productoId = int.Parse(httpRequest["ProductoId"]);
                int estadoId = int.Parse(httpRequest["EstadoId"]);

                byte[] imagenBytes = null;

                if (file != null && file.ContentLength > 0)
                {
                    using (var br = new BinaryReader(file.InputStream))
                    {
                        imagenBytes = br.ReadBytes(file.ContentLength);
                    }
                }

                var dto = new ImagenProductoDto
                {
                    RutaImagen = imagenBytes,
                    ProductoId = productoId,
                    EstadoId = estadoId
                };

                _service.Create(dto);

                return Ok(new { mensaje = "Imagen creada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
        }

        // PUT api/imagenes/editar
        [HttpPut]
        [Route("editar/{id:int}")]
        public IHttpActionResult Edit(int id)
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                var file = httpRequest.Files["Archivo"];
                int productoId = int.Parse(httpRequest["ProductoId"]);
                int estadoId = int.Parse(httpRequest["EstadoId"]);

                byte[] imagenBytes = null;

                var existente = _service.GetById(id);
                if (existente == null) return NotFound();

                if (file != null)
                {
                    using (var br = new BinaryReader(file.InputStream))
                    {
                        imagenBytes = br.ReadBytes(file.ContentLength);
                    }
                }
                else
                {
                    imagenBytes = existente.RutaImagen;
                }

                var dto = new ImagenProductoDto
                {
                    ImagenProductoId = id,
                    RutaImagen = imagenBytes,
                    ProductoId = productoId,
                    EstadoId = estadoId
                };

                _service.Update(dto);

                return Ok(new { mensaje = "Imagen actualizada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
        }

        // DELETE api/imagenes/eliminar/5
        [HttpDelete]
        [Route("eliminar/{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return Ok("Imagen eliminada correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
        }
    }
}
