-- Создание таблиц для TattooStudio
-- Выполните этот скрипт ВТОРЫМ

-- Подключаемся к базе данных
\c tattoostudio;

-- Таблица мастеров
CREATE TABLE IF NOT EXISTS "Masters" (
    "Id" SERIAL PRIMARY KEY,
    "FullName" VARCHAR(150) NOT NULL,
    "Specialization" VARCHAR(100) NOT NULL,
    "ExperienceYears" INT NOT NULL DEFAULT 0,
    "Email" VARCHAR(200) UNIQUE NOT NULL,
    "Phone" VARCHAR(20) NOT NULL,
    "PortfolioUrl" VARCHAR(500),
    "IsActive" BOOLEAN NOT NULL DEFAULT TRUE,
    "CreatedAt" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Таблица татуировок
CREATE TABLE IF NOT EXISTS "Tattoos" (
    "Id" SERIAL PRIMARY KEY,
    "Title" VARCHAR(200) NOT NULL,
    "Description" TEXT,
    "Style" VARCHAR(100) NOT NULL,
    "BodyPlacement" VARCHAR(100) NOT NULL,
    "ImageUrl" VARCHAR(500),
    "Price" DECIMAL(10,2) NOT NULL CHECK ("Price" > 0),
    "MasterId" INT NOT NULL REFERENCES "Masters"("Id") ON DELETE CASCADE,
    "CreatedAt" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Таблица записей клиентов
CREATE TYPE IF NOT EXISTS appointment_status AS ENUM ('Scheduled', 'Completed', 'Cancelled');

CREATE TABLE IF NOT EXISTS "Appointments" (
    "Id" SERIAL PRIMARY KEY,
    "ClientName" VARCHAR(100) NOT NULL,
    "ClientPhone" VARCHAR(20) NOT NULL,
    "ClientEmail" VARCHAR(200),
    "AppointmentDate" TIMESTAMP NOT NULL,
    "DurationHours" INT NOT NULL DEFAULT 1 CHECK ("DurationHours" > 0),
    "Status" appointment_status NOT NULL DEFAULT 'Scheduled',
    "Notes" TEXT,
    "TattooId" INT REFERENCES "Tattoos"("Id") ON DELETE SET NULL,
    "MasterId" INT NOT NULL REFERENCES "Masters"("Id") ON DELETE CASCADE,
    "CreatedAt" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Создание индексов для производительности
CREATE INDEX IF NOT EXISTS "IX_Tattoos_MasterId" ON "Tattoos"("MasterId");
CREATE INDEX IF NOT EXISTS "IX_Appointments_MasterId" ON "Appointments"("MasterId");
CREATE INDEX IF NOT EXISTS "IX_Appointments_AppointmentDate" ON "Appointments"("AppointmentDate");
CREATE INDEX IF NOT EXISTS "IX_Masters_Email" ON "Masters"("Email");