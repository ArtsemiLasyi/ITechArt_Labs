CREATE TABLE [dbo].[SeatTypePrices]
(
    [SessionId] INT NOT NULL,
    [SeatTypeId] INT NOT NULL,
    [Price] DECIMAL(19, 4) NOT NULL,
    [CurrencyId] INT NOT NULL,
    CONSTRAINT [PK_SeatTypePrices] PRIMARY KEY ([SessionId], [SeatTypeId]),
    CONSTRAINT [FK_SeatTypePrices_SessionId_Sessions_Id] FOREIGN KEY ([SessionId]) REFERENCES [dbo].[Sessions] ([Id]),
    CONSTRAINT [FK_SeatTypePrices_SeatTypeId_SeatTypes_Id] FOREIGN KEY ([SeatTypeId]) REFERENCES [dbo].[SeatTypes] ([Id]),
    CONSTRAINT [FK_SeatTypePrices_CurrencyId_Currencies_Id] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Currencies] ([Id]),
    CONSTRAINT [CK_SeatTypePrices_Price] CHECK (Price >= 0)
)
