#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80 8080

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ApiGateways/ClientAggregator/clientaggregator.api/clientaggregator.api.csproj", "ApiGateways/ClientAggregator/clientaggregator.api/"]
COPY ["Common/Common.API.Authentication/Common.API.Authentication.csproj", "Common/Common.API.Authentication/"]
COPY ["ApiGateways/ClientAggregator/clientaggregator.infrastructure/clientaggregator.infrastructure.csproj", "ApiGateways/ClientAggregator/clientaggregator.infrastructure/"]
COPY ["Common/Common.Infrastructure/Common.Infrastructure.csproj", "Common/Common.Infrastructure/"]
COPY ["ApiGateways/ClientAggregator/clientaggregator.application/clientaggregator.application.csproj", "ApiGateways/ClientAggregator/clientaggregator.application/"]
COPY ["Common/Common.Application/Common.Application.csproj", "Common/Common.Application/"]
COPY ["Common/Common.API/Common.API.csproj", "Common/Common.API/"]
COPY ["Common/Common.Utilities/Common.Utilities.csproj", "Common/Common.Utilities/"]
COPY ["Common/Common.API.Swagger/Common.API.Swagger.csproj", "Common/Common.API.Swagger/"]
COPY ["Common/Common.Authentication/Common.Authentication.csproj", "Common/Common.Authentication/"]
RUN dotnet restore "ApiGateways/ClientAggregator/clientaggregator.api/clientaggregator.api.csproj"
COPY . .
WORKDIR "/src/ApiGateways/ClientAggregator/clientaggregator.api"
RUN dotnet build "clientaggregator.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "clientaggregator.api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "clientaggregator.api.dll"]
