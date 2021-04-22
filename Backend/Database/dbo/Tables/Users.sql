CREATE TABLE [dbo].[Users] (
    [Id] INT NOT NULL,
    [RoleId] TINYINT NOT NULL,
    [Email] NVARCHAR (254) NOT NULL,
    [Password] NVARCHAR (50) NOT NULL,
    [Salt] NVARCHAR (20) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Users_UserRoles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[UserRoles] ([Id]),
    CONSTRAINT [UIX_Users_Email] UNIQUE NONCLUSTERED ([Email] ASC)
);




GO
EXECUTE sp_addextendedproperty 
    @name = N'MS_Description',
    @value = N'Describes the user who performs actions in the application',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Users';

