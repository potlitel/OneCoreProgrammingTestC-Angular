-- ========================================================================================= ----
-- Este script debe ser ejecutado en el servidor donde se desea probar el ejercicio Onecore
-- ========================================================================================= ----
-- Detalle	  : Script para crear tabla Compras.
-- Autor	    : Equipo Desarrollo Cuba
-- Creacion	  : Creación:1/18/2023 10:08 PM
-- Modificado por : 
-- Modificado     : 
-- ========================================================================================= ----
USE OnecoreProgrammingTest
GO
IF OBJECT_ID('dbo.Compras', 'U') IS NOT NULL 
  DROP TABLE dbo.Compras; 

CREATE TABLE [dbo].[Compras](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [IdCliente] [int] NOT NULL,
    [IdDocumento] [int] NOT NULL,
    [Cantidad] [int] NOT NULL,
    [Descripcion] [varchar](150) NOT NULL,
    [PrecioUnitario] [MONEY] NOT NULL,
    [Total] [MONEY] NOT NULL,
 CONSTRAINT [PK_Compras] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
 
GO
 
SET ANSI_PADDING OFF
GO
 
ALTER TABLE [dbo].[Compras]  WITH CHECK ADD  CONSTRAINT [FK_Compras_Cliente] FOREIGN KEY([IdCliente])
REFERENCES [dbo].[Clientes] ([Id])
GO

ALTER TABLE [dbo].[Compras]  WITH CHECK ADD  CONSTRAINT [FK_Compras_Documento] FOREIGN KEY([IdDocumento])
REFERENCES [dbo].[Documentos] ([Id])
GO
 
ALTER TABLE [dbo].[Compras] CHECK CONSTRAINT [FK_Compras_Cliente]
GO