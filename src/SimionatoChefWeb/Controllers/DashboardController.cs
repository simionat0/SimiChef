using Microsoft.AspNetCore.Mvc;
using SimionatoChefBusiness;
using SimionatoChefBusiness.Models.View;
using SimionatoChefDAO.Models;
using SimionatoChefWeb.Controllers;

namespace Simionato.PageControllers
{
    public class DashboardController : Controller
    {
        private IDashboardBusiness _dashboardBusiness;
        public DashboardController(IDashboardBusiness dashboardBusiness)
        {
            _dashboardBusiness = dashboardBusiness;
        }

        public ActionResult Index()
        {
            return RedirectToAction("Home");
        }

        public ActionResult Home()
        {
            Usuario UsuarioLogado;

            if (HttpContext.Session.IsExists("UsuarioLogado"))
            {
                UsuarioLogado = HttpContext.Session.GetObject<Usuario>("UsuarioLogado");
                Dashboard dashboard = _dashboardBusiness.Dashboard(UsuarioLogado);
                return View("Home", dashboard);
            }
            else
            {
                UsuarioLogado = null;
                return RedirectToAction("../Login");
            }
        }
    }
}
