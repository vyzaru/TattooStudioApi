\c tattoostudio;

CREATE TABLE "Masters" (
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

CREATE TABLE "Tattoos" (
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

CREATE TABLE "Appointments" (
    "Id" SERIAL PRIMARY KEY,
    "ClientName" VARCHAR(100) NOT NULL,
    "ClientPhone" VARCHAR(20) NOT NULL,
    "ClientEmail" VARCHAR(200),
    "AppointmentDate" TIMESTAMP NOT NULL,
    "DurationHours" INT NOT NULL DEFAULT 1,
    "Notes" TEXT,
    "TattooId" INT REFERENCES "Tattoos"("Id") ON DELETE SET NULL,
    "MasterId" INT NOT NULL REFERENCES "Masters"("Id") ON DELETE CASCADE,
    "CreatedAt" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX "IX_Tattoos_MasterId" ON "Tattoos"("MasterId");
CREATE INDEX "IX_Appointments_MasterId" ON "Appointments"("MasterId");
CREATE INDEX "IX_Appointments_AppointmentDate" ON "Appointments"("AppointmentDate");
CREATE INDEX "IX_Masters_Email" ON "Masters"("Email");

INSERT INTO "Masters" ("FullName", "Specialization", "ExperienceYears", "Email", "Phone", "PortfolioUrl", "IsActive", "CreatedAt") VALUES
('Алексей Соколов', 'Реализм, портреты', 8, 'alexey@tattoostudio.com', '+7-999-123-45-67', 'https://instagram.com/alexey.tattoo', true, NOW()),
('Мария Иванова', 'Олдскул, традишнл', 5, 'maria@tattoostudio.com', '+7-999-234-56-78', 'https://instagram.com/maria.oldchool', true, NOW()),
('Дмитрий Волков', 'Графика, черно-белое', 12, 'dmitry@tattoostudio.com', '+7-999-345-67-89', 'https://instagram.com/dmitry.blackwork', true, NOW());

INSERT INTO "Tattoos" ("Title", "Description", "Style", "BodyPlacement", "ImageUrl", "Price", "MasterId", "CreatedAt") VALUES
('Лев в реализме', 'Портрет африканского льва', 'Реализм', 'Плечо', '/images/lion.jpg', 25000, 1, NOW()),
('Роза с черепом', 'Классический олдскул мотив', 'Олдскул', 'Предплечье', '/images/rose_skull.jpg', 15000, 2, NOW()),
('Геометрический волк', 'Волк из геометрических фигур', 'Графика', 'Голень', '/images/wolf.jpg', 20000, 3, NOW());

INSERT INTO "Appointments" ("ClientName", "ClientPhone", "ClientEmail", "AppointmentDate", "DurationHours", "Notes", "TattooId", "MasterId", "CreatedAt") VALUES
('Иван Петров', '+7-999-888-77-66', 'ivan@mail.ru', '2025-05-15 14:00:00', 3, 'Первый раз', 1, 1, NOW()),
('Елена Смирнова', '+7-999-777-66-55', 'elena@mail.ru', '2025-05-16 11:00:00', 2, 'Уточнить детали', 2, 2, NOW()),
('Павел Новиков', '+7-999-666-55-44', NULL, '2025-05-10 16:00:00', 4, NULL, 3, 3, NOW());