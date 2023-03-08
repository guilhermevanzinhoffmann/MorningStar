using MorningStar.Models.ViewModels;
using MorningStar.Services.Interfaces;

namespace MorningStar.Services
{
    public class GraficoService : IGraficoService
    {
        private readonly IEntradaService _entradaService;
        private readonly ISaidaService _saidaService;

        public GraficoService(IEntradaService entradaService, ISaidaService saidaService)
        {
            _entradaService = entradaService;
            _saidaService = saidaService;
        }

        public IEnumerable<ViewEntrada> GetEntradas()
        {            
            var entradas = (from ent in _entradaService.GetAll()
                            group ent by ent.DataHoraEntrada.Month into grupo
                            select new ViewEntrada
                            {
                                Mes = grupo.Key,
                                Quantidade = grupo.Sum(x => x.Quantidade)
                            })
                            .OrderBy(x => x.Mes)
                            .ToList();

            return entradas;
        }

        public IEnumerable<ViewEntradaDia> GetEntradasDia(int mercadoriaID, int mes)
        {
            var entradasDia = (from ent in _entradaService.GetAll()
                            where ent.MercadoriaID == mercadoriaID && ent.DataHoraEntrada.Month == mes
                            group ent by ent.DataHoraEntrada.Day into grupo
                            select new ViewEntradaDia
                            {
                                Dia = grupo.Key,
                                Quantidade = grupo.Sum(x => x.Quantidade)
                            })
                            .OrderBy(x => x.Dia)
                            .ToList();

            return entradasDia;
        }

        public IEnumerable<ViewSaidaDia> GetSaidasDia(int mercadoriaID, int mes)
        {
            var saidasDia = (from ent in _saidaService.GetAll()
                               where ent.MercadoriaID == mercadoriaID && ent.DataHoraSaida.Month == mes
                               group ent by ent.DataHoraSaida.Day into grupo
                               select new ViewSaidaDia
                               {
                                   Dia = grupo.Key,
                                   Quantidade = grupo.Sum(x => x.Quantidade)
                               })
                            .OrderBy(x => x.Dia)
                            .ToList();

            return saidasDia;
        }

        public IEnumerable<ViewSaida> GetSaidas()
        {
            var saidas = (from ent in _saidaService.GetAll()
                            group ent by ent.DataHoraSaida.Month into grupo
                            select new ViewSaida
                            {
                                Mes = grupo.Key,
                                Quantidade = grupo.Sum(x => x.Quantidade)
                            })
                            .OrderBy(x => x.Mes)
                            .ToList();

            return saidas;
        }

        public IEnumerable<ViewEntrada> GetEntradasMes(int mercadoriaID)
        {
            var entradas = (from ent in _entradaService.GetAll()
                            where ent.MercadoriaID == mercadoriaID
                            group ent by ent.DataHoraEntrada.Month into grupo
                            select new ViewEntrada
                            {
                                Mes = grupo.Key,
                                Quantidade = grupo.Sum(x => x.Quantidade)
                            })
                            .OrderBy(x => x.Mes)
                            .ToList();

            return entradas;
        }

        public IEnumerable<ViewSaida> GetSaidasMes(int mercadoriaID)
        {
            var saidas = (from ent in _saidaService.GetAll()
                          where ent.MercadoriaID == mercadoriaID
                          group ent by ent.DataHoraSaida.Month into grupo
                          select new ViewSaida
                          {
                              Mes = grupo.Key,
                              Quantidade = grupo.Sum(x => x.Quantidade)
                          })
                            .OrderBy(x => x.Mes)
                            .ToList();

            return saidas;
        }
    }
}
