using SimionatoChefDAO;

namespace SimionatoChefBusiness
{
    public class APIBusiness : IAPIBusiness
    {
        private readonly IAPIDAO _IAPIDAO;

        public APIBusiness(IAPIDAO obj)
        {
            _IAPIDAO = obj;
        }

        public bool ValidarToken(string token)
        {
            return _IAPIDAO.ValidarToken(token);
        }

    }
}