using Despesas.Api.Application.DTOs;
using Despesas.Api.Application.Results;
using Despesas.Api.Domain.Entities;
using Despesas.Api.Domain.Enums;
using Despesas.Api.Infrastructure.Data;

namespace Despesas.Api.Application.Services;

public class DespesaService(DespesasDbContext db)
{
    private const decimal LimiteAlimentacao = 100m;

    public async Task<ResultadoRegistro> RegistrarAsync(
        RegistrarDespesaRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
            return ResultadoRegistro.EntradaInvalida("Requisição inválida");

        if (string.IsNullOrWhiteSpace(request.NomeFuncionario))
            return ResultadoRegistro.EntradaInvalida("Nome do funcionário é obrigatório");

        if (string.IsNullOrWhiteSpace(request.TipoDespesa))
            return ResultadoRegistro.EntradaInvalida("Tipo de despesa é obrigatório");

        if (!TryParseTipo(request.TipoDespesa, out var tipo))
            return ResultadoRegistro.EntradaInvalida(
                $"Tipo de despesa inválido: '{request.TipoDespesa}'. Valores permitidos: 'transporte', 'alimentacao'");

        if (request.Valor <= 0)
            return ResultadoRegistro.EntradaInvalida("Valor da despesa deve ser maior que zero");

        if (tipo == TipoDespesa.Alimentacao && request.Valor > LimiteAlimentacao)
            return ResultadoRegistro.RegraDeNegocioViolada(
                $"Despesa de alimentação não pode ultrapassar R$ {LimiteAlimentacao:F2}");

        var despesa = new Despesa
        {
            NomeFuncionario = request.NomeFuncionario.Trim(),
            Tipo = tipo,
            Valor = request.Valor,
            RegistradaEm = DateTime.UtcNow
        };

        db.Despesas.Add(despesa);
        await db.SaveChangesAsync(cancellationToken);

        return ResultadoRegistro.Ok(despesa.Id);
    }

    private static bool TryParseTipo(string entrada, out TipoDespesa tipo)
    {
        var normalizado = entrada.Trim().ToLowerInvariant();
        switch (normalizado)
        {
            case "transporte":
                tipo = TipoDespesa.Transporte;
                return true;
            case "alimentacao":
            case "alimentação":
                tipo = TipoDespesa.Alimentacao;
                return true;
            default:
                tipo = default;
                return false;
        }
    }
}
