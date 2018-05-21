using SimionatoChefDAO.Models;
using System.Collections.Generic;

namespace SimionatoChefBusiness.Models.View
{
    public class UsuarioView
    {
        public Usuario UsuarioLogado { get; set; }
        public Usuario Usuario { get; set; }
        public List<Usuario> ListaUsuario { get; set; }
    }
}