USE [Teste]
GO

ALTER TABLE [dbo].[NotaFiscalItem] ADD [BaseIpi] [decimal](18, 5) NULL
GO

ALTER TABLE [dbo].[NotaFiscalItem] ADD [AliquotaIpi] [decimal](18, 5) NULL
GO

ALTER TABLE [dbo].[NotaFiscalItem] ADD [ValorIpi] [decimal](18, 5) NULL
GO
