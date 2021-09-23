﻿CREATE TABLE [dbo].[SeatTypes]
(
    [Id] INT NOT NULL,
    [Name] NVARCHAR(50) NOT NULL,
    [ColorRgb] NVARCHAR(7) NOT NULL, 
    CONSTRAINT [PK_SeatTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
)
