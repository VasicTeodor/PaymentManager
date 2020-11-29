#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["BitCoin.Service/BitCoin.Service.csproj", "BitCoin.Service/"]
RUN dotnet restore "BitCoin.Service/BitCoin.Service.csproj"
COPY . .
WORKDIR "/src/BitCoin.Service"
RUN dotnet build "BitCoin.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BitCoin.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BitCoin.Service.dll"]