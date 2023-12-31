USE [OnecoreProgrammingTest]
GO
IF EXISTS (SELECT name FROM dbo.sysobjects 
WHERE id = OBJECT_ID(N'[dbo].[Select_Compras_byIdDocumento]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
    DROP PROCEDURE [dbo].[Select_Compras_byIdDocumento]
GO
-- =============================================
-- Author:		Reynol Gonzalez
-- Create date: 23/01/2023
-- Description:	Devuelve todas las compras dado un IdDocumento
-- =============================================
CREATE PROCEDURE [dbo].[Select_Compras_byIdDocumento] 
	@idDocumento int 
	
AS
BEGIN
	SELECT Nombre, IdCliente, Cantidad, Descripcion, PrecioUnitario, Total
  FROM Compras 
  INNER JOIN Clientes c ON Compras.IdCliente = c.Id 
  WHERE IdDocumento = @idDocumento
END
