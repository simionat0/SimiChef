using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimionatoChefBusiness;
using SimionatoChefBusiness.Models.View;
using SimionatoChefDAO.Models;
using SimionatoChefWeb.Controllers;
using System;
using System.Collections.Generic;

namespace Simionato.PageControllers
{
    public class VendaController : Controller
    {
        private IVendaBusiness _vendaBusiness;
        private IClienteBusiness _clienteBusiness;
        private IProdutoBusiness _produtoBusiness;

        public VendaController(IClienteBusiness clienteBusiness, IVendaBusiness vendaBusiness, IProdutoBusiness produtoBusiness)
        {
            _vendaBusiness = vendaBusiness;
            _clienteBusiness = clienteBusiness;
            _produtoBusiness = produtoBusiness;
        }

        // GET: Venda
        public ActionResult Index()
        {
            return RedirectToAction("CriarVenda");
        }

        // GET: Venda/CriarVenda
        public ActionResult CriarVenda()
        {
            if (HttpContext.Session.IsExists("UsuarioLogado"))
            {
                VendaView VendaView = new VendaView();
                if (HttpContext.Session.IsExists("VendaView"))
                {
                    if (HttpContext.Session.GetObject<VendaView>("VendaView") != null)
                    {
                        VendaView = HttpContext.Session.GetObject<VendaView>("VendaView");
                    }
                    else
                    {
                        VendaView = _vendaBusiness.CriarVendaView(HttpContext.Session.GetObject<Usuario>("UsuarioLogado"), new Venda(), _clienteBusiness.ListaCliente(), _produtoBusiness.ListaProduto());
                    }
                    HttpContext.Session.SetObject("VendaView", VendaView);
                    return View("CriarVenda", VendaView);
                }
                else
                {
                    VendaView = _vendaBusiness.CriarVendaView(HttpContext.Session.GetObject<Usuario>("UsuarioLogado"), new Venda(), _clienteBusiness.ListaCliente(), _produtoBusiness.ListaProduto());
                    HttpContext.Session.SetObject("VendaView", VendaView);
                    return View("CriarVenda", VendaView);
                }
            }
            else
            {
                return RedirectToAction("../Login");
            }
        }

        // GET: Venda/MinhasVendas
        public ActionResult MinhasVendas(int id = 0)
        {
            if (HttpContext.Session.GetObject<Usuario>("UsuarioLogado") != null)
            {
                MinhasVendasView MinhasVendasView = new MinhasVendasView()
                {
                    Usuario = (Usuario)HttpContext.Session.GetObject<Usuario>("UsuarioLogado"),
                    Status = _vendaBusiness.ListaStatus(),
                    ListaVendas = _vendaBusiness.ListaVendasDashboard()
                };
                if (id != 0)
                {
                    MinhasVendasView.Venda = _vendaBusiness.ObterDetalheVenda(id);
                }
                else
                {
                    MinhasVendasView.Venda = _vendaBusiness.ObterDetalheVenda(_vendaBusiness.IdUltimaVenda());
                }
                return View("MinhasVendas", MinhasVendasView);
            }
            else
            {
                return RedirectToAction("../Login");
            }
        }

        // GET: Venda/BuscarProduto/5
        public ActionResult BuscarProduto(int id)
        {
            if (HttpContext.Session.GetObject<Usuario>("UsuarioLogado") != null)
            {
                if (HttpContext.Session.GetObject<VendaView>("VendaView") != null)
                {
                    VendaView VendaView = HttpContext.Session.GetObject<VendaView>("VendaView");
                    VendaView.Produto = _produtoBusiness.ObterProduto(id); ;
                    HttpContext.Session.SetObject("VendaView", VendaView);
                    return View("CriarVenda", VendaView);
                }
                else
                {

                    return RedirectToAction("CriarVenda");
                }
            }
            else
            {
                return RedirectToAction("../Login");
            }
        }

