CREATE TABLE [dbo].[OFX] (
    [Id]        INT             NOT NULL Identity(1,1),
    [IdTrnType] INT             NOT NULL,
    [DtPosted]  DATETIME        NOT NULL,
    [TrNamt]    DECIMAL (18, 8) NOT NULL,
    [FitId]     VARCHAR (100)   NOT NULL,
    [CheckNum]  VARCHAR (100)   NOT NULL,
    [Memo]      VARCHAR (100)   NOT NULL,
    PRIMARY KEY CLUSTERED ([DtPosted],[CheckNum], [Memo] ASC),
    CONSTRAINT [FK_TrnType] FOREIGN KEY ([IdTrnType]) REFERENCES [dbo].[TrnType] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);
