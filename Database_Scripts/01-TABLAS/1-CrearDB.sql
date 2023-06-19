-- ========================================================================================= ----
-- Este script debe ser ejecutado en el servidor donde se desea probar el ejercicio Onecore
-- ========================================================================================= ----
-- Detalle	  : Script para crear base de datos inicial.
-- Autor	    : Equipo Desarrollo Cuba
-- Creacion	  : Creación:1/18/2023 9:49 PM
-- Modificado por : 
-- Modificado     : 
-- ========================================================================================= ----
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'OnecoreProgrammingTest')
BEGIN
  CREATE DATABASE OnecoreProgrammingTest;
END;
GO