CREATE TABLE [dbo].[Users] (
    [Id] INT IDENTITY (1, 1) NOT NULL,
    [RoleId] INT NOT NULL,
    [Email] NVARCHAR (320) NOT NULL,
    [PasswordHash] BINARY (32) NOT NULL,
    [Salt] NVARCHAR (20) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Users_UserRoles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[UserRoles] ([Id]),
    CONSTRAINT [UIX_Users_Email] UNIQUE NONCLUSTERED ([Email] ASC)
);