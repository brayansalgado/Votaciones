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
    public class GruposController : Controller
    {
        private VotacionesContext db = new VotacionesContext();

        [HttpGet]
        public ActionResult BorrarMiembro(int id)
        {
            var miembro = db.MiembrosDeGrupo.Find(id);
            if (miembro!=null)
            {
                db.MiembrosDeGrupo.Remove(miembro);
                db.SaveChanges();
            }
            return RedirectToAction(string.Format("Details/{0}",miembro.idGrupo));
        }
        [HttpGet]
        public ActionResult AdicionarMiembro(int idGrupo)
        {
            ViewBag.idPersona = new SelectList(db.Personas.OrderBy(u=>u.nombrePersona), "idPersona", "nombrePersona");
            var view = new AdicionarMiembroVista()
            {
                idGrupo = idGrupo
            };
            return View(view);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdicionarMiembro(AdicionarMiembroVista view)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.idPersona = new SelectList(db.Personas.OrderBy(u => u.nombrePersona), "idPersona", "nombrePersona");
                return View(view);
            }

            var miembro = db.MiembrosDeGrupo.Where(gm => gm.idGrupo == view.idGrupo && gm.idPersona == view.idPersona).FirstOrDefault();
            if (miembro!=null)
            {
                ViewBag.Error = "El miembro ya ha sido asignado al grupo ";
                return View(view);
            }
            miembro = new MiembroDeGrupo
            {
                idGrupo=view.idGrupo,
                idPersona=view.idPersona,
            };
            db.MiembrosDeGrupo.Add(miembro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Grupos
        public ActionResult Index()
        {
            return View(db.Grupos.ToList());
        }

        // GET: Grupos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupo grupo = db.Grupos.Find(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            var view = new DetalleGrupoVista
            {
                idGrupo = grupo.idGrupo,
                descripcion= grupo.descripcion,
                miembros= grupo.miembrosDeGrupo.ToList(),

            };
            return View(view);
        }

        // GET: Grupos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Grupos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                db.Grupos.Add(grupo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(grupo);
        }

        // GET: Grupos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupo grupo = db.Grupos.Find(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            return View(grupo);
        }

        // POST: Grupos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grupo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(grupo);
        }

        // GET: Grupos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupo grupo = db.Grupos.Find(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            return View(grupo);
        }

        // POST: Grupos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Grupo grupo = db.Grupos.Find(id);
            db.Grupos.Remove(grupo);
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
                return View(grupo);
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
