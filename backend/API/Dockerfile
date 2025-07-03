# Use a imagem oficial do SDK do .NET para construir a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copiar os arquivos csproj e restaurar dependências
COPY backend/API/*.csproj ./API/
RUN dotnet restore ./API/API.csproj

# Copiar o restante do código e publicar
COPY backend/. ./backend/
WORKDIR /app/backend/API
RUN dotnet publish -c Release -o out

# Imagem final para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/backend/API/out ./

ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "API.dll"]
