CREATE TABLE [dbo].[Trip] (
    [LeavingTime] BIGINT        NOT NULL,
    [ReturnTime]  BIGINT        NOT NULL,
    [Location]    NVARCHAR (50) NOT NULL,
    [EventId]     INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([EventId] ASC),
    CONSTRAINT [FK_Trip_ToMainEvent] FOREIGN KEY ([EventId]) REFERENCES [dbo].[MainEvent] ([Id])
);

CREATE TABLE [dbo].[Speaches] (
    [EventId]      INT           NOT NULL,
    [NameLecturer] NVARCHAR (20) NOT NULL,
    PRIMARY KEY CLUSTERED ([EventId] ASC),
    CONSTRAINT [FK_Speaches_ToSpeaches] FOREIGN KEY ([EventId]) REFERENCES [dbo].[MainEvent] ([Id])
);

CREATE TABLE [dbo].[Salary] (
    [Price] FLOAT (53) NOT NULL,
    [Bonus] FLOAT (53) NULL,
    [Hour]  FLOAT (53) NULL,
    PRIMARY KEY CLUSTERED ([Price] ASC)
);

CREATE TABLE [dbo].[Person] (
    [FirstName] NVARCHAR (50) NOT NULL,
    [LastName]  NVARCHAR (50) NOT NULL,
    [Phone]     NVARCHAR (10) NULL,
    [Gmail]     NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Gmail] ASC)
);

CREATE TABLE [dbo].[Maneger] (
    [Gmail]    NVARCHAR (50) NOT NULL,
    [Salary]   FLOAT (53)    NOT NULL,
    [Password] NVARCHAR (20) NOT NULL,
    [Id]       NCHAR (9)     NOT NULL,
    PRIMARY KEY CLUSTERED ([Gmail] ASC),
    CONSTRAINT [FK_Maneger_ToPerson] FOREIGN KEY ([Gmail]) REFERENCES [dbo].[Person] ([Gmail]),
    CONSTRAINT [FK_Maneger_ToSalary] FOREIGN KEY ([Salary]) REFERENCES [dbo].[Salary] ([Price])
);

CREATE TABLE [dbo].[MainEventToCustomer] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [IdEvent]    INT           NOT NULL,
    [IdCustomer] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MainEventToCustomer_ToMainEvent] FOREIGN KEY ([IdEvent]) REFERENCES [dbo].[MainEvent] ([Id]),
    CONSTRAINT [FK_MainEventToCustomer_ToCustomer] FOREIGN KEY ([IdCustomer]) REFERENCES [dbo].[Customer] ([Gmail])
);

CREATE TABLE [dbo].[MainEvent] (
    [Id]                  INT            IDENTITY (1, 1) NOT NULL,
    [Name]                NVARCHAR (50)  NOT NULL,
    [Description]         NVARCHAR (500) NOT NULL,
    [EventDate]           DATETIME       NOT NULL,
    [NumOfParticipance]   INT            NOT NULL,
    [CostPerParticipance] INT            NOT NULL,
    [EventCost]           FLOAT (53)     NOT NULL,
    [MinAge]              INT            NULL,
    [MaxAge]              INT            NULL,
    [Gender]              NVARCHAR (10)  NOT NULL,
    [ImagePath]           NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Customer] (
    [Gmail]    NVARCHAR (50) NOT NULL,
    [UserName] NVARCHAR (20) NOT NULL,
    [Password] NVARCHAR (20) NOT NULL,
    CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([Gmail] ASC),
    CONSTRAINT [FK_Customer_ToPerson] FOREIGN KEY ([Gmail]) REFERENCES [dbo].[Person] ([Gmail])
);

CREATE TABLE [dbo].[Contacts] (
    [contactId] INT             IDENTITY (1, 1) NOT NULL,
    [Question]  NVARCHAR (200)  NOT NULL,
    [Answer]    NVARCHAR (2000) NULL,
    [GmailId]   NVARCHAR (50)   NOT NULL,
    PRIMARY KEY CLUSTERED ([contactId] ASC),
    CONSTRAINT [FK_Contacts_ToCustomer] FOREIGN KEY ([GmailId]) REFERENCES [dbo].[Customer] ([Gmail])
);

