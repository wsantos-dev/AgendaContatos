# Etapa 1: Build da aplicação
FROM node:20-alpine AS build


WORKDIR /app

# Copia package.json e package-lock.json (se existir)
COPY package*.json ./

# Instala as dependências
RUN npm install

# Copia todo o código fonte para o container
COPY . .


RUN npm run build


FROM nginx:alpine


RUN rm -rf /usr/share/nginx/html/*


COPY --from=build /app/dist /usr/share/nginx/html


# Expõe a porta 80
EXPOSE 80

# Comando para rodar o Nginx em primeiro plano
CMD ["nginx", "-g", "daemon off;"]
