using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimionatoChefBusiness;
using SimionatoChefDAO.Models;
using System;

namespace SimionatoChefAPI.Controllers
{
    [Route("api/[controller]")]
    public class ClienteController : Controller
    {
        private IAPIBusiness _APIBusiness;
        private IClienteBusiness _clienteBusiness;
        private string token;


        public ClienteController(IAPIBusiness apiBusiness, IClienteBusiness clienteBusiness)
        {
            _APIBusiness = apiBusiness;
            _clienteBusiness = clienteBusiness;
        }

        [HttpGet]
        public dynamic Get()
        {
            try
            {
                token = HttpContext.Request?.Headers["token"];
                if (_APIBusiness.ValidarToken(token))
                {
                    return Ok(_clienteBusiness.ListaCliente());
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
                    return Ok(_clienteBusiness.ObterCliente(id));
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
        public dynamic Post([FromBody]Cliente cliente)
        {
            try
            {
                token = HttpContext.Request?.Headers["token"];
                if (_APIBusiness.ValidarToken(token))
                {
                    return Created("true", _clienteBusiness.NovoCliente(cliente));
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
        public dynamic Put([FromBody]Cliente Cliente)
        {
            try
            {
                token = HttpContext.Request?.Headers["token"];
                if (_APIBusiness.ValidarToken(token))
                {
                    return Ok(_clienteBusiness.AtualizaCliente(Cliente));
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
                    return Ok(_clienteBusiness.PesquisarClienteNome(nome));
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
