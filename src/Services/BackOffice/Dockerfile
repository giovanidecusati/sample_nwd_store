FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR .
COPY ./src/BuildingBlocks/ BuildingBlocks/
COPY ./src/Services/BackOffice Services/BackOffice
COPY ./src/Contracts/BackOffice.Sales.Contracts Contracts/BackOffice.Sales.Contracts
RUN dotnet restore Services/BackOffice/BackOffice.csproj

WORKDIR ./Services/BackOffice
RUN dotnet build BackOffice.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish BackOffice.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BackOffice.dll"]
