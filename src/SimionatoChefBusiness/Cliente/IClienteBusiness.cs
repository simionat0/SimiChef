using SimionatoChefDAO.Models;
using System;
using System.Collections.Generic;

namespace SimionatoChefBusiness
{
    public interface IClienteBusiness
    {
        Cliente ObterCliente(int idCliente);
        Cliente ObterClienteCompleto(int idCliente);
        List<Cliente> ListaCliente();
        List<Cliente> PesquisarClienteNome(String nomeCliente);
        bool NovoCliente(Cliente cliente);
        bool AtualizaCliente(Cliente cliente);
        bool VerificaClienteBase(int idCliente);
        bool SalvarCliete(Cliente cliente);
        int ContarClientes();
    }
}
