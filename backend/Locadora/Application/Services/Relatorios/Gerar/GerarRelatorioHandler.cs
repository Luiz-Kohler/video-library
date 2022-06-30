using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Syncfusion.Drawing;
using Syncfusion.XlsIO;
using System.Data;

namespace Application.Services.Relatorios.Gerar
{
    public class GerarRelatorioHandler : IRequestHandler<GerarRelatorioRequest, GerarRelatorioResponse>
    {
        private readonly ILocacoesRepository _locacoesRepository;
        private readonly IFilmesRepository _filmesRepository;
        private readonly IClientesRepository _clientesRepository;
        private readonly ILogger<GerarRelatorioHandler> _logger;

        public GerarRelatorioHandler(
             ILocacoesRepository locacoesRepository,
             IFilmesRepository filmesRepository,
             IClientesRepository clientesRepository,
             ILogger<GerarRelatorioHandler> logger)
        {
            _locacoesRepository = locacoesRepository;
            _filmesRepository = filmesRepository;
            _clientesRepository = clientesRepository;
            _filmesRepository = filmesRepository;
            _logger = logger;
        }

        public async Task<GerarRelatorioResponse> Handle(GerarRelatorioRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Gerando relatorio");

            using (var excelEngine = new ExcelEngine())
            {
                var application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2013;

                var xlApp = application.Workbooks.Create();

                await GerarWorkSheetClientesComPendencias(xlApp);
                await GerarWorkSheetFilmesNuncaAlocados(xlApp);

                var filmes = await _filmesRepository.SelecionarVariasPorIncluindoLocacoes(filme => filme.EhAtivo);

                GerarWorkSheetFilmesMaisAlocadosDoUltimoAno(xlApp, filmes);
                GerarWorkSheetFilmesMenosAlocadosUltimaSemana(xlApp, filmes);

                await GerarWorkSheetSegundoClienteMaisAlocou(xlApp);

                var stream = new MemoryStream();
                xlApp.SaveAs(stream);

                stream.Position = 0;

                var fileStreamResult = new FileStreamResult(stream, "application/excel");
                fileStreamResult.FileDownloadName = $"Relatorio_{DateTime.UtcNow.ToLocalTime().ToString("yyyy-MM-dd_hh:mm")}.xlsx";

                return new GerarRelatorioResponse
                {
                    Arquivo = fileStreamResult
                };
            }
        }

        private async Task GerarWorkSheetClientesComPendencias(IWorkbook xlApp)
        {
            var clienteComPendenciasAtrasadas = await _clientesRepository.SelecionarVariasPorIncluindoLocacoes(cliente =>
                    cliente.Locacoes.Any(locacao => !locacao.DataDevolucao.HasValue && locacao.EhAtivo) && cliente.EhAtivo);

            var worksheet = xlApp.Worksheets[0];
            worksheet.Name = "Relatorio";

            worksheet.Range["A1"].Text = "Clientes com pendências";

            worksheet.Range["A2"].Text = "Id";
            worksheet.Range["B2"].Text = "Nome";
            worksheet.Range["C2"].Text = "CPF";
            worksheet.Range["D2"].Text = "Data Nascimento";

            worksheet.Range["A2:D2"].CellStyle.Font.Bold = true;
            worksheet.Range["A2:D2"].CellStyle.Color = Color.LightBlue;

            var linhaAtual = 3;

            foreach (var cliente in clienteComPendenciasAtrasadas)
            {
                worksheet.Range[$"A{linhaAtual}"].Text = $"{cliente.Id}";
                worksheet.Range[$"B{linhaAtual}"].Text = cliente.Nome;
                worksheet.Range[$"C{linhaAtual}"].Text = cliente.Cpf;
                worksheet.Range[$"D{linhaAtual}"].Text = cliente.DataNascimento.ToLocalTime().ToString("yyyy-MM-dd");

                linhaAtual++;
            }
        }

        private async Task GerarWorkSheetFilmesNuncaAlocados(IWorkbook xlApp)
        {
            var filmesNuncaAlocados = await _filmesRepository.SelecionarVariasPorIncluindoLocacoes(filme =>
                    filme.Locacoes.Count(locacao => locacao.EhAtivo) == 0 && filme.EhAtivo);

            var worksheet = xlApp.Worksheets[0];
            worksheet.Range["F1"].Text = "Filmes Nunca Alocados";

            worksheet.Range["F2"].Text = "Id";
            worksheet.Range["G2"].Text = "Titulo";
            worksheet.Range["H2"].Text = "Lançamento";

            worksheet.Range["F2:H2"].CellStyle.Font.Bold = true;
            worksheet.Range["F2:H2"].CellStyle.Color = Color.LightBlue;

            var linhaAtual = 3;

            foreach (var filme in filmesNuncaAlocados)
            {
                worksheet.Range[$"F{linhaAtual}"].Text = $"{filme.Id}";
                worksheet.Range[$"G{linhaAtual}"].Text = filme.Titulo;
                worksheet.Range[$"H{linhaAtual}"].Text = filme.EhLancamento ? "Sim" : "Não";

                linhaAtual++;
            }
        }

