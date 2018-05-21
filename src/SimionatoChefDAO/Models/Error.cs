using System;

namespace SimionatoChefDAO.Models
{
    public class Error: Exception
    {
        public int ErrorCode { get; set; }
        public string Status { get; set; }
        public string Menssage { get; set; }

        public Error()
        {
        }

        public Error ErroToken() {
            Error error = new Error()
            {
                ErrorCode = 1,
                Menssage = "Token Invalido",
                Status = "Error"
            };
            return error;
        }

        public Error ErroObjetoIncompleto()
        {
            Error error = new Error();
            error.ErrorCode = 2;
            error.Menssage = "Objeto não foi enviado por completo";
            error.Status = "Error";
            return error;
        }


        public Error ErroServidor(string menssage)
        {
            Error error = new Error()
            {
                ErrorCode = 3,
                Menssage = "Error interno no servidor: " + menssage,
                Status = "Error"
            };
            return error;
        }


        public Error ErroUsuarioNotLogado()
        {
            Error error = new Error()
            {
                ErrorCode = 4,
                Menssage = "E-mail ou/e senha incorretos",
                Status = "Login"
            };
            return error;
        }
    }
}