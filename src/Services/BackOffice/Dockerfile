FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR .
COPY ./BackOffice.csproj BackOffice/
RUN dotnet restore BackOffice/BackOffice.csproj

COPY . .
WORKDIR .
RUN dotnet build BackOffice.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish BackOffice.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Nwd.BackOffice.dll"]