        private void GerarWorkSheetFilmesMaisAlocadosDoUltimoAno(IWorkbook xlApp, IList<Filme> filmes)
        {
            var dataAtual = DateTime.UtcNow;
            var dataUmAnoAtras = dataAtual.AddYears(-1);

            var filmesMaisAlocadosDoUltimoAno = filmes.Where(filme => filme.CriadoEm <= dataAtual && filme.CriadoEm >= dataUmAnoAtras)
                .OrderByDescending(filme => filme.Locacoes.Count(locacao => locacao.EhAtivo)).ToList();

            var worksheet = xlApp.Worksheets[0];
            worksheet.Range["J1"].Text = "Top 5 Filmes Mais Alocados do ultimo ano";

            worksheet.Range["J2"].Text = "Id";
            worksheet.Range["K2"].Text = "Titulo";
            worksheet.Range["L2"].Text = "Lançamento";

            worksheet.Range["J2:L2"].CellStyle.Font.Bold = true;
            worksheet.Range["J2:L2"].CellStyle.Color = Color.LightBlue;

            var linhaAtual = 3;
            foreach (var filme in filmesMaisAlocadosDoUltimoAno.Take(5))
            {
                worksheet.Range[$"J{linhaAtual}"].Text = $"{filme.Id}";
                worksheet.Range[$"K{linhaAtual}"].Text = filme.Titulo;
                worksheet.Range[$"L{linhaAtual}"].Text = filme.EhLancamento ? "Sim" : "Não";

                linhaAtual++;
            }
        }        
        
        private void GerarWorkSheetFilmesMenosAlocadosUltimaSemana(IWorkbook xlApp, IList<Filme> filmes)
        {
            var dataAtual = DateTime.UtcNow;
            var dataUmaSemanaAtras = dataAtual.AddDays(-7);
            var filmesMenosAlocadosSemanaPassada = filmes.Where(filme => filme.CriadoEm <= dataAtual && filme.CriadoEm >= dataUmaSemanaAtras)
                .OrderByDescending(filme => filme.Locacoes.Count(locacao => locacao.EhAtivo)).ToList();

            var worksheet = xlApp.Worksheets[0];
            worksheet.Range["N1"].Text = "Top 3 Filmes Menos Alocados Semana Passada";

            worksheet.Range["N2"].Text = "Id";
            worksheet.Range["O2"].Text = "Titulo";
            worksheet.Range["P2"].Text = "Lançamento";

            worksheet.Range["N2:P2"].CellStyle.Font.Bold = true;
            worksheet.Range["N2:P2"].CellStyle.Color = Color.LightBlue;

            var linhaAtual = 3;

            foreach (var filme in filmesMenosAlocadosSemanaPassada.Take(3))
            {
                worksheet.Range[$"N{linhaAtual}"].Text = $"{filme.Id}";
                worksheet.Range[$"O{linhaAtual}"].Text = filme.Titulo;
                worksheet.Range[$"P{linhaAtual}"].Text = filme.EhLancamento ? "Sim" : "Não";

                linhaAtual++;
            }
        }        
        
        private async Task GerarWorkSheetSegundoClienteMaisAlocou(IWorkbook xlApp)
        {
            var clientes = await _clientesRepository.SelecionarVariasPorIncluindoLocacoes(cliente => cliente.EhAtivo);
            var segundoClienteQueMaisAlocou = clientes.OrderByDescending(cliente => cliente.Locacoes.Count(locacao => locacao.EhAtivo)).Skip(1).FirstOrDefault();

            var worksheet = xlApp.Worksheets[0];
            worksheet.Range["S1"].Text = "Segundo Cliente Que Mais Alocou filmes";

            worksheet.Range["S2"].Text = "Id";
            worksheet.Range["T2"].Text = "Nome";
            worksheet.Range["U2"].Text = "CPF";
            worksheet.Range["V2"].Text = "Data Nascimento";

            worksheet.Range["S2:V2"].CellStyle.Font.Bold = true;
            worksheet.Range["S2:V2"].CellStyle.Color = Color.LightBlue;

            worksheet.Range[$"S3"].Text = $"{segundoClienteQueMaisAlocou?.Id}";
            worksheet.Range[$"T3"].Text = segundoClienteQueMaisAlocou?.Nome;
            worksheet.Range[$"U3"].Text = segundoClienteQueMaisAlocou?.Cpf;
            worksheet.Range[$"V3"].Text = segundoClienteQueMaisAlocou?.DataNascimento.ToLocalTime().ToString("yyyy-MM-dd");
        }
    }
}
