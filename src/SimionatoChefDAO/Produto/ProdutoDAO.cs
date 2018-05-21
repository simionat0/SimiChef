using MySql.Data.MySqlClient;
using SimionatoChefDAO.Factory;
using System;
using System.Collections.Generic;
using SimionatoChefDAO.Models;

namespace SimionatoChefDAO
{
    public class ProdutoDAO : DBConnect, IProdutoDAO
    {

        //Obter Produtos
        public Produto ObterProduto(int idProduto)
        {
            string query = "SELECT * FROM produtos WHERE produtos.id_produtos = @IdProduto ";
            Produto Produto = new Produto();
            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@IdProduto", idProduto);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    Produto.Id = dataReader.GetInt32(dataReader.GetOrdinal("id_produtos"));
                    Produto.Nome = dataReader.GetString(dataReader.GetOrdinal("nome_produtos"));
                    Produto.Valor = dataReader.GetDouble(dataReader.GetOrdinal("valor_produtos"));
                    Produto.Quantidade = dataReader.GetInt32(dataReader.GetOrdinal("quantidade_produtos"));
                    Produto.Descricao = dataReader.GetString(dataReader.GetOrdinal("descritivo_produtos"));
                    Produto.Foto = dataReader.GetString(dataReader.GetOrdinal("foto_produtos"));
                    Produto.Categoria = dataReader.GetString(dataReader.GetOrdinal("tipo_produtos"));
                    Produto.Ativo = dataReader.GetString(dataReader.GetOrdinal("ativo_produtos"));
                }
                dataReader.Close();
                CloseConnection();
                return Produto;
            }
            else
            {
                return Produto;
            }
        }

        // Obter Produtos da Venda
        public List<Produto> ObterProdutoVenda(int idVenda)
        {
            string query = "select * from detalhe_produto_venda where id_vendas = @IdVenda";
            List<Produto> listProduto = new List<Produto> { };
            Produto Produto = new Produto();
            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@IdVenda", idVenda);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    listProduto.Add(new Produto()
                    {
                        Id = dataReader.GetInt32(dataReader.GetOrdinal("id_produtos")),
                        Nome = dataReader.GetString(dataReader.GetOrdinal("nome_produtos")),
                        Valor = dataReader.GetDouble(dataReader.GetOrdinal("valor_venda")),
                        Quantidade = dataReader.GetInt32(dataReader.GetOrdinal("qtd_item_lista")),
                        Descricao = dataReader.GetString(dataReader.GetOrdinal("descritivo_produtos")),
                        Foto = dataReader.GetString(dataReader.GetOrdinal("foto_produtos")),
                        Categoria = dataReader.GetString(dataReader.GetOrdinal("tipo_produtos")),
                        Ativo = dataReader.GetString(dataReader.GetOrdinal("ativo_produtos"))
                    });
                }
                dataReader.Close();
                CloseConnection();
                return listProduto;
            }
            else
            {
                return listProduto;
            }
        }

        //Lista Todos os Produtos
        public List<Produto> ListarProdutos()
        {
            string query = "SELECT * FROM produtos;";
            List<Produto> listProduto = new List<Produto> { };
            Produto Produto = new Produto();
            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    listProduto.Add(new Produto()
                    {
                        Id = dataReader.GetInt32(dataReader.GetOrdinal("id_produtos")),
                        Nome = dataReader.GetString(dataReader.GetOrdinal("nome_produtos")),
                        Valor = dataReader.GetDouble(dataReader.GetOrdinal("valor_produtos")),
                        Quantidade = dataReader.GetInt32(dataReader.GetOrdinal("quantidade_produtos")),
                        Descricao = dataReader.GetString(dataReader.GetOrdinal("descritivo_produtos")),
                        Foto = dataReader.GetString(dataReader.GetOrdinal("foto_produtos")),
                        Categoria = dataReader.GetString(dataReader.GetOrdinal("tipo_produtos")),
                        Ativo = dataReader.GetString(dataReader.GetOrdinal("ativo_produtos"))
                    });
                }
                dataReader.Close();
                CloseConnection();
                return listProduto;
            }
            else
            {
                return listProduto;
            }
        }

        //Insere Novo Produto
        public bool NovoProduto(Produto produto)
        {
            string query = "CALL SP_Salvar_Produto (@Nome, @Valor, @Quantidade, @Descricao, @Ativo);";

            //Abre conexao com banco
            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                cmd.Parameters.AddWithValue("@Valor", produto.Valor);
                cmd.Parameters.AddWithValue("@Quantidade", produto.Quantidade);
                cmd.Parameters.AddWithValue("@Descricao", produto.Descricao);
                cmd.Parameters.AddWithValue("@Ativo", 1);
                cmd.ExecuteNonQuery();
                CloseConnection();
                return true;
            }
            else
            {
                return false;
            }
        }

        //Atualiza 
        public bool AtualizaProduto(Produto produto)
        {
            string query = @"UPDATE produtos SET
                nome_produtos= @Nome, 
                valor_produtos= @Valor,
                quantidade_produtos= @Quantidade,
                descritivo_produtos= @Descricao,
                tipo_produtos= @Categoria
                WHERE id_produtos = @IdProduto";

            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                cmd.Parameters.AddWithValue("@Valor", produto.Valor);
                cmd.Parameters.AddWithValue("@Quantidade", produto.Quantidade);
                cmd.Parameters.AddWithValue("@Descricao", produto.Descricao);
                cmd.Parameters.AddWithValue("@Categoria", produto.Categoria);
                cmd.Parameters.AddWithValue("@IdProduto", produto.Id);
                cmd.CommandText = query;
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
                CloseConnection();
                return true;
            }
            else
            {
                return false;
            }
        }

        // Pesquisa produtos por nome
        public List<Produto> PesquisarProdutoNome(String nomeProduto)
        {
            string query = "CALL SP_BuscaProduto(@Nome) ;";
            List<Produto> listProduto = new List<Produto> { };
            var Produto = new Produto();
            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Nome", nomeProduto);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    listProduto.Add(new Produto()
                    {
                        Id = dataReader.GetInt32(dataReader.GetOrdinal("id_produtos")),
                        Nome = dataReader.GetString(dataReader.GetOrdinal("nome_produtos")),
                        Valor = dataReader.GetDouble(dataReader.GetOrdinal("valor_produtos")),
                        Quantidade = dataReader.GetInt32(dataReader.GetOrdinal("quantidade_produtos")),
                        Descricao = dataReader.GetString(dataReader.GetOrdinal("descritivo_produtos")),
                        Foto = dataReader.GetString(dataReader.GetOrdinal("foto_produtos")),
                        Categoria = dataReader.GetString(dataReader.GetOrdinal("tipo_produtos")),
                        Ativo = dataReader.GetString(dataReader.GetOrdinal("ativo_produtos"))
                    });
                }
                dataReader.Close();
                CloseConnection();
                return listProduto;
            }
            else
            {
                return listProduto;
            }
        }

        //Verifica os produtos na base
        public bool VerificaProdutoBase(int idProduto)
        {
            string query = "SELECT * FROM produtos WHERE produtos.id_produtos = @IdProduto";
            bool retorno = false;
            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@IdProduto", idProduto);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    retorno = true;
                }
                else
                {
                    retorno = false;
                }
                dataReader.Close();
                CloseConnection();
                return retorno;
            }
            else
            {
                return retorno;
            }
        }

        //Count
        public int ContarProdutos()
        {
            string query = "SELECT Count(*) FROM produto";
            int Count = -1;
            //Open Connection
            if (OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");
                CloseConnection();
                return Count;
            }
            else
            {
                return Count;
            }
        }

    }
}