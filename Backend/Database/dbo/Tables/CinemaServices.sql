CREATE TABLE [dbo].[CinemaServices]
(
    [CinemaId] INT NOT NULL,
    [ServiceId] INT NOT NULL,
    [Price] DECIMAL(19,4) NOT NULL,
    [CurrencyId] INT NOT NULL,
    CONSTRAINT [PK_CinemaServices] PRIMARY KEY ([ServiceId], [CinemaId]),
    CONSTRAINT [FK_CinemaServices_ServiceId_Services_Id] FOREIGN KEY ([ServiceId]) REFERENCES [dbo].[Services] ([Id]),
    CONSTRAINT [FK_CinemaServices_CinemaId_Cinemas_Id] FOREIGN KEY ([CinemaId]) REFERENCES [dbo].[Cinemas] ([Id]),
    CONSTRAINT [FK_CinemaServices_CurrencyId_Currencies_Id] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Currencies] ([Id]),
    CONSTRAINT [CK_CinemaServices_Price] CHECK (Price >= 0)
)
