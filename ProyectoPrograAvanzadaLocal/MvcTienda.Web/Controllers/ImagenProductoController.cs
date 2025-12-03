using MvcTienda.Aplicacion.Imagenes;
using MvcTienda.Aplicacion.Productos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcTienda.Web.Controllers
{
    public class ImagenProductoController : Controller
    {
        private readonly IImagenProductoService _service;
        private readonly IProductoService _productoService;

        public ImagenProductoController(IImagenProductoService service, IProductoService productoService)
        {
            _service = service;
            _productoService = productoService;
        }

        // GET: ImagenProducto
        public ActionResult Index()
        {
            try
            {
                var imagenes = _service.GetAll();
                return View(imagenes);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al cargar las imágenes: " + ex.Message;
                return View(new List<ImagenProductoDto>());
            }
        }

        // GET: ImagenProducto/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            ViewBag.ListaProductos = new SelectList(_productoService.GetAll(), "ProductoId", "Nombre");

            return View(new ImagenProductoDto());
        }

        // POST: ImagenProducto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Create(HttpPostedFileBase RutaImagen, [Bind(Exclude = "RutaImagen")] ImagenProductoDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ListaProductos = new SelectList(_productoService.GetAll(), "ProductoId", "Nombre");

                return View(dto);
            }
            try
            {
                if (RutaImagen != null && RutaImagen.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(RutaImagen.InputStream))
                    {
                        dto.RutaImagen = binaryReader.ReadBytes(RutaImagen.ContentLength);
                    }
                }
                _service.Create(dto);
                TempData["Mensaje"] = "Imagen creada correctamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al crear la imagen: " + ex.Message;
                return View(dto);
            }
        }

        // GET: ImagenProducto/Edit/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int id)
        {
            try
            {
                var imagen = _service.GetById(id);
                if (imagen == null)
                {
                    TempData["Error"] = "No se encontró la imagen solicitada.";
                    return RedirectToAction("Index");
                }
                ViewBag.ListaProductos = new SelectList(
                    _productoService.GetAll(),
                    "ProductoId",
                    "Nombre",
                    imagen.ProductoId
                );

                ViewBag.EstadoId = imagen.EstadoId;

                return View(imagen);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error al cargar la imagen: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        // POST: ImagenProducto/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(ImagenProductoDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.ListaProductos = new SelectList(
                        _productoService.GetAll(),
                        "ProductoId",
                        "Nombre",
                        dto.ProductoId
                    );
                    ViewBag.EstadoId = dto.EstadoId;

                    return View(dto);
                }

                var imagenExistente = _service.GetById(dto.ImagenProductoId);
                if (imagenExistente == null)
                {
                    TempData["Error"] = "La imagen ya no existe en el sistema.";
                    return RedirectToAction("Index");
                }

                if (dto.ArchivoImagen != null && dto.ArchivoImagen.ContentLength > 0)
                {
                    var extension = Path.GetExtension(dto.ArchivoImagen.FileName).ToLower();
                    var extensionesPermitidas = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };

                    if (!extensionesPermitidas.Contains(extension))
                    {
                        ModelState.AddModelError("ArchivoImagen", "Formato de imagen no válido. Use JPG, PNG o GIF.");
                        ViewBag.ListaProductos = new SelectList(_productoService.GetAll(), "ProductoId", "Nombre", dto.ProductoId);
                        ViewBag.EstadoId = dto.EstadoId;
                        return View(dto);
                    }

                    if (dto.ArchivoImagen.ContentLength > 5 * 1024 * 1024)
                    {
                        ModelState.AddModelError("ArchivoImagen", "La imagen es demasiado grande. Máximo 5MB.");
                        ViewBag.ListaProductos = new SelectList(_productoService.GetAll(), "ProductoId", "Nombre", dto.ProductoId);
                        ViewBag.EstadoId = dto.EstadoId;
                        return View(dto);
                    }

                    using (var binaryReader = new BinaryReader(dto.ArchivoImagen.InputStream))
                    {
                        dto.RutaImagen = binaryReader.ReadBytes(dto.ArchivoImagen.ContentLength);
                    }
                }
                else
                {
                    dto.RutaImagen = imagenExistente.RutaImagen;
                }

                _service.Update(dto);

                TempData["Mensaje"] = "¡Imagen actualizada correctamente!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ListaProductos = new SelectList(
                    _productoService.GetAll(),
                    "ProductoId",
                    "Nombre",
                    dto.ProductoId
                );
                ViewBag.EstadoId = dto.EstadoId;
                ViewBag.Error = $"Error al actualizar la imagen: {ex.Message}";

                return View(dto);
            }
        }


        // GET: ImagenProducto/Details/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Details(int id)
        {
            var imagen = _service.GetById(id);

            if (imagen == null)
                return HttpNotFound();

            return View(imagen);
        }

        // GET: ImagenProducto/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int id)
        {
            var imagen = _service.GetById(id);

            if (imagen == null)
                return HttpNotFound();

            return View(imagen);
        }

        // POST: ImagenProducto/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _service.Delete(id);
                TempData["Mensaje"] = "Imagen eliminada correctamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al eliminar la imagen: " + ex.Message;
                return RedirectToAction("Delete", new { id });
            }
        }
    }
}
