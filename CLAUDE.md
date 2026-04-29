# Comportamento e Boas Práticas — Despesas API

## Perfil de Atuação

Atuar como **engenheiro de software sênior** especialista em .NET e construção de APIs REST.
Decisões de design devem priorizar clareza, segurança e manutenibilidade. Evitar over-engineering.

---

## Boas Práticas de API REST

### Status Codes — Regra Estrita

| Situação | Status Code | Método `Results.*` |
|---|---|---|
| Recurso criado com sucesso | **201 Created** | `Results.Created(location, body)` |
| Operação bem-sucedida sem criação | **200 OK** | `Results.Ok(body)` |
| Erro de validação de entrada (formato, campo obrigatório) | **400 Bad Request** | `Results.BadRequest(problem)` |
| Regra de negócio violada (valor excede limite, tipo inválido) | **422 Unprocessable Entity** | `Results.UnprocessableEntity(problem)` |
| Recurso não encontrado | **404 Not Found** | `Results.NotFound()` |
| Conflito de estado (duplicata) | **409 Conflict** | `Results.Conflict(problem)` |
| Erro interno inesperado | **500 Internal Server Error** | tratado por middleware |

> Nunca retornar `200 OK` com um body contendo mensagem de erro.
> Nunca retornar `400` para violação de regra de negócio — use `422`.

### Respostas de Erro

Usar **ProblemDetails** (RFC 7807) como padrão de resposta de erro:

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "Validation error",
  "status": 422,
  "detail": "Despesa de alimentação não pode ultrapassar R$ 100,00"
}
```

Usar `Results.Problem(...)` ou `TypedResults.Problem(...)` em vez de objetos anônimos `{ erro: "..." }`.

### POST que Cria Recurso

```csharp
return Results.Created($"/api/despesas/{despesa.Id}", new { mensagem = "Despesa registrada com sucesso" });
```

Sempre incluir o `Location` header apontando para o recurso criado.

---

## Organização de Código

- Separar responsabilidades em camadas: Domain → Application → Infrastructure → Endpoints
- Serviços de negócio (`*Service`) não acessam `HttpContext`; recebem e retornam DTOs/results
- Endpoints são thin — apenas deserialização, chamada ao serviço e mapeamento de resultado
- Usar `record` para DTOs imutáveis de request/response
- Usar `enum` para tipos de negócio em vez de strings mágicas

## Validação

- Validar na borda do sistema (endpoint): campos obrigatórios, formato → `400`
- Validar regra de negócio no serviço → `422`
- Não duplicar validação nas duas camadas

## Nomenclatura

- Português para nomes de domínio (alinhado com a User Story e regras de negócio)
- Inglês para infraestrutura, configuração, padrões técnicos

## O que Evitar

- Retornar exceções não tratadas ao cliente
- Usar `double` para valores monetários (usar `decimal`)
- Strings hardcoded em múltiplos lugares — centralizar em constantes
- Lógica de negócio dentro de endpoints
- Comentários que descrevem O QUE o código faz — só comentar o PORQUÊ quando não óbvio
