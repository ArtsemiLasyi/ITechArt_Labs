CREATE TABLE [dbo].[Films]
(
    [Id] INT IDENTITY(1,1) NOT NULL, 
    [Name] NVARCHAR(100) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
    [PosterFileName] NVARCHAR(200) NOT NULL, 
    [ReleaseYear] INT NOT NULL, 
    [DurationInTicks] BIGINT NOT NULL,
    CONSTRAINT [PK_Films] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_Film_Year] CHECK (ReleaseYear >= 0), 
    CONSTRAINT [CK_Films_Duration] CHECK (DurationInTicks > 0)
)
