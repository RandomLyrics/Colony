USE [VAUABU]
GO

--GATEWAYS
INSERT INTO [dbo].[Gateways]([Name],[Metadata])
     VALUES
           ('Cardsaver', null),
		   ('IPG', null),
		   ('SNAP', null),
		   ('Nestpay', null)

--FILETYPES
INSERT INTO [dbo].[FileTypes] ([Organization],[FType])
     VALUES
           ('VISA' , 'REQUEST'),
		   ('VISA' , 'RESPONSE'),
		   ('MC' , 'REQUEST'),
		   ('MC' , 'RESPONSE')

--PARSERULES
--VISA
INSERT INTO [dbo].[RecordTypes]([FileTypeId],[RType],[ParseRules])
     VALUES
           ((select max(FileTypeId) from dbo.FileTypes where Organization = 'VISA' and FType = 'REQUEST')
           ,'H'
           ,null),
		   ((select max(FileTypeId) from dbo.FileTypes where Organization = 'VISA' and FType = 'REQUEST')
           ,'D'
           ,'1 19 PAN; 82 8 ExpiryDate'),
		   ((select max(FileTypeId) from dbo.FileTypes where Organization = 'VISA' and FType = 'REQUEST')
           ,'T'
           ,'9 9 RecordCount')

INSERT INTO [dbo].[RecordTypes]([FileTypeId],[RType],[ParseRules])
     VALUES
           ((select max(FileTypeId) from dbo.FileTypes where Organization = 'VISA' and FType = 'RESPONSE')
           ,'H'
           ,null),
		   ((select max(FileTypeId) from dbo.FileTypes where Organization = 'VISA' and FType = 'RESPONSE')
           ,'D'
           ,'28 19 PAN; 47 8 ExpiryDate'),
		   ((select max(FileTypeId) from dbo.FileTypes where Organization = 'VISA' and FType = 'RESPONSE')
           ,'T'
           ,'9 9 RecordCount')

--MASTERCARD
INSERT INTO [dbo].[RecordTypes]([FileTypeId],[RType],[ParseRules])
     VALUES
           ((select max(FileTypeId) from dbo.FileTypes where Organization = 'MC' and FType = 'REQUEST')
           ,'H'
           ,null),
		   ((select max(FileTypeId) from dbo.FileTypes where Organization = 'MC' and FType = 'REQUEST')
           ,'D'
           ,'17 19 PAN; 36 6 ExpiryDate'),
		   ((select max(FileTypeId) from dbo.FileTypes where Organization = 'MC' and FType = 'REQUEST')
           ,'T'
           ,'13 9 RecordCount')

INSERT INTO [dbo].[RecordTypes]([FileTypeId],[RType],[ParseRules])
     VALUES
           ((select max(FileTypeId) from dbo.FileTypes where Organization = 'MC' and FType = 'RESPONSE')
           ,'H'
           ,null),
		   ((select max(FileTypeId) from dbo.FileTypes where Organization = 'MC' and FType = 'RESPONSE')
           ,'D'
           ,'17 19 PAN; 36 6 ExpiryDate'),
		   ((select max(FileTypeId) from dbo.FileTypes where Organization = 'MC' and FType = 'RESPONSE')
           ,'T'
           ,'13 9 RecordCount')

--MC
