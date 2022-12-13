#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ApiGateway/Tsi.Erp.ApiGateway/Tsi.Erp.ApiGateway.csproj", "ApiGateway/Tsi.Erp.ApiGateway/"]
RUN dotnet restore "ApiGateway/Tsi.Erp.ApiGateway/Tsi.Erp.ApiGateway.csproj"
COPY . .
WORKDIR "/src/ApiGateway/Tsi.Erp.ApiGateway"
RUN dotnet build "Tsi.Erp.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tsi.Erp.ApiGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tsi.Erp.ApiGateway.dll"]