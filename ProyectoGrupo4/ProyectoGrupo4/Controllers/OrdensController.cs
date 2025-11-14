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
    public class OrdensController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orden
        public ActionResult Index()
        {
            var ordens = db.Ordenes.Include(o => o.Estado).Include(o => o.Usuario);
            return View(ordens.ToList());
        }

        // GET: Orden/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orden orden = db.Ordenes.Find(id);
            if (orden == null)
            {
                return HttpNotFound();
            }
            return View(orden);
        }

        // GET: Orden/Create
        public ActionResult Create()
        {
            ViewBag.ID_Estado = new SelectList(db.Estados, "ID_Estado", "Nombre");
            ViewBag.ID_Usuario = new SelectList(db.ApplicationUsers, "Id", "NombreUsuario");
            return View();
        }

        // POST: Orden/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Orden,Fecha_Orden,Total,ID_Usuario,ID_Estado")] Orden orden)
        {
            if (ModelState.IsValid)
            {
                db.Ordenes.Add(orden);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Estado = new SelectList(db.Estados, "ID_Estado", "Nombre", orden.ID_Estado);
            ViewBag.ID_Usuario = new SelectList(db.ApplicationUsers, "Id", "NombreUsuario", orden.ID_Usuario);
            return View(orden);
        }

        // GET: Orden/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orden orden = db.Ordenes.Find(id);
            if (orden == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Estado = new SelectList(db.Estados, "ID_Estado", "Nombre", orden.ID_Estado);
            ViewBag.ID_Usuario = new SelectList(db.ApplicationUsers, "Id", "NombreUsuario", orden.ID_Usuario);
            return View(orden);
        }

        // POST: Orden/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Orden,Fecha_Orden,Total,ID_Usuario,ID_Estado")] Orden orden)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orden).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Estado = new SelectList(db.Estados, "ID_Estado", "Nombre", orden.ID_Estado);
            ViewBag.ID_Usuario = new SelectList(db.ApplicationUsers, "Id", "NombreUsuario", orden.ID_Usuario);
            return View(orden);
        }

        // GET: Orden/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orden orden = db.Ordenes.Find(id);
            if (orden == null)
            {
                return HttpNotFound();
            }
            return View(orden);
        }

        // POST: Orden/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orden orden = db.Ordenes.Find(id);
            db.Ordenes.Remove(orden);
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
