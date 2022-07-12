# authentication-is
Authentication Service Project

## Secrets
### Jantz.Authentication.Api
```json
{
  "ConnectionStrings": {
    "DatabaseConnection": "Host=localhost;Port=5432;Pooling=true;Database=__database__;User Id=__database_user__;Password=__database_password__;"
  },
  "ApplyPatchs": true,
  "JwtSettings": {
    "Secret": "__jwt_secret__",
    "Audience": "__audience__",
    "Issuer": "__issuer__",
    "ExpirationMinutes": 3600
  }
}
```

## Entity Framework

### Instalação `dotnet-ef`

1. No powershell, executar o seguinte comando
```bash
dotnet tool install --global dotnet-ef
```

### Criação/Atualização do banco (ambiente desenvolvimento)
Em um prompt de comando:
1. Ir para a pasta raiz do projeto
2. Ir para a pasta `src`
3. Executar o comando
```bash
dotnet ef database update -s "Jantz.Authentication.Api" -p "Jantz.Authentication.Infra.Data"
```
O comando irá criar no banco informado no secrets do **Jantz.Authentication.Api** (startup project) as tabelas baseado nas Migrations do **Jantz.Authentication.Infra.Data** (projeto que será usado).

### Criação de Migrations
Em um prompt de comando:
1. Ir para a pasta raiz do projeto
2. Ir para a pasta `src`
3. Executar o comando
```bash
dotnet ef migrations add [NomeMigration] -s "Jantz.Authentication.Api" -p "Jantz.Authentication.Infra.Data"
```
O comando irá criar a Migration dentro de **Jantz.Authentication.Infra.Data**.