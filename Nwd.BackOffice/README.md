# Packages
1. EntityFramework
2.


# Migrations
$ dotnet ef migrations add InitialDb --output-dir Migrations --context IntegrationEventLogContext --project ../BuildingBlock.IntegrationEvent/BuildingBlock.IntegrationEventLog.csproj
$ dotnet ef database update --context IntegrationEventLogContext --project ../BuildingBlock.IntegrationEvent/BuildingBlock.IntegrationEventLog.csproj

$ dotnet ef migrations add InitialDb --output-dir SalesContext/Data/Migrations --context SalesDbContext
$ dotnet ef database update --context SalesDbContext
