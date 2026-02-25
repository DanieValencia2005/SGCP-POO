# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar csproj y restaurar dependencias
COPY *.csproj ./
RUN dotnet restore

# Copiar el resto de archivos
COPY . ./

# Publicar la aplicación en modo Release
RUN dotnet publish SGCP_POO.csproj -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Puerto que usará la app en Render
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Comando para iniciar la app
ENTRYPOINT ["dotnet", "SGCP_POO.dll"]
