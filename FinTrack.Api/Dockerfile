# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY FinTrack.Api/FinTrack.Api.csproj FinTrack.Api/
RUN dotnet restore FinTrack.Api/FinTrack.Api.csproj

COPY . .
WORKDIR /src/FinTrack.Api
RUN dotnet publish FinTrack.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

# Etapa de runtime: imagem menor, sem SDK
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

COPY --from=build /app/publish .

RUN mkdir -p /app/data && chown -R $APP_UID:$APP_UID /app
USER $APP_UID

EXPOSE 8080
ENTRYPOINT ["dotnet", "FinTrack.Api.dll"]
