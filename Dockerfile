#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["TransactionDiscovery.Host/TransactionDiscovery.Host.csproj", "TransactionDiscovery.Host/"]
RUN dotnet restore "TransactionDiscovery.Host/TransactionDiscovery.Host.csproj"
COPY . .
WORKDIR "/src/TransactionDiscovery.Host"
RUN dotnet build "TransactionDiscovery.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TransactionDiscovery.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TransactionDiscovery.Host.dll"]