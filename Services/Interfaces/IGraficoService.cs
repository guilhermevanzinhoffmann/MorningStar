using MorningStar.Models;
using MorningStar.Models.ViewModels;

namespace MorningStar.Services.Interfaces
{
    public interface IGraficoService
    {
        IEnumerable<ViewEntrada> GetEntradas();
        IEnumerable<ViewEntrada> GetEntradasMes(int mercadoriaID);
        IEnumerable<ViewSaida> GetSaidas();
        IEnumerable<ViewSaida> GetSaidasMes(int mercadoriaID);
        IEnumerable<ViewEntradaDia> GetEntradasDia(int mercadoriaID, int mes);
        IEnumerable<ViewSaidaDia> GetSaidasDia(int mercadoriaID, int mes);
    }
}
