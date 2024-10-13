# Fase 1: Construcción de la aplicación
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copia el archivo .csproj y restaura las dependencias
COPY *.csproj ./
RUN dotnet restore

# Copia el resto de los archivos y construye la aplicación
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Fase 2: Generación de la imagen para producción
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./

# Exponer el puerto en el que se ejecutará la aplicación
EXPOSE 80

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "DemoApi.dll"]
