# TesteOData
Estudando uma API com ASP.Net Core e OData

## Contexto
Esta API manipula as iformações da entidade `Student`, sendo esta, um modelo contendo alguns simples atributos para motivos de testes.

## Rodando a API
Para rodar: `dotnet run`
O endereço padrão está configurado para: `http://localhost:5000/odata/Students`

Há um arquivo chamado `TesteOData.postman_collection.json`, nele há exemplos de como usar a API.

A API usa o Entity Framework Core em memória, logo os valores dos IDs das entidades pré-configuradas mudam randomicamente a cada nova execução da API.
