CREATE TABLE [dbo].[SessionSeats]
(
    [SeatId] INT NOT NULL,
    [SessionId] INT NOT NULL,
    [UserId] INT NULL,
    [Status] INT NOT NULL,
    [DateTime] DateTime NOT NULL,
    CONSTRAINT [PK_SessionSeats] PRIMARY KEY ([SeatId], [SessionId]),
    CONSTRAINT [FK_SessionSeats_SeatId_Seats_Id] FOREIGN KEY ([SeatId]) REFERENCES [dbo].[Seats] ([Id]),
    CONSTRAINT [FK_SessionSeats_SessionId_Seats_Id] FOREIGN KEY ([SessionId]) REFERENCES [dbo].[Sessions] ([Id]),
    CONSTRAINT [FK_SessionSeats_UserId_Users_Id] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
)
