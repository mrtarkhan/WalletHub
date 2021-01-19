#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/WalletHub.Crawler/WalletHub.Crawler.csproj", "src/WalletHub.Crawler/"]
RUN dotnet restore "src/WalletHub.Crawler/WalletHub.Crawler.csproj"
COPY . .
WORKDIR "/src/src/WalletHub.Crawler"
RUN dotnet build "WalletHub.Crawler.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WalletHub.Crawler.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WalletHub.Crawler.dll"]