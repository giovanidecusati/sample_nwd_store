FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY Nwd.BackOffice/Nwd.BackOffice.csproj Nwd.BackOffice/
COPY BuildingBlock.Core/BuildingBlock.Core.csproj BuildingBlock.Core/
RUN dotnet restore Nwd.BackOffice/Nwd.BackOffice.csproj
COPY . .
WORKDIR /src/Nwd.BackOffice
RUN dotnet build Nwd.BackOffice.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Nwd.BackOffice.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Nwd.BackOffice.dll"]
