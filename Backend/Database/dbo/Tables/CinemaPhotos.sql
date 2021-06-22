CREATE TABLE [dbo].[CinemaPhotos]
(
    [CinemaId] INT NOT NULL, 
    [FileName] NVARCHAR(200) NOT NULL,
    CONSTRAINT [PK_CinemaPhotos] PRIMARY KEY CLUSTERED ([CinemaId] ASC),
)
