CREATE TABLE [dbo].[Halls]
(
    [Id] INT NOT NULL PRIMARY KEY, 
    [CinemaId] INT NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [IsDeleted] BIT NOT NULL, 
    CONSTRAINT [FK_Halls_CinemaId_Cinemas_Id] FOREIGN KEY ([CinemaId]) REFERENCES [dbo].[Cinemas] ([Id])
)
