# authentication-identity
[![GitHub Action Build Status][gh-actions-image]][gh-actions-url]

Authentication Project using .NET 6 with EF Core Identity

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
    "ExpirationSeconds": 3600
  }
}
```

## Entity Framework

### Prerequisites
- [`dotnet` CLI](https://docs.microsoft.com/en-us/dotnet/core/install/windows?tabs=net60)
- [`dotnet-ef` CLI](https://docs.microsoft.com/en-us/ef/core/get-started/overview/install#get-the-net-core-cli-tools)

### Create/Update Database
In a command prompt:
1. Go to `./src`
2. Run the following command:
```bash
dotnet ef database update -s "Jantz.Authentication.Api" -p "Jantz.Authentication.Infra.Data"
```
This command will create/update the tables on the database informed on startup project secrets (**Jantz.Authentication.Api**) based on existing migrations in **Jantz.Authentication.Infra.Data** project. 

### Creating Migrations
In a command prompt:
1. Go to `./src`
2. Run the following command:
```bash
dotnet ef migrations add [MigrationDescription] -s "Jantz.Authentication.Api" -p "Jantz.Authentication.Infra.Data"
```
This command will create a Migration in **Jantz.Authentication.Infra.Data** project. 

> Don't forget to change `[MigrationDescription]`.

[gh-actions-url]: https://github.com/alanjantz/authentication-identity/actions/workflows/dotnet-build.yml?query=branch%3Amain
[gh-actions-image]: https://img.shields.io/github/workflow/status/tldr-pages/tldr-node-client/Test/master
