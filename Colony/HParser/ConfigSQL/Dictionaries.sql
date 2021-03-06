﻿USE [VAUABU]
GO

--CLEAR TABLES
DELETE FROM dbo.Gateways  
DBCC CHECKIDENT ([dbo.Gateways], RESEED, 0)
DELETE FROM dbo.RecordTypes   
DBCC CHECKIDENT ([dbo.RecordTypes], RESEED, 0)
DELETE FROM dbo.FileTypes    
DBCC CHECKIDENT ([dbo.FileTypes], RESEED, 0)

--GATEWAYS
INSERT INTO [dbo].[Gateways]([Name],[Metadata])
     VALUES
           ('Cardsaver', null),
		   ('IPG', null),
		   ('SNAP', null),
		   ('Nestpay', null)

--FILETYPES
INSERT INTO [dbo].[FileTypes] ([Organization],[FType],[TypeName])
     VALUES
           ('VISA' , 'REQUEST', ''),
		   ('VISA' , 'RESPONSE', ''),
		   ('MC' , 'REQUEST', 'R611'),
		   ('MC' , 'RESPONSE', 'T612'),
		   ('TEST', 'REQUEST', 'T001'),
		   ('TEST', 'RESPONSE', 'T002')

--PARSERULES
--TEST
INSERT INTO [dbo].[RecordTypes]([FileTypeId],[RType],[ParseRules])
     VALUES
           ((select max(FileTypeId) from dbo.FileTypes where Organization = 'TEST' and FType = 'REQUEST')
           ,'['
           ,null),
		   ((select max(FileTypeId) from dbo.FileTypes where Organization = 'TEST' and FType = 'REQUEST')
           ,'|'
           ,'1 6 PAN'),
		   ((select max(FileTypeId) from dbo.FileTypes where Organization = 'TEST' and FType = 'REQUEST')
           ,']'
           ,'')
INSERT INTO [dbo].[RecordTypes]([FileTypeId],[RType],[ParseRules])
     VALUES
           ((select max(FileTypeId) from dbo.FileTypes where Organization = 'TEST' and FType = 'RESPONSE')
           ,'['
           ,null),
		   ((select max(FileTypeId) from dbo.FileTypes where Organization = 'TEST' and FType = 'RESPONSE')
           ,'|'
           ,'1 6 PAN'),
		   ((select max(FileTypeId) from dbo.FileTypes where Organization = 'TEST' and FType = 'RESPONSE')
           ,']'
           ,'')

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
           ,'1'
           ,null),
		   ((select max(FileTypeId) from dbo.FileTypes where Organization = 'MC' and FType = 'REQUEST')
           ,'2'
           ,'17 19 PAN; 36 6 ExpiryDate'),
		   ((select max(FileTypeId) from dbo.FileTypes where Organization = 'MC' and FType = 'REQUEST')
           ,'T'
           ,'13 9 RecordCount')

INSERT INTO [dbo].[RecordTypes]([FileTypeId],[RType],[ParseRules])
     VALUES
           ((select max(FileTypeId) from dbo.FileTypes where Organization = 'MC' and FType = 'RESPONSE')
           ,'1'
           ,null),
		   ((select max(FileTypeId) from dbo.FileTypes where Organization = 'MC' and FType = 'RESPONSE')
           ,'2'
           ,'17 19 PAN; 36 6 ExpiryDate'),
		   ((select max(FileTypeId) from dbo.FileTypes where Organization = 'MC' and FType = 'RESPONSE')
           ,'T'
           ,'13 9 RecordCount')

--MC


