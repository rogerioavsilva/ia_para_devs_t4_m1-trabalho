# 🧾 User Story - Registro de Despesas Corporativas

## 📌 Descrição

A empresa precisa de um endpoint (API) onde os funcionários possam registrar rapidamente suas despesas corporativas diárias, como transporte ou alimentação.

O sistema deve receber:
- Nome do funcionário
- Tipo de despesa
- Valor

---

## 🎯 User Story

**Como** funcionário  
**Quero** registrar minhas despesas corporativas (transporte ou alimentação)  
**Para** que eu seja reembolsado de forma rápida e controlada

---

## ✅ Critérios de Aceitação (BDD)

```gherkin
Cenário 1: Registrar despesa de transporte com sucesso
Dado que o funcionário informa seu nome, tipo "transporte" e um valor válido
Quando a requisição é enviada para o endpoint
Então o sistema deve registrar a despesa
E retornar uma mensagem de sucesso

Cenário 2: Registrar despesa de alimentação até R$ 100,00
Dado que o funcionário informa tipo "alimentação" com valor menor ou igual a 100,00
Quando a requisição é enviada
Então o sistema deve registrar a despesa
E retornar uma mensagem de sucesso

Cenário 3: Recusar despesa de alimentação acima de R$ 100,00
Dado que o funcionário informa tipo "alimentação" com valor maior que 100,00
Quando a requisição é enviada
Então o sistema não deve registrar a despesa
E deve retornar uma mensagem de erro informando que o valor excede o limite permitido
```

---

## 📏 Regras de Negócio

- Tipos de despesa permitidos:
  - transporte
  - alimentacao
- Campos obrigatórios:
  - Nome do funcionário
  - Tipo de despesa
  - Valor
- Regra crítica:
  - Despesas de alimentação acima de R$ 100,00 devem ser recusadas
- Despesas válidas:
  - Devem ser persistidas
  - Devem retornar mensagem de sucesso

---

## ⚠️ Edge Cases

- Valor igual a 100,00 → deve ser aceito
- Valor negativo → deve ser rejeitado
- Valor zero → sugerido rejeitar
- Tipo de despesa inválido → rejeitar
- Nome vazio ou nulo → rejeitar
- Case insensitive no tipo → normalizar
- Precisão monetária → usar BigDecimal
- Caracteres especiais no nome → validar encoding

---

## 🔧 Especificação Técnica (Sugestão)

### Endpoint

POST /api/despesas

### Request

```json
{
  "nomeFuncionario": "João Silva",
  "tipoDespesa": "alimentacao",
  "valor": 85.50
}
```

### Response - Sucesso

```json
{
  "mensagem": "Despesa registrada com sucesso"
}
```

### Response - Erro

```json
{
  "erro": "Despesa de alimentação não pode ultrapassar R$ 100,00"
}
```

---

## 🧠 Considerações Técnicas

- Utilizar Bean Validation
- Criar enum para tipo de despesa
- Implementar validação na camada de serviço
- Evitar uso de double para valores monetários

---

## 💡 Observação de Evolução

Preparar estrutura para expansão futura (Strategy Pattern ou Rule Engine simples), evitando regras hardcoded.
