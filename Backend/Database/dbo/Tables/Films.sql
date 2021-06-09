CREATE TABLE [dbo].[Films]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(100) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
    [PhotoPath] NVARCHAR(260) NULL, 
    [Year] INT NOT NULL, 
    [Duration] INT NOT NULL, 
    CONSTRAINT [CK_Film_Year] CHECK (Year >= 0), 
    CONSTRAINT [CK_Films_Duration] CHECK (Duration > 0)
)
