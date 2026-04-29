#language: pt
Funcionalidade: Registro de Despesas Corporativas
  Como funcionário
  Quero registrar minhas despesas corporativas (transporte ou alimentação)
  Para que eu seja reembolsado de forma rápida e controlada

  Cenário: Registrar despesa de transporte com sucesso
    Dado que o funcionário "Joao Silva" informa tipo "transporte" e valor 50.00
    Quando a requisição é enviada para o endpoint de despesas
    Então a resposta deve ter status 201
    E o corpo da resposta deve conter a mensagem "Despesa registrada com sucesso"

  Cenário: Registrar despesa de alimentação até R$ 100,00
    Dado que o funcionário "Maria Souza" informa tipo "alimentacao" e valor 100.00
    Quando a requisição é enviada para o endpoint de despesas
    Então a resposta deve ter status 201
    E o corpo da resposta deve conter a mensagem "Despesa registrada com sucesso"

  Cenário: Recusar despesa de alimentação acima de R$ 100,00
    Dado que o funcionário "Carlos Lima" informa tipo "alimentacao" e valor 150.00
    Quando a requisição é enviada para o endpoint de despesas
    Então a resposta deve ter status 422
    E o corpo da resposta deve conter o detalhe "Despesa de alimentação não pode ultrapassar"

  Cenário: Rejeitar despesa com valor negativo
    Dado que o funcionário "Ana Costa" informa tipo "transporte" e valor -10.00
    Quando a requisição é enviada para o endpoint de despesas
    Então a resposta deve ter status 400
    E o corpo da resposta deve conter o detalhe "maior que zero"

  Cenário: Rejeitar despesa com tipo inválido
    Dado que o funcionário "Paulo Melo" informa tipo "taxi" e valor 30.00
    Quando a requisição é enviada para o endpoint de despesas
    Então a resposta deve ter status 400
    E o corpo da resposta deve conter o detalhe "Tipo de despesa inválido"

  Cenário: Rejeitar despesa com nome vazio
    Dado que o funcionário "" informa tipo "transporte" e valor 30.00
    Quando a requisição é enviada para o endpoint de despesas
    Então a resposta deve ter status 400
    E o corpo da resposta deve conter o detalhe "Nome do funcionário"

  Cenário: Aceitar tipo com letras maiúsculas (case insensitive)
    Dado que o funcionário "Bia Torres" informa tipo "ALIMENTACAO" e valor 80.00
    Quando a requisição é enviada para o endpoint de despesas
    Então a resposta deve ter status 201
    E o corpo da resposta deve conter a mensagem "Despesa registrada com sucesso"
