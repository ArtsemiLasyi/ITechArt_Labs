CREATE TABLE [dbo].[Halls]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [CinemaId] INT NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [IsDeleted] BIT NOT NULL,
    CONSTRAINT [PK_Halls] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Halls_CinemaId_Cinemas_Id] FOREIGN KEY ([CinemaId]) REFERENCES [dbo].[Cinemas] ([Id])
)
