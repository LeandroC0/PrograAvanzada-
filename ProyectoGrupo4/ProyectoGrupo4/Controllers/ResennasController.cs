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
    public class ResennasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Resennas
        public ActionResult Index()
        {
            var resennas = db.Resennas.Include(r => r.Estado).Include(r => r.Producto).Include(r => r.Usuario);
            return View(resennas.ToList());
        }

        // GET: Resennas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resenna resenna = db.Resennas.Find(id);
            if (resenna == null)
            {
                return HttpNotFound();
            }
            return View(resenna);
        }

        // GET: Resennas/Create
        public ActionResult Create()
        {
            ViewBag.ID_Estado = new SelectList(db.Estados, "ID_Estado", "Nombre");
            ViewBag.ID_Producto = new SelectList(db.Productos, "ID_Producto", "Nombre");
            ViewBag.ID_Usuario = new SelectList(db.ApplicationUsers, "Id", "NombreUsuario");
            return View();
        }

        // POST: Resennas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Reseña,Comentario,Calificación,Fecha_Reseña,ID_Producto,ID_Estado,ID_Usuario")] Resenna resenna)
        {
            if (ModelState.IsValid)
            {
                db.Resennas.Add(resenna);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Estado = new SelectList(db.Estados, "ID_Estado", "Nombre", resenna.ID_Estado);
            ViewBag.ID_Producto = new SelectList(db.Productos, "ID_Producto", "Nombre", resenna.ID_Producto);
            ViewBag.ID_Usuario = new SelectList(db.ApplicationUsers, "Id", "NombreUsuario", resenna.ID_Usuario);
            return View(resenna);
        }

        // GET: Resennas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resenna resenna = db.Resennas.Find(id);
            if (resenna == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Estado = new SelectList(db.Estados, "ID_Estado", "Nombre", resenna.ID_Estado);
            ViewBag.ID_Producto = new SelectList(db.Productos, "ID_Producto", "Nombre", resenna.ID_Producto);
            ViewBag.ID_Usuario = new SelectList(db.ApplicationUsers, "Id", "NombreUsuario", resenna.ID_Usuario);
            return View(resenna);
        }

        // POST: Resennas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Reseña,Comentario,Calificación,Fecha_Reseña,ID_Producto,ID_Estado,ID_Usuario")] Resenna resenna)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resenna).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Estado = new SelectList(db.Estados, "ID_Estado", "Nombre", resenna.ID_Estado);
            ViewBag.ID_Producto = new SelectList(db.Productos, "ID_Producto", "Nombre", resenna.ID_Producto);
            ViewBag.ID_Usuario = new SelectList(db.ApplicationUsers, "Id", "NombreUsuario", resenna.ID_Usuario);
            return View(resenna);
        }

        // GET: Resennas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resenna resenna = db.Resennas.Find(id);
            if (resenna == null)
            {
                return HttpNotFound();
            }
            return View(resenna);
        }

        // POST: Resennas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Resenna resenna = db.Resennas.Find(id);
            db.Resennas.Remove(resenna);
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
