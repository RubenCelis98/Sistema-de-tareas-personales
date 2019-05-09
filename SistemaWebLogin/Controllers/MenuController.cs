using SistemaWebLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SistemaWebLogin.Controllers
{
    public class MenuController : Controller
    {
       
        [HttpGet]
        public ActionResult Index()
        {
            using (SistWebLoginEnti db = new SistWebLoginEnti())
            {
                var consulta = (from registro in db.Usuario
                                where registro.IdUsu == VarGlo.IdUsu
                                select registro).ToList();

                
                ViewBag.Usuario = consulta[0].NomUsu;
               
            }

            return View();
        }

        
        [HttpGet]
        public ActionResult TareaSimple()
        {
            using (SistWebLoginEnti db = new SistWebLoginEnti())
            {
                var item = db.TipoTarea.ToList();
                SelectList lis = new SelectList(item, "IdTip", "NomTip");
                ViewBag.ItemList = lis;
                return View();
            }
        }

        [HttpPost]
        public ActionResult TareaSimpleGuardar(NuevaTareaSimple nueTar)
        {
            using (SistWebLoginEnti db = new SistWebLoginEnti())
            {
                nueTar.IdUsu = VarGlo.IdUsu;

                if (ModelState.IsValid)
                {
                    db.NuevaTareaSimple.Add(nueTar);
                    db.SaveChanges();
                }
            }

            ModelState.Clear();
            return View("Index");
        }

        [HttpGet]
        public ActionResult TareaSimplePerzonalizada()
        {
            using (SistWebLoginEnti db = new SistWebLoginEnti())
            {
                var item = db.TipoTarea.ToList();
                SelectList lis = new SelectList(item, "IdTip", "NomTip");
                ViewBag.ItemList = lis;
                return View();
            }
        }

        [HttpPost]
        public ActionResult TareaPersonalizadaGuardar(NuevaTareaPersonalizada nueTarPer)
        {
            using (SistWebLoginEnti db = new SistWebLoginEnti())
            {
                nueTarPer.IdUsu = VarGlo.IdUsu;

                if (ModelState.IsValid)
                {
                    db.NuevaTareaPersonalizada.Add(nueTarPer);
                    db.SaveChanges();
                }
            }

            ModelState.Clear();
            return View("Index");
        }

        [HttpGet]
        public ActionResult Cuenta()
        {
            using (SistWebLoginEnti db = new SistWebLoginEnti())
            {
                var canRegSim = (from nueTar in db.NuevaTareaSimple
                                 where nueTar.IdUsu == VarGlo.IdUsu
                                 select nueTar).ToList();

                var canRegPer = (from nueTar in db.NuevaTareaPersonalizada
                                 where nueTar.IdUsu == VarGlo.IdUsu
                                 select nueTar).ToList();

                var consulta = (from tarea in db.NuevaTareaSimple
                                join usuario in db.Usuario on tarea.IdUsu equals usuario.IdUsu
                                where tarea.IdUsu == VarGlo.IdUsu && usuario.IdUsu == VarGlo.IdUsu
                                select new { NuevaTareaSimple = tarea, Usuario = usuario }).ToList();
                               
                               
                 
                if (consulta.Count != 0)
                {
                    ViewBag.NomApe = consulta[0].Usuario.NomUsu + " " + consulta[0].Usuario.ApeUsu;
                    ViewBag.CanRegSim = canRegSim.Count;
                    ViewBag.CanRegPer = canRegPer.Count;
                    ViewBag.Email = consulta[0].Usuario.EmaUsu;
                }

            }

                return View();
        }

        [HttpGet]
        public ActionResult CambiarPass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Menu_ActPass(Usuario TbUsuario)
        {
            using (SistWebLoginEnti db = new SistWebLoginEnti())
            {
                var ConsultaUsuario = (from Usu in db.Usuario
                                       where Usu.IdUsu == VarGlo.IdUsu
                                       select Usu).ToList();

                string Pass = ConsultaUsuario[0].Contrasena;

                if (TbUsuario.ConContrasena == TbUsuario.NuevaContrasena)
                {
                    if (Pass == TbUsuario.Contrasena)
                    {
                        ConsultaUsuario[0].Contrasena = TbUsuario.NuevaContrasena;
                        db.SaveChanges();

                        ModelState.Clear();
                        return View("Index");
                    }
                    else
                    {
                        ViewBag.ErrorPass = "Contraseña ingresada inválida";
                    }
                }
                else
                {
                    ViewBag.ErrorPassNue = "No coinciden las contraseñas";
                   
                }

                ModelState.Clear();
                return View("CambiarPass");
            }
        }
        public ActionResult Compartir()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
        
    }
}