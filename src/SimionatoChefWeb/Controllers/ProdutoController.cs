using Microsoft.AspNetCore.Mvc;
using SimionatoChefBusiness;
using SimionatoChefBusiness.Models.View;
using SimionatoChefDAO.Models;
using SimionatoChefWeb.Controllers;
using System;

namespace Simionato.PageControllers
{
    public class ProdutoController : Controller
    {
        private IProdutoBusiness _produtoBusiness;
        public ProdutoController(IProdutoBusiness produtoBusiness)
        {
            _produtoBusiness = produtoBusiness;
        }

        // GET: Produto
        public ActionResult Index()
        {
            return RedirectToAction("Produtos");
        }

        // GET: Produto/Produtos
        public ActionResult Produtos()
        {
            var usuarioLogado = HttpContext.Session.GetObject<Usuario>("UsuarioLogado");
            if (usuarioLogado != null)
            {
                ProdutoView ProdutoView = new ProdutoView()
                {
                    Usuario = (Usuario)usuarioLogado,
                    ListaProdutos = _produtoBusiness.ListaProduto()
                };
                return View("Produtos", ProdutoView);
            }
            else
            {
                return RedirectToAction("../Login");
            }
        }

        // GET: Produto/Buscar/5
        public ActionResult Buscar(int id)
        {
            ProdutoView ProdutoView = new ProdutoView()
            {
                Usuario = (Usuario)HttpContext.Session.GetObject<Usuario>("UsuarioLogado"),
                Produto = _produtoBusiness.ObterProduto(id),
                ListaProdutos = _produtoBusiness.ListaProduto()
            };
            return View("Produtos", ProdutoView);
        }

        // POST: Produto/Salvar
        [HttpPost]
        [Route("Produto/Salvar")]
        public ActionResult Salvar(Produto Produto)
        {
            try
            {
                if (_produtoBusiness.SalvarProduto(Produto))
                {
                    ViewBag.Sucesso = "Produto " + Produto.Nome + " Salvo com Sucesso!";
                }
                ProdutoView ProdutoView = new ProdutoView()
                {
                    Usuario = HttpContext.Session.GetObject<Usuario>("UsuarioLogado"),
                    ListaProdutos = _produtoBusiness.ListaProduto()
                };
                return View("Produtos", ProdutoView);
            }
            catch (Exception e)
            {
                ViewBag.Mensagem = e.Message;
                ProdutoView ProdutoView = new ProdutoView()
                {
                    Usuario = HttpContext.Session.GetObject<Usuario>("UsuarioLogado"),
                    ListaProdutos = _produtoBusiness.ListaProduto(),
                    Produto = Produto
                };
                return View("Produtos", ProdutoView);
            }
        }
    }
}
