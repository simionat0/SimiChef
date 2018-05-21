using SimionatoChefDAO.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimionatoChefBusiness.Models.View
{
    public class FinanceiroView
    {
        public Usuario Usuario { get; set; }
        public List<Venda> ListaVendas { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public double Total { get; set; }
    }
}