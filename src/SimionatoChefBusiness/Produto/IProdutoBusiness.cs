using SimionatoChefDAO.Models;
using System.Collections.Generic;

namespace SimionatoChefBusiness
{
    public interface IProdutoBusiness
    {
        Produto ObterProduto(int idProduto);
        List<Produto> ObterProdutoVenda(int idVenda);
        List<Produto> ListaProduto();
        List<Produto> PesquisarProdutoNome(string nomeProduto);
        bool NovoProduto(Produto produto);
        bool AtualizaProduto(Produto produto);
        bool SalvarProduto(Produto produto);
    }
}
