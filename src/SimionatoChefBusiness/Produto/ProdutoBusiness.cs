using SimionatoChefDAO;
using SimionatoChefDAO.Models;
using System;
using System.Collections.Generic;

namespace SimionatoChefBusiness
{
    public class ProdutoBusiness : IProdutoBusiness
    {
        private IProdutoDAO _IProdutoDAO;

        public ProdutoBusiness(IProdutoDAO obj)
        {
            _IProdutoDAO = obj;
        }

        public Produto ObterProduto(int idProduto)
        {
            return _IProdutoDAO.ObterProduto(idProduto);
        }

        public List<Produto> ObterProdutoVenda(int idVenda)
        {
            return _IProdutoDAO.ObterProdutoVenda(idVenda);
        }

        public List<Produto> ListaProduto()
        {
            return _IProdutoDAO.ListarProdutos();
        }

        public List<Produto> PesquisarProdutoNome(string nomeProduto)
        {
            return _IProdutoDAO.PesquisarProdutoNome(nomeProduto);
        }

        public bool NovoProduto(Produto produto)
        {
            if (ValidaRegistroProduto(produto))
            {
                return _IProdutoDAO.NovoProduto(produto);
            }
            else
            {
                return false;
            }
        }

        public bool AtualizaProduto(Produto produto)
        {
            if (ValidaRegistroProduto(produto))
            {
                if (VerificaProdutoBase(produto.Id))
                {
                    return _IProdutoDAO.AtualizaProduto(produto);
                }
                else
                {
                    return NovoProduto(produto);
                }
            }
            else
            {
                return false;
            }
        }

        public bool SalvarProduto(Produto produto)
        {
            if (produto.Id != 0)
            {
                return AtualizaProduto(produto);
            }
            else
            {
                return NovoProduto(produto);
            }
        }

        // ----------------------------------------------------------------------


        public bool VerificaProdutoBase(int idProduto)
        {
            return _IProdutoDAO.VerificaProdutoBase(idProduto);
        }

        public bool VerificaEstoqueProduto(int idProduto, int qtd)
        {
            Produto produto = _IProdutoDAO.ObterProduto(idProduto);
            if (produto.Quantidade <= qtd)
            {
                return true;
            }
            else
            {
                throw new Exception("O produto não possui estoque disponivel");
            }
        }

        public bool ValidaRegistroProduto(Produto produto)
        {
            if (produto.Nome == null)
            {
                throw new Exception("Nome do Produto não enviado");
            }
            else if (produto.Valor == 0)
            {
                throw new Exception("Valor do produto não foi enviado");
            }
            else if (produto.Quantidade == 0)
            {
                throw new Exception("Quantidade do produto não foi enviado");
            }
            else if (produto.Descricao == null)
            {
                throw new Exception("Descrição do produto não foi enviado");
            }
            else if (produto.Categoria == null)
            {
                throw new Exception("A categoria do produto não foi enviado");
            }
            else
            {
                return true;
            }
        }


    }
}