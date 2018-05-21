using SimionatoChefBusiness.Models.View;
using SimionatoChefDAO.Models;
using System;
using System.Collections.Generic;

namespace SimionatoChefBusiness
{
    public interface IVendaBusiness
    {
        Venda ObterDetalheVenda(int idVenda);
        List<Venda> ObterDetalheTodasVendas();
        List<Venda> ObterDetalheVendas(int qtd, int pagina);
        bool CriarVenda(Venda venda);
        List<Venda> ListaVendasDashboard(int quantidade = 0);
        bool AdicionarProdutoVenda(int idProduto, int idQuantidade, double valor, int idVenda);
        bool AtualizaStatusVenda(int idVenda, int idStatus);
        List<Venda> ListaVendaDia(DateTime data);
        List<Venda> ListaPedidosEntreDias(DateTime dataInicio, DateTime dataFim);
        List<VendaSemanal> RelatorioVendaSemanal();
        Venda ObterDetalheVendaDashboard(int idVenda);
        double CalculaTotalVenda(Venda venda);
        double CalculoTotalVendas(List<Venda> ListaVenda);
        List<Status> ListaStatus();
        VendaView CriarVendaView(Usuario Usuario, Venda Venda, List<Cliente> ListaClientes, List<Produto> ListaProdutos);
        int IdUltimaVenda();
    }
}
