CREATE TABLE [dbo].[Cinemas]
(
    [Id] INT IDENTITY(1,1) NOT NULL, 
    [CityId] INT NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
    [IsDeleted] BIT NOT NULL,
    CONSTRAINT [PK_Cinemas] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Cinemas_CityId_Cities_Id] FOREIGN KEY ([CityId]) REFERENCES [dbo].[Cities] ([Id])
)
