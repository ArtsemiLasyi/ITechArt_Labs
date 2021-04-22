CREATE TABLE [dbo].[UserRoles] (
    [Id]   TINYINT       NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Describes the role that the user has (administrator, common user, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserRoles';

