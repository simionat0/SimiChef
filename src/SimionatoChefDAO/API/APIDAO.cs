using MySql.Data.MySqlClient;
using SimionatoChefDAO.Factory;

namespace SimionatoChefDAO
{
    public class APIDAO : DBConnect, IAPIDAO
    {
        //Validar Token
        public bool ValidarToken(string token)
        {
            bool vToken = false;
            string query = "SELECT * FROM token_api WHERE token_api.token = @Token ";
            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Token", token);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    vToken =  true;
                }
                else
                {
                    vToken =  false;
                }
                dataReader.Close();
                CloseConnection();
                return vToken;
            }
            else
            {
                return vToken;
            }
        }
    }
}
