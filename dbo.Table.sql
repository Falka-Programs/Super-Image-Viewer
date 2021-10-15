CREATE TABLE [dbo].[History] (
    [Id]        INT IDENTITY(1,1)           NOT NULL,
    [ImageName] NVARCHAR (50)  NOT NULL,
    [ImagePath] NVARCHAR (256) NOT NULL,
    [ImageDate] DATETIME       NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

