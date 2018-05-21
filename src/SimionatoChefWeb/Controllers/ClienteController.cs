using Microsoft.AspNetCore.Mvc;
using SimionatoChefBusiness;
using SimionatoChefBusiness.Models.View;
using SimionatoChefDAO.Models;
using SimionatoChefWeb.Controllers;
using System;

namespace Simionato.PageControllers
{
    public class ClienteController : Controller
    {
        private IClienteBusiness _clienteBusiness;
        public ClienteController(IClienteBusiness clienteBusiness)
        {
            _clienteBusiness = clienteBusiness;
        }

        public ActionResult Index()
        {
            return RedirectToAction("Clientes");
        }

        // GET: Cliente/Clientes
        public ActionResult Clientes()
        {
            try
            {
                var usuarioLogado = HttpContext.Session.GetObject<Usuario>("UsuarioLogado");
                if (usuarioLogado != null)
                {
                    ClienteView clienteView = new ClienteView()
                    {
                        Usuario = (Usuario)usuarioLogado,
                        ListaCliente = _clienteBusiness.ListaCliente()
                    };
                    return View("Clientes", clienteView);
                }
                else
                {
                    return RedirectToAction("../Login");
                }
            }
            catch (Exception e)
            {
                ViewBag.Mensagem = e.Message;
                return RedirectToAction("Index");
            }
        }

        // GET: Cliente/Buscar/5
        public ActionResult Buscar(int id)
        {
            try
            {
                ClienteView clienteView = new ClienteView()
                {
                    Usuario = (Usuario)HttpContext.Session.GetObject<Usuario>("UsuarioLogado"),
                    Cliente = _clienteBusiness.ObterClienteCompleto(id),
                    ListaCliente = _clienteBusiness.ListaCliente()
                };
                return View("Clientes", clienteView);
            }
            catch (Exception e)
            {
                ViewBag.Mensagem = e.Message;
                return RedirectToAction("Index");
            }
        }

        // POST: Cliente/Salvar
        [HttpPost]
        [Route("Cliente/Salvar")]
        public ActionResult Salvar(Cliente cliente)
        {
            try
            {
                if (_clienteBusiness.SalvarCliete(cliente))
                {
                    ViewBag.Sucesso = "Cliente " + cliente.Nome + " Salvo com Sucesso!";
                }
                ClienteView clienteView = new ClienteView()
                {
                    Usuario = (Usuario)HttpContext.Session.GetObject<Usuario>("UsuarioLogado"),
                    ListaCliente = _clienteBusiness.ListaCliente()
                };
                return View("Clientes", clienteView);
            }
            catch (Exception e)
            {
                ClienteView clienteView = new ClienteView()
                {
                    Usuario = (Usuario)HttpContext.Session.GetObject<Usuario>("UsuarioLogado"),
                    ListaCliente = _clienteBusiness.ListaCliente()
                };
                ViewBag.Mensagem = e.Message;
                return View("Clientes", clienteView);
            }
        }
    }
}
