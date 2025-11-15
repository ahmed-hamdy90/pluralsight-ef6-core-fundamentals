BEGIN TRANSACTION;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'FirstName', N'LastName') AND [object_id] = OBJECT_ID(N'[Authors]'))
    SET IDENTITY_INSERT [Authors] ON;
INSERT INTO [Authors] ([Id], [FirstName], [LastName])
VALUES (1, N'Rhoda', N'Lerman'),
(2, N'Ruth', N'Ozeki'),
(3, N'Sofia', N'Segovia'),
(4, N'Ursula K.', N'LeGuin'),
(5, N'Hugh', N'Howey'),
(6, N'Isabelle', N'Allende');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'FirstName', N'LastName') AND [object_id] = OBJECT_ID(N'[Authors]'))
    SET IDENTITY_INSERT [Authors] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251015205929_seedauthors', N'6.0.36');
GO

COMMIT;
GO

