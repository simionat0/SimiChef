using SimionatoChefDAO.Models;
using System;
using System.Collections.Generic;

namespace SimionatoChefDAO
{
    public interface IVendaDAO
    {
        Venda ObterVenda(int idVenda);
        bool CriarVenda(Usuario usuario, Cliente cliente);
        bool AdicionarProdutoVenda(int idProduto, int idQuantidade, double valor, int idVenda);
        bool AtualizaStatusVenda(int idVenda, int idStatus);
        List<Venda> ListaVendaDia(DateTime data);
        List<Venda> ListaPedidosEntreDias(DateTime dataInicio, DateTime dataFim);
        List<VendaSemanal> RelatorioVendaSemanal();
        Venda ObterVendaDashboard(int idVenda);
        List<Status> ListaStatus();
        int IdUltimaVenda();
    }
}
