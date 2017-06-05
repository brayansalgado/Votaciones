using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Votaciones.Models;

namespace Votaciones.Controllers
{
    [Authorize]
    public class CensosController : Controller
    {
       
        private VotacionesContext db = new VotacionesContext();

        // GET: Censos
        public ActionResult Index()
        {
            var censoes = db.Censos.Include(c => c.estado);
            return View(censoes.ToList());
        }

        // GET: Censos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Censo censo = db.Censos.Find(id);
            if (censo == null)
            {
                return HttpNotFound();
            }
            return View(censo);
        }

        // GET: Censos/Create
        public ActionResult Create()
        {
            ViewBag.idEstado = new SelectList(db.Estados, "idEstado", "descripcion");
            return View();
        }

        // POST: Censos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Censo censo)
        {
            if (ModelState.IsValid)
            {
                db.Censos.Add(censo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idEstado = new SelectList(db.Estados, "idEstado", "descripcion", censo.idEstado);
            return View(censo);
        }

        // GET: Censos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Censo censo = db.Censos.Find(id);
            if (censo == null)
            {
                return HttpNotFound();
            }
            ViewBag.idEstado = new SelectList(db.Estados, "idEstado", "descripcion", censo.idEstado);
            return View(censo);
        }

        // POST: Censos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Censo censo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(censo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idEstado = new SelectList(db.Estados, "idEstado", "descripcion", censo.idEstado);
            return View(censo);
        }

        // GET: Censos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Censo censo = db.Censos.Find(id);
            if (censo == null)
            {
                return HttpNotFound();
            }
            return View(censo);
        }

        // POST: Censos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Censo censo = db.Censos.Find(id);
            db.Censos.Remove(censo);
            db.SaveChanges();
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                {
                    ViewBag.Error = "No se puede borrar el elemento porque está relacionado con otros datos";
                }
                else
                {
                    ViewBag.Error = ex.Message;
                }
                return View(censo);
            }
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
