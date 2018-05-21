using SimionatoChefBusiness.Models.View;
using SimionatoChefDAO.Models;

namespace SimionatoChefBusiness
{
    public class DashboardBusiness : IDashboardBusiness
    {
        private IVendaBusiness _IVendaBusiness;

        public DashboardBusiness(IVendaBusiness obj)
        {
            _IVendaBusiness = obj;
        }

        public Dashboard Dashboard(Usuario usuario)
        {
            Dashboard dashboard = new Dashboard()
            {
                GraficoVendaSemanal = _IVendaBusiness.RelatorioVendaSemanal(),
                UltimasVendas = _IVendaBusiness.ListaVendasDashboard(12),
                Usuario = usuario
            };
            return dashboard;
        }
    }
}