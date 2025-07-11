-- Crear base de datos REGISTRO
CREATE DATABASE REGISTRO;
GO

-- Usar base de datos REGISTRO
USE REGISTRO;
GO

-- Crear tabla alumno
CREATE TABLE alumno (
    Codigo INT PRIMARY KEY,
    Nombres NVARCHAR(100),
    Apellidos NVARCHAR(100),
    Direccion NVARCHAR(200)
);

-- Verificar tabla
SELECT * FROM alumno;
