namespace Despesas.Api.Application.Results;

public record ResultadoRegistro
{
    public bool Sucesso { get; init; }
    public int? DespesaId { get; init; }
    public string Mensagem { get; init; } = string.Empty;
    public int StatusCode { get; init; }

    public static ResultadoRegistro Ok(int despesaId) => new()
    {
        Sucesso = true,
        DespesaId = despesaId,
        Mensagem = "Despesa registrada com sucesso",
        StatusCode = StatusCodes.Status201Created
    };

    public static ResultadoRegistro EntradaInvalida(string mensagem) => new()
    {
        Sucesso = false,
        Mensagem = mensagem,
        StatusCode = StatusCodes.Status400BadRequest
    };

    public static ResultadoRegistro RegraDeNegocioViolada(string mensagem) => new()
    {
        Sucesso = false,
        Mensagem = mensagem,
        StatusCode = StatusCodes.Status422UnprocessableEntity
    };
}
