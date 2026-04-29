namespace Despesas.BddTests.Support;

public class ApiContext
{
    public HttpResponseMessage? Response { get; set; }
    public string? ResponseBody { get; set; }
    public string? NomeFuncionario { get; set; }
    public string? TipoDespesa { get; set; }
    public decimal Valor { get; set; }
}
