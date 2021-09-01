CREATE TABLE [dbo].[Orders]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [UserId] INT NOT NULL,
    [SessionId] INT NOT NULL,
    [Price] DECIMAL(19, 4) NOT NULL,
    [CurrencyId] INT NOT NULL,
    [RegistratedAt] DATETIME NOT NULL,
    [IsDeleted] BIT NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Orders_UserId_Users_Id] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]),
    CONSTRAINT [FK_Orders_SessionId_Sessions_Id] FOREIGN KEY ([SessionId]) REFERENCES [dbo].[Sessions] ([Id]),
    CONSTRAINT [FK_Orders_CurrencyId_Currencies_Id] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Currencies] ([Id]),
    CONSTRAINT [CK_Orders_Price] CHECK (Price >= 0)
)
