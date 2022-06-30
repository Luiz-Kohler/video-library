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

            var clienteComPendenciasAtrasadas = await _clientesRepository.SelecionarVariasPorIncluindoLocacoes(cliente =>
                cliente.Locacoes.Any(locacao => !locacao.DataDevolucao.HasValue && locacao.EhAtivo) && cliente.EhAtivo);

            var filmesNuncaAlocados = await _filmesRepository.SelecionarVariasPorIncluindoLocacoes(filme =>
                filme.Locacoes.Count(locacao => locacao.EhAtivo) == 0 && filme.EhAtivo);

            var filmes = await _filmesRepository.SelecionarVariasPorIncluindoLocacoes(filme => filme.EhAtivo);

            var dataAtual = DateTime.UtcNow;
            var dataUmAnoAtras = dataAtual.AddYears(-1);

            var filmesMaisAlocadosDoUltimoAno = filmes.Where(filme => filme.CriadoEm <= dataAtual && filme.CriadoEm >= dataUmAnoAtras)
                .OrderByDescending(filme => filme.Locacoes.Count(locacao => locacao.EhAtivo)).ToList();

            var dataUmaSemanaAtras = dataAtual.AddDays(-7);

            var filmesMenosAlocadosSemanaPassada = filmes.Where(filme => filme.CriadoEm <= dataAtual && filme.CriadoEm >= dataUmaSemanaAtras)
                .OrderByDescending(filme => filme.Locacoes.Count(locacao => locacao.EhAtivo)).ToList();

            var clientes = await _clientesRepository.SelecionarVariasPorIncluindoLocacoes(cliente => cliente.EhAtivo);
            var segundoClienteQueMaisAlocou = clientes.OrderByDescending(cliente => cliente.Locacoes.Count(locacao => locacao.EhAtivo)).Skip(1).FirstOrDefault();

            using (var excelEngine = new ExcelEngine())
            {
                var application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2013;
                application.IgnoreSheetNameException = true;

                var xlApp = application.Workbooks.Create();

                foreach (var worksheetsDesnecessarios in xlApp.Worksheets.OrderByDescending(x => x.Index).Skip(1).ToList())
                    worksheetsDesnecessarios.Remove();

                var worksheet = xlApp.Worksheets[0];
                worksheet.Name = "Clientes com pendências";

                worksheet.Range["A1"].Text = "Id";
                worksheet.Range["B1"].Text = "Nome";
                worksheet.Range["C1"].Text = "CPF";
                worksheet.Range["D1"].Text = "Data Nascimento";

                worksheet.Range["A1:D1"].CellStyle.Font.Bold = true;
                worksheet.Range["A1:D1"].CellStyle.Color = Color.LightBlue;

                var linhaAtual = 2;

                foreach (var cliente in clienteComPendenciasAtrasadas)
                {
                    worksheet.Range[$"A{linhaAtual}"].Text = $"{cliente.Id}";
                    worksheet.Range[$"B{linhaAtual}"].Text = cliente.Nome;
                    worksheet.Range[$"C{linhaAtual}"].Text = cliente.Cpf;
                    worksheet.Range[$"D{linhaAtual}"].Text = cliente.DataNascimento.ToLocalTime().ToString("yyyy-MM-dd");

                    linhaAtual++;
                }

                xlApp.Worksheets.AddCopy(0);
                var worksheetFilmesNaoAlugados = xlApp.Worksheets[1];
                worksheetFilmesNaoAlugados.Name = "Filmes Nunca Alocados";

                worksheetFilmesNaoAlugados.Range["A1"].Text = "Id";
                worksheetFilmesNaoAlugados.Range["B1"].Text = "Titulo";
                worksheetFilmesNaoAlugados.Range["C1"].Text = "Lançamento";

                worksheetFilmesNaoAlugados.Range["A1:C1"].CellStyle.Font.Bold = true;
                worksheetFilmesNaoAlugados.Range["A1:C1"].CellStyle.Color = Color.LightBlue;

                linhaAtual = 2;

                foreach (var filme in filmesNuncaAlocados)
                {
                    worksheetFilmesNaoAlugados.Range[$"A{linhaAtual}"].Text = $"{filme.Id}";
                    worksheetFilmesNaoAlugados.Range[$"B{linhaAtual}"].Text = filme.Titulo;
                    worksheetFilmesNaoAlugados.Range[$"C{linhaAtual}"].Text = filme.EhLancamento ? "Sim" : "Não";

                    linhaAtual++;
                }

                xlApp.Worksheets.AddCopy(1);
                var worksheetFilmesMaisAlugados = xlApp.Worksheets[2];
                worksheetFilmesMaisAlugados.Name = "Top 5 Filmes Mais Alocados Ano Passado";

                worksheetFilmesMaisAlugados.Range["A1"].Text = "Id";
                worksheetFilmesMaisAlugados.Range["B1"].Text = "Titulo";
                worksheetFilmesMaisAlugados.Range["C1"].Text = "Lançamento";

                worksheetFilmesMaisAlugados.Range["A1:C1"].CellStyle.Font.Bold = true;
                worksheetFilmesMaisAlugados.Range["A1:C1"].CellStyle.Color = Color.LightBlue;

                linhaAtual = 2;
                foreach (var filme in filmesMaisAlocadosDoUltimoAno.Take(5))
                {
                    worksheetFilmesMaisAlugados.Range[$"A{linhaAtual}"].Text = $"{filme.Id}";
                    worksheetFilmesMaisAlugados.Range[$"B{linhaAtual}"].Text = filme.Titulo;
                    worksheetFilmesMaisAlugados.Range[$"C{linhaAtual}"].Text = filme.EhLancamento ? "Sim" : "Não";

                    linhaAtual++;
                }

                xlApp.Worksheets.AddCopy(2);
                var worksheetFilmesMenosAlugados = xlApp.Worksheets[3];
                worksheetFilmesMenosAlugados.Name = "Top 3 Filmes Menos Alocados Semana Passada";

                worksheetFilmesMenosAlugados.Range["A1"].Text = "Id";
                worksheetFilmesMenosAlugados.Range["B1"].Text = "Titulo";
                worksheetFilmesMenosAlugados.Range["C1"].Text = "Lançamento";

                worksheetFilmesMenosAlugados.Range["A1:C1"].CellStyle.Font.Bold = true;
                worksheetFilmesMenosAlugados.Range["A1:C1"].CellStyle.Color = Color.LightBlue;

                linhaAtual = 2;

                foreach (var filme in filmesMenosAlocadosSemanaPassada.Take(3))
                {
                    worksheetFilmesMenosAlugados.Range[$"A{linhaAtual}"].Text = $"{filme.Id}";
                    worksheetFilmesMenosAlugados.Range[$"B{linhaAtual}"].Text = filme.Titulo;
                    worksheetFilmesMenosAlugados.Range[$"C{linhaAtual}"].Text = filme.EhLancamento ? "Sim" : "Não";

                    linhaAtual++;
                }

                xlApp.Worksheets.AddCopy(3);
                var worksheetSegundoClienteQueMenosAlugou = xlApp.Worksheets[4];
                worksheetSegundoClienteQueMenosAlugou.Name = "Segundo Cliente Que Mais Alocou filmes";

                worksheetSegundoClienteQueMenosAlugou.Range["A1"].Text = "Id";
                worksheetSegundoClienteQueMenosAlugou.Range["B1"].Text = "Nome";
                worksheetSegundoClienteQueMenosAlugou.Range["C1"].Text = "CPF";
                worksheetSegundoClienteQueMenosAlugou.Range["D1"].Text = "Data Nascimento";

                worksheetSegundoClienteQueMenosAlugou.Range["A1:D1"].CellStyle.Font.Bold = true;
                worksheetSegundoClienteQueMenosAlugou.Range["A1:D1"].CellStyle.Color = Color.LightBlue;

                worksheetSegundoClienteQueMenosAlugou.Range[$"A2"].Text = $"{segundoClienteQueMaisAlocou?.Id}";
                worksheetSegundoClienteQueMenosAlugou.Range[$"B2"].Text = segundoClienteQueMaisAlocou?.Nome;
                worksheetSegundoClienteQueMenosAlugou.Range[$"C2"].Text = segundoClienteQueMaisAlocou?.Cpf;
                worksheetSegundoClienteQueMenosAlugou.Range[$"D2"].Text = segundoClienteQueMaisAlocou?.DataNascimento.ToLocalTime().ToString("yyyy-MM-dd");

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
    }
}
