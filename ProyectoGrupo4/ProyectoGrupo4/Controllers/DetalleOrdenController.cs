using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoGrupo4.Models;

namespace ProyectoGrupo4.Controllers
{
    public class DetalleOrdenController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DetalleOrden
        public ActionResult Index()
        {
            var detallesOrden = db.DetallesOrden.Include(d => d.Estado).Include(d => d.Orden).Include(d => d.Producto);
            return View(detallesOrden.ToList());
        }

        // GET: DetalleOrden/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleOrden detalleOrden = db.DetallesOrden.Find(id);
            if (detalleOrden == null)
            {
                return HttpNotFound();
            }
            return View(detalleOrden);
        }

        // GET: DetalleOrden/Create
        public ActionResult Create()
        {
            ViewBag.ID_Estado = new SelectList(db.Estados, "ID_Estado", "Nombre");
            ViewBag.ID_Orden = new SelectList(db.Ordenes, "ID_Orden", "ID_Usuario");
            ViewBag.ID_Producto = new SelectList(db.Productos, "ID_Producto", "Nombre");
            return View();
        }

        // POST: DetalleOrden/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_DetalleOrden,Cantidad,PrecioUnitario,ID_Producto,ID_Orden,ID_Estado")] DetalleOrden detalleOrden)
        {
            if (ModelState.IsValid)
            {
                db.DetallesOrden.Add(detalleOrden);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Estado = new SelectList(db.Estados, "ID_Estado", "Nombre", detalleOrden.ID_Estado);
            ViewBag.ID_Orden = new SelectList(db.Ordenes, "ID_Orden", "ID_Usuario", detalleOrden.ID_Orden);
            ViewBag.ID_Producto = new SelectList(db.Productos, "ID_Producto", "Nombre", detalleOrden.ID_Producto);
            return View(detalleOrden);
        }

        // GET: DetalleOrden/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleOrden detalleOrden = db.DetallesOrden.Find(id);
            if (detalleOrden == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Estado = new SelectList(db.Estados, "ID_Estado", "Nombre", detalleOrden.ID_Estado);
            ViewBag.ID_Orden = new SelectList(db.Ordenes, "ID_Orden", "ID_Usuario", detalleOrden.ID_Orden);
            ViewBag.ID_Producto = new SelectList(db.Productos, "ID_Producto", "Nombre", detalleOrden.ID_Producto);
            return View(detalleOrden);
        }

        // POST: DetalleOrden/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_DetalleOrden,Cantidad,PrecioUnitario,ID_Producto,ID_Orden,ID_Estado")] DetalleOrden detalleOrden)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalleOrden).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Estado = new SelectList(db.Estados, "ID_Estado", "Nombre", detalleOrden.ID_Estado);
            ViewBag.ID_Orden = new SelectList(db.Ordenes, "ID_Orden", "ID_Usuario", detalleOrden.ID_Orden);
            ViewBag.ID_Producto = new SelectList(db.Productos, "ID_Producto", "Nombre", detalleOrden.ID_Producto);
            return View(detalleOrden);
        }

        // GET: DetalleOrden/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleOrden detalleOrden = db.DetallesOrden.Find(id);
            if (detalleOrden == null)
            {
                return HttpNotFound();
            }
            return View(detalleOrden);
        }

        // POST: DetalleOrden/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetalleOrden detalleOrden = db.DetallesOrden.Find(id);
            db.DetallesOrden.Remove(detalleOrden);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
