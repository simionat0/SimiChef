using SimionatoChefDAO.Models;
using System;
using System.Collections.Generic;

namespace SimionatoChefDAO
{
    public interface IProdutoDAO
    {
        Produto ObterProduto(int idProduto);
        List<Produto> ObterProdutoVenda(int idVenda);
        List<Produto> ListarProdutos();
        bool NovoProduto(Produto produto);
        bool AtualizaProduto(Produto produto);
        List<Produto> PesquisarProdutoNome(String nomeProduto);
        bool VerificaProdutoBase(int idProduto);
        int ContarProdutos();
    }
}
