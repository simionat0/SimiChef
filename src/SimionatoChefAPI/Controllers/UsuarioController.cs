using Microsoft.AspNetCore.Mvc;
using SimionatoChefBusiness;
using SimionatoChefDAO.Models;
using System;

namespace Simionato.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private IAPIBusiness _APIBusiness;
        private IUsuarioBusiness _usuarioBusiness;
        private string token;

        public UsuarioController(IAPIBusiness apiBusiness, IUsuarioBusiness usuarioBusiness)
        {
            _APIBusiness = apiBusiness;
            _usuarioBusiness = usuarioBusiness;
        }

        [HttpGet]
        public dynamic Get()
        {
            try
            {
                token = HttpContext.Request?.Headers["token"];
                if (_APIBusiness.ValidarToken(token))
                {
                    return Ok(_usuarioBusiness.ListaUsuarios());
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
                    return Ok(_usuarioBusiness.ObterUsuario(id));
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
        public dynamic Post([FromBody]Usuario usuario)
        {
            try
            {
                token = HttpContext.Request?.Headers["token"];
                if (_APIBusiness.ValidarToken(token))
                {
                    return Ok(_usuarioBusiness.NovoUsuario(usuario));
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
        [Route("login")]
        public dynamic Logon([FromBody]Usuario usuario)
        {
            try
            {
                token = HttpContext.Request?.Headers["token"];
                if (_APIBusiness.ValidarToken(token))
                {
                    var usuarioLogado = _usuarioBusiness.LoginUsuario(usuario);
                    if (usuarioLogado != null)
                    {
                        return Ok(usuarioLogado);
                    }
                    else
                    {
                        return NotFound(new Error().ErroUsuarioNotLogado());
                    }
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
        public dynamic Put([FromBody]Usuario usuario)
        {
            try
            {
                token = HttpContext.Request?.Headers["token"];
                if (_APIBusiness.ValidarToken(token))
                {
                    return Ok(_usuarioBusiness.AtualizaUsuario(usuario));
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
                    return Ok(_usuarioBusiness.PesquisarUsuarioNome(nome));
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