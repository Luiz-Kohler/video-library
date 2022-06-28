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
                cliente.Locacoes.Any(localizacao => !localizacao.DataDevolucao.HasValue) && cliente.EhAtivo);

            var filmesSemAlocacoes = await _filmesRepository.SelecionarVariasPorIncluindoLocacoes(filme =>
                filme.Locacoes.Count == 0 && filme.EhAtivo);

            var filmes = await _filmesRepository.SelecionarVariasPorIncluindoLocacoes(filme => filme.EhAtivo && filme.CriadoEm > DateTime.UtcNow.AddYears(-1));

            var filmesOrdernadoPorQtdLocacoesCrescente = filmes.OrderBy(filme => filme.Locacoes.Where(locacao => locacao.EhAtivo).Count());
            var filmesOrdernadoPorQtdLocacoesDecrescente = filmes.OrderByDescending(filme => filme.Locacoes.Where(locacao => locacao.EhAtivo).Count());

            var clientes = await _clientesRepository.SelecionarVariasPorIncluindoLocacoes(cliente => cliente.EhAtivo);

            var clienteOrdernadoPorQtdLocacoesDecrescente = clientes.OrderByDescending(cliente => cliente.Locacoes.Count);

            using (var excelEngine = new ExcelEngine())
            {
                var application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2013;
                application.IgnoreSheetNameException = true;

                var xlApp = application.Workbooks.Create();

                foreach (var worksheetsDesnecessarios in xlApp.Worksheets.OrderByDescending(x => x.Index).Skip(1).ToList())
                    worksheetsDesnecessarios.Remove();

                //================================================================================================================
                //CLIENTE COM LOCACOES ATRASADAS
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
                //================================================================================================================

                //================================================================================================================
                //FILMES NUNCA ALUGADOS
                xlApp.Worksheets.AddCopy(0);

                var worksheetFilmesNaoAlugados = xlApp.Worksheets[1];
                worksheetFilmesNaoAlugados.Name = "Filmes nunca alugados";

                worksheetFilmesNaoAlugados.Range["A1"].Text = "Id";
                worksheetFilmesNaoAlugados.Range["B1"].Text = "Titulo";
                worksheetFilmesNaoAlugados.Range["C1"].Text = "Lançamento";

                worksheetFilmesNaoAlugados.Range["A1:C1"].CellStyle.Font.Bold = true;
                worksheetFilmesNaoAlugados.Range["A1:C1"].CellStyle.Color = Color.LightBlue;

                //var linhaAtual = 2;
                linhaAtual = 2;

                foreach (var filme in filmesSemAlocacoes)
                {
                    worksheetFilmesNaoAlugados.Range[$"A{linhaAtual}"].Text = $"{filme.Id}";
                    worksheetFilmesNaoAlugados.Range[$"B{linhaAtual}"].Text = filme.Titulo;
                    worksheetFilmesNaoAlugados.Range[$"C{linhaAtual}"].Text = filme.EhLancamento ? "Sim" : "Não";

                    linhaAtual++;
                }
                //================================================================================================================

                //================================================================================================================
                //5 FILMES MAIS ALUGADOS

                xlApp.Worksheets.AddCopy(1);

                var worksheetFilmesMaisAlugados = xlApp.Worksheets[2];
                worksheetFilmesMaisAlugados.Name = "Top 5 Filmes Mais Alugados";

                worksheetFilmesMaisAlugados.Range["A1"].Text = "Id";
                worksheetFilmesMaisAlugados.Range["B1"].Text = "Titulo";
                worksheetFilmesMaisAlugados.Range["C1"].Text = "Lançamento";

                worksheetFilmesMaisAlugados.Range["A1:C1"].CellStyle.Font.Bold = true;
                worksheetFilmesMaisAlugados.Range["A1:C1"].CellStyle.Color = Color.LightBlue;

                //var linhaAtual = 2;
                linhaAtual = 2;

                foreach (var filme in filmesOrdernadoPorQtdLocacoesDecrescente.Take(5))
                {
                    worksheetFilmesMaisAlugados.Range[$"A{linhaAtual}"].Text = $"{filme.Id}";
                    worksheetFilmesMaisAlugados.Range[$"B{linhaAtual}"].Text = filme.Titulo;
                    worksheetFilmesMaisAlugados.Range[$"C{linhaAtual}"].Text = filme.EhLancamento ? "Sim" : "Não";

                    linhaAtual++;
                }
                //================================================================================================================

                //================================================================================================================
                //3 FILMES menos ALUGADOS

                xlApp.Worksheets.AddCopy(2);

                var worksheetFilmesMenosAlugados = xlApp.Worksheets[3];
                worksheetFilmesMenosAlugados.Name = "Top 3 Filmes Menos Alugados";

                worksheetFilmesMenosAlugados.Range["A1"].Text = "Id";
                worksheetFilmesMenosAlugados.Range["B1"].Text = "Titulo";
                worksheetFilmesMenosAlugados.Range["C1"].Text = "Lançamento";

                worksheetFilmesMenosAlugados.Range["A1:C1"].CellStyle.Font.Bold = true;
                worksheetFilmesMenosAlugados.Range["A1:C1"].CellStyle.Color = Color.LightBlue;

                //var linhaAtual = 2;
                linhaAtual = 2;

                foreach (var filme in filmesOrdernadoPorQtdLocacoesCrescente.Take(3))
                {
                    worksheetFilmesMenosAlugados.Range[$"A{linhaAtual}"].Text = $"{filme.Id}";
                    worksheetFilmesMenosAlugados.Range[$"B{linhaAtual}"].Text = filme.Titulo;
                    worksheetFilmesMenosAlugados.Range[$"C{linhaAtual}"].Text = filme.EhLancamento ? "Sim" : "Não";

                    linhaAtual++;
                }
                //================================================================================================================

                //================================================================================================================
                //SEGUNDO CLIENTE QUE MAIS ALUGOU FILMES

                xlApp.Worksheets.AddCopy(3);

                var worksheetSegundoClienteQueMenosAlugou = xlApp.Worksheets[4];
                worksheetSegundoClienteQueMenosAlugou.Name = "Segundo Cliente Que Mais Alugou filmes";

                worksheetSegundoClienteQueMenosAlugou.Range["A1"].Text = "Id";
                worksheetSegundoClienteQueMenosAlugou.Range["B1"].Text = "Nome";
                worksheetSegundoClienteQueMenosAlugou.Range["C1"].Text = "CPF";
                worksheetSegundoClienteQueMenosAlugou.Range["D1"].Text = "Data Nascimento";

                worksheetSegundoClienteQueMenosAlugou.Range["A1:D1"].CellStyle.Font.Bold = true;
                worksheetSegundoClienteQueMenosAlugou.Range["A1:D1"].CellStyle.Color = Color.LightBlue;

                var segundoClienteMaisAlugou = clienteOrdernadoPorQtdLocacoesDecrescente.Skip(1).Take(1).First();
                worksheetSegundoClienteQueMenosAlugou.Range[$"A2"].Text = $"{segundoClienteMaisAlugou.Id}";
                worksheetSegundoClienteQueMenosAlugou.Range[$"B2"].Text = segundoClienteMaisAlugou.Nome;
                worksheetSegundoClienteQueMenosAlugou.Range[$"C2"].Text = segundoClienteMaisAlugou.Cpf;
                worksheetSegundoClienteQueMenosAlugou.Range[$"D2"].Text = segundoClienteMaisAlugou.DataNascimento.ToLocalTime().ToString("yyyy-MM-dd");

                //================================================================================================================

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
