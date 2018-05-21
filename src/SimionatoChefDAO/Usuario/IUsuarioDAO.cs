using SimionatoChefDAO.Models;
using System;
using System.Collections.Generic;

namespace SimionatoChefDAO
{
    public interface IUsuarioDAO
    {
        Usuario ObterUsuario(int idUsuario);
        Usuario ObterUsuarioCompleto(int idUsuario);
        List<Usuario> ListarUsuarios();
        bool NovoUsuario(Usuario usuario);
        Usuario LoginUsuario(Usuario usuario);
        bool AtualizaUsuario(Usuario usuario);
        List<Usuario> PesquisarUsuarioNome(String nomeUsuario);
        bool VerificaUsuarioBase(int idUsuario);
        int ContarUsuarios();

    }
}
