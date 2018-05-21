using MySql.Data.MySqlClient;
using SimionatoChefDAO.Factory;
using SimionatoChefDAO.Models;
using System;
using System.Collections.Generic;

namespace SimionatoChefDAO
{
    public class ClienteDAO : DBConnect, IClienteDAO
    {
        //Obter Clientes
        public Cliente ObterCliente(int idCliente)
        {
            string query = "SELECT * FROM Cliente where id_cli = @IdCliente ";
            Cliente Cliente = new Cliente();
            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@IdCliente", idCliente);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    Cliente.Id = dataReader.GetInt32(dataReader.GetOrdinal("id_cli"));
                    Cliente.Nome = dataReader.GetString(dataReader.GetOrdinal("nome_cli"));
                    Cliente.Cep = dataReader.GetString(dataReader.GetOrdinal("cep_cli"));
                    Cliente.Logradouro = dataReader.GetString(dataReader.GetOrdinal("logradouro_cli"));
                    Cliente.Numero = dataReader.GetString(dataReader.GetOrdinal("logradouro_num_cli"));
                    Cliente.Bairro = dataReader.GetString(dataReader.GetOrdinal("bairro_cli"));
                    Cliente.Cidade = dataReader.GetString(dataReader.GetOrdinal("cidade_cli"));
                    Cliente.Uf = dataReader.GetString(dataReader.GetOrdinal("uf_cli"));
                    Cliente.Telefone = dataReader.GetString(dataReader.GetOrdinal("telefone_cli"));
                    Cliente.Email = dataReader.GetString(dataReader.GetOrdinal("email_cli"));
                    Cliente.Status = dataReader.GetInt32(dataReader.GetOrdinal("status_ativo_cli"));
                }
                dataReader.Close();
                CloseConnection();
                return Cliente;
            }
            else
            {
                return Cliente;
            }
        }

        public Cliente ObterClienteCompleto(int idCliente)
        {
            string query = "SELECT * FROM Cliente where id_cli = @IdCliente ";
            Cliente Cliente = new Cliente();
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@IdCliente", idCliente);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    Cliente.Id = dataReader.GetInt32(dataReader.GetOrdinal("id_cli"));
                    Cliente.Nome = dataReader.GetString(dataReader.GetOrdinal("nome_cli"));
                    Cliente.Cep = dataReader.GetString(dataReader.GetOrdinal("cep_cli"));
                    Cliente.Logradouro = dataReader.GetString(dataReader.GetOrdinal("logradouro_cli"));
                    Cliente.Numero = dataReader.GetString(dataReader.GetOrdinal("logradouro_num_cli"));
                    Cliente.Bairro = dataReader.GetString(dataReader.GetOrdinal("bairro_cli"));
                    Cliente.Cidade = dataReader.GetString(dataReader.GetOrdinal("cidade_cli"));
                    Cliente.Uf = dataReader.GetString(dataReader.GetOrdinal("uf_cli"));
                    Cliente.Telefone = dataReader.GetString(dataReader.GetOrdinal("telefone_cli"));
                    Cliente.Email = dataReader.GetString(dataReader.GetOrdinal("email_cli"));
                    Cliente.Senha = dataReader.GetString(dataReader.GetOrdinal("senha_cli"));
                    Cliente.Status = dataReader.GetInt32(dataReader.GetOrdinal("status_ativo_cli"));
                }
                dataReader.Close();
                CloseConnection();
                return Cliente;
            }
            else
            {
                return Cliente;
            }
        }

        //Lista Clientes
        public List<Cliente> ListarClientes()
        {
            string query = "SELECT * FROM Cliente";
            List<Cliente> listCliente = new List<Cliente> { };
            Cliente Cliente = new Cliente();
            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    listCliente.Add(new Cliente()
                    {
                        Id = dataReader.GetInt32(dataReader.GetOrdinal("id_cli")),
                        Nome = dataReader.GetString(dataReader.GetOrdinal("nome_cli")),
                        Cep = dataReader.GetString(dataReader.GetOrdinal("cep_cli")),
                        Logradouro = dataReader.GetString(dataReader.GetOrdinal("logradouro_cli")),
                        Numero = dataReader.GetString(dataReader.GetOrdinal("logradouro_num_cli")),
                        Bairro = dataReader.GetString(dataReader.GetOrdinal("bairro_cli")),
                        Cidade = dataReader.GetString(dataReader.GetOrdinal("cidade_cli")),
                        Uf = dataReader.GetString(dataReader.GetOrdinal("uf_cli")),
                        Telefone = dataReader.GetString(dataReader.GetOrdinal("telefone_cli")),
                        Email = dataReader.GetString(dataReader.GetOrdinal("email_cli")),
                        Status = dataReader.GetInt32(dataReader.GetOrdinal("status_ativo_cli"))
                    });
                }
                dataReader.Close();
                CloseConnection();
                return listCliente;
            }
            else
            {
                return listCliente;
            }
        }

        //Insere Novo Cliente
        public bool NovoCliente(Cliente cliente)
        {
            string query = @"CALL SP_Salvar_Cliente(
                @Nome, @Cep, @Logradouro,
                @Numero, @Bairro, @Cidade,
                @Uf, @Telefone, @Email,
                @Senha);";

            //Abre conexao com banco
            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@Cep", cliente.Cep);
                cmd.Parameters.AddWithValue("@Logradouro", cliente.Logradouro);
                cmd.Parameters.AddWithValue("@Numero", cliente.Numero);
                cmd.Parameters.AddWithValue("@Bairro", cliente.Bairro);
                cmd.Parameters.AddWithValue("@Cidade", cliente.Cidade);
                cmd.Parameters.AddWithValue("@Uf", cliente.Uf);
                cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                cmd.Parameters.AddWithValue("@Email", cliente.Email);
                cmd.Parameters.AddWithValue("@Senha", cliente.Senha);
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
        public bool AtualizaCliente(Cliente cliente)
        {
            string query = @" UPDATE Cliente SET
                nome_cli = @Nome , 
                cep_cli = @Cep , 
                logradouro_cli = @Logradouro , 
                logradouro_num_cli = @Numero , 
                bairro_cli = @Bairro , 
                cidade_cli = @Cidade , 
                uf_cli = @Uf , 
                telefone_cli = @Telefone , 
                email_cli = @Email , 
                senha_cli = MD5(@Senha)
                WHERE id_cli = @IdCliente ";

            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@Cep", cliente.Cep);
                cmd.Parameters.AddWithValue("@Logradouro", cliente.Logradouro);
                cmd.Parameters.AddWithValue("@Numero", cliente.Numero);
                cmd.Parameters.AddWithValue("@Bairro", cliente.Bairro);
                cmd.Parameters.AddWithValue("@Cidade", cliente.Cidade);
                cmd.Parameters.AddWithValue("@Uf", cliente.Uf);
                cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                cmd.Parameters.AddWithValue("@Email", cliente.Email);
                cmd.Parameters.AddWithValue("@Senha", cliente.Senha);
                cmd.Parameters.AddWithValue("@IdCliente", cliente.Id);
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

        //Excluir 
        public bool ExcluirCliente(Cliente cliente)
        {
            string query = "DELETE FROM cliente WHERE id_cli = @IdCliente ";

            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Parameters.AddWithValue("@IdCliente", cliente.Id);
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

        //Lista Clientes
        public List<Cliente> PesquisarClienteNome(String nomeCliente)
        {
            string query = "CALL SP_BuscaCliente(@Nome)";
            List<Cliente> listCliente = new List<Cliente> { };
            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Nome", nomeCliente);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                try
                {
                    while (dataReader.Read())
                    {
                        var cliente = new Cliente()
                        {
                            Id = dataReader.GetInt32(dataReader.GetOrdinal("id_cli")),
                            Nome = dataReader.GetString(dataReader.GetOrdinal("nome_cli")),
                            Cep = dataReader.GetString(dataReader.GetOrdinal("cep_cli")),
                            Logradouro = dataReader.GetString(dataReader.GetOrdinal("logradouro_cli")),
                            Numero = dataReader.GetString(dataReader.GetOrdinal("logradouro_num_cli")),
                            Bairro = dataReader.GetString(dataReader.GetOrdinal("bairro_cli")),
                            Cidade = dataReader.GetString(dataReader.GetOrdinal("cidade_cli")),
                            Uf = dataReader.GetString(dataReader.GetOrdinal("uf_cli")),
                            Telefone = dataReader.GetString(dataReader.GetOrdinal("telefone_cli")),
                            Email = dataReader.GetString(dataReader.GetOrdinal("email_cli")),
                            Status = dataReader.GetInt32(dataReader.GetOrdinal("status_ativo_cli"))
                        };
                        listCliente.Add(cliente);
                    }
                    dataReader.Close();
                    CloseConnection();
                    return listCliente;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            else
            {
                return listCliente;
            }
        }

        //Verificar se o cliente esta na base
        public bool VerificaClienteBase(int idCliente)
        {
            string query = "SELECT * FROM Cliente where id_cli = @IdCliente ";
            bool retorno = false;
            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@IdCliente", idCliente);
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
        public int ContarClientes()
        {
            string query = "SELECT Count(*) FROM cliente";
            int Count = -1;
            //Open Connection
            if (OpenConnection())
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