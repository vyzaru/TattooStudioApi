# 🎨 Tattoo Studio API

REST API для управления тату-салоном. Позволяет управлять мастерами, татуировками и записями клиентов.

## 📋 Технологии

- **.NET 6.0** — основной фреймворк
- **Entity Framework Core 6.0** — ORM для работы с базой данных
- **PostgreSQL 15** — реляционная база данных
- **Swagger / OpenAPI** — документация API
- **Docker & Docker Compose** — контейнеризация

## 🚀 Быстрый старт

### Требования

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [PostgreSQL 15+](https://www.postgresql.org/download/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (опционально)

### Локальный запуск

#### 1. Клонируйте репозиторий

```bash
git clone <your-repo-url>
cd PetAPI
```

#### 2. Настройте базу данных

Выполните SQL скрипты в папке Scripts/ в указанном порядке:

```bash
# Подключитесь к PostgreSQL
psql -h localhost -U postgres

# Выполните скрипты по порядку
\i Scripts/01_create_database.sql
\c tattoostudio
\i Scripts/02_create_tables.sql
\i Scripts/03_insert_masters.sql
\i Scripts/04_insert_tattoos.sql
\i Scripts/05_insert_appointments.sql
```

#### 3. Настройте строку подключения

Отредактируйте appsettings.json:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=tattoostudio;Username=postgres;Password=your_password"
  }
}
```

#### 4. Запустите приложение

```bash
dotnet restore
dotnet build
dotnet run
```

#### 5. Откройте Swagger

```text
https://localhost:5001/swagger
```

### Запуск через Docker

```bash
# Запустить контейнеры
docker-compose up -d

# Остановить контейнеры
docker-compose down

# Остановить с удалением данных
docker-compose down -v
```

После запуска API будет доступен по адресу:

API: http://localhost:5000

Swagger: http://localhost:5000/swagger

PostgreSQL: localhost:5432
