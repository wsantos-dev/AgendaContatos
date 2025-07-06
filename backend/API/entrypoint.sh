#!/bin/sh

echo "⏳ Aguardando o SQL Server ficar disponível..."

# Aguarda a porta 1433 do SQL Server
until nc -z sqlserver 1433; do
  sleep 1
done

echo "✅ SQL Server disponível - executando migrations..."

export PATH="$PATH:/root/.dotnet/tools"

dotnet ef database update --project ./API --startup-project ./API

echo "✅ Migrations aplicadas. Iniciando a aplicação..."

dotnet API.dll