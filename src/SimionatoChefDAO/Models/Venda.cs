using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimionatoChefDAO.Models
{
    public class Venda
    {
        public int Id { get; set; }
        public DateTime Datavenda { get; set; }
        public Usuario Usuario { get; set; }
        public Cliente Cliente { get; set; }
        public string StatusVenda { get; set; }
        public List<Produto> Produtos { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public double Total { get; set; }
    }
}