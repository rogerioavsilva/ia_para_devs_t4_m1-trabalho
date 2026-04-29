namespace Despesas.Api.Application.DTOs;

public record RegistrarDespesaRequest(
    string? NomeFuncionario,
    string? TipoDespesa,
    decimal Valor
);
