using SimionatoChefDAO.Models;
using System;
using System.Collections.Generic;

namespace SimionatoChefDAO
{
    public interface IClienteDAO
    {
        Cliente ObterCliente(int idCliente);

        Cliente ObterClienteCompleto(int idCliente);

        List<Cliente> ListarClientes();

        bool NovoCliente(Cliente cliente);

        bool AtualizaCliente(Cliente cliente);

        bool ExcluirCliente(Cliente cliente);

        List<Cliente> PesquisarClienteNome(String nomeCliente);

        int ContarClientes();

        bool VerificaClienteBase(int idCliente);

    }
}
