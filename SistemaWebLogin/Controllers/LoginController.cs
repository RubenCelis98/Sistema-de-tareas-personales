using SistemaWebLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaWebLogin.Controllers
{
    public class LoginController : Controller
    {

        // GET: Login
        public string NomUsu { get; set; }


        public ActionResult Index()
        {
            ModelState.Clear();
            return View();  
        }

        [HttpGet]
        public ActionResult ValidarLogin(Usuario usu)
        {
            using (SistWebLoginEnti db = new SistWebLoginEnti())
            {
                var consulta = (from usuario in db.Usuario
                                where usuario.EmaUsu == usu.EmaUsu && usuario.Contrasena == usu.Contrasena
                                select usuario).ToList();

                if (consulta.Count != 0)
                {
                    ModelState.Clear();
                    VarGlo.IdUsu = consulta[0].IdUsu;
                    Session["IdUsu"] = consulta[0].IdUsu;
                    return RedirectToAction("Index", "Menu");
                }
                else
                {
                    ModelState.Clear();
                    ViewBag.MensajeError = "Usuario y/o Contraseña incorrecta";
                    return View("Index");
                }
            }
        }

        public ActionResult RegistroCuenta()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistroCuenta(Usuario Reg)
        {
            using (SistWebLoginEnti db = new SistWebLoginEnti())
            {
                if (ModelState.IsValid)
                {
                    db.Usuario.Add(Reg);
                    db.SaveChanges();
                }
            }

            ModelState.Clear();
            return View("Index");
        }


        public ActionResult ValidarEmail()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ValidarEmailUsu(Usuario Reg)
        {
            using (SistWebLoginEnti db = new SistWebLoginEnti())
            {
                var consulta = (from registro in db.Usuario
                                where registro.EmaUsu == Reg.EmaUsu
                                select registro).ToList();

                if (consulta.Count != 0)
                {
                    ModelState.Clear();
                    VarGlo.IdUsu = consulta[0].IdUsu; ;
                    Session["IdUsu"] = consulta[0].IdUsu;
                    return View("CambiarPass");
                }
                else
                {
                    ViewBag.MensajeError = "Correo ingresado inválido";
                    return View("ValidarEmail");
                }
            }
        }

        
        public ActionResult CambiarPass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdatePass(Usuario registro)
        {
            using (SistWebLoginEnti db = new SistWebLoginEnti())
            {
                if (registro.Contrasena == registro.ConContrasena)
                {
                    if (ModelState.IsValid)
                    {
                        int usu = VarGlo.IdUsu;
                        var consulta = db.Usuario.FirstOrDefault(tblRegistro => tblRegistro.IdUsu == usu);

                        consulta.Contrasena = registro.Contrasena;
                        db.Entry(consulta).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        ModelState.Clear();
                        return View("Index");
                    }
                }
                else
                {
                    ViewBag.MensajeError = "Contraseñas no coinciden";
                    return View("CambiarPass");
                }
            }

            return View();
        }

    }
}