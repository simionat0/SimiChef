using Microsoft.AspNetCore.Mvc;
using SimionatoChefBusiness;
using SimionatoChefDAO.Models;
using SimionatoChefWeb.Controllers;
using System;

namespace Simionato.PageControllers
{
    public class LoginController : Controller
    {
        private IUsuarioBusiness _usuarioBusiness;
        public LoginController(IUsuarioBusiness usuarioBusiness)
        {
            _usuarioBusiness = usuarioBusiness;
        }

        // GET: Login
        public ActionResult Index()
        {
            Usuario UsuarioLogado;
            if (HttpContext.Session.IsExists("UsuarioLogado"))
            {
                UsuarioLogado = HttpContext.Session.GetObject<Usuario>("UsuarioLogado");
                return RedirectToAction("../Dashboard");
            }
            else
            {
                UsuarioLogado = null;
                return View("Login");
            }
        }

        // POST: Login/Autenticar
        [HttpPost]
        public ActionResult Autenticar(Usuario usuario)
        {
            try
            {
                Usuario usuarioLogin = _usuarioBusiness.LoginUsuario(usuario);

                if (usuarioLogin != null)
                {
                    HttpContext.Session.SetObject("UsuarioLogado", usuarioLogin);
                    return RedirectToAction("../Dashboard");
                }
                else
                {
                    ViewBag.Mensagem = "E-mail ou Senha Incorretos!";
                    return View("Login");
                }

            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = "Ops, alguma coisa deu errado. " + ex.Message;
                return View("Login");
            }
        }

        public ActionResult Login()
        {
            var usuarioLogado = HttpContext.Session.GetObject<Usuario>("UsuarioLogado");

            if (usuarioLogado != null)
            {
                return View("../Dashboard");
            }
            else
            {
                return View("Login");
            }
        }

        public ActionResult Logoff()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }
    }
}
