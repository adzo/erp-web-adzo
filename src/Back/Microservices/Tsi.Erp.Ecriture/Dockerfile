#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Microservices/Tsi.Erp.Ecriture/Tsi.Erp.Ecriture.csproj", "Microservices/Tsi.Erp.Ecriture/"]
RUN dotnet restore "Microservices/Tsi.Erp.Ecriture/Tsi.Erp.Ecriture.csproj"
COPY . .
WORKDIR "/src/Microservices/Tsi.Erp.Ecriture"
RUN dotnet build "Tsi.Erp.Ecriture.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tsi.Erp.Ecriture.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tsi.Erp.Ecriture.dll"]