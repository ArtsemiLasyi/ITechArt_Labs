CREATE TABLE [dbo].[SessionSeats]
(
    [SeatId] INT NOT NULL,
    [SessionId] INT NOT NULL,
    [IsTaken] BIT NOT NULL, 
    CONSTRAINT [PK_SessionSeats] PRIMARY KEY ([SeatId], [SessionId]),
    CONSTRAINT [FK_SessionSeats_SeatId_Seats_Id] FOREIGN KEY ([SeatId]) REFERENCES [dbo].[Seats] ([Id]),
    CONSTRAINT [FK_SessionSeats_SessionId_Seats_Id] FOREIGN KEY ([SessionId]) REFERENCES [dbo].[Sessions] ([Id])
)
