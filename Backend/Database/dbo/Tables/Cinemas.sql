CREATE TABLE [dbo].[Cinemas]
(
    [Id] INT NOT NULL PRIMARY KEY, 
    [CityId] INT NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
    [IsDeleted] BIT NOT NULL, 
    CONSTRAINT [FK_Cinemas_CityId_Cities_Id] FOREIGN KEY ([CityId]) REFERENCES [dbo].[Cities] ([Id])
)
