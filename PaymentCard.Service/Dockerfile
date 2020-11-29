#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["PaymentCard.Service/PaymentCard.Service.csproj", "PaymentCard.Service/"]
RUN dotnet restore "PaymentCard.Service/PaymentCard.Service.csproj"
COPY . .
WORKDIR "/src/PaymentCard.Service"
RUN dotnet build "PaymentCard.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PaymentCard.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentCard.Service.dll"]