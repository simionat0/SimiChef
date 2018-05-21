using SimionatoChefDAO.Models;
using System;
using System.Collections.Generic;

namespace SimionatoChefBusiness
{
    public interface IUsuarioBusiness
    {
        Usuario ObterUsuario(int idUsuario);
        Usuario ObterUsuarioCompleto(int idUsuario);
        List<Usuario> ListaUsuarios();
        bool NovoUsuario(Usuario usuario);
        bool AtualizaUsuario(Usuario usuario);
        bool SalvarUsuario(Usuario usuario);
        Usuario LoginUsuario(Usuario usuario);
        bool VerificaUsuarioBase(int idUsuario);
        List<Usuario> PesquisarUsuarioNome(String nomeUsuario);
    }
}
