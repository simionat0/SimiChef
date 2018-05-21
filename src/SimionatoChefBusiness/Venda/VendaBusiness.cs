using SimionatoChefBusiness.Models.View;
using SimionatoChefDAO;
using SimionatoChefDAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimionatoChefBusiness
{
    public class VendaBusiness : IVendaBusiness
    {
        private IVendaDAO _IVendaDAO;
        private IProdutoBusiness _IProdutoBusiness;
        private IClienteBusiness _IClienteBusiness;
        private IUsuarioBusiness _IUsuarioBusiness;

        public VendaBusiness(IVendaDAO objVenda, IProdutoBusiness objProduto, IClienteBusiness objCliente, IUsuarioBusiness objUsuario)
        {
            _IVendaDAO = objVenda;
            _IProdutoBusiness = objProduto;
            _IClienteBusiness = objCliente;
            _IUsuarioBusiness = objUsuario;
        }

        public Venda ObterDetalheVenda(int idVenda)
        {
            var venda = _IVendaDAO.ObterVenda(idVenda);
            venda.Produtos = _IProdutoBusiness.ObterProdutoVenda(venda.Id);
            return venda;
        }

        public List<Venda> ObterDetalheTodasVendas()
        {
            List<Venda> listaVendas = new List<Venda> { };
            for (int i = 1; i <= _IVendaDAO.IdUltimaVenda(); i++)
            {
                listaVendas.Add(ObterDetalheVenda(i));
            }
            return listaVendas;
        }

        public List<Venda> ObterDetalheVendas(int qtd, int pagina)
        {
            List<Venda> listaVendas = new List<Venda> { };
            for (int i = 1; i <= _IVendaDAO.IdUltimaVenda(); i++)
            {
                listaVendas.Add(ObterDetalheVenda(i));
            }
            return listaVendas;
        }

        public int IdUltimaVenda()
        {
            return _IVendaDAO.IdUltimaVenda();
        }

        public bool CriarVenda(Venda venda)
        {
            if (ValidaCriarVenda(venda) == true)
            {
                _IVendaDAO.CriarVenda(venda.Usuario, venda.Cliente);
                venda.Id = _IVendaDAO.IdUltimaVenda();
                for (int i = 0; i < venda.Produtos.Count; i++)
                {
                    _IVendaDAO.AdicionarProdutoVenda(venda.Produtos.ElementAt(i).Id,
                            venda.Produtos.ElementAt(i).Quantidade, venda.Produtos.ElementAt(i).Valor,
                            venda.Id);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Venda> ListaVendasDashboard(int Quantidade = 0)
        {
            List<Venda> ListaVendas = new List<Venda> { };

            if (Quantidade != 0)
            {
                for (int i = IdUltimaVenda(); i >= (IdUltimaVenda() - Quantidade); i--)
                {
                    ListaVendas.Add(ObterDetalheVendaDashboard(i));
                }
            }
            else
            {
                for (int i = IdUltimaVenda(); i > 0; i--)
                {
                    ListaVendas.Add(ObterDetalheVendaDashboard(i));
                }
            }
            return ListaVendas;
        }

        public bool AdicionarProdutoVenda(int idProduto, int idQuantidade, double Valor, int idVenda)
        {
            return _IVendaDAO.AdicionarProdutoVenda(idProduto, idQuantidade, Valor, idVenda);
        }

        public bool AtualizaStatusVenda(int idVenda, int idStatus)
        {
            return _IVendaDAO.AtualizaStatusVenda(idVenda, idStatus);
        }

        public List<Venda> ListaVendaDia(DateTime data)
        {
            var listVenda = _IVendaDAO.ListaVendaDia(data);

            for (int i = 0; i < listVenda.Count; i++)
            {
                listVenda.ElementAt(i).Produtos = _IProdutoBusiness.ObterProdutoVenda(listVenda.ElementAt(i).Id);
            }

            return listVenda;
        }

        public List<Venda> ListaPedidosEntreDias(DateTime dataInicio, DateTime dataFim)
        {
            var listVenda = _IVendaDAO.ListaPedidosEntreDias(dataInicio, dataFim);

            for (int i = 0; i < listVenda.Count; i++)
            {
                listVenda.ElementAt(i).Produtos = _IProdutoBusiness.ObterProdutoVenda(listVenda.ElementAt(i).Id);
            }

            return listVenda;
        }

        public List<VendaSemanal> RelatorioVendaSemanal()
        {
            return _IVendaDAO.RelatorioVendaSemanal();
        }

        public Venda ObterDetalheVendaDashboard(int idVenda)
        {
            var venda = _IVendaDAO.ObterVendaDashboard(idVenda);
            venda.Produtos = _IProdutoBusiness.ObterProdutoVenda(venda.Id);
            return venda;
        }

        public double CalculaTotalVenda(Venda venda)
        {
            venda.Total = 0;
            for (int i = 0; i < venda.Produtos.Count; i++)
            {
                venda.Total = venda.Total + (venda.Produtos[i].Quantidade * venda.Produtos[i].Valor);
            }
            return venda.Total;
        }

        public double CalculoTotalVendas(List<Venda> ListaVenda)
        {
            double Total = 0;
            for (int i = 0; i < ListaVenda.Count; i++)
            {
                Total = Total + ListaVenda[i].Total;
            }
            return Total;
        }

        public List<Status> ListaStatus()
        {
            return _IVendaDAO.ListaStatus();
        }

        public VendaView CriarVendaView(Usuario Usuario, Venda Venda, List<Cliente> ListaClientes, List<Produto> ListaProdutos)
        {
            VendaView VendaView = new VendaView()
            {
                Usuario = Usuario,
                Venda = Venda,
                ListaClientes = ListaClientes,
                ListaProdutos = ListaProdutos
            };
            VendaView.Venda.Usuario = Usuario;
            return VendaView;
        }

        // ------------------------------------------------------------------------------------------------------------------------

        public bool ValidaCriarVenda(Venda venda)
        {
            if (venda.Produtos.ElementAt(0).Id == 0)
            {
                return false;
            }
            else if (venda.Cliente.Id == 0)
            {
                return false;
            }
            else if (venda.Usuario.Id == 0)
            {
                return false;
            }
            else
            {
                if (VerificaDadosVenda(venda) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool VerificaDadosVenda(Venda venda)
        {
            if (_IClienteBusiness.VerificaClienteBase(venda.Cliente.Id) == false)
            {
                return false;
            }
            else if (_IUsuarioBusiness.VerificaUsuarioBase(venda.Usuario.Id) == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}