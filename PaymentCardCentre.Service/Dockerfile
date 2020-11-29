#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["PaymentCardCentre.Service/PaymentCardCentre.Service.csproj", "PaymentCardCentre.Service/"]
RUN dotnet restore "PaymentCardCentre.Service/PaymentCardCentre.Service.csproj"
COPY . .
WORKDIR "/src/PaymentCardCentre.Service"
RUN dotnet build "PaymentCardCentre.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PaymentCardCentre.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentCardCentre.Service.dll"]