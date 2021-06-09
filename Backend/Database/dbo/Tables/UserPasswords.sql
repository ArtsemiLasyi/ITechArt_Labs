CREATE TABLE [dbo].[UserPasswords] (
    [PasswordHash] BINARY (32) NOT NULL,
    [Salt]         BINARY (20) NOT NULL,
    [UserId]       INT         NOT NULL,
    CONSTRAINT [PK_UserPasswords] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_UserPasswords_UserId_Users_Id] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
    ON DELETE CASCADE
);