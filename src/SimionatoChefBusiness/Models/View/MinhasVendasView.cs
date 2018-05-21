using SimionatoChefDAO.Models;
using System.Collections.Generic;

namespace SimionatoChefBusiness.Models.View
{
    public class MinhasVendasView
    {
        public Usuario Usuario { get; set; }
        public List<Venda> ListaVendas { get; set; }
        public Venda Venda { get; set; }
        public List<Status> Status { get; set; }
    }
}