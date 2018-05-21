using Microsoft.AspNetCore.Mvc;
using SimionatoChefBusiness;
using SimionatoChefBusiness.Models.View;
using SimionatoChefDAO.Models;
using SimionatoChefWeb.Controllers;

namespace Simionato.PageControllers
{
    public class UsuarioController : Controller
    {
        private IUsuarioBusiness _usuarioBusiness;
        public UsuarioController(IUsuarioBusiness usuarioBusiness)
        {
            _usuarioBusiness = usuarioBusiness;
        }

        // GET: Usuario
        public ActionResult Index()
        {
            return RedirectToAction("Usuarios");
        }

        public ActionResult Usuarios()
        {
            var usuarioLogado = HttpContext.Session.GetObject<Usuario>("UsuarioLogado");
            if (usuarioLogado != null)
            {
                UsuarioView usuarioView = new UsuarioView()
                {
                    UsuarioLogado = (Usuario)usuarioLogado,
                    ListaUsuario = _usuarioBusiness.ListaUsuarios()
                };
                return View("Usuarios", usuarioView);
            }
            else
            {
                return RedirectToAction("../Login");
            }
        }

        // POST: Usuario/Salvar
        [HttpPost]
        public ActionResult Salvar(Usuario usuario)
        {
            UsuarioView UsuarioView = new UsuarioView()
            {
                UsuarioLogado = HttpContext.Session.GetObject<Usuario>("UsuarioLogado") 
            };
            _usuarioBusiness.SalvarUsuario(usuario);
            ViewBag.Mensagem = "Usuario Salvo com Sucesso";
            return RedirectToAction("Usuarios", UsuarioView);
        }

        // GET: Usuario/Buscar/5
        public ActionResult Buscar(int id)
        {
            UsuarioView UsuarioView = new UsuarioView()
            {
                Usuario = _usuarioBusiness.ObterUsuarioCompleto(id),
                UsuarioLogado = HttpContext.Session.GetObject<Usuario>("UsuarioLogado") ,
                ListaUsuario = _usuarioBusiness.ListaUsuarios()
            };
            return View("Usuarios", UsuarioView);
        }
    }
}
