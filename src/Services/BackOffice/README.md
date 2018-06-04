# Packages
1. EntityFramework
2.

# Sample
1. https://github.com/Azure-Samples/active-directory-dotnet-webapp-webapi-openidconnect-aspnetcore
2. https://docs.microsoft.com/en-us/azure/api-management/api-management-howto-protect-backend-with-aad
3. https://blogs.msdn.microsoft.com/devkeydet/2016/03/22/using-postman-with-azure-ad/

# Migrations
$ dotnet ef migrations add InitialDb --output-dir Migrations --context IntegrationEventLogContext --project ../BuildingBlock.IntegrationEvent/BuildingBlock.IntegrationEventLog.csproj
$ dotnet ef database update --context IntegrationEventLogContext --project ../BuildingBlock.IntegrationEvent/BuildingBlock.IntegrationEventLog.csproj

$ dotnet ef migrations add InitialDb --output-dir SalesContext/Data/Migrations --context SalesDbContext
$ dotnet ef database update --context SalesDbContext
