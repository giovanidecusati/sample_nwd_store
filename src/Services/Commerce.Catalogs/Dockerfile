FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR .
COPY ./src/BuildingBlocks/ BuildingBlocks/
COPY ./src/Contracts/BackOffice.Sales.Contracts Contracts/BackOffice.Sales.Contracts
COPY ./src/Services/Commerce.Catalogs Services/Commerce.Catalogs
RUN dotnet restore Services/Commerce.Catalogs/Commerce.Catalogs.csproj

WORKDIR ./Services/Commerce.Catalogs
RUN dotnet build Commerce.Catalogs.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Commerce.Catalogs.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Commerce.Catalogs.dll"]
