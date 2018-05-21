using SimionatoChefDAO.Models;
using System.Collections.Generic;

namespace SimionatoChefBusiness.Models.View
{
    public class ClienteView
    {
        public Usuario Usuario { get; set; }
        public Cliente Cliente { get; set; }
        public List<Cliente> ListaCliente { get; set; }
    }
}