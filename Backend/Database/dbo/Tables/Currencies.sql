﻿CREATE TABLE [dbo].[Currencies]
(
    [Id] INT IDENTITY(1,1) NOT NULL, 
    [Name] NVARCHAR(3) NOT NULL,
    CONSTRAINT [PK_Currencies] PRIMARY KEY CLUSTERED ([Id] ASC)
)
