using Microsoft.AspNetCore.Mvc;
using SimionatoChefBusiness;
using SimionatoChefDAO.Models;
using System;

namespace Simionato.Controllers
{
    [Route("api/[controller]")]
    public class VendaController : Controller
    {
        private IAPIBusiness _APIBusiness;
        private IVendaBusiness _vendaBusiness;
        private string token;

        public VendaController(IAPIBusiness apiBusiness, IVendaBusiness vendaBusiness)
        {
            _APIBusiness = apiBusiness;
            _vendaBusiness = vendaBusiness;
        }

        // Obtem todas as vendas
        [HttpGet]
        public dynamic Get()
        {
            try
            {
                token = HttpContext.Request?.Headers["token"];
                if (_APIBusiness.ValidarToken(token))
                {
                    return Ok(_vendaBusiness.ObterDetalheTodasVendas());
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

        // Obtem uma unica venda
        [HttpGet("{id}")]
        public dynamic Get(int id)
        {
            try
            {
                token = HttpContext.Request?.Headers["token"];
                if (_APIBusiness.ValidarToken(token))
                {
                    return Ok(_vendaBusiness.ObterDetalheVenda(id));
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

        //Obtem todas as vendas do dia informado
        [HttpGet("dataVenda/{data}")]
        public dynamic Get(DateTime data)
        {
            try
            {
                token = HttpContext.Request?.Headers["token"];
                if (_APIBusiness.ValidarToken(token))
                {
                    return Ok(_vendaBusiness.ListaVendaDia(data));
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

        //Obtem as vendas do periodo enviado
        [HttpGet("DataInicio/{dataInicio}/DataFim/{dataFim}")]
        public dynamic Get(DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                token = HttpContext.Request?.Headers["token"];
                if (_APIBusiness.ValidarToken(token))
                {
                    return Ok(_vendaBusiness.ListaPedidosEntreDias(dataInicio, dataFim));
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

        //Relatorio semanal de vendas
        [HttpGet]
        [Route("relatoriosemanal")]
        public dynamic RelatorioVendaSemanal()
        {
            return Ok(_vendaBusiness.RelatorioVendaSemanal());
        }

        //Criar Venda
        [HttpPost]
        public dynamic Post([FromBody]Venda venda)
        {
            try
            {
                token = HttpContext.Request?.Headers["token"];
                if (_APIBusiness.ValidarToken(token))
                {
                    return Created("true", _vendaBusiness.CriarVenda(venda));
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

        //Atualiza statis do pedido
        [HttpPut]
        [Route("AtualizaStatus/{id}/Status/{idStatus}")]
        public dynamic Put(int id, int idStatus)
        {
            try
            {
                token = HttpContext.Request?.Headers["token"];
                if (_APIBusiness.ValidarToken(token))
                {
                    return Ok(_vendaBusiness.AtualizaStatusVenda(id, idStatus));
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
