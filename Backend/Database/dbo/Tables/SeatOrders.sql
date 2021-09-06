CREATE TABLE [dbo].[SeatOrders]
(
    [SeatId] INT NOT NULL,
    [OrderId] INT NOT NULL,
    CONSTRAINT [PK_SeatOrders] PRIMARY KEY ([SeatId], [OrderId]),
    CONSTRAINT [FK_SeatOrders_OrderId_Orders_Id] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders] ([Id]),
    CONSTRAINT [FK_SeatOrders_SeatId_Seats_Id] FOREIGN KEY ([SeatId]) REFERENCES [dbo].[Seats] ([Id])
)
