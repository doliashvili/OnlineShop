CREATE TABLE [dbo].[User] (
    [Id]                   INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]            NVARCHAR (75)  NULL,
    [LastName]             NVARCHAR (75)  NULL,
    [PersonalNumber]       NVARCHAR (75)  NULL,
    [Country]              NVARCHAR (30)  NULL,
    [City]                 NVARCHAR (30)  NULL,
    [Address]              NVARCHAR (100) NULL,
    [IdentificationNumber] NVARCHAR (100) NULL,
    [Email]                NVARCHAR (256) NOT NULL,
    [EmailConfirmed]       BIT            NOT NULL,
    [PasswordHash]         NVARCHAR (MAX) NULL,
    [PhoneNumber]          NVARCHAR (50)  NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL,
    [Created]              DATETIME       NOT NULL,
    [DateOfBirth]          DATETIME       NULL,
    CONSTRAINT [PK__Applicat__3214EC079083AE7D] PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[Role] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (256) NOT NULL,
    [Created]        DATETIME       NOT NULL,
    CONSTRAINT [PK__SiteRole__3214EC076D185A2A] PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[UserRole] (
    [UserId] INT NOT NULL,
    [RoleId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_SiteUserRole_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id]),
    CONSTRAINT [FK_SiteUserRole_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
);


CREATE TABLE [dbo].[ExternalLogin] (
    [LoginProvider]       NVARCHAR (450) NOT NULL,
    [ProviderKey]         NVARCHAR (450) NOT NULL,
    [ProviderDisplayName] NVARCHAR (450) NOT NULL,
    [Created]             DATETIME       NOT NULL,
    [UserID]              INT            NOT NULL
);