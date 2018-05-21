using Newtonsoft.Json;

namespace SimionatoChefDAO.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Senha { get; set; }
        public int Status { get; set; }
    }
}