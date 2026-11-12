# FinTrack API

API de gestão financeira pessoal desenvolvida com ASP.NET Core .NET 8, Entity Framework Core e SQLite.

## Requisitos
- .NET SDK 8.0
- Ferramenta dotnet-ef 8.x
- Docker Desktop (opcional)

## Estrutura
- `FinTrack.Api`: aplicação Web API.
- `FinTrack.Tests`: testes unitários xUnit.
- `Models`: entidades de domínio.
- `DTOs`: contratos de entrada.
- `Validators`: regras de validação.
- `Data`: contexto e migrations do EF Core.
- `Services`: regras de negócio.
- `Controllers`: endpoints HTTP.

## Restaurar, compilar e testar
```bash
dotnet restore
dotnet build
dotnet test
```

## Criar/aplicar migration
Na raiz da solution:
```bash
dotnet tool install --global dotnet-ef --version 8.0.11
dotnet ef migrations add InitialCreate --project FinTrack.Api --startup-project FinTrack.Api --output-dir Data/Migrations
dotnet ef database update --project FinTrack.Api --startup-project FinTrack.Api
```

## Executar localmente
```bash
dotnet run --project FinTrack.Api
```
Acesse a URL HTTPS/HTTP exibida no terminal e acrescente `/swagger`. O Swagger é habilitado no ambiente Development.

## Docker Compose
Execute a partir da pasta raiz:
```bash
docker compose -f FinTrack.Api/docker-compose.yml up --build
```
A API ficará disponível em `http://localhost:8080`. Para encerrar:
```bash
docker compose -f FinTrack.Api/docker-compose.yml down
```
O banco fica no volume Docker `fintrack-data` e permanece após reiniciar o container. Para apagar também os dados:
```bash
docker compose -f FinTrack.Api/docker-compose.yml down -v
```

## Endpoints
- `GET /api/categorias`
- `GET /api/categorias/{id}`
- `POST /api/categorias`
- `GET /api/transacoes`
- `GET /api/transacoes/{id}`
- `POST /api/transacoes`

## Exemplos de payload

Categoria:
```json
{
  "nome": "Alimentação",
  "cor": "#FF9800"
}
```

Transação:
```json
{
  "descricao": "Mercado",
  "valor": 250.75,
  "tipo": 2,
  "data": "2026-11-09T12:00:00",
  "categoriaId": 1
}
```

`tipo`: 1 = Receita; 2 = Despesa.

## Observações
- As migrations são geradas pelo comando `dotnet ef migrations add`.
- O Dockerfile usa build multi-stage: SDK somente na etapa de compilação e runtime na imagem final.
- O banco SQLite é persistido em `/app/data/fintrack.db` no container.
- Em produção real, recomenda-se autenticação/autorização, tratamento global de exceções, paginação, logging estruturado, configuração segura de CORS e estratégia de migrations no deploy.
