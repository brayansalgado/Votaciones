using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Votaciones.Models;

namespace Votaciones.Controllers
{
    [Authorize]
    public class PersonasController : Controller
    {
        private VotacionesContext db = new VotacionesContext();

        // GET: Personas
        public ActionResult Index()
        {
            return View(db.Personas.ToList());
        }

        // GET: Personas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Personas.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // GET: Personas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Personas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( PersonaVista personaVista)
        {
            if (!ModelState.IsValid)
            {
                return View(personaVista);
            }
            string path = string.Empty;
            string pic = string.Empty;
            if(personaVista.foto!=null)
            {
                pic = Path.GetFileName(personaVista.foto.FileName);
                path = Path.Combine(Server.MapPath("~/Content/Fotos"), pic);
                personaVista.foto.SaveAs(path);
                using (MemoryStream ms = new MemoryStream())
                {
                    personaVista.foto.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
            }
            var persona = new Persona
            {
                nombrePersona = personaVista.nombrePersona,
                usuario=personaVista.usuario,
                email=personaVista.email,
                cargo= personaVista.cargo,
                grupo=personaVista.grupo,
                foto=pic==string.Empty ? string.Empty : string.Format("~/Content/Fotos/{0}",pic)
            };

            db.Personas.Add(persona);

            try
            {
                db.SaveChanges();
                this.CreateASPUser(personaVista);                                                                                                                                                                                       
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null &&ex.InnerException.InnerException!=null&&ex.InnerException.InnerException.Message.Contains("usuarioIndex"))
                {
                    ViewBag.Error = " El nombre usuario es usado por otro usuario ";
                }
                else
                {
                    ViewBag.Error = ex.Message;
                }
                return View(personaVista);
            }

            return RedirectToAction("Index");

                    }

        private void CreateASPUser(PersonaVista personaVista)
        {
            var userContext = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(userContext));

            string roleName = "User";

            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new IdentityRole(roleName));
            }

            var userASP = new ApplicationUser
            {
                UserName=personaVista.usuario,
                Email=personaVista.email
     
            };
            userManager.Create(userASP, userASP.UserName);

            userASP = userManager.FindByName(personaVista.usuario);
            userManager.AddToRole(userASP.Id, "Usuario");
        }

        // GET: Personas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Personas.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            var personaVista= new PersonaVista
            {
                nombrePersona = persona.nombrePersona,
                usuario = persona.usuario,
                email= persona.email,
                cargo = persona.cargo,
                idPersona=persona.idPersona,
                grupo=persona.grupo
                
            };
            return View(personaVista);
        }

        // POST: Personas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PersonaVista personaVista)
        {
            if (!ModelState.IsValid)
            {
                return View(personaVista);
            }
            string path = string.Empty;
            string pic = string.Empty;
            if (personaVista.foto != null)
            {
                pic = Path.GetFileName(personaVista.foto.FileName);
                path = Path.Combine(Server.MapPath("~/Content/Fotos"), pic);
                personaVista.foto.SaveAs(path);
                using (MemoryStream ms = new MemoryStream())
                {
                    personaVista.foto.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
            }
            var persona = db.Personas.Find(personaVista.idPersona);

            persona.nombrePersona = personaVista.nombrePersona;
            persona.usuario = personaVista.usuario;
            persona.cargo = personaVista.cargo;
            persona.grupo = personaVista.grupo;
            persona.email = personaVista.email;
            if(!string.IsNullOrEmpty(pic))
            {
                persona.foto = pic == string.Empty ? string.Empty : string.Format("~/Content/Fotos/{0}", pic);
            }
           
            
            db.Entry(persona).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
            
        }

        // GET: Personas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Personas.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Persona persona = db.Personas.Find(id);
            db.Personas.Remove(persona);
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
                return View(persona);
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
