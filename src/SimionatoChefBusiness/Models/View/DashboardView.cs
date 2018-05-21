using SimionatoChefDAO.Models;
using System.Collections.Generic;

namespace SimionatoChefBusiness.Models.View
{
    public class Dashboard
    {
        public Usuario Usuario { get; set; }
        public List<VendaSemanal> GraficoVendaSemanal { get; set; }
        public List<Venda> UltimasVendas { get; set; }
    }
}