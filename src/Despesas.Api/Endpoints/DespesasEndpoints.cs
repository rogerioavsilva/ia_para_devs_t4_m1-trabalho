using Despesas.Api.Application.DTOs;
using Despesas.Api.Application.Services;

namespace Despesas.Api.Endpoints;

public static class DespesasEndpoints
{
    public static IEndpointRouteBuilder MapDespesasEndpoints(this IEndpointRouteBuilder app)
    {
        var grupo = app.MapGroup("/api/despesas")
            .WithTags("Despesas");

        grupo.MapPost("/", RegistrarAsync)
            .WithName("RegistrarDespesa")
            .WithSummary("Registra uma nova despesa corporativa")
            .Produces(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity);

        return app;
    }

    private static async Task<IResult> RegistrarAsync(
        RegistrarDespesaRequest request,
        DespesaService service,
        CancellationToken cancellationToken)
    {
        var resultado = await service.RegistrarAsync(request, cancellationToken);

        if (resultado.Sucesso)
        {
            return Results.Created(
                $"/api/despesas/{resultado.DespesaId}",
                new { mensagem = resultado.Mensagem });
        }

        return Results.Problem(
            detail: resultado.Mensagem,
            statusCode: resultado.StatusCode,
            title: resultado.StatusCode == StatusCodes.Status422UnprocessableEntity
                ? "Regra de negócio violada"
                : "Requisição inválida");
    }
}
