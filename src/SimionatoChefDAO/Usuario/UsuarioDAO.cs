using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using SimionatoChefDAO.Factory;
using SimionatoChefDAO.Models;

namespace SimionatoChefDAO
{
    public class UsuarioDAO : DBConnect, IUsuarioDAO
    {

        //Obter Usuarios
        public Usuario ObterUsuario(int idUsuario)
        {
            string query = "SELECT * FROM usuario where id_user = @IdUsuario";
            Usuario usuario = new Usuario();
            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    usuario.Id = dataReader.GetInt32(dataReader.GetOrdinal("id_user"));
                    usuario.Nome = dataReader.GetString(dataReader.GetOrdinal("nome_user"));
                    usuario.Cargo = dataReader.GetString(dataReader.GetOrdinal("cargo_user"));
                    usuario.Telefone = dataReader.GetString(dataReader.GetOrdinal("telefone_user"));
                    usuario.Email = dataReader.GetString(dataReader.GetOrdinal("email_user"));
                    usuario.Status = dataReader.GetInt32(dataReader.GetOrdinal("status_ativo_user"));
                }
                dataReader.Close();
                CloseConnection();
                return usuario;
            }
            else
            {
                return usuario;
            }
        }

        //Obter Usuario com senha
        public Usuario ObterUsuarioCompleto(int idUsuario)
        {
            string query = "SELECT * FROM usuario where id_user = @IdUsuario";
            Usuario usuario = new Usuario();
            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    usuario.Id = dataReader.GetInt32(dataReader.GetOrdinal("id_user"));
                    usuario.Nome = dataReader.GetString(dataReader.GetOrdinal("nome_user"));
                    usuario.Cargo = dataReader.GetString(dataReader.GetOrdinal("cargo_user"));
                    usuario.Telefone = dataReader.GetString(dataReader.GetOrdinal("telefone_user"));
                    usuario.Email = dataReader.GetString(dataReader.GetOrdinal("email_user"));
                    usuario.Status = dataReader.GetInt32(dataReader.GetOrdinal("status_ativo_user"));
                    usuario.Senha = dataReader.GetString(dataReader.GetOrdinal("senha_user"));
                }
                dataReader.Close();
                CloseConnection();
                return usuario;
            }
            else
            {
                return usuario;
            }
        }

        //Lista Usuarios
        public List<Usuario> ListarUsuarios()
        {
            string query = "SELECT * FROM usuario";
            List<Usuario> listUsuario = new List<Usuario> { };
            Usuario usuario = new Usuario();
            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    listUsuario.Add(new Usuario()
                    {
                        Id = dataReader.GetInt32(dataReader.GetOrdinal("id_user")),
                        Nome = dataReader.GetString(dataReader.GetOrdinal("nome_user")),
                        Cargo = dataReader.GetString(dataReader.GetOrdinal("cargo_user")),
                        Telefone = dataReader.GetString(dataReader.GetOrdinal("telefone_user")),
                        Email = dataReader.GetString(dataReader.GetOrdinal("email_user")),
                        Status = dataReader.GetInt32(dataReader.GetOrdinal("status_ativo_user"))
                    });
                }
                dataReader.Close();
                CloseConnection();
                return listUsuario;
            }
            else
            {
                return listUsuario;
            }
        }

        //Insere Novo Usuario
        public bool NovoUsuario(Usuario usuario)
        {
            string query = "CALL SP_Salvar_Usuario (@Nome, @Cargo, @Telefone, @Email, @Senha);";

            //Abre conexao com banco
            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@Cargo", usuario.Cargo);
                cmd.Parameters.AddWithValue("@Telefone", usuario.Telefone);
                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                cmd.Parameters.AddWithValue("@Senha", usuario.Senha);
                cmd.ExecuteNonQuery();
                CloseConnection();
                return true;
            }
            else
            {
                return false;
            }
        }

        //Login Usuario
        public Usuario LoginUsuario(Usuario usuario)
        {
            string query = "CALL SP_LoginUsuario(@Email, @Senha );";

            //Abre conexao com banco
            if (OpenConnection())
            {
                var usuarioLogado = new Usuario();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                cmd.Parameters.AddWithValue("@Senha", usuario.Senha);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    usuarioLogado.Id = dataReader.GetInt32(dataReader.GetOrdinal("id_user"));
                    usuarioLogado.Nome = dataReader.GetString(dataReader.GetOrdinal("nome_user"));
                    usuarioLogado.Cargo = dataReader.GetString(dataReader.GetOrdinal("cargo_user"));
                    usuarioLogado.Telefone = dataReader.GetString(dataReader.GetOrdinal("telefone_user"));
                    usuarioLogado.Email = dataReader.GetString(dataReader.GetOrdinal("email_user"));
                    usuarioLogado.Status = dataReader.GetInt32(dataReader.GetOrdinal("status_ativo_user"));
                }
                else
                {
                    return null;
                }
                dataReader.Close();
                CloseConnection();
                return usuarioLogado;
            }
            else
            {
                throw new Exception(new Error().ErroUsuarioNotLogado().Menssage);
            }
        }

        //Atualiza 
        public bool AtualizaUsuario(Usuario usuario)
        {
            string query = "CALL SP_AtualizaUsuario(@Nome, @Cargo, @Telefone, @Email, @Senha, @IdUsuario)";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@Cargo", usuario.Cargo);
                cmd.Parameters.AddWithValue("@Telefone", usuario.Telefone);
                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                cmd.Parameters.AddWithValue("@Senha", usuario.Senha);
                cmd.Parameters.AddWithValue("@IdUsuario", usuario.Id);
                cmd.CommandText = query;
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                return true;
            }
            else
            {
                return false;
            }
        }

        //Pesquisar usuarios por nome
        public List<Usuario> PesquisarUsuarioNome(String nomeUsuario)
        {
            string query = "CALL SP_BuscaUsuario(@Nome)";
            List<Usuario> listUsuario = new List<Usuario> { };
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Nome", nomeUsuario);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    Usuario usuario = new Usuario()
                    {
                        Id = dataReader.GetInt32(dataReader.GetOrdinal("id_user")),
                        Nome = dataReader.GetString(dataReader.GetOrdinal("nome_user")),
                        Cargo = dataReader.GetString(dataReader.GetOrdinal("cargo_user")),
                        Telefone = dataReader.GetString(dataReader.GetOrdinal("telefone_user")),
                        Email = dataReader.GetString(dataReader.GetOrdinal("email_user")),
                        Status = dataReader.GetInt32(dataReader.GetOrdinal("status_ativo_user"))
                    };
                    listUsuario.Add(usuario);
                }
                dataReader.Close();
                this.CloseConnection();
                return listUsuario;
            }
            else
            {
                return listUsuario;
            }
        }

        //Verifica usuario na base
        public bool VerificaUsuarioBase(int idUsuario)
        {
            bool retorno = false;
            string query = "SELECT * FROM usuario WHERE usuario.id_user = @Id";

            //Abre conexao com banco
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", idUsuario);
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
                this.CloseConnection();
                return retorno;
            }
            else
            {
                return retorno;
            }
        }

        //Count
        public int ContarUsuarios()
        {
            string query = "SELECT Count(*) FROM usuario";
            int Count = -1;
            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");
                this.CloseConnection();
                return Count;
            }
            else
            {
                return Count;
            }
        }

    }
}