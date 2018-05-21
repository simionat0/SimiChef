using System.ComponentModel.DataAnnotations;

namespace SimionatoChefDAO.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public double Valor { get; set; }
        public int Quantidade { get; set; }
        public string Descricao { get; set; }
        public string Foto { get; set; }
        public string Categoria { get; set; }
        public string Ativo { get; set; }
    }
}