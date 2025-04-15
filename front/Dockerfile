# Базовый образ для сборки Node.js приложения
FROM node:20 AS build

# Установка рабочей директории
WORKDIR /app

# Копирование package.json и package-lock.json
COPY package*.json ./

# Установка зависимостей
RUN npm install

# Копирование остальных файлов
COPY . .

# Сборка приложения
RUN npm run build

# Финальный образ для выполнения приложения
FROM node:20-alpine

# Установка рабочей директории
WORKDIR /app

# Копирование собранных файлов из этапа build
COPY --from=build /app .

# Порт, на котором работает Next.js
EXPOSE 3000

# Запуск приложения
CMD ["npm", "start"]