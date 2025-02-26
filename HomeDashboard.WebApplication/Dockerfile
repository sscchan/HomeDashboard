FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS backend_build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HomeDashboard.WebApplication/HomeDashboard.WebApplication.csproj", "HomeDashboard.WebApplication/"]
RUN dotnet restore "HomeDashboard.WebApplication/HomeDashboard.WebApplication.csproj"
COPY . .
WORKDIR "/src/HomeDashboard.WebApplication"
RUN dotnet build "HomeDashboard.WebApplication.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM backend_build AS backend_publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HomeDashboard.WebApplication.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM node:22-alpine AS frontend_build
WORKDIR /src
COPY ["HomeDashboard.WebClient", "HomeDashboard.WebClient"]
WORKDIR "/src/HomeDashboard.WebClient"
RUN npm install
RUN npm run build

FROM base AS final
WORKDIR /app
COPY --from=backend_publish /app/publish .
COPY --from=frontend_build /src/HomeDashboard.WebClient/dist ./wwwroot
ENTRYPOINT ["dotnet", "HomeDashboard.WebApplication.dll"]
