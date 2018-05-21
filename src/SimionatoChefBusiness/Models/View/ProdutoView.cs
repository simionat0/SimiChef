using SimionatoChefDAO.Models;
using System.Collections.Generic;

namespace SimionatoChefBusiness.Models.View
{
    public class ProdutoView
    {
        public Usuario Usuario { get; set; }
        public Produto Produto { get; set; }
        public List<Produto> ListaProdutos { get; set; }
    }
}