        // Venda/ImpressaoVenda/?id
        public ActionResult ImpressaoVenda(int id = 0)
        {
            if (HttpContext.Session.GetObject<Usuario>("UsuarioLogado") != null)
            {
                if (id != 0)
                {
                    VendaView VendaView = new VendaView()
                    {
                        Venda = _vendaBusiness.ObterDetalheVenda(id)
                    };
                    return View("ImpressaoVenda", VendaView);
                }
                else
                {
                    VendaView VendaView = new VendaView()
                    {
                        Venda = _vendaBusiness.ObterDetalheVenda(_vendaBusiness.IdUltimaVenda())
                    };
                    return View("ImpressaoVenda", VendaView);
                }
            }
            else
            {
                return RedirectToAction("../Login");
            }
        }

        // Venda/LimparProdutosVenda
        public ActionResult LimparProdutosVenda()
        {
            if (HttpContext.Session.GetObject<Usuario>("UsuarioLogado") != null)
            {
                if (HttpContext.Session.GetObject<VendaView>("VendaView") != null)
                {
                    VendaView VendaView = HttpContext.Session.GetObject<VendaView>("VendaView");
                    VendaView.Venda.Produtos = null;
                    VendaView.Venda.Total = 0;
                    HttpContext.Session.SetObject("VendaView", VendaView);
                    return RedirectToAction("CriarVenda");
                }
                else
                {
                    return RedirectToAction("CriarVenda");
                }
            }
            else
            {
                return RedirectToAction("../Login");
            }
        }

        // POST: Venda/AdicionarProdutoVenda
        [HttpPost]
        public ActionResult AdicionarProdutoVenda(Produto produto)
        {
            if (HttpContext.Session.GetObject<Usuario>("UsuarioLogado") != null)
            {
                if (HttpContext.Session.GetObject<VendaView>("VendaView") != null)
                {
                    VendaView VendaView = HttpContext.Session.GetObject<VendaView>("VendaView");
                    if (VendaView.Venda.Produtos != null)
                    {
                        VendaView.Venda.Produtos.Add(produto);
                        VendaView.Produto = null;
                        VendaView.Venda.Total = _vendaBusiness.CalculaTotalVenda(VendaView.Venda);
                        HttpContext.Session.SetObject("VendaView", VendaView);
                        return RedirectToAction("CriarVenda");
                    }
                    else
                    {
                        List<Produto> ListaProdutos = new List<Produto> { };
                        ListaProdutos.Add(produto);
                        VendaView.Venda.Produtos = ListaProdutos;
                        VendaView.Produto = null;
                        VendaView.Venda.Total = _vendaBusiness.CalculaTotalVenda(VendaView.Venda);
                        HttpContext.Session.SetObject("VendaView", VendaView);
                        return RedirectToAction("CriarVenda");
                    }
                }
                else
                {
                    return RedirectToAction("CriarVenda");
                }
            }
            else
            {
                return RedirectToAction("../Login");
            }
        }

        // POST: Venda/FinalizarVenda
        [HttpPost]
        public ActionResult FinalizarVenda(Cliente cliente)
        {
            if (HttpContext.Session.GetObject<VendaView>("VendaView") != null)
            {
                VendaView VendaView = HttpContext.Session.GetObject<VendaView>("VendaView");
                VendaView.Venda.Cliente = _clienteBusiness.ObterCliente(cliente.Id);
                if (_vendaBusiness.CriarVenda(VendaView.Venda))
                {
                    HttpContext.Session.SetObject("VendaView", null);
                    return RedirectToAction("ImpressaoVenda");
                }
                else
                {
                    return RedirectToAction("CriarVenda");
                }
            }
            else
            {
                return RedirectToAction("CriarVenda");
            }
        }

        // POST: Venda/AlterarStatusVenda
        [HttpPost]
        [Route("Venda/AlterarStatusVenda")]
        public ActionResult AlterarStatusVenda(IFormCollection formCollection)
        {
            int idVenda = Convert.ToInt32(formCollection["idVenda"]);
            int idStatus = Convert.ToInt32(formCollection["id"]);
            _vendaBusiness.AtualizaStatusVenda(idVenda, idStatus);
            return RedirectToAction("MinhasVendas");
        }
    }
}
