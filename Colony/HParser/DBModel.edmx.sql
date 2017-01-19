
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/19/2017 14:49:35
-- Generated from EDMX file: C:\SANDBOX\Colony\Colony\HParser\DBModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [VAUABU];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_FileTypeFile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Files] DROP CONSTRAINT [FK_FileTypeFile];
GO
IF OBJECT_ID(N'[dbo].[FK_FileTypeRecordType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RecordTypes] DROP CONSTRAINT [FK_FileTypeRecordType];
GO
IF OBJECT_ID(N'[dbo].[FK_GatewayFile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Files] DROP CONSTRAINT [FK_GatewayFile];
GO
IF OBJECT_ID(N'[dbo].[FK_RecordTypeRecord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Records] DROP CONSTRAINT [FK_RecordTypeRecord];
GO
IF OBJECT_ID(N'[dbo].[FK_FileRecord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Records] DROP CONSTRAINT [FK_FileRecord];
GO
IF OBJECT_ID(N'[dbo].[FK_FileFile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Files] DROP CONSTRAINT [FK_FileFile];
GO
IF OBJECT_ID(N'[dbo].[FK_RecordHRecord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Records] DROP CONSTRAINT [FK_RecordHRecord];
GO
IF OBJECT_ID(N'[dbo].[FK_RecordTRecord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Records] DROP CONSTRAINT [FK_RecordTRecord];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Files]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Files];
GO
IF OBJECT_ID(N'[dbo].[FileTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FileTypes];
GO
IF OBJECT_ID(N'[dbo].[RecordTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RecordTypes];
GO
IF OBJECT_ID(N'[dbo].[Gateways]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Gateways];
GO
IF OBJECT_ID(N'[dbo].[Records]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Records];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Files'
CREATE TABLE [dbo].[Files] (
    [FileId] bigint IDENTITY(1,1) NOT NULL,
    [GatewayId] int  NOT NULL,
    [FileTypeId] int  NOT NULL,
    [MatchedFileId] bigint  NULL,
    [Name] nvarchar(max)  NOT NULL,
    [InsertDate] nvarchar(max)  NOT NULL,
    [Metadata] nvarchar(max)  NULL
);
GO

-- Creating table 'FileTypes'
CREATE TABLE [dbo].[FileTypes] (
    [FileTypeId] int IDENTITY(1,1) NOT NULL,
    [Organization] nvarchar(max)  NOT NULL,
    [FType] nvarchar(max)  NOT NULL,
    [TypeName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'RecordTypes'
CREATE TABLE [dbo].[RecordTypes] (
    [RecordTypeId] int IDENTITY(1,1) NOT NULL,
    [FileTypeId] int  NOT NULL,
    [RType] nchar(1)  NOT NULL,
    [ParseRules] nvarchar(max)  NULL
);
GO

-- Creating table 'Gateways'
CREATE TABLE [dbo].[Gateways] (
    [GatewayId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Metadata] nvarchar(max)  NULL
);
GO

-- Creating table 'Records'
CREATE TABLE [dbo].[Records] (
    [RecordId] bigint IDENTITY(1,1) NOT NULL,
    [FileId] bigint  NOT NULL,
    [RecordTypeId] int  NOT NULL,
    [HeaderRecordId] bigint  NULL,
    [TrailerRecordId] bigint  NULL,
    [Value] nchar(120)  NOT NULL,
    [PAN] nvarchar(max)  NULL,
    [ExpiryDate] nvarchar(max)  NULL,
    [RecordCount] nvarchar(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [FileId] in table 'Files'
ALTER TABLE [dbo].[Files]
ADD CONSTRAINT [PK_Files]
    PRIMARY KEY CLUSTERED ([FileId] ASC);
GO

-- Creating primary key on [FileTypeId] in table 'FileTypes'
ALTER TABLE [dbo].[FileTypes]
ADD CONSTRAINT [PK_FileTypes]
    PRIMARY KEY CLUSTERED ([FileTypeId] ASC);
GO

-- Creating primary key on [RecordTypeId] in table 'RecordTypes'
ALTER TABLE [dbo].[RecordTypes]
ADD CONSTRAINT [PK_RecordTypes]
    PRIMARY KEY CLUSTERED ([RecordTypeId] ASC);
GO

-- Creating primary key on [GatewayId] in table 'Gateways'
ALTER TABLE [dbo].[Gateways]
ADD CONSTRAINT [PK_Gateways]
    PRIMARY KEY CLUSTERED ([GatewayId] ASC);
GO

-- Creating primary key on [RecordId] in table 'Records'
ALTER TABLE [dbo].[Records]
ADD CONSTRAINT [PK_Records]
    PRIMARY KEY CLUSTERED ([RecordId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [FileTypeId] in table 'Files'
ALTER TABLE [dbo].[Files]
ADD CONSTRAINT [FK_FileTypeFile]
    FOREIGN KEY ([FileTypeId])
    REFERENCES [dbo].[FileTypes]
        ([FileTypeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FileTypeFile'
CREATE INDEX [IX_FK_FileTypeFile]
ON [dbo].[Files]
    ([FileTypeId]);
GO

-- Creating foreign key on [FileTypeId] in table 'RecordTypes'
ALTER TABLE [dbo].[RecordTypes]
ADD CONSTRAINT [FK_FileTypeRecordType]
    FOREIGN KEY ([FileTypeId])
    REFERENCES [dbo].[FileTypes]
        ([FileTypeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FileTypeRecordType'
CREATE INDEX [IX_FK_FileTypeRecordType]
ON [dbo].[RecordTypes]
    ([FileTypeId]);
GO

-- Creating foreign key on [GatewayId] in table 'Files'
ALTER TABLE [dbo].[Files]
ADD CONSTRAINT [FK_GatewayFile]
    FOREIGN KEY ([GatewayId])
    REFERENCES [dbo].[Gateways]
        ([GatewayId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GatewayFile'
CREATE INDEX [IX_FK_GatewayFile]
ON [dbo].[Files]
    ([GatewayId]);
GO

-- Creating foreign key on [RecordTypeId] in table 'Records'
ALTER TABLE [dbo].[Records]
ADD CONSTRAINT [FK_RecordTypeRecord]
    FOREIGN KEY ([RecordTypeId])
    REFERENCES [dbo].[RecordTypes]
        ([RecordTypeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RecordTypeRecord'
CREATE INDEX [IX_FK_RecordTypeRecord]
ON [dbo].[Records]
    ([RecordTypeId]);
GO

-- Creating foreign key on [FileId] in table 'Records'
ALTER TABLE [dbo].[Records]
ADD CONSTRAINT [FK_FileRecord]
    FOREIGN KEY ([FileId])
    REFERENCES [dbo].[Files]
        ([FileId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FileRecord'
CREATE INDEX [IX_FK_FileRecord]
ON [dbo].[Records]
    ([FileId]);
GO

-- Creating foreign key on [MatchedFileId] in table 'Files'
ALTER TABLE [dbo].[Files]
ADD CONSTRAINT [FK_FileFile]
    FOREIGN KEY ([MatchedFileId])
    REFERENCES [dbo].[Files]
        ([FileId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FileFile'
CREATE INDEX [IX_FK_FileFile]
ON [dbo].[Files]
    ([MatchedFileId]);
GO

-- Creating foreign key on [HeaderRecordId] in table 'Records'
ALTER TABLE [dbo].[Records]
ADD CONSTRAINT [FK_RecordHRecord]
    FOREIGN KEY ([HeaderRecordId])
    REFERENCES [dbo].[Records]
        ([RecordId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RecordHRecord'
CREATE INDEX [IX_FK_RecordHRecord]
ON [dbo].[Records]
    ([HeaderRecordId]);
GO

-- Creating foreign key on [TrailerRecordId] in table 'Records'
ALTER TABLE [dbo].[Records]
ADD CONSTRAINT [FK_RecordTRecord]
    FOREIGN KEY ([TrailerRecordId])
    REFERENCES [dbo].[Records]
        ([RecordId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RecordTRecord'
CREATE INDEX [IX_FK_RecordTRecord]
ON [dbo].[Records]
    ([TrailerRecordId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------