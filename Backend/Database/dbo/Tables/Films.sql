CREATE TABLE [dbo].[Films]
(
    [Id] INT IDENTITY(1,1) NOT NULL, 
    [Name] NVARCHAR(100) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
    [ReleaseYear] INT NOT NULL, 
    [DurationInTicks] BIGINT NOT NULL,
    [IsDeleted] BIT NOT NULL, 
    CONSTRAINT [PK_Films] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_Film_Year] CHECK (ReleaseYear >= 1895), 
    CONSTRAINT [CK_Films_Duration] CHECK (DurationInTicks > 0)
)
