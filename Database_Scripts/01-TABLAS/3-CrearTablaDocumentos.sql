-- ========================================================================================= ----
-- Este script debe ser ejecutado en el servidor donde se desea probar el ejercicio Onecore
-- ========================================================================================= ----
-- Detalle	  : Script para crear tabla Documentos.
-- Autor	    : Equipo Desarrollo Cuba
-- Creacion	  : Creación:1/18/2023 10:08 PM
-- Modificado por : 
-- Modificado     : 
-- ========================================================================================= ----
USE OnecoreProgrammingTest
GO
IF OBJECT_ID('dbo.Documentos', 'U') IS NOT NULL 
BEGIN
  DECLARE @sqlDeleteFk nvarchar(MAX)
	--Obtenemos el foreign que hace referencia a la tabla Clientes(y la sentencia que la elimina) y 
	--la almacenamos en la variable @sqlDeleteFk
	SET @sqlDeleteFk = (SELECT 
    'ALTER TABLE [' +  OBJECT_SCHEMA_NAME(parent_object_id) +
    '].[' + OBJECT_NAME(parent_object_id) + 
    '] DROP CONSTRAINT [' + name + ']'
	FROM sys.foreign_keys
	WHERE referenced_object_id = object_id('Documentos'))
	--Ejecutamos la sentencia sql para eliminar la llave foránea
	EXEC sp_executesql @sqlDeleteFk
  DROP TABLE dbo.Documentos;
END

CREATE TABLE [dbo].[Documentos](
    [IdDocum] [int] IDENTITY(1,1) NOT NULL,
    [Nombre] [varchar](150) NOT NULL,
    [DirecFisica] [varchar](150) NOT NULL,
    [EstadoNotifi] [char] NOT NULL,
    [Fecha] [datetime] NOT NULL,
 CONSTRAINT [PK_Documentos] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
 
GO
