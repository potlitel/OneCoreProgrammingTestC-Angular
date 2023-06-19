USE [OnecoreProgrammingTest]
GO
IF EXISTS (SELECT name FROM dbo.sysobjects 
WHERE id = OBJECT_ID(N'[dbo].[Select_Documento_byIdDocumento]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
    DROP PROCEDURE [dbo].[Select_Documento_byIdDocumento]
GO
-- =============================================
-- Author:		Reynol Gonzalez
-- Create date: 23/01/2023
-- Description:	Devuelve todas las compras dado un IdDocumento
-- =============================================
CREATE PROCEDURE [dbo].[Select_Documento_byIdDocumento] 
	@idDocumento int 
	
AS
BEGIN
	SELECT * 
  FROM Documentos d 
  WHERE Id = @idDocumento
END
