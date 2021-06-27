CREATE TABLE [dbo].[ServiceCinemas]
(
    [ServiceId] INT NOT NULL,
    [CinemaId] INT NOT NULL,
    [PriceInDollars] DECIMAL(19,4) NOT NULL,
    CONSTRAINT [PK_ServiceCinemas] PRIMARY KEY ([ServiceId], [CinemaId]),
    CONSTRAINT [FK_ServiceCinemas_ServiceId_Services_Id] FOREIGN KEY ([ServiceId]) REFERENCES [dbo].[Services] ([Id]),
    CONSTRAINT [FK_ServiceCinemas_CinemaId_Cinemas_Id] FOREIGN KEY ([CinemaId]) REFERENCES [dbo].[Cinemas] ([Id]), 
    CONSTRAINT [CK_ServiceCinemas_PriceInDollars] CHECK (PriceInDollars >= 0)
)
