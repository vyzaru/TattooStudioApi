-- Создание базы данных TattooStudio
-- Выполните этот скрипт ПЕРВЫМ

-- Удаляем базу, если существует (осторожно!)
-- DROP DATABASE IF EXISTS tattoostudio;

-- Создаём новую базу данных с правильной кодировкой
CREATE DATABASE tattoostudio
    WITH 
    ENCODING = 'UTF8'
    LC_COLLATE = 'Russian_Russia.UTF-8'
    LC_CTYPE = 'Russian_Russia.UTF-8'
    TEMPLATE = template0;

-- Подключаемся к новой базе
\c tattoostudio;

-- Создаём схему public (по умолчанию уже есть, но для уверенности)
CREATE SCHEMA IF NOT EXISTS public;