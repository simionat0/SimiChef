using SimionatoChefDAO;
using SimionatoChefDAO.Models;
using System;
using System.Collections.Generic;

namespace SimionatoChefBusiness
{
    public class ClienteBusiness : IClienteBusiness
    {
        private IClienteDAO _IClienteDAO;

        public ClienteBusiness(IClienteDAO obj)
        {
            _IClienteDAO = obj;
        }

        public ClienteBusiness()
        {
        }

        public Cliente ObterCliente(int idCliente)
        {
            return _IClienteDAO.ObterCliente(idCliente);
        }

        public Cliente ObterClienteCompleto(int idCliente)
        {
            return _IClienteDAO.ObterClienteCompleto(idCliente);
        }

        public List<Cliente> ListaCliente()
        {
            return _IClienteDAO.ListarClientes();
        }

        public List<Cliente> PesquisarClienteNome(String nomeCliente)
        {
            return _IClienteDAO.PesquisarClienteNome(nomeCliente);
        }

        public bool NovoCliente(Cliente cliente)
        {
            if (ValidaNovoCliente(cliente))
            {
                return _IClienteDAO.NovoCliente(cliente);
            }
            else
            {
                return false;
            }
        }

        public bool AtualizaCliente(Cliente cliente)
        {
            if (ValidaClienteEditado(cliente) == true)
            {
                return _IClienteDAO.AtualizaCliente(cliente);
            }
            else
            {
                return false;
            }
        }

        public bool SalvarCliete(Cliente cliente)
        {
            if (cliente.Id != 0)
            {
                return AtualizaCliente(cliente);
            }
            else
            {
                return NovoCliente(cliente);
            }
        }

        public int ContarClientes()
        {
            return _IClienteDAO.ContarClientes();
        }

        //---------------------------------------------------------------------------------------------------------------


        public bool VerificaClienteBase(int idCliente)
        {
            return _IClienteDAO.VerificaClienteBase(idCliente);
        }

        public bool ValidaNovoCliente(Cliente cliente)
        {
            if (ValidaDadosCliente(cliente))
            {
                return true;
            }
            else
            {
                return true;
            }
        }

        public bool ValidaClienteEditado(Cliente cliente)
        {
            if (ValidaDadosCliente(cliente))
            {
                if (VerificaClienteBase(cliente.Id))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool ValidaDadosCliente(Cliente cliente)
        {
            if (cliente.Nome == null)
            {
                throw new Exception("Nome do cliente não enviado");
            }
            else if (cliente.Cep == null)
            {
                throw new Exception("Cep do cliente não enviado");
            }
            else if (cliente.Logradouro == null)
            {
                throw new Exception("Logradouro do cliente não enviado");
            }
            else if (cliente.Numero == null)
            {
                throw new Exception("Numero do cliente não enviado");
            }
            else if (cliente.Bairro == null)
            {
                throw new Exception("Bairro do cliente não enviado");
            }
            else if (cliente.Cidade == null)
            {
                throw new Exception("Cidade do cliente não enviado");
            }
            else if (cliente.Uf == null)
            {
                throw new Exception("Uf do cliente não enviado");
            }
            else if (cliente.Telefone == null)
            {
                throw new Exception("Telefone do cliente não enviado");
            }
            else if (cliente.Email == null)
            {
                throw new Exception("Email do cliente não enviado");
            }
            else if (cliente.Senha == null)
            {
                throw new Exception("Senha do cliente não enviado");
            }
            else
            {
                return true;
            }
        }
    }
}