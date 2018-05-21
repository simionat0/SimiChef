using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimionatoChefBusiness;
using SimionatoChefDAO.Models;
using Moq;

namespace SimionatoChefTest
{
    [TestClass]
    public class ClienteBusinessTest
    {
        private IClienteBusiness ClienteBusiness = new ClienteBusiness();

        [TestMethod]
        public void ListaDeRegistrosRetornados()
        {
            Assert.AreNotEqual(0, ClienteBusiness.ListaCliente().Count);
        }

        [TestMethod]
        public void SalvarNovoCliente()
        {
            Cliente Cliente = new Cliente()
            {
                Nome = "João da Silva",
                Cep = "82.600-540",
                Logradouro = "Rua Gustavo Knorr",
                Numero = "122",
                Bairro = "Bacacheri",
                Cidade = "Curitiba",
                Uf = "PR",
                Telefone = "(41) 3258-1846_",
                Email = "joao_silva@simionato.com",
                Senha = "123abc987yxz"
            };
            Mock<IClienteBusiness> _IClienteBusiness = new Mock<IClienteBusiness>();

            Assert.AreEqual(true, _IClienteBusiness.Setup(x => x.SalvarCliete(Cliente)).Returns(true));
        }
    }
}
