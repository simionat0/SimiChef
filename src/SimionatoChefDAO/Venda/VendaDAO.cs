using MySql.Data.MySqlClient;
using SimionatoChefDAO.Factory;
using SimionatoChefDAO.Models;
using System;
using System.Collections.Generic;

namespace SimionatoChefDAO
{
    public class VendaDAO : DBConnect, IVendaDAO
    {

        //Obter Vendas
        public Venda ObterVenda(int idVenda)
        {
            string query = "SELECT * FROM detalhe_vendas where id_vendas = @IdVenda";
            var venda = new Venda();
            var usuario = new Usuario();
            var cliente = new Cliente();

            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@IdVenda", idVenda);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    venda.Id = dataReader.GetInt32(dataReader.GetOrdinal("id_vendas"));
                    venda.Datavenda = dataReader.GetDateTime(dataReader.GetOrdinal("data_vendas"));
                    venda.StatusVenda = dataReader.GetString(dataReader.GetOrdinal("nome_status_venda"));

                    usuario.Id = dataReader.GetInt32(dataReader.GetOrdinal("id_user"));
                    usuario.Nome = dataReader.GetString(dataReader.GetOrdinal("nome_user"));
                    usuario.Cargo = dataReader.GetString(dataReader.GetOrdinal("cargo_user"));
                    usuario.Telefone = dataReader.GetString(dataReader.GetOrdinal("telefone_user"));
                    usuario.Email = dataReader.GetString(dataReader.GetOrdinal("email_user"));
                    usuario.Status = dataReader.GetInt32(dataReader.GetOrdinal("status_ativo_user"));

                    cliente.Id = dataReader.GetInt32(dataReader.GetOrdinal("id_cli"));
                    cliente.Nome = dataReader.GetString(dataReader.GetOrdinal("nome_cli"));
                    cliente.Cep = dataReader.GetString(dataReader.GetOrdinal("cep_cli"));
                    cliente.Logradouro = dataReader.GetString(dataReader.GetOrdinal("logradouro_cli"));
                    cliente.Numero = dataReader.GetString(dataReader.GetOrdinal("logradouro_num_cli"));
                    cliente.Bairro = dataReader.GetString(dataReader.GetOrdinal("bairro_cli"));
                    cliente.Cidade = dataReader.GetString(dataReader.GetOrdinal("cidade_cli"));
                    cliente.Uf = dataReader.GetString(dataReader.GetOrdinal("uf_cli"));
                    cliente.Telefone = dataReader.GetString(dataReader.GetOrdinal("telefone_cli"));
                    cliente.Email = dataReader.GetString(dataReader.GetOrdinal("email_cli"));
                    cliente.Status = dataReader.GetInt32(dataReader.GetOrdinal("status_ativo_cli"));

                    venda.Total = dataReader.GetDouble(dataReader.GetOrdinal("total_venda"));

                    venda.Cliente = cliente;
                    venda.Usuario = usuario;
                }
                dataReader.Close();
                CloseConnection();
                return venda;
            }
            else
            {
                return venda;
            }
        }

        //Criar Nova Venda
        public bool CriarVenda(Usuario usuario, Cliente cliente)
        {
            string query = "CALL SP_Criar_Venda (@IdUsuario , @IdCliente , '1');";

            //Abre conexao com banco
            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@IdUsuario", usuario.Id);
                cmd.Parameters.AddWithValue("@IdCliente", cliente.Id);
                cmd.ExecuteNonQuery();
                CloseConnection();
                return true;
            }
            else
            {
                return false;
            }
        }

        //Adicionar produto na venda
        public bool AdicionarProdutoVenda(int idProduto, int idQuantidade, double valor, int idVenda)
        {
            string query = " CALL SP_InsertProdutoPedido(@IdProduto, @IdQuantidade, @Valor, @IdVenda)";
            //Abre conexao com banco
            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@IdProduto", idProduto);
                cmd.Parameters.AddWithValue("@IdQuantidade", idQuantidade);
                cmd.Parameters.AddWithValue("@Valor", valor);
                cmd.Parameters.AddWithValue("@IdVenda", idVenda);

                cmd.ExecuteNonQuery();
                CloseConnection();
                return true;
            }
            else
            {
                return false;
            }
        }

        //Obter ID da ultima venda
        public int IdUltimaVenda()
        {
            string query = "SELECT * FROM vendas ORDER BY id_vendas DESC LIMIT 1 ";
            int idVenda;
            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    idVenda = dataReader.GetInt32(dataReader.GetOrdinal("id_vendas"));
                }
                else
                {
                    idVenda = 0;
                }
                dataReader.Close();
                CloseConnection();
                return idVenda;
            }
            else
            {
                return 0;
            }
        }

        //Atualiza Status da Venda
        public bool AtualizaStatusVenda(int idVenda, int idStatus)
        {
            string query = @"UPDATE vendas SET id_status_venda = @IdStatusVenda 
                                WHERE id_vendas = @IdVenda ";
            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@IdVenda", idVenda);
                cmd.Parameters.AddWithValue("@IdStatusVenda", idStatus);
                cmd.ExecuteNonQuery();
                CloseConnection();
                return true;
            }
            else
            {
                return false;
            }
        }

        // Relatorio de vendas por dia
        public List<Venda> ListaVendaDia(DateTime data)
        {
            string dat = data.ToString("yyyy-MM-dd");
            string query = "CALL SP_ListaVendaDia(@Data)";
            List<Venda> listVenda = new List<Venda> { };

            var venda = new Venda();
            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Data", data);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    var usuario = new Usuario();
                    var cliente = new Cliente();
                    usuario.Id = dataReader.GetInt32(dataReader.GetOrdinal("id_user"));
                    usuario.Nome = dataReader.GetString(dataReader.GetOrdinal("nome_user"));
                    usuario.Cargo = dataReader.GetString(dataReader.GetOrdinal("cargo_user"));
                    usuario.Telefone = dataReader.GetString(dataReader.GetOrdinal("telefone_user"));
                    usuario.Email = dataReader.GetString(dataReader.GetOrdinal("email_user"));
                    usuario.Status = dataReader.GetInt32(dataReader.GetOrdinal("status_ativo_user"));

                    cliente.Id = dataReader.GetInt32(dataReader.GetOrdinal("id_cli"));
                    cliente.Nome = dataReader.GetString(dataReader.GetOrdinal("nome_cli"));
                    cliente.Cep = dataReader.GetString(dataReader.GetOrdinal("cep_cli"));
                    cliente.Logradouro = dataReader.GetString(dataReader.GetOrdinal("logradouro_cli"));
                    cliente.Numero = dataReader.GetString(dataReader.GetOrdinal("logradouro_num_cli"));
                    cliente.Bairro = dataReader.GetString(dataReader.GetOrdinal("bairro_cli"));
                    cliente.Cidade = dataReader.GetString(dataReader.GetOrdinal("cidade_cli"));
                    cliente.Uf = dataReader.GetString(dataReader.GetOrdinal("uf_cli"));
                    cliente.Telefone = dataReader.GetString(dataReader.GetOrdinal("telefone_cli"));
                    cliente.Email = dataReader.GetString(dataReader.GetOrdinal("email_cli"));
                    cliente.Status = dataReader.GetInt32(dataReader.GetOrdinal("status_ativo_cli"));

                    listVenda.Add(new Venda()
                    {
                        Id = dataReader.GetInt32(dataReader.GetOrdinal("id_vendas")),
                        Datavenda = dataReader.GetDateTime(dataReader.GetOrdinal("data_vendas")),
                        StatusVenda = dataReader.GetString(dataReader.GetOrdinal("nome_status_venda")),
                        Total = dataReader.GetDouble(dataReader.GetOrdinal("total")),
                        Cliente = cliente,
                        Usuario = usuario
                    });
                }
                dataReader.Close();
                CloseConnection();
                return listVenda;
            }
            else
            {
                return listVenda;
            }
        }

        // Relatorio de vendas entre dias
        public List<Venda> ListaPedidosEntreDias(DateTime dataInicio, DateTime dataFim)
        {
            string dataI = dataInicio.ToString("yyyy-MM-dd");
            string dataF = dataFim.ToString("yyyy-MM-dd");

            string query = "CALL SP_ListaPedidosEntreDias(@DataInicio, @DataFim)";
            List<Venda> listVenda = new List<Venda> { };

            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@DataInicio", dataInicio);
                cmd.Parameters.AddWithValue("@DataFim", dataFim);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    var usuario = new Usuario();
                    var cliente = new Cliente();
                    usuario.Id = dataReader.GetInt32(dataReader.GetOrdinal("id_user"));
                    usuario.Nome = dataReader.GetString(dataReader.GetOrdinal("nome_user"));
                    usuario.Cargo = dataReader.GetString(dataReader.GetOrdinal("cargo_user"));
                    usuario.Telefone = dataReader.GetString(dataReader.GetOrdinal("telefone_user"));
                    usuario.Email = dataReader.GetString(dataReader.GetOrdinal("email_user"));
                    usuario.Status = dataReader.GetInt32(dataReader.GetOrdinal("status_ativo_user"));

                    cliente.Id = dataReader.GetInt32(dataReader.GetOrdinal("id_cli"));
                    cliente.Nome = dataReader.GetString(dataReader.GetOrdinal("nome_cli"));
                    cliente.Cep = dataReader.GetString(dataReader.GetOrdinal("cep_cli"));
                    cliente.Logradouro = dataReader.GetString(dataReader.GetOrdinal("logradouro_cli"));
                    cliente.Numero = dataReader.GetString(dataReader.GetOrdinal("logradouro_num_cli"));
                    cliente.Bairro = dataReader.GetString(dataReader.GetOrdinal("bairro_cli"));
                    cliente.Cidade = dataReader.GetString(dataReader.GetOrdinal("cidade_cli"));
                    cliente.Uf = dataReader.GetString(dataReader.GetOrdinal("uf_cli"));
                    cliente.Telefone = dataReader.GetString(dataReader.GetOrdinal("telefone_cli"));
                    cliente.Email = dataReader.GetString(dataReader.GetOrdinal("email_cli"));
                    cliente.Status = dataReader.GetInt32(dataReader.GetOrdinal("status_ativo_cli"));

                    listVenda.Add(new Venda()
                    {
                        Id = dataReader.GetInt32(dataReader.GetOrdinal("id_vendas")),
                        Datavenda = dataReader.GetDateTime(dataReader.GetOrdinal("data_vendas")),
                        StatusVenda = dataReader.GetString(dataReader.GetOrdinal("nome_status_venda")),
                        Total = dataReader.GetDouble(dataReader.GetOrdinal("total")),
                        Cliente = cliente,
                        Usuario = usuario
                    });
                }
                dataReader.Close();
                CloseConnection();
                return listVenda;
            }
            else
            {
                return listVenda;
            }
        }

        //Obter Vendas
        public List<VendaSemanal> RelatorioVendaSemanal()
        {
            string query = "SELECT * FROM view_ultimos_pedidos;";
            List<VendaSemanal> vendas = new List<VendaSemanal> { };

            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection); ;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    vendas.Add(new VendaSemanal()
                    {
                        Data = dataReader.GetString(dataReader.GetOrdinal("data_vendas")),
                        Total = dataReader.GetString(dataReader.GetOrdinal("total"))
                    });

                }
                dataReader.Close();
                CloseConnection();
                return vendas;
            }
            else
            {
                return vendas;
            }
        }

        //Obter Vendas
        public Venda ObterVendaDashboard(int idVenda)
        {
            string query = "SELECT * FROM detalhe_vendas_dashboard where id_vendas = @IdVenda";
            var venda = new Venda();
            var usuario = new Usuario();
            var cliente = new Cliente();

            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@IdVenda", idVenda);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    venda.Id = dataReader.GetInt32(dataReader.GetOrdinal("id_vendas"));
                    venda.Datavenda = dataReader.GetDateTime(dataReader.GetOrdinal("data_vendas"));
                    venda.StatusVenda = dataReader.GetString(dataReader.GetOrdinal("nome_status_color"));

                    usuario.Id = dataReader.GetInt32(dataReader.GetOrdinal("id_user"));
                    usuario.Nome = dataReader.GetString(dataReader.GetOrdinal("nome_user"));
                    usuario.Cargo = dataReader.GetString(dataReader.GetOrdinal("cargo_user"));
                    usuario.Telefone = dataReader.GetString(dataReader.GetOrdinal("telefone_user"));
                    usuario.Email = dataReader.GetString(dataReader.GetOrdinal("email_user"));
                    usuario.Status = dataReader.GetInt32(dataReader.GetOrdinal("status_ativo_user"));

                    cliente.Id = dataReader.GetInt32(dataReader.GetOrdinal("id_cli"));
                    cliente.Nome = dataReader.GetString(dataReader.GetOrdinal("nome_cli"));
                    cliente.Cep = dataReader.GetString(dataReader.GetOrdinal("cep_cli"));
                    cliente.Logradouro = dataReader.GetString(dataReader.GetOrdinal("logradouro_cli"));
                    cliente.Numero = dataReader.GetString(dataReader.GetOrdinal("logradouro_num_cli"));
                    cliente.Bairro = dataReader.GetString(dataReader.GetOrdinal("bairro_cli"));
                    cliente.Cidade = dataReader.GetString(dataReader.GetOrdinal("cidade_cli"));
                    cliente.Uf = dataReader.GetString(dataReader.GetOrdinal("uf_cli"));
                    cliente.Telefone = dataReader.GetString(dataReader.GetOrdinal("telefone_cli"));
                    cliente.Email = dataReader.GetString(dataReader.GetOrdinal("email_cli"));
                    cliente.Status = dataReader.GetInt32(dataReader.GetOrdinal("status_ativo_cli"));

                    venda.Total = dataReader.GetDouble(dataReader.GetOrdinal("total_venda"));

                    venda.Cliente = cliente;
                    venda.Usuario = usuario;
                }
                dataReader.Close();
                CloseConnection();
                return venda;
            }
            else
            {
                return venda;
            }
        }

        //Obter Vendas
        public List<Status> ListaStatus()
        {
            string query = "SELECT * FROM status_venda;";
            List<Status> ListaStatus = new List<Status> { };

            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection); ;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    ListaStatus.Add(new Status()
                    {
                        Id = dataReader.GetInt32(dataReader.GetOrdinal("id_status_venda")),
                        Nome = dataReader.GetString(dataReader.GetOrdinal("nome_status_venda"))
                    });

                }
                dataReader.Close();
                CloseConnection();
                return ListaStatus;
            }
            else
            {
                return ListaStatus;
            }
        }

    }
}