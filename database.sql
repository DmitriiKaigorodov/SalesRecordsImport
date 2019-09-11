IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [SalesRecords] (
    [Id] int NOT NULL IDENTITY,
    [Region] nvarchar(max) NULL,
    [Country] nvarchar(450) NULL,
    [ItemType] nvarchar(max) NULL,
    [SalesChannel] int NOT NULL,
    [OrderPriority] int NOT NULL,
    [OrderDate] datetime2 NOT NULL,
    [ExternalId] bigint NOT NULL,
    [ShipDate] datetime2 NOT NULL,
    [UnitsSold] money NOT NULL,
    [UnitPrice] money NOT NULL,
    [UnitCost] money NOT NULL,
    [TotalRevenue] money NOT NULL,
    [TotalCost] money NOT NULL,
    [TotalProfit] money NOT NULL,
    CONSTRAINT [PK_SalesRecords] PRIMARY KEY ([Id])
);

GO

CREATE INDEX [IX_SalesRecords_Country] ON [SalesRecords] ([Country]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190907190108_Init', N'2.2.6-servicing-10079');

GO

CREATE INDEX [IX_SalesRecords_OrderDate] ON [SalesRecords] ([OrderDate]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190909002057_AddedIndexToOrderDate', N'2.2.6-servicing-10079');

GO

