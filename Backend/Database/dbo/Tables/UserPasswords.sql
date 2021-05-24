CREATE TABLE [dbo].[UserPasswords] (
    [PasswordHash] BINARY (32) NOT NULL,
    [Salt]         BINARY (20) NOT NULL,
    [UserId]       INT         NOT NULL,
    CONSTRAINT [PK_UserPasswords_1] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_UserPasswords_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);






