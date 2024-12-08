FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/services/NSE.Cliente.API/NSE.Cliente.API.csproj", "src/services/NSE.Cliente.API/"]
RUN dotnet restore "src/services/NSE.Cliente.API/NSE.Cliente.API.csproj"
COPY . .
WORKDIR "/src/src/services/NSE.Cliente.API"
RUN dotnet build "NSE.Cliente.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "NSE.Cliente.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NSE.Cliente.API.dll"]
