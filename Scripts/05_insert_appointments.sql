-- Вставка данных записей клиентов
-- Выполните этот скрипт ПЯТЫМ

\c tattoostudio;

-- Очистить таблицу перед вставкой (если нужно)
-- TRUNCATE "Appointments" RESTART IDENTITY CASCADE;

INSERT INTO "Appointments" ("ClientName", "ClientPhone", "ClientEmail", "AppointmentDate", "DurationHours", "Status", "Notes", "TattooId", "MasterId", "CreatedAt") VALUES
('Иван Петров', '+7-999-888-77-66', 'ivan@mail.ru', '2025-05-15 14:00:00', 3, 'Scheduled', 'Первый раз, нужна консультация', 1, 1, NOW()),
('Елена Смирнова', '+7-999-777-66-55', 'elena@mail.ru', '2025-05-16 11:00:00', 2, 'Scheduled', 'Уточнить детали', 2, 2, NOW()),
('Павел Новиков', '+7-999-666-55-44', NULL, '2025-05-10 16:00:00', 4, 'Completed', NULL, 3, 3, NOW()),
('Анна Кузнецова', '+7-999-555-44-33', 'anna@mail.ru', '2025-05-20 12:00:00', 3, 'Scheduled', 'Эскиз по фотографии', 4, 1, NOW()),
('Сергей Морозов', '+7-999-444-33-22', 'sergey@mail.ru', '2025-05-22 15:00:00', 2, 'Cancelled', 'Перенос на июнь', 5, 2, NOW());

-- Проверка
SELECT 
    a."Id", 
    a."ClientName", 
    a."AppointmentDate", 
    a."Status",
    m."FullName" AS "MasterName"
FROM "Appointments" a
JOIN "Masters" m ON a."MasterId" = m."Id";