
# 📘 Agenda de Contatos

Aplicação para gerenciamento de contatos.

---

## ✅ Regras de Validação


### 📌 Nome

- Obrigatório
- Mínimo de 3 caracteres


### 📌 Email

- Obrigatório
- Formato válido: nome.sobrenome@dominio.com.br


### Telefone

- Obrigatório
- Somente números (10 ou 11 dígitos)
- Deve conter DDD válido (ex: 11 a 99)




---

## 🧰 O que utilizei neste projeto


- WebAPI .NET Core 8 
- Banco de Dados SQL Server 2022 (via Docker)
- Padrão CQRS
- Padrão Repository
- Services
- Controllers
- Interfaces
- Injeção de Dependência
- Entity Framework Core 8
- Libs MediatR, AutoMapper e FluentValidation)
- Autenticação/Autorização no Backend com JWT (JSON Web Token)
- Swagger
- Cobertura de testes no Backend
- Docker



## 📥 Clonando o Projeto

Antes de tudo, é necessário realizar o clone deste projeto. Selecione o terminal Git de sua preferência e execute:

```bash
git clone https://github.com/wsantos-dev/AgendaContatos.git
```

---

## 🐳 Requisitos

Instale o Docker Desktop conforme sua plataforma (Caso não tenha instalado em sua máquina):

- **Windows:** https://docs.docker.com/desktop/setup/install/windows-install/
- **Linux:** https://docs.docker.com/desktop/setup/install/linux/
- **MacOS:** https://docs.docker.com/desktop/setup/install/mac-install/

---

## ⚙️ Configuração Inicial

### 1. Criar uma rede Docker

```bash
docker network create dev_network
```


### 2. Criar um container SQL Server 2022

> ⚠️ Para Linux/MacOS substitua os acentos graves (\`) por barras invertidas (`\`)

```powershell
docker run `
  -e "ACCEPT_EULA=Y" `
  -e "SA_PASSWORD=DotNet@2025" `
  -e "MSSQL_PID=Express" `
  -p 1433:1433 `
  --name sqlserver-agenda `
  --network dev_network `
  -v meu_banco:/var/opt/mssql `
  -d mcr.microsoft.com/mssql/server:2022-latest
```

---

## 🧱 Configuração da API

1. Acesse a solução `.sln` com Visual Studio 2022:  
   `AgendaContatos\backend\backend\Agenda.sln`

2. Acesse o menu:  
   `Tools` → `NuGet Package Manager` → `Package Manager Console`

3. Em **Default Project**, selecione o projeto `API`.

4. Execute o comando:

```bash
dotnet ef database update
```

---

## 🚀 Executando os Containers

No terminal, vá até a raiz do projeto onde está o `docker-compose.yml`:

```bash
cd AgendaContatos
```

Execute o seguinte comando:

```bash
docker-compose up --build
```

---

## 📘 Testando a API com Swagger

Acesse no navegador:

```
http://localhost:5000/swagger/index.html
```

### 🔐 Autenticação JWT

1. Vá até o endpoint `POST /api/Auth/login` e clique em **Try it out**.
2. No corpo da requisição, informe:

```json
{
  "email": "admin.dev@agendaapp.com",
  "senha": "De$af10Ag3nd@DotNet#$2025!#"
}
```

3. Clique em **Execute** e copie o token JWT retornado.
4. Clique em **Authorize** e cole no campo:

```
Bearer <seu-token>
```

Substitua `<seu-token>` pelo token JWT que você recebeu.

---
