-- Вставка данных мастеров
-- Выполните этот скрипт ТРЕТЬИМ

\c tattoostudio;

-- Очистить таблицу перед вставкой (если нужно)
-- TRUNCATE "Masters" RESTART IDENTITY CASCADE;

INSERT INTO "Masters" ("FullName", "Specialization", "ExperienceYears", "Email", "Phone", "PortfolioUrl", "IsActive", "CreatedAt") VALUES
('Алексей Соколов', 'Реализм, портреты', 8, 'alexey@tattoostudio.com', '+7-999-123-45-67', 'https://instagram.com/alexey.tattoo', true, NOW()),
('Мария Иванова', 'Олдскул, традишнл', 5, 'maria@tattoostudio.com', '+7-999-234-56-78', 'https://instagram.com/maria.oldchool', true, NOW()),
('Дмитрий Волков', 'Графика, черно-белое', 12, 'dmitry@tattoostudio.com', '+7-999-345-67-89', 'https://instagram.com/dmitry.blackwork', true, NOW());

-- Проверка
SELECT "Id", "FullName", "Specialization" FROM "Masters";