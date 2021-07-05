CREATE TABLE [dbo].[Sessions]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [FilmId] INT NOT NULL,
    [HallId] INT NOT NULL,
    [StartDateTime] DATETIME NOT NULL,
    [IsDeleted] BIT NOT NULL,
    CONSTRAINT [PK_Sessions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Sessions_FilmId_Films_Id] FOREIGN KEY ([FilmId]) REFERENCES [dbo].[Films] ([Id]),
    CONSTRAINT [FK_Sessions_HallId_Halls_Id] FOREIGN KEY ([HallId]) REFERENCES [dbo].[Halls] ([Id])
)
