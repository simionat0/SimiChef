using SimionatoChefDAO.Models;
using System.Collections.Generic;

namespace SimionatoChefBusiness.Models.View
{
    public class VendaView
    {
        public Usuario Usuario { get; set; }
        public Cliente Cliente { get; set; }
        public Venda Venda { get; set; }
        public Produto Produto { get; set; }
        public List<Cliente> ListaClientes { get; set; }
        public List<Produto> ListaProdutos { get; set; }
    }
}