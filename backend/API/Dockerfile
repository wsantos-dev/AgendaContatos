# Etapa 1: build da aplica��o
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar csproj e restaurar depend�ncias
COPY backend/API/*.csproj ./API/
RUN dotnet restore ./API/API.csproj

# Copiar o restante do c�digo e publicar
COPY backend/. ./backend/

WORKDIR /app/backend/API
RUN dotnet publish -c Release -o out

# imagem final
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS runtime
WORKDIR /app


# Copiar app publicada
COPY --from=build /app/backend/API/out ./

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "API.dll"]

