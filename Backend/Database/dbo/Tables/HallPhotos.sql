CREATE TABLE [dbo].[HallPhotos]
(
    [HallId] INT NOT NULL, 
    [FileName] NVARCHAR(200) NOT NULL,
    CONSTRAINT [PK_HallPhotos] PRIMARY KEY CLUSTERED ([HallId] ASC),
)
