# Stack de Tecnologia — Despesas API

## Runtime

| Componente | Tecnologia | Versão |
|---|---|---|
| Plataforma | .NET | 10 |
| Modelo de API | ASP.NET Core Minimal API | 10.0 |
| Target Framework | `net10.0` | — |

## Persistência

| Componente | Tecnologia | Pacote NuGet |
|---|---|---|
| ORM | Entity Framework Core | `Microsoft.EntityFrameworkCore` |
| Provider | In-Memory (sem banco real) | `Microsoft.EntityFrameworkCore.InMemory` |

> O provider In-Memory é usado por simplicidade no contexto deste projeto.
> Para produção, substituir por `Microsoft.EntityFrameworkCore.SqlServer` ou equivalente.

## Documentação da API

| Componente | Tecnologia | Pacote NuGet |
|---|---|---|
| Geração OpenAPI | ASP.NET Core OpenAPI nativo | `Microsoft.AspNetCore.OpenApi` |
| UI de documentação | Scalar | `Scalar.AspNetCore` |
| URL de acesso (dev) | `/scalar/v1` | — |

## Testes

| Componente | Tecnologia | Pacote NuGet |
|---|---|---|
| Framework de testes | xUnit | via `Reqnroll.xUnit` |
| BDD / Gherkin | ReqNroll | `Reqnroll.xUnit` |
| Test Host (API in-process) | WebApplicationFactory | `Microsoft.AspNetCore.Mvc.Testing` |

## Estrutura da Solution

```
Despesas.sln
  src/
    Despesas.Api/              ← TargetFramework: net10.0
      Domain/
        Enums/TipoDespesa.cs
        Entities/Despesa.cs
      Application/
        DTOs/RegistrarDespesaRequest.cs
        Services/DespesaService.cs
      Infrastructure/
        Data/DespesasDbContext.cs
      Endpoints/DespesasEndpoints.cs
      Program.cs
  tests/
    Despesas.BddTests/         ← TargetFramework: net10.0
      Features/RegistrarDespesas.feature
      StepDefinitions/DespesasStepDefinitions.cs
      Support/ApiContext.cs
      Support/Hooks.cs
```

## Endpoint Exposto

```
POST /api/despesas
```

### Request Body

```json
{
  "nomeFuncionario": "João Silva",
  "tipoDespesa": "alimentacao",
  "valor": 85.50
}
```

### Responses

| Caso | Status | Body |
|---|---|---|
| Despesa registrada | `201 Created` | `{ "mensagem": "Despesa registrada com sucesso" }` |
| Regra de negócio violada | `422 Unprocessable Entity` | ProblemDetails com `detail` descritivo |
| Campos inválidos/faltando | `400 Bad Request` | ProblemDetails com `detail` descritivo |

## Tipos de Despesa Suportados

| Valor no request | Normalizado para |
|---|---|
| `transporte` / `TRANSPORTE` | `TipoDespesa.Transporte` |
| `alimentacao` / `ALIMENTACAO` | `TipoDespesa.Alimentacao` |
| qualquer outro valor | `400 Bad Request` |

## Regras de Negócio

| Regra | Comportamento |
|---|---|
| `alimentacao` com `valor > 100.00` | `422` — recusada |
| `alimentacao` com `valor <= 100.00` | `201` — registrada |
| `transporte` com qualquer valor positivo | `201` — registrada |
| `valor <= 0` | `422` — recusada |
| `nomeFuncionario` vazio ou nulo | `400` — inválido |
| Tipo de despesa desconhecido | `400` — inválido |
