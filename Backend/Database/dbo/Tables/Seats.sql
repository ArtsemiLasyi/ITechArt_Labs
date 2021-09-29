CREATE TABLE [dbo].[Seats]
(
    [Id] INT IDENTITY(1,1) NOT NULL, 
    [Row] INT NOT NULL, 
    [Place] INT NOT NULL,
    [HallId] INT NOT NULL, 
    [SeatTypeId] INT NOT NULL,
    [IsDeleted] BIT NOT NULL,
    CONSTRAINT [PK_Seats] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [CK_Seats_Row] CHECK (Row > 0), 
    CONSTRAINT [CK_Seats_Place] CHECK (Place > 0),
    CONSTRAINT [FK_Seats_HallId_Halls_Id] FOREIGN KEY ([HallId]) REFERENCES [dbo].[Halls] ([Id]),
    CONSTRAINT [FK_Seats_SeatTypeId_SeatTypes_Id] FOREIGN KEY ([SeatTypeId]) REFERENCES [dbo].[SeatTypes] ([Id])
)
