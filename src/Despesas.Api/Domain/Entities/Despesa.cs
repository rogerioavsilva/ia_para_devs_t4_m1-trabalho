using Despesas.Api.Domain.Enums;

namespace Despesas.Api.Domain.Entities;

public class Despesa
{
    public int Id { get; set; }
    public string NomeFuncionario { get; set; } = string.Empty;
    public TipoDespesa Tipo { get; set; }
    public decimal Valor { get; set; }
    public DateTime RegistradaEm { get; set; } = DateTime.UtcNow;
}
