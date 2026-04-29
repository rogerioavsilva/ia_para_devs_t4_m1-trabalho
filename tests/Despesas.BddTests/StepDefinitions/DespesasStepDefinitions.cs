using System.Globalization;
using System.Net.Http.Json;
using Despesas.BddTests.Support;
using Reqnroll;

namespace Despesas.BddTests.StepDefinitions;

[Binding]
public class DespesasStepDefinitions(HttpClient httpClient, ApiContext apiContext)
{
    [Given(@"que o funcionário ""(.*)"" informa tipo ""(.*)"" e valor (-?\d+(?:[\.,]\d+)?)")]
    public void DadoQueOFuncionarioInformaTipoEValor(string nome, string tipo, string valor)
    {
        apiContext.NomeFuncionario = nome;
        apiContext.TipoDespesa = tipo;
        apiContext.Valor = decimal.Parse(
            valor.Replace(',', '.'),
            NumberStyles.Number,
            CultureInfo.InvariantCulture);
    }

    [When(@"a requisição é enviada para o endpoint de despesas")]
    public async Task QuandoARequisicaoEEnviada()
    {
        var payload = new
        {
            nomeFuncionario = apiContext.NomeFuncionario,
            tipoDespesa = apiContext.TipoDespesa,
            valor = apiContext.Valor
        };

        apiContext.Response = await httpClient.PostAsJsonAsync("/api/despesas", payload);
        apiContext.ResponseBody = await apiContext.Response.Content.ReadAsStringAsync();
    }

    [Then(@"a resposta deve ter status (\d+)")]
    public void EntaoARespostaDeveTerStatus(int statusEsperado)
    {
        Assert.NotNull(apiContext.Response);
        Assert.Equal(statusEsperado, (int)apiContext.Response!.StatusCode);
    }

    [Then(@"o corpo da resposta deve conter a mensagem ""(.*)""")]
    public void EntaoOCorpoDaRespostaDeveConterAMensagem(string mensagemEsperada)
    {
        Assert.NotNull(apiContext.ResponseBody);
        Assert.Contains(mensagemEsperada, apiContext.ResponseBody!);
    }

    [Then(@"o corpo da resposta deve conter o detalhe ""(.*)""")]
    public void EntaoOCorpoDaRespostaDeveConterODetalhe(string trechoEsperado)
    {
        Assert.NotNull(apiContext.ResponseBody);
        Assert.Contains(trechoEsperado, apiContext.ResponseBody!);
    }
}
