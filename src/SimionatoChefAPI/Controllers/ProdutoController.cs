using Microsoft.AspNetCore.Mvc;
using SimionatoChefBusiness;
using SimionatoChefDAO.Models;
using System;

namespace Simionato.Controllers
{
    [Route("api/[controller]")]
    public class ProdutoController : Controller
    {
        private IAPIBusiness _APIBusiness;
        private IProdutoBusiness _produtoBusiness;
        private string token;

        public ProdutoController(IAPIBusiness apiBusiness, IProdutoBusiness produtoBusiness)
        {
            _APIBusiness = apiBusiness;
            _produtoBusiness = produtoBusiness;
        }

        [HttpGet]
        public dynamic Get()
        {
            try
            {
                token = HttpContext.Request?.Headers["token"];
                if (_APIBusiness.ValidarToken(token))
                {
                    return Ok(_produtoBusiness.ListaProduto());
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public dynamic Get(int id)
        {
            try
            {
                token = HttpContext.Request?.Headers["token"];
                if (_APIBusiness.ValidarToken(token))
                {
                    return Ok(_produtoBusiness.ObterProduto(id));
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public dynamic Post([FromBody]Produto Produto)
        {
            try
            {
                token = HttpContext.Request?.Headers["token"];
                if (_APIBusiness.ValidarToken(token))
                {
                    return Created("true", _produtoBusiness.NovoProduto(Produto));
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public dynamic Put([FromBody]Produto Produto)
        {
            try
            {
                token = HttpContext.Request?.Headers["token"];
                if (_APIBusiness.ValidarToken(token))
                {
                    return Ok(_produtoBusiness.AtualizaProduto(Produto));
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("pesquisar/{nome}")]
        public dynamic Get(string nome)
        {
            try
            {
                token = HttpContext.Request?.Headers["token"];
                if (_APIBusiness.ValidarToken(token))
                {
                    return Ok(_produtoBusiness.PesquisarProdutoNome(nome));
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
