#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["TransactionDiscorvery.Host/TransactionDiscorvery.Host.csproj", "TransactionDiscorvery.Host/"]
RUN dotnet restore "TransactionDiscorvery.Host/TransactionDiscorvery.Host.csproj"
COPY . .
WORKDIR "/src/TransactionDiscorvery.Host"
RUN dotnet build "TransactionDiscorvery.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TransactionDiscorvery.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TransactionDiscorvery.Host.dll"]