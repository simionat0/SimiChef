using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimionatoChefBusiness;
using SimionatoChefDAO.Models;
using SimionatoChefBusiness.Models.View;
using SimionatoChefWeb.Controllers;
using System;

namespace Simionato.PageControllers
{
    public class FinanceiroController : Controller
    {
        private IVendaBusiness _vendaBusiness;
        public FinanceiroController(IVendaBusiness vendaBusiness)
        {
            _vendaBusiness = vendaBusiness;
        }

        // GET: Financeiro
        public ActionResult Index()
        {
            return RedirectToAction("RelatorioVendas");
        }

        public ActionResult RelatorioVendas()
        {
            if (HttpContext.Session.IsExists("UsuarioLogado"))
            {
                FinanceiroView FinanceiroView = new FinanceiroView()
                {
                    Usuario = (Usuario)HttpContext.Session.GetObject<Usuario>("UsuarioLogado"),
                    ListaVendas = _vendaBusiness.ListaVendaDia(DateTime.Now)
                };
                FinanceiroView.Total = _vendaBusiness.CalculoTotalVendas(FinanceiroView.ListaVendas);
                return View("RelatorioVendas", FinanceiroView);
            }
            else
            {
                return RedirectToAction("../Login");
            }
        }

        [HttpPost]
        [Route("Financeiro/ListaVendaDia")]
        public ActionResult ListaVendaDia(IFormCollection formCollection)
        {
            DateTime dataInicio = Convert.ToDateTime(formCollection["datepicker"]);

            if (HttpContext.Session.IsExists("UsuarioLogado"))
            {
                FinanceiroView FinanceiroView = new FinanceiroView()
                {
                    Usuario = HttpContext.Session.GetObject<Usuario>("UsuarioLogado"),
                    ListaVendas = _vendaBusiness.ListaVendaDia(dataInicio)
                };
                FinanceiroView.Total = _vendaBusiness.CalculoTotalVendas(FinanceiroView.ListaVendas);
                ViewBag.Data = formCollection["datepicker"];
                return View("RelatorioVendas", FinanceiroView);
            }
            else
            {
                return RedirectToAction("../Login");
            }
        }

        [HttpPost]
        [Route("Financeiro/ListaPedidosEntreDias")]
        public ActionResult ListaPedidosEntreDias(IFormCollection collection)
        {
            //    string[] datas = formCollection["reservation"].Split('-');
            string form = collection["reservation"];
            string[] datas = form.Split('-');
            DateTime dataInicio = Convert.ToDateTime(datas[0]);
            DateTime dataFim = Convert.ToDateTime(datas[1]);

            if (HttpContext.Session.IsExists("UsuarioLogado"))
            {
                FinanceiroView FinanceiroView = new FinanceiroView()
                {
                    Usuario = (Usuario)HttpContext.Session.GetObject<Usuario>("UsuarioLogado"),
                    ListaVendas = _vendaBusiness.ListaPedidosEntreDias(dataInicio, dataFim)
                };
                FinanceiroView.Total = _vendaBusiness.CalculoTotalVendas(FinanceiroView.ListaVendas);
                ViewBag.Data = collection["reservation"];
                return View("RelatorioVendas", FinanceiroView);
            }
            else
            {
                return RedirectToAction("../Login");
            }
        }
    }
}
