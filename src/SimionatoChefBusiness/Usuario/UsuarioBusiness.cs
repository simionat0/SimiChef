using SimionatoChefDAO;
using SimionatoChefDAO.Models;
using System;
using System.Collections.Generic;

namespace SimionatoChefBusiness
{
    public class UsuarioBusiness : IUsuarioBusiness

    {
        private IUsuarioDAO _IUsuarioDAO;

        public UsuarioBusiness(IUsuarioDAO obj)
        {
            _IUsuarioDAO = obj;
        }

        public Usuario ObterUsuario(int idUsuario)
        {
            return _IUsuarioDAO.ObterUsuario(idUsuario);
        }

        public Usuario ObterUsuarioCompleto(int idUsuario)
        {
            return _IUsuarioDAO.ObterUsuarioCompleto(idUsuario);
        }

        public List<Usuario> ListaUsuarios()
        {
            return _IUsuarioDAO.ListarUsuarios();
        }

        public bool NovoUsuario(Usuario usuario)
        {
            if (ValidaDadosUsuario(usuario))
            {
                return _IUsuarioDAO.NovoUsuario(usuario);
            }
            else
            {
                return false;
            }

        }

        public bool AtualizaUsuario(Usuario usuario)
        {
            if (ValidaDadosUsuarioAtualiza(usuario))
            {
                return _IUsuarioDAO.AtualizaUsuario(usuario);
            }
            else
            {
                return false;
            }
        }

        public bool SalvarUsuario(Usuario usuario)
        {
            if (usuario.Id == 0)
            {
                return NovoUsuario(usuario);
            }
            else
            {
                return AtualizaUsuario(usuario);
            }
        }

        public Usuario LoginUsuario(Usuario usuario)
        {
            var respostaUsuario = _IUsuarioDAO.LoginUsuario(usuario);

            if (respostaUsuario == null)
            {
                return null;
            }
            else
            {
                return respostaUsuario;
            }

        }

        public List<Usuario> PesquisarUsuarioNome(String nomeUsuario)
        {
            return _IUsuarioDAO.PesquisarUsuarioNome(nomeUsuario);
        }

        public bool VerificaUsuarioBase(int idUsuario)
        {
            return _IUsuarioDAO.VerificaUsuarioBase(idUsuario);
        }

        public bool ValidaDadosUsuario(Usuario usuario)
        {

            if (usuario.Nome == null)
            {
                throw new Exception("nome do usuario não enviado");
            }
            else if (usuario.Cargo == null)
            {
                throw new Exception("cargo do usuario não enviado");
            }
            else if (usuario.Telefone == null)
            {
                throw new Exception("telefone do usuario não enviado");
            }
            else if (usuario.Email == null)
            {
                throw new Exception("email do usuario não enviado");
            }
            else if (usuario.Senha == null)
            {
                throw new Exception("senha do usuario não enviado");
            }
            else
            {
                return true;
            }
        }

        public bool ValidaDadosUsuarioAtualiza(Usuario usuario)
        {
            if (usuario.Id == 0)
            {
                throw new Exception("id do usuario não enviado");
            }
            else if (usuario.Nome == null)
            {
                throw new Exception("nome do usuario não enviado");
            }
            else if (usuario.Cargo == null)
            {
                throw new Exception("cargo do usuario não enviado");
            }
            else if (usuario.Telefone == null)
            {
                throw new Exception("telefone do usuario não enviado");
            }
            else if (usuario.Email == null)
            {
                throw new Exception("email do usuario não enviado");
            }
            else if (usuario.Senha == null)
            {
                throw new Exception("senha do usuario não enviado");
            }
            else
            {
                return true;
            }
        }

    }
}