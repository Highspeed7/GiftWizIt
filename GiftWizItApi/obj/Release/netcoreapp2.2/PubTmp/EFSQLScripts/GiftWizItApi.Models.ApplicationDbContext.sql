IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190413061551_InitialMigration')
BEGIN
    CREATE TABLE [GiftLists] (
        [gift_list_id] int NOT NULL IDENTITY,
        [name] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_GiftLists] PRIMARY KEY ([gift_list_id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190413061551_InitialMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190413061551_InitialMigration', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190419072542_AddAllOtherTablesAndConstraints')
BEGIN
    ALTER TABLE [GiftLists] ADD [created_on] datetime2 NOT NULL DEFAULT (GETUTCDATE());
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190419072542_AddAllOtherTablesAndConstraints')
BEGIN
    CREATE TABLE [Contacts] (
        [contact_id] int NOT NULL IDENTITY,
        [name] nvarchar(100) NOT NULL,
        [email] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_Contacts] PRIMARY KEY ([contact_id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190419072542_AddAllOtherTablesAndConstraints')
BEGIN
    CREATE TABLE [Items] (
        [item_id] int NOT NULL IDENTITY,
        [name] nvarchar(50) NOT NULL,
        [upc] nvarchar(max) NULL,
        [image] nvarchar(max) NULL,
        [created_on] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        CONSTRAINT [PK_Items] PRIMARY KEY ([item_id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190419072542_AddAllOtherTablesAndConstraints')
BEGIN
    CREATE TABLE [Partners] (
        [PartnerId] int NOT NULL IDENTITY,
        [name] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_Partners] PRIMARY KEY ([PartnerId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190419072542_AddAllOtherTablesAndConstraints')
BEGIN
    CREATE TABLE [Users] (
        [user_id] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([user_id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190419072542_AddAllOtherTablesAndConstraints')
BEGIN
    CREATE TABLE [WishLists] (
        [wish_list_id] int NOT NULL IDENTITY,
        [name] nvarchar(50) NOT NULL,
        [created_on] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        CONSTRAINT [PK_WishLists] PRIMARY KEY ([wish_list_id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190419072542_AddAllOtherTablesAndConstraints')
BEGIN
    CREATE TABLE [GList_Items] (
        [g_list_id] int NOT NULL,
        [item_id] int NOT NULL,
        CONSTRAINT [PK_GList_Items] PRIMARY KEY ([g_list_id], [item_id]),
        CONSTRAINT [FK_GList_Items_GiftLists_g_list_id] FOREIGN KEY ([g_list_id]) REFERENCES [GiftLists] ([gift_list_id]) ON DELETE CASCADE,
        CONSTRAINT [FK_GList_Items_Items_item_id] FOREIGN KEY ([item_id]) REFERENCES [Items] ([item_id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190419072542_AddAllOtherTablesAndConstraints')
BEGIN
    CREATE TABLE [Links_Items_Partners] (
        [afflt_link] nvarchar(450) NOT NULL,
        [item_id] int NOT NULL,
        [partner_id] int NOT NULL,
        CONSTRAINT [PK_Links_Items_Partners] PRIMARY KEY ([afflt_link]),
        CONSTRAINT [FK_Links_Items_Partners_Items_item_id] FOREIGN KEY ([item_id]) REFERENCES [Items] ([item_id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Links_Items_Partners_Partners_partner_id] FOREIGN KEY ([partner_id]) REFERENCES [Partners] ([PartnerId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190419072542_AddAllOtherTablesAndConstraints')
BEGIN
    CREATE TABLE [ContactUsers] (
        [contact_id] int NOT NULL,
        [user_id] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_ContactUsers] PRIMARY KEY ([user_id], [contact_id]),
        CONSTRAINT [FK_ContactUsers_Contacts_contact_id] FOREIGN KEY ([contact_id]) REFERENCES [Contacts] ([contact_id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ContactUsers_Users_user_id] FOREIGN KEY ([user_id]) REFERENCES [Users] ([user_id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190419072542_AddAllOtherTablesAndConstraints')
BEGIN
    CREATE TABLE [WList_Items] (
        [item_id] int NOT NULL,
        [w_list_id] int NOT NULL,
        CONSTRAINT [PK_WList_Items] PRIMARY KEY ([w_list_id], [item_id]),
        CONSTRAINT [FK_WList_Items_Items_item_id] FOREIGN KEY ([item_id]) REFERENCES [Items] ([item_id]) ON DELETE CASCADE,
        CONSTRAINT [FK_WList_Items_WishLists_w_list_id] FOREIGN KEY ([w_list_id]) REFERENCES [WishLists] ([wish_list_id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190419072542_AddAllOtherTablesAndConstraints')
BEGIN
    CREATE INDEX [IX_ContactUsers_contact_id] ON [ContactUsers] ([contact_id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190419072542_AddAllOtherTablesAndConstraints')
BEGIN
    CREATE INDEX [IX_GList_Items_item_id] ON [GList_Items] ([item_id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190419072542_AddAllOtherTablesAndConstraints')
BEGIN
    CREATE INDEX [IX_Links_Items_Partners_item_id] ON [Links_Items_Partners] ([item_id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190419072542_AddAllOtherTablesAndConstraints')
BEGIN
    CREATE INDEX [IX_Links_Items_Partners_partner_id] ON [Links_Items_Partners] ([partner_id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190419072542_AddAllOtherTablesAndConstraints')
BEGIN
    CREATE INDEX [IX_WList_Items_item_id] ON [WList_Items] ([item_id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190419072542_AddAllOtherTablesAndConstraints')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190419072542_AddAllOtherTablesAndConstraints', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190419183316_AddForeignKeyToUsersInGiftLists')
BEGIN
    ALTER TABLE [GiftLists] ADD [user_id] nvarchar(450) NOT NULL DEFAULT N'';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190419183316_AddForeignKeyToUsersInGiftLists')
BEGIN
    CREATE UNIQUE INDEX [IX_GiftLists_user_id] ON [GiftLists] ([user_id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190419183316_AddForeignKeyToUsersInGiftLists')
BEGIN
    ALTER TABLE [GiftLists] ADD CONSTRAINT [FK_GiftLists_Users_user_id] FOREIGN KEY ([user_id]) REFERENCES [Users] ([user_id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190419183316_AddForeignKeyToUsersInGiftLists')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190419183316_AddForeignKeyToUsersInGiftLists', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190419231452_ChangingGiftListUsersFKConstraints')
BEGIN
    ALTER TABLE [GiftLists] DROP CONSTRAINT [FK_GiftLists_Users_user_id];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190419231452_ChangingGiftListUsersFKConstraints')
BEGIN
    DROP INDEX [IX_GiftLists_user_id] ON [GiftLists];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190419231452_ChangingGiftListUsersFKConstraints')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[GiftLists]') AND [c].[name] = N'user_id');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [GiftLists] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [GiftLists] ALTER COLUMN [user_id] nvarchar(450) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190419231452_ChangingGiftListUsersFKConstraints')
BEGIN
    CREATE INDEX [IX_GiftLists_user_id] ON [GiftLists] ([user_id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190419231452_ChangingGiftListUsersFKConstraints')
BEGIN
    ALTER TABLE [GiftLists] ADD CONSTRAINT [FK_GiftLists_Users_user_id] FOREIGN KEY ([user_id]) REFERENCES [Users] ([user_id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190419231452_ChangingGiftListUsersFKConstraints')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190419231452_ChangingGiftListUsersFKConstraints', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190423205355_AddedDeletedFlagToGiftLists')
BEGIN
    ALTER TABLE [GiftLists] ADD [_deleted] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190423205355_AddedDeletedFlagToGiftLists')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190423205355_AddedDeletedFlagToGiftLists', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190507065330_AddedUserToWishListTable')
BEGIN
    ALTER TABLE [WishLists] ADD [Item_Id] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190507065330_AddedUserToWishListTable')
BEGIN
    ALTER TABLE [WishLists] ADD [UserId] nvarchar(450) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190507065330_AddedUserToWishListTable')
BEGIN
    CREATE INDEX [IX_WishLists_Item_Id] ON [WishLists] ([Item_Id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190507065330_AddedUserToWishListTable')
BEGIN
    CREATE INDEX [IX_WishLists_UserId] ON [WishLists] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190507065330_AddedUserToWishListTable')
BEGIN
    ALTER TABLE [WishLists] ADD CONSTRAINT [FK_WishLists_Items_Item_Id] FOREIGN KEY ([Item_Id]) REFERENCES [Items] ([item_id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190507065330_AddedUserToWishListTable')
BEGIN
    ALTER TABLE [WishLists] ADD CONSTRAINT [FK_WishLists_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([user_id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190507065330_AddedUserToWishListTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190507065330_AddedUserToWishListTable', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190507174735_AddedDomainToPartnersTable')
BEGIN
    ALTER TABLE [WishLists] DROP CONSTRAINT [FK_WishLists_Items_Item_Id];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190507174735_AddedDomainToPartnersTable')
BEGIN
    DROP INDEX [IX_WishLists_Item_Id] ON [WishLists];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190507174735_AddedDomainToPartnersTable')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[WishLists]') AND [c].[name] = N'Item_Id');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [WishLists] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [WishLists] DROP COLUMN [Item_Id];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190507174735_AddedDomainToPartnersTable')
BEGIN
    ALTER TABLE [Partners] ADD [Domain] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190507174735_AddedDomainToPartnersTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190507174735_AddedDomainToPartnersTable', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190507202210_SeedDatabase')
BEGIN
    INSERT INTO Partners (name, Domain) VALUES ('Amazon', 'https://www.amazon.com')
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190507202210_SeedDatabase')
BEGIN
    INSERT INTO Partners (name, Domain) VALUES ('Walmart', 'https://www.walmart.com')
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190507202210_SeedDatabase')
BEGIN
    INSERT INTO Partners (name, Domain) VALUES ('Target', 'https://www.target.com')
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190507202210_SeedDatabase')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190507202210_SeedDatabase', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190508040938_RemovedLinkFromLnksItmsPtnrsTable')
BEGIN
    ALTER TABLE [Links_Items_Partners] DROP CONSTRAINT [PK_Links_Items_Partners];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190508040938_RemovedLinkFromLnksItmsPtnrsTable')
BEGIN
    DROP INDEX [IX_Links_Items_Partners_item_id] ON [Links_Items_Partners];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190508040938_RemovedLinkFromLnksItmsPtnrsTable')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Links_Items_Partners]') AND [c].[name] = N'afflt_link');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Links_Items_Partners] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Links_Items_Partners] DROP COLUMN [afflt_link];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190508040938_RemovedLinkFromLnksItmsPtnrsTable')
BEGIN
    ALTER TABLE [Links_Items_Partners] ADD CONSTRAINT [PK_Links_Items_Partners] PRIMARY KEY ([item_id], [partner_id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190508040938_RemovedLinkFromLnksItmsPtnrsTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190508040938_RemovedLinkFromLnksItmsPtnrsTable', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190508041517_AddedAffiliateLinkBackAsRegularColumn')
BEGIN
    ALTER TABLE [Links_Items_Partners] ADD [afflt_link] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190508041517_AddedAffiliateLinkBackAsRegularColumn')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190508041517_AddedAffiliateLinkBackAsRegularColumn', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190509175755_UpdatedItemNameCharacterLength')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Items]') AND [c].[name] = N'name');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Items] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Items] ALTER COLUMN [name] nvarchar(250) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190509175755_UpdatedItemNameCharacterLength')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190509175755_UpdatedItemNameCharacterLength', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190509201937_AddedImagePropertyMaxLengthToItems')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Items]') AND [c].[name] = N'image');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Items] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Items] ALTER COLUMN [image] nvarchar(450) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190509201937_AddedImagePropertyMaxLengthToItems')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190509201937_AddedImagePropertyMaxLengthToItems', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190515204927_AddDeletedFlagToWishItemTable')
BEGIN
    ALTER TABLE [WList_Items] ADD [_deleted] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190515204927_AddDeletedFlagToWishItemTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190515204927_AddDeletedFlagToWishItemTable', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190522023628_AddedEmailAndAccessToContactsTable')
BEGIN
    ALTER TABLE [Contacts] ADD [email_sent] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190522023628_AddedEmailAndAccessToContactsTable')
BEGIN
    ALTER TABLE [Contacts] ADD [verified] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190522023628_AddedEmailAndAccessToContactsTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190522023628_AddedEmailAndAccessToContactsTable', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190522035717_AddedUniqueEmailColumnToUsersTable')
BEGIN
    ALTER TABLE [Users] ADD [Email] nvarchar(450) NOT NULL DEFAULT N'';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190522035717_AddedUniqueEmailColumnToUsersTable')
BEGIN
    ALTER TABLE [Users] ADD CONSTRAINT [email] UNIQUE ([Email]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190522035717_AddedUniqueEmailColumnToUsersTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190522035717_AddedUniqueEmailColumnToUsersTable', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190522053149_ChanedColumnNameOfEmailInUsersTable')
BEGIN
    EXEC sp_rename N'[Users].[Email]', N'email', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190522053149_ChanedColumnNameOfEmailInUsersTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190522053149_ChanedColumnNameOfEmailInUsersTable', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190525230156_AddedVerifyGUIDToContactsTable')
BEGIN
    ALTER TABLE [Contacts] ADD [verify_guid] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190525230156_AddedVerifyGUIDToContactsTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190525230156_AddedVerifyGUIDToContactsTable', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190527185632_AddSharedListsToDatabase')
BEGIN
    CREATE TABLE [Shared_Lists] (
        [g_list_id] int NOT NULL,
        [user_id] nvarchar(450) NOT NULL,
        [password] nvarchar(max) NOT NULL,
        [email_sent] nvarchar(max) NULL DEFAULT N'False',
        [contact_id] int NOT NULL,
        CONSTRAINT [PK_Shared_Lists] PRIMARY KEY ([user_id], [g_list_id]),
        CONSTRAINT [FK_Shared_Lists_Contacts_contact_id] FOREIGN KEY ([contact_id]) REFERENCES [Contacts] ([contact_id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Shared_Lists_GiftLists_g_list_id] FOREIGN KEY ([g_list_id]) REFERENCES [GiftLists] ([gift_list_id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Shared_Lists_Users_user_id] FOREIGN KEY ([user_id]) REFERENCES [Users] ([user_id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190527185632_AddSharedListsToDatabase')
BEGIN
    CREATE INDEX [IX_Shared_Lists_contact_id] ON [Shared_Lists] ([contact_id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190527185632_AddSharedListsToDatabase')
BEGIN
    CREATE INDEX [IX_Shared_Lists_g_list_id] ON [Shared_Lists] ([g_list_id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190527185632_AddSharedListsToDatabase')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190527185632_AddSharedListsToDatabase', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190527185812_ChangedEmailSentColumnToBoolForSharedListsEntity')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Shared_Lists]') AND [c].[name] = N'email_sent');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Shared_Lists] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [Shared_Lists] ALTER COLUMN [email_sent] bit NOT NULL;
    ALTER TABLE [Shared_Lists] ADD DEFAULT 0 FOR [email_sent];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190527185812_ChangedEmailSentColumnToBoolForSharedListsEntity')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190527185812_ChangedEmailSentColumnToBoolForSharedListsEntity', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190530074223_RemovedSharedListsCompositeKey')
BEGIN
    ALTER TABLE [Shared_Lists] DROP CONSTRAINT [FK_Shared_Lists_Contacts_contact_id];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190530074223_RemovedSharedListsCompositeKey')
BEGIN
    ALTER TABLE [Shared_Lists] DROP CONSTRAINT [FK_Shared_Lists_GiftLists_g_list_id];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190530074223_RemovedSharedListsCompositeKey')
BEGIN
    ALTER TABLE [Shared_Lists] DROP CONSTRAINT [FK_Shared_Lists_Users_user_id];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190530074223_RemovedSharedListsCompositeKey')
BEGIN
    ALTER TABLE [Shared_Lists] DROP CONSTRAINT [PK_Shared_Lists];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190530074223_RemovedSharedListsCompositeKey')
BEGIN
    EXEC sp_rename N'[Shared_Lists]', N'SharedLists';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190530074223_RemovedSharedListsCompositeKey')
BEGIN
    EXEC sp_rename N'[SharedLists].[IX_Shared_Lists_g_list_id]', N'IX_SharedLists_g_list_id', N'INDEX';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190530074223_RemovedSharedListsCompositeKey')
BEGIN
    EXEC sp_rename N'[SharedLists].[IX_Shared_Lists_contact_id]', N'IX_SharedLists_contact_id', N'INDEX';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190530074223_RemovedSharedListsCompositeKey')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SharedLists]') AND [c].[name] = N'user_id');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [SharedLists] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [SharedLists] ALTER COLUMN [user_id] nvarchar(450) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190530074223_RemovedSharedListsCompositeKey')
BEGIN
    ALTER TABLE [SharedLists] ADD [ShareId] int NOT NULL IDENTITY;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190530074223_RemovedSharedListsCompositeKey')
BEGIN
    ALTER TABLE [SharedLists] ADD CONSTRAINT [PK_SharedLists] PRIMARY KEY ([ShareId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190530074223_RemovedSharedListsCompositeKey')
BEGIN
    CREATE INDEX [IX_SharedLists_user_id] ON [SharedLists] ([user_id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190530074223_RemovedSharedListsCompositeKey')
BEGIN
    ALTER TABLE [SharedLists] ADD CONSTRAINT [FK_SharedLists_Contacts_contact_id] FOREIGN KEY ([contact_id]) REFERENCES [Contacts] ([contact_id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190530074223_RemovedSharedListsCompositeKey')
BEGIN
    ALTER TABLE [SharedLists] ADD CONSTRAINT [FK_SharedLists_GiftLists_g_list_id] FOREIGN KEY ([g_list_id]) REFERENCES [GiftLists] ([gift_list_id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190530074223_RemovedSharedListsCompositeKey')
BEGIN
    ALTER TABLE [SharedLists] ADD CONSTRAINT [FK_SharedLists_Users_user_id] FOREIGN KEY ([user_id]) REFERENCES [Users] ([user_id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190530074223_RemovedSharedListsCompositeKey')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190530074223_RemovedSharedListsCompositeKey', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190530080912_AddedAppropriateCompositeKeyForSharedListsTable')
BEGIN
    ALTER TABLE [SharedLists] DROP CONSTRAINT [FK_SharedLists_Contacts_contact_id];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190530080912_AddedAppropriateCompositeKeyForSharedListsTable')
BEGIN
    ALTER TABLE [SharedLists] DROP CONSTRAINT [FK_SharedLists_GiftLists_g_list_id];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190530080912_AddedAppropriateCompositeKeyForSharedListsTable')
BEGIN
    ALTER TABLE [SharedLists] DROP CONSTRAINT [FK_SharedLists_Users_user_id];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190530080912_AddedAppropriateCompositeKeyForSharedListsTable')
BEGIN
    ALTER TABLE [SharedLists] DROP CONSTRAINT [PK_SharedLists];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190530080912_AddedAppropriateCompositeKeyForSharedListsTable')
BEGIN
    DROP INDEX [IX_SharedLists_contact_id] ON [SharedLists];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190530080912_AddedAppropriateCompositeKeyForSharedListsTable')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SharedLists]') AND [c].[name] = N'ShareId');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [SharedLists] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [SharedLists] DROP COLUMN [ShareId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190530080912_AddedAppropriateCompositeKeyForSharedListsTable')
BEGIN
    EXEC sp_rename N'[SharedLists]', N'Shared_Lists';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190530080912_AddedAppropriateCompositeKeyForSharedListsTable')
BEGIN
    EXEC sp_rename N'[Shared_Lists].[IX_SharedLists_user_id]', N'IX_Shared_Lists_user_id', N'INDEX';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190530080912_AddedAppropriateCompositeKeyForSharedListsTable')
BEGIN
    EXEC sp_rename N'[Shared_Lists].[IX_SharedLists_g_list_id]', N'IX_Shared_Lists_g_list_id', N'INDEX';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190530080912_AddedAppropriateCompositeKeyForSharedListsTable')
BEGIN
    ALTER TABLE [Shared_Lists] ADD CONSTRAINT [PK_Shared_Lists] PRIMARY KEY ([contact_id], [g_list_id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190530080912_AddedAppropriateCompositeKeyForSharedListsTable')
BEGIN
    ALTER TABLE [Shared_Lists] ADD CONSTRAINT [FK_Shared_Lists_Contacts_contact_id] FOREIGN KEY ([contact_id]) REFERENCES [Contacts] ([contact_id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190530080912_AddedAppropriateCompositeKeyForSharedListsTable')
BEGIN
    ALTER TABLE [Shared_Lists] ADD CONSTRAINT [FK_Shared_Lists_GiftLists_g_list_id] FOREIGN KEY ([g_list_id]) REFERENCES [GiftLists] ([gift_list_id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190530080912_AddedAppropriateCompositeKeyForSharedListsTable')
BEGIN
    ALTER TABLE [Shared_Lists] ADD CONSTRAINT [FK_Shared_Lists_Users_user_id] FOREIGN KEY ([user_id]) REFERENCES [Users] ([user_id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190530080912_AddedAppropriateCompositeKeyForSharedListsTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190530080912_AddedAppropriateCompositeKeyForSharedListsTable', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190609222804_AddedUserIdColumnToContactsForAssociationWithUserAccount')
BEGIN
    ALTER TABLE [Contacts] ADD [UserId] nvarchar(450) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190609222804_AddedUserIdColumnToContactsForAssociationWithUserAccount')
BEGIN
    CREATE INDEX [IX_Contacts_UserId] ON [Contacts] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190609222804_AddedUserIdColumnToContactsForAssociationWithUserAccount')
BEGIN
    ALTER TABLE [Contacts] ADD CONSTRAINT [FK_Contacts_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([user_id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190609222804_AddedUserIdColumnToContactsForAssociationWithUserAccount')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190609222804_AddedUserIdColumnToContactsForAssociationWithUserAccount', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190609231647_AddedFavoritesTable')
BEGIN
    CREATE TABLE [Favorites] (
        [Id] int NOT NULL IDENTITY,
        [g_list_id] int NOT NULL,
        [item_id] int NOT NULL,
        [contact_id] int NOT NULL,
        CONSTRAINT [id] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Favorites_Contacts_contact_id] FOREIGN KEY ([contact_id]) REFERENCES [Contacts] ([contact_id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Favorites_GiftLists_g_list_id] FOREIGN KEY ([g_list_id]) REFERENCES [GiftLists] ([gift_list_id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Favorites_Items_item_id] FOREIGN KEY ([item_id]) REFERENCES [Items] ([item_id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190609231647_AddedFavoritesTable')
BEGIN
    CREATE INDEX [IX_Favorites_contact_id] ON [Favorites] ([contact_id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190609231647_AddedFavoritesTable')
BEGIN
    CREATE INDEX [IX_Favorites_g_list_id] ON [Favorites] ([g_list_id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190609231647_AddedFavoritesTable')
BEGIN
    CREATE INDEX [IX_Favorites_item_id] ON [Favorites] ([item_id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190609231647_AddedFavoritesTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190609231647_AddedFavoritesTable', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190610023122_ChangedFavoritesTableIdColumnName')
BEGIN
    ALTER TABLE [Favorites] DROP CONSTRAINT [id];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190610023122_ChangedFavoritesTableIdColumnName')
BEGIN
    EXEC sp_rename N'[Favorites].[Id]', N'id', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190610023122_ChangedFavoritesTableIdColumnName')
BEGIN
    ALTER TABLE [Favorites] ADD CONSTRAINT [PK_Favorites] PRIMARY KEY ([id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190610023122_ChangedFavoritesTableIdColumnName')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190610023122_ChangedFavoritesTableIdColumnName', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190610025142_AddedNotificationsTable')
BEGIN
    CREATE TABLE [Notifications] (
        [id] int NOT NULL IDENTITY,
        [user_id] nvarchar(450) NOT NULL,
        [type] nvarchar(50) NOT NULL,
        [created_on] datetime2 NOT NULL,
        [deleted] bit NOT NULL DEFAULT 0,
        CONSTRAINT [PK_Notifications] PRIMARY KEY ([id]),
        CONSTRAINT [FK_Notifications_Users_user_id] FOREIGN KEY ([user_id]) REFERENCES [Users] ([user_id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190610025142_AddedNotificationsTable')
BEGIN
    CREATE INDEX [IX_Notifications_user_id] ON [Notifications] ([user_id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190610025142_AddedNotificationsTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190610025142_AddedNotificationsTable', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190610050224_AddedFacebookIdToUsersTable')
BEGIN
    ALTER TABLE [Users] ADD [facebook_id] nvarchar(50) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190610050224_AddedFacebookIdToUsersTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190610050224_AddedFacebookIdToUsersTable', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190610094346_RemovedFacebookIdFromUsersTable')
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'facebook_id');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [Users] DROP COLUMN [facebook_id];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190610094346_RemovedFacebookIdFromUsersTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190610094346_RemovedFacebookIdFromUsersTable', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190610095700_AddingUserFacebookAssociativeTable')
BEGIN
    CREATE TABLE [User_Facebook_Assoc] (
        [user_id] nvarchar(450) NOT NULL,
        [facebook_id] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_User_Facebook_Assoc] PRIMARY KEY ([user_id], [facebook_id]),
        CONSTRAINT [FK_User_Facebook_Assoc_Users_user_id] FOREIGN KEY ([user_id]) REFERENCES [Users] ([user_id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190610095700_AddingUserFacebookAssociativeTable')
BEGIN
    CREATE UNIQUE INDEX [IX_User_Facebook_Assoc_user_id] ON [User_Facebook_Assoc] ([user_id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190610095700_AddingUserFacebookAssociativeTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190610095700_AddingUserFacebookAssociativeTable', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190610182832_RemovedIsRequiredUserIdFromNotificationsTable')
BEGIN
    ALTER TABLE [Notifications] DROP CONSTRAINT [FK_Notifications_Users_user_id];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190610182832_RemovedIsRequiredUserIdFromNotificationsTable')
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notifications]') AND [c].[name] = N'user_id');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Notifications] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [Notifications] ALTER COLUMN [user_id] nvarchar(450) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190610182832_RemovedIsRequiredUserIdFromNotificationsTable')
BEGIN
    ALTER TABLE [Notifications] ADD CONSTRAINT [FK_Notifications_Users_user_id] FOREIGN KEY ([user_id]) REFERENCES [Users] ([user_id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190610182832_RemovedIsRequiredUserIdFromNotificationsTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190610182832_RemovedIsRequiredUserIdFromNotificationsTable', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190610184458_AddedNotificationsFieldContactIdToNotificationsTable')
BEGIN
    ALTER TABLE [Notifications] ADD [contact_id] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190610184458_AddedNotificationsFieldContactIdToNotificationsTable')
BEGIN
    ALTER TABLE [Notifications] ADD [message] nvarchar(250) NOT NULL DEFAULT N'';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190610184458_AddedNotificationsFieldContactIdToNotificationsTable')
BEGIN
    ALTER TABLE [Notifications] ADD [title] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190610184458_AddedNotificationsFieldContactIdToNotificationsTable')
BEGIN
    CREATE INDEX [IX_Notifications_contact_id] ON [Notifications] ([contact_id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190610184458_AddedNotificationsFieldContactIdToNotificationsTable')
BEGIN
    ALTER TABLE [Notifications] ADD CONSTRAINT [FK_Notifications_Contacts_contact_id] FOREIGN KEY ([contact_id]) REFERENCES [Contacts] ([contact_id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190610184458_AddedNotificationsFieldContactIdToNotificationsTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190610184458_AddedNotificationsFieldContactIdToNotificationsTable', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190611030643_RemovedContactFromNotificationsTable')
BEGIN
    ALTER TABLE [Notifications] DROP CONSTRAINT [FK_Notifications_Contacts_contact_id];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190611030643_RemovedContactFromNotificationsTable')
BEGIN
    DROP INDEX [IX_Notifications_contact_id] ON [Notifications];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190611030643_RemovedContactFromNotificationsTable')
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notifications]') AND [c].[name] = N'contact_id');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Notifications] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [Notifications] DROP COLUMN [contact_id];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190611030643_RemovedContactFromNotificationsTable')
BEGIN
    ALTER TABLE [Notifications] ADD [ContactsContactId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190611030643_RemovedContactFromNotificationsTable')
BEGIN
    CREATE INDEX [IX_Notifications_ContactsContactId] ON [Notifications] ([ContactsContactId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190611030643_RemovedContactFromNotificationsTable')
BEGIN
    ALTER TABLE [Notifications] ADD CONSTRAINT [FK_Notifications_Contacts_ContactsContactId] FOREIGN KEY ([ContactsContactId]) REFERENCES [Contacts] ([contact_id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190611030643_RemovedContactFromNotificationsTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190611030643_RemovedContactFromNotificationsTable', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190611031134_RemovedNotificationsFromContacts')
BEGIN
    ALTER TABLE [Notifications] DROP CONSTRAINT [FK_Notifications_Contacts_ContactsContactId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190611031134_RemovedNotificationsFromContacts')
BEGIN
    DROP INDEX [IX_Notifications_ContactsContactId] ON [Notifications];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190611031134_RemovedNotificationsFromContacts')
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notifications]') AND [c].[name] = N'ContactsContactId');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Notifications] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [Notifications] DROP COLUMN [ContactsContactId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190611031134_RemovedNotificationsFromContacts')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190611031134_RemovedNotificationsFromContacts', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612061843_AddedPurchasStatusToGiftListItemsTable')
BEGIN
    ALTER TABLE [GList_Items] ADD [purchase_status] nvarchar(5) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612061843_AddedPurchasStatusToGiftListItemsTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190612061843_AddedPurchasStatusToGiftListItemsTable', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190701221451_AddedDeleteFlagToGiftItemsTable')
BEGIN
    ALTER TABLE [GList_Items] ADD [_deleted] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190701221451_AddedDeleteFlagToGiftItemsTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190701221451_AddedDeleteFlagToGiftItemsTable', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190704043648_RemovedSharedListsCompositeKeyAndReplaceWithId')
BEGIN
    ALTER TABLE [Shared_Lists] DROP CONSTRAINT [PK_Shared_Lists];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190704043648_RemovedSharedListsCompositeKeyAndReplaceWithId')
BEGIN
    ALTER TABLE [Shared_Lists] ADD [shared_list_id] int NOT NULL IDENTITY;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190704043648_RemovedSharedListsCompositeKeyAndReplaceWithId')
BEGIN
    ALTER TABLE [Shared_Lists] ADD CONSTRAINT [PK_Shared_Lists] PRIMARY KEY ([shared_list_id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190704043648_RemovedSharedListsCompositeKeyAndReplaceWithId')
BEGIN
    CREATE INDEX [IX_Shared_Lists_contact_id] ON [Shared_Lists] ([contact_id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190704043648_RemovedSharedListsCompositeKeyAndReplaceWithId')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190704043648_RemovedSharedListsCompositeKeyAndReplaceWithId', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190814235736_RemoveRequiredPassFromSharedList')
BEGIN
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Shared_Lists]') AND [c].[name] = N'password');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Shared_Lists] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [Shared_Lists] ALTER COLUMN [password] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190814235736_RemoveRequiredPassFromSharedList')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190814235736_RemoveRequiredPassFromSharedList', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190815015045_AddedPublicFlagToSharedListsTable')
BEGIN
    ALTER TABLE [Shared_Lists] ADD [is_public] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190815015045_AddedPublicFlagToSharedListsTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190815015045_AddedPublicFlagToSharedListsTable', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190815022442_RemovedPublicFlagFromSharedListAndAddedToGiftLists')
BEGIN
    DECLARE @var13 sysname;
    SELECT @var13 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Shared_Lists]') AND [c].[name] = N'is_public');
    IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Shared_Lists] DROP CONSTRAINT [' + @var13 + '];');
    ALTER TABLE [Shared_Lists] DROP COLUMN [is_public];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190815022442_RemovedPublicFlagFromSharedListAndAddedToGiftLists')
BEGIN
    ALTER TABLE [GiftLists] ADD [is_public] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190815022442_RemovedPublicFlagFromSharedListAndAddedToGiftLists')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190815022442_RemovedPublicFlagFromSharedListAndAddedToGiftLists', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190816002420_RemovePasswordFromSharedListsAddPasswordToGiftLists')
BEGIN
    DECLARE @var14 sysname;
    SELECT @var14 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Shared_Lists]') AND [c].[name] = N'password');
    IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [Shared_Lists] DROP CONSTRAINT [' + @var14 + '];');
    ALTER TABLE [Shared_Lists] DROP COLUMN [password];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190816002420_RemovePasswordFromSharedListsAddPasswordToGiftLists')
BEGIN
    ALTER TABLE [GiftLists] ADD [password] nvarchar(max) NOT NULL DEFAULT N'';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190816002420_RemovePasswordFromSharedListsAddPasswordToGiftLists')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190816002420_RemovePasswordFromSharedListsAddPasswordToGiftLists', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190825183733_AddEventDateToGiftListsTable')
BEGIN
    ALTER TABLE [GiftLists] ADD [event_date] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190825183733_AddEventDateToGiftListsTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190825183733_AddEventDateToGiftListsTable', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190830212202_AddPersistColumnToNotifications')
BEGIN
    ALTER TABLE [Notifications] ADD [dismissed] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190830212202_AddPersistColumnToNotifications')
BEGIN
    ALTER TABLE [Notifications] ADD [persist] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190830212202_AddPersistColumnToNotifications')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190830212202_AddPersistColumnToNotifications', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190907155218_AddNameToUsersTable')
BEGIN
    ALTER TABLE [Users] ADD [name] nvarchar(max) NOT NULL DEFAULT N'';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190907155218_AddNameToUsersTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190907155218_AddNameToUsersTable', N'2.2.1-servicing-10028');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190910172527_AddDeletedFlagToContactUsersTable')
BEGIN
    ALTER TABLE [ContactUsers] ADD [_deleted] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190910172527_AddDeletedFlagToContactUsersTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190910172527_AddDeletedFlagToContactUsersTable', N'2.2.1-servicing-10028');
END;

GO

