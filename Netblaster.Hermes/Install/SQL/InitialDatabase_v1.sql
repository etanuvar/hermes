﻿USE [HermesDb]
GO


CREATE TABLE [dbo].[Attachment] (
    [Id] [int] NOT NULL IDENTITY,
    [CreateDate] [datetime] NOT NULL,
    [BinaryData] [varbinary](max),
    [FileName] [nvarchar](max),
    [MimeType] [nvarchar](max),
    [TaskItemId] [int],
    [ContactId] [int],
    CONSTRAINT [PK_dbo.Attachment] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_TaskItemId] ON [dbo].[Attachment]([TaskItemId])
CREATE INDEX [IX_ContactId] ON [dbo].[Attachment]([ContactId])
CREATE TABLE [dbo].[CalendarComponent] (
    [Id] [int] NOT NULL IDENTITY,
    [CreateDate] [datetime] NOT NULL,
    [Note] [nvarchar](max),
    [EndDate] [datetime],
    [KntId] [int],
    [SelectedContactType] [nvarchar](max),
    [ApplicationUserId] [nvarchar](128),
    [Title] [nvarchar](max),
    [CreatedById] [nvarchar](128),
    [DeadlineDate] [datetime],
    [GroupId] [int],
    [ItemStatus] [int],
    [Discriminator] [nvarchar](128) NOT NULL,
    [ApplicationUser_Id] [nvarchar](128),
    CONSTRAINT [PK_dbo.CalendarComponent] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ApplicationUserId] ON [dbo].[CalendarComponent]([ApplicationUserId])
CREATE INDEX [IX_CreatedById] ON [dbo].[CalendarComponent]([CreatedById])
CREATE INDEX [IX_GroupId] ON [dbo].[CalendarComponent]([GroupId])
CREATE INDEX [IX_ApplicationUser_Id] ON [dbo].[CalendarComponent]([ApplicationUser_Id])
CREATE TABLE [dbo].[User] (
    [Id] [nvarchar](128) NOT NULL,
    [CreateDate] [datetime] NOT NULL,
    [FirstName] [nvarchar](max),
    [LastName] [nvarchar](max),
    [JobTitle] [nvarchar](max),
    [Photo] [varbinary](max),
    [LastLoginDate] [datetime] NOT NULL,
    [Email] [nvarchar](256),
    [EmailConfirmed] [bit] NOT NULL,
    [PasswordHash] [nvarchar](500),
    [SecurityStamp] [nvarchar](max),
    [PhoneNumber] [nvarchar](50),
    [PhoneNumberConfirmed] [bit] NOT NULL,
    [TwoFactorEnabled] [bit] NOT NULL,
    [LockoutEndDateUtc] [datetime],
    [LockoutEnabled] [bit] NOT NULL,
    [AccessFailedCount] [int] NOT NULL,
    [UserName] [nvarchar](256) NOT NULL,
    CONSTRAINT [PK_dbo.User] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [UserNameIndex] ON [dbo].[User]([UserName])
CREATE TABLE [dbo].[Group] (
    [Id] [int] NOT NULL IDENTITY,
    [CreateDate] [datetime] NOT NULL,
    [IsActive] [bit] NOT NULL,
    [Name] [nvarchar](max),
    CONSTRAINT [PK_dbo.Group] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[UserGroup] (
    [Id] [int] NOT NULL IDENTITY,
    [ApplicationUserId] [nvarchar](128),
    [GroupId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.UserGroup] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ApplicationUserId] ON [dbo].[UserGroup]([ApplicationUserId])
CREATE INDEX [IX_GroupId] ON [dbo].[UserGroup]([GroupId])
CREATE TABLE [dbo].[TaskDetail] (
    [Id] [int] NOT NULL IDENTITY,
    [CreateDate] [datetime] NOT NULL,
    [TaskItemId] [int] NOT NULL,
    [Text] [nvarchar](max),
    [TaskDetailType] [int] NOT NULL,
    [ApplicationUserId] [nvarchar](128),
    CONSTRAINT [PK_dbo.TaskDetail] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_TaskItemId] ON [dbo].[TaskDetail]([TaskItemId])
CREATE INDEX [IX_ApplicationUserId] ON [dbo].[TaskDetail]([ApplicationUserId])
CREATE TABLE [dbo].[TaskItemUser] (
    [Id] [int] NOT NULL IDENTITY,
    [ApplicationUserId] [nvarchar](128),
    [TaskItemId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.TaskItemUser] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ApplicationUserId] ON [dbo].[TaskItemUser]([ApplicationUserId])
CREATE INDEX [IX_TaskItemId] ON [dbo].[TaskItemUser]([TaskItemId])
CREATE TABLE [dbo].[TaskSubItem] (
    [Id] [int] NOT NULL IDENTITY,
    [Text] [nvarchar](max),
    [IsFinished] [bit] NOT NULL,
    [FinishedDate] [datetime],
    [TaskItemId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.TaskSubItem] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_TaskItemId] ON [dbo].[TaskSubItem]([TaskItemId])
CREATE TABLE [dbo].[ToDoItem] (
    [Id] [int] NOT NULL IDENTITY,
    [CreateDate] [datetime] NOT NULL,
    [EndDate] [datetime] NOT NULL,
    [Note] [nvarchar](max),
    [IsReady] [bit] NOT NULL,
    [FinishedById] [int],
    [TaskItemId] [int] NOT NULL,
    [FinishedBy_Id] [nvarchar](128),
    CONSTRAINT [PK_dbo.ToDoItem] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_TaskItemId] ON [dbo].[ToDoItem]([TaskItemId])
CREATE INDEX [IX_FinishedBy_Id] ON [dbo].[ToDoItem]([FinishedBy_Id])
CREATE TABLE [dbo].[WorkTime] (
    [Id] [int] NOT NULL IDENTITY,
    [StartDate] [datetime] NOT NULL,
    [EndDate] [datetime] NOT NULL,
    [Description] [nvarchar](max),
    [TaskItemId] [int],
    [ApplicationUserId] [int] NOT NULL,
    [ApplicationUser_Id] [nvarchar](128),
    CONSTRAINT [PK_dbo.WorkTime] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_TaskItemId] ON [dbo].[WorkTime]([TaskItemId])
CREATE INDEX [IX_ApplicationUser_Id] ON [dbo].[WorkTime]([ApplicationUser_Id])
CREATE TABLE [dbo].[ChatItem] (
    [Id] [int] NOT NULL IDENTITY,
    [CreateDate] [datetime] NOT NULL,
    [Text] [nvarchar](max),
    [ApplicationUserId] [nvarchar](128),
    CONSTRAINT [PK_dbo.ChatItem] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ApplicationUserId] ON [dbo].[ChatItem]([ApplicationUserId])
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] [int] NOT NULL IDENTITY,
    [UserId] [nvarchar](128) NOT NULL,
    [ClaimType] [nvarchar](max),
    [ClaimValue] [nvarchar](max),
    CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]([UserId])
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] [nvarchar](128) NOT NULL,
    [ProviderKey] [nvarchar](128) NOT NULL,
    [UserId] [nvarchar](128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey], [UserId])
)
CREATE INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]([UserId])
CREATE TABLE [dbo].[AspNetUserRoles] (
    [UserId] [nvarchar](128) NOT NULL,
    [RoleId] [nvarchar](128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId])
)
CREATE INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]([UserId])
CREATE INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]([RoleId])
CREATE TABLE [dbo].[UserMessage] (
    [Id] [int] NOT NULL IDENTITY,
    [CreateDate] [datetime] NOT NULL,
    [Text] [nvarchar](max),
    [IsDeleted] [bit] NOT NULL,
    [IsRead] [bit] NOT NULL,
    [SenderDisplayName] [nvarchar](max),
    [SenderId] [nvarchar](max),
    [ReceiverId] [nvarchar](128),
    CONSTRAINT [PK_dbo.UserMessage] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ReceiverId] ON [dbo].[UserMessage]([ReceiverId])
CREATE TABLE [dbo].[Parameter] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max),
    [Value] [nvarchar](max),
    CONSTRAINT [PK_dbo.Parameter] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] [nvarchar](128) NOT NULL,
    [Name] [nvarchar](256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]([Name])
ALTER TABLE [dbo].[Attachment] ADD CONSTRAINT [FK_dbo.Attachment_dbo.CalendarComponent_TaskItemId] FOREIGN KEY ([TaskItemId]) REFERENCES [dbo].[CalendarComponent] ([Id])
ALTER TABLE [dbo].[Attachment] ADD CONSTRAINT [FK_dbo.Attachment_dbo.CalendarComponent_ContactId] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[CalendarComponent] ([Id])
ALTER TABLE [dbo].[CalendarComponent] ADD CONSTRAINT [FK_dbo.CalendarComponent_dbo.User_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[User] ([Id])
ALTER TABLE [dbo].[CalendarComponent] ADD CONSTRAINT [FK_dbo.CalendarComponent_dbo.Group_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Group] ([Id])
ALTER TABLE [dbo].[CalendarComponent] ADD CONSTRAINT [FK_dbo.CalendarComponent_dbo.User_ApplicationUser_Id] FOREIGN KEY ([ApplicationUser_Id]) REFERENCES [dbo].[User] ([Id])
ALTER TABLE [dbo].[CalendarComponent] ADD CONSTRAINT [FK_dbo.CalendarComponent_dbo.User_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [dbo].[User] ([Id])
ALTER TABLE [dbo].[UserGroup] ADD CONSTRAINT [FK_dbo.UserGroup_dbo.User_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [dbo].[User] ([Id])
ALTER TABLE [dbo].[UserGroup] ADD CONSTRAINT [FK_dbo.UserGroup_dbo.Group_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Group] ([Id])
ALTER TABLE [dbo].[TaskDetail] ADD CONSTRAINT [FK_dbo.TaskDetail_dbo.User_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [dbo].[User] ([Id])
ALTER TABLE [dbo].[TaskDetail] ADD CONSTRAINT [FK_dbo.TaskDetail_dbo.CalendarComponent_TaskItemId] FOREIGN KEY ([TaskItemId]) REFERENCES [dbo].[CalendarComponent] ([Id])
ALTER TABLE [dbo].[TaskItemUser] ADD CONSTRAINT [FK_dbo.TaskItemUser_dbo.User_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [dbo].[User] ([Id])
ALTER TABLE [dbo].[TaskItemUser] ADD CONSTRAINT [FK_dbo.TaskItemUser_dbo.CalendarComponent_TaskItemId] FOREIGN KEY ([TaskItemId]) REFERENCES [dbo].[CalendarComponent] ([Id])
ALTER TABLE [dbo].[TaskSubItem] ADD CONSTRAINT [FK_dbo.TaskSubItem_dbo.CalendarComponent_TaskItemId] FOREIGN KEY ([TaskItemId]) REFERENCES [dbo].[CalendarComponent] ([Id])
ALTER TABLE [dbo].[ToDoItem] ADD CONSTRAINT [FK_dbo.ToDoItem_dbo.User_FinishedBy_Id] FOREIGN KEY ([FinishedBy_Id]) REFERENCES [dbo].[User] ([Id])
ALTER TABLE [dbo].[ToDoItem] ADD CONSTRAINT [FK_dbo.ToDoItem_dbo.CalendarComponent_TaskItemId] FOREIGN KEY ([TaskItemId]) REFERENCES [dbo].[CalendarComponent] ([Id])
ALTER TABLE [dbo].[WorkTime] ADD CONSTRAINT [FK_dbo.WorkTime_dbo.User_ApplicationUser_Id] FOREIGN KEY ([ApplicationUser_Id]) REFERENCES [dbo].[User] ([Id])
ALTER TABLE [dbo].[WorkTime] ADD CONSTRAINT [FK_dbo.WorkTime_dbo.CalendarComponent_TaskItemId] FOREIGN KEY ([TaskItemId]) REFERENCES [dbo].[CalendarComponent] ([Id])
ALTER TABLE [dbo].[ChatItem] ADD CONSTRAINT [FK_dbo.ChatItem_dbo.User_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [dbo].[User] ([Id])
ALTER TABLE [dbo].[AspNetUserClaims] ADD CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
ALTER TABLE [dbo].[AspNetUserLogins] ADD CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
ALTER TABLE [dbo].[AspNetUserRoles] ADD CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
ALTER TABLE [dbo].[AspNetUserRoles] ADD CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id])
ALTER TABLE [dbo].[UserMessage] ADD CONSTRAINT [FK_dbo.UserMessage_dbo.User_ReceiverId] FOREIGN KEY ([ReceiverId]) REFERENCES [dbo].[User] ([Id])
CREATE TABLE [dbo].[__MigrationHistory] (
    [MigrationId] [nvarchar](150) NOT NULL,
    [ContextKey] [nvarchar](300) NOT NULL,
    [Model] [varbinary](max) NOT NULL,
    [ProductVersion] [nvarchar](32) NOT NULL,
    CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY ([MigrationId], [ContextKey])
)
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'201711242210586_InitialDatabase_v1', N'Netblaster.Hermes.DAL.Migrations.Configuration',  0x1F8B0800000000000400ED5D5B6FDC3A927E5F60FF43A39F660619B7ED4C0ECE04F60C1C5FCEF19C3831626776DF02B99BB6B5514B3D923A89B1D85F360FF393F62F2CA90B4991C59B445DDA6B0401DC22592C16BF2ADEAAC8FFFDE7BF8EFEFA631DCDBEA1340B93F8787EB0B73F9FA17899ACC2F8E178BECDEFFFF8F3FCAF7FF9F77F3B3A5FAD7FCCFE5EE77B4DF2E19271763C7FCCF3CDDBC5225B3EA27590EDADC3659A64C97DBEB74CD68B60952C0EF7F7FFBC383858204C628E69CD66479FB6711EAE51F103FF3C4DE225DAE4DB20BA4A5628CAAAEF38E5A6A03AFB10AC51B60996E878FE01E5775190E528DDFB15A5F8F3DED9C9FBF9EC240A03CCCC0D8AEEE7B3208E933CC831AB6F3F67E8264F93F8E166833F04D1EDD306E17CF74194A1AA096F5976DBD6EC1F92D62C58C19AD4729BE5C9DA91E0C1EB4A3C0BB1782B21CFA9F8B000CFB1A0F327D2EA4288C7F3933C0F968F6B14E7F39958DDDBD32825591552DE2B7A678F51783503F3BDA240C17822FF5ECD4EB751BE4DD1718CB6791A44AF66D7DBBB285CFE869E6E93AF283E8EB751C4F38D39C7698D0FF8D3759A6C509A3F7D42F7556B2E57F3D9A2596E2116A4C5B832652B2FE3FCF5E17CF601571EDC4588C28293C84D9EA4E81714A334C8D1EA3AC871536342031582956A17EA3A4D112E7786FFD77592BF6F31F6816AF5A4DE8571903EE1E2414DAAFC329F5D053FDEA3F8217F3C9EE33FE7B38BF0075AD55FAC685F8411227FD594B1CA60036047F9731C627B810BE5E9D658D1156E39A9A2F78A6E83ECEB658ED652671BFA2B8931B87353A90FC1B7F0A100085C7E3EFB84A2223D7B0C37A559DAABD2BE30FDC97043D364FD298958513EF9CB6D903E204CED3651E7B949B6E952968792C55A32208F75A28A49285DE212CC04B179B460E6496BB4A858DB59ACAAB86773357B1764A88271D1BF4184E255909E26EB4D121303AB85DA6FB10C33479380AB454B6C96AAF60DA258279B0D9644217C3CB6A6AC09408D07873FB7A951095DA16EBD968979014D6B66A11095B44DC857E3DD9A6D5E97FC180625AB805AB6D238497C2DE70A4D32BB3E61E8807147D5F63871B808D32C1F64747F1F0C54D1DF92BBDB308FFAAFE8FA31C913FFF32D22A8F7C943187BE9E0F37510461A511CBEF9C983288A5AB09DB90FB1DA528D789760FB13C4CE3C5F0759F63D4957BF06D9A386F537FBFB1E58BF41CB6D8AEDDB4D1EAC37436026461FB6EB3B6236352DF35B95B79EB9FD9E5CE0A12449CF6352AA33BDF7C9F26BB2CDCFE31501F8E77C29E3DD928017764E964B94651718CB64DEB425CBE02E533032AA194C9EADFA996A562F3AC489273CCD10A731503136EB30E796262116455CA74EA78F414E96128A895395AA9DEC29F2C86B2B5546D705D66914840A862509555935622F7298455D6673156F3106D9B15A6755B35AE630B25A6573659510B3E3B4CAA966B4C860E4B3CCE5CA2659FB9EA11C9B17985996AE03AD3A17B8D856646DBB33400AABB9AF7398F857E5536E1780995DDB400AFD9226DB0DDC009AACE35E9949625D9DB30DDF5778600A1E1428E7327CF9849628FC26332DA683FC4A995C59FD8F24FD4AC66D98CF3A552760551E895F65C66EBB49D2464DDB7D2591D0AEAF6F776A43FC4392EBA65D7E26F3D5B4553F67B5461EDB7B6D07B8BAFCB4763287598A97C859BD7B1A633BE60C05AB288CFD00B718311C0F2648AFE3E56BBECD68E3F901B94E6ABD8630ED51B63A1810A758DAD303EBE9758D033D9F5C36804B9AAAE6916571E5B0E85F3D77551680B3723AA1E4AA4CEE7156CA4C14381DA58C68E6A1344FCF135015AB5206FD94B313BB37DB3BF5FA94CBA064564C0779953239B39A9C251A3EAB5498493151E650CAD1D39412624F4A544F22B5EC598FE195EAB61BC08BC22FB3C401678997D9C932C7EB9CAEFB86FD9C6B745FC74A238990A459B336879A56CA4089B555084AE045292670F26E35516D3FC7B438BEEFB84123CE9BCC5B39DD27750EBAA8664F3DAF735A5C9633B02ECBCB92C28B360E3844297DD55CE9A01F79EFCB70869186BB5173155126B99ECC4DDCB3A8F3D63DB4A833ECF27B73EA735CDD695895E6D99D76C2BA3817F1345E0CD604D4CB975DEBACA69DCFA854FB2FDA032DAF1EB88E3B1C5A86FDA96CB511D145632B122F0A3B89E1FC32BB08E3307BECEE5853D3311F970C61298C3AE6BC2F07699872F3AE9D8255FB68ADB5AB2AFFA25A034EDE950784533CCDBCCC3EA160F5E44BD5F9B338AB532CFF7ACE58D1EF6CF3F980BD6D962CEB3990C7EF88EFB6FBAEE4CF8F11AA77CBDB1AA1BAFC8B11327A25E7419A4FCA069DA16C99861BD2B8A906E8295731432C2D3AF936897A6B7482F2625C1CCFCE944CFA312EB58F6D6B87ABAAFC8B7119727B728875C8D437FF3AB99A4BD17A269FF456CA554383502AFCC1755A76456F1038C9365899F6EAD27B25DD8B14D3FC8EB57F4F22FB6A665D9829E4A1AD42BE3EB8BB7FFDF39B9F82D5EB9FFE845EBF798ECAE90DE1AE4185A4FB0689122E6AFA7B106D7D57D54A1B8A9003FFDA50909DBE36146CE2CFDFC215B1570B73893A33266F95BFC6B3ABCE099C0DAD0E8D660E5DF93036A095BA9041CABFB610AAD3571618CA6056D2A036A81FCBFAD7FC4E04715C2C4A17E7A18AC4CB8AE0B9AD082EB33314A1BCFBC144B9E9D995CA0D8AF1507116669B2878EAC71910AC51ABB17E2AAA23C1065D74B118365FC16E9073953222AE95C9BA0EC8E894B7F758A0045ECCD538FEB642259358A1F89F6EEDC6546B57AEE23120B1F7EB1DD4D1307C7F7FA9B2312329A74A2612C8E26620B76BC0A7AB0E46BBCC2EA2E081DDB6D9CE66924A3221D0CD77FCE36732D2464FB86B79F3D6EC9F2B44AE59A947023C361740282CC8F17C5FEACE46F64BD2A70F291E8C68890343914FE8BF8ABBDD6881434381B3244634F3EB7DB907CBBE32F41FEFBFE9BBFF18EDD1FB8FB0F36B889B43EE73B2EB4352E434599737A72A3A1116F3499625CBB01099A0298D68CA66DDE7F16A66135A29C78262EB848516929D65ACB3B85D7B7B0752D30CE469D00823CF5F1CDBACE00F12756CE4514A4C4B406E8CCA70CF85712E8F0861BC0C374164D14EA1ACE578423A84D622A69CA10D9960C7B9851C6CAAE70F346536686DC2806792D4D182438F25A858E8ABB1CF81385833A0E4FED65286B0241EA2342B90E1DA0D50122F43C24912B14DE58D60F751D0A40EC45175BD45540E43001738660F2E8B8B59AC31065AC5563033723500DA8CB2B7E10138831D197A2542CC7010A2ABBCC14C88A06474419ADEF1D4A87E501435046A53330D151C77E8D3034675C982A721CF0D2DFE0C10CCC09063DC0EC24511C9A5EB645358171409E70A24D3AD6F238C6F66B606829A5EFEBB34C241117916A890DDF43C424EBE3FC36C16FD234CE46258688902DEA9751F18F0661AB4F4D16F32022053633338EA6F821CC9AA99181B7008D5F5C3AE5936397CD10A1D26EBD6197E235B38251F43C36C67AD9C14B3A7EB7675005FB3D76948AB1BAAD4F7760D0A2A151B03614A25E4DD8014101CA6EC725DA418D7E33484D3014D9A00B32E23631B4998354B1D8FE6490A632A948A8721B44925D89D5025656494AABBCD6152ACD75948A23D9CCC574C0FA35972FC9591639D667512451BCDF237E156B23180722965BB13CA65F17087AAEF5D5EF1E8B60873AA1300207029BB16E20EF253C66EA95A600EE4E2F8A6218DF61A697E93648405B189A901D4D424F75D5A082B9E7AB1561AE1DD970E67E7C64A00D401B184BDF87CE89B3D00E4F422B161605228AB5EE9B10580F8644F2F28131EFC51A0AC8AD11B0465CD668F80B2A648760E65A451F6202B72F78CB1B20E03C44A47EB4110D668F308006BC863F2F802C33D545DAF8FFD68BA7ED0F83237E70FF53B4A23B917A9181A00593A71DB54CF07178D022ED583D0CAC9B9E975686EEE5F3F4BEE30F537BC293DCACC5FCFD310137FBDCC7769DE0F3DE66D0483DEB15B0133C3325CF724F8906EDD9A460E89AC764EDD55F1D1D00444F8A8FA5B17EE23CF87A0B9900E4D9A48A1E1675BEAA60E8028B524AC8643F0D2805ED054C66C15080E631AC65246DB9C057940528A406E29AE07B7AA0AEDC9AAE835111684F20DCA6BDBCBBF09C642C5000B23E1AB49A88A6193498843A1810EF4B4B14414D8713490AD1F649148550E9E16AD5392E03C950D6498BF124487F703B320543B05A84831A70B0B62EC312C90163D693791628F55C974E8D1A28108F7A49444849DA29870C49E9F96E143377E0D44A4BD34881AB0E1E640B67E9C594BB6DA6171205BAEF3F5544B336F817CF6BA2D887DBA543390A221F410212E40DF40C6D434B0599CA195B5A8F932229755F782A2380A5846FDD1D6C0A64E1A5C2CA3FD38B28DC68851DA4D39B8C8887B9551232145001BDC103984CD8374E4E8358E68652F3B4B451D620508C7321E4BDACBD0476409ADAA47278D88CC3158BD4AAA1A2275F2016240142D68868174914533F483A30453E9A2424A09E8826060942BDBDF566D861183EA111E582436811E526B0CA11E5CCB1AB331838C0CC11D3D280E1468A09713ECA7A26A8AE4ABD24D3292878A13263B6815E83BAFD131B3AF3DA8205A6F7B4174DCCCDC42F5B4FEF53D014BF6F33649CC125C6A97F0EE321A0562F26323B0A0F40ECE52A3942ECE8298D8BACC2025A55373EF4282DE69006464F2D86DB648E3B3CB4B882D3775E2D178E9F6A15FF2A3101A71D8E045E9B8DB5E1483C3447D01BF2C1C3B97D446C38C4EA95CFBB8FD058DA88C6EA83D4007B8F25F231D0BE8A83D53DBCB6370E858385102627275BD6C34D6C1F9D2040357BA4349557D2DBE2C4B3B37CCE61996C911936F1FDBABD3C8CEE87AD983422A5C016DE006380DEAA1D0741BEC06ABA67720470BDAD1F42EA57A4FD32C25C8E94DDB32C1EDAD939404EF368594EAC6789752B58F691612E0B3A56D57D36BAB93889ACE590A09550DF1B255245F1D0BEF17E97D8EA4AD1EA5D791201DB6D36DD83B52FA19F56084546E3090A1B6F198817D0D2CCCB4DBD692C95FA64F49E9F7F38D8E1ED6AE1E1EA433E4663E74EBA82C1E93E782ADEF02D70CD044D87A2978323BF555A8F4589DA61D2D6E968F681D541F8E1638CB126DF26D101517616675C255B0D984F143C64A565F66379B60497AF58F37F3D98F751467C7F3C73CDFBC5D2CB28274B6B7A677FA2E93F52258258BC3FDFD3F2F0E0E16EB92C662D990B3E804406BCA93145B1E2195006A852EC23423EFD2057701B9F5F374B596B2C94E048AD3B6BA3ED94F40EEC4FA24AE2E43FEAE6EE005AF17E5FC0A64CF8B8AC8056E25C9513418812A2117C6C56F964114A4C0E5C3A749B45DC76A47127569FEFE7D9E0AFFDD9EDABB300ED227D2034D6AFC777B6A176184CA6B8E795AECAB3DA52BBCB82B6F8FE529B1AFF694F8B03B9E962E1C4F237DE6C8D510BEDABF8BA8B0802211B30B09B4821911F5C04A4BACD698AECA52FB10BA6B8AEC263390C2FC164BDD557DB2A78187A2E216E5AAF93232C10CF6F401AF539EBA85536A411BD7BE0A0BB7BFEA6261EAF2C9533B0BC9F39C6BACE1B88BFA00AC01599719F9FBE3FDEF14101381F2FB5D429B5FF35CBEE6CCD329BFD85338AFDF74E589D08F83F73DDCE5AACDC32977F46D98478258AB4FAE60A9AEE795D1A2B8B7574DEF0C05AB084FADE41E6FA6D853A4172CF2C494B72E6A649DB3FBFC1B32E7BEDB9934B61B3D259B6669CA260A7079DC907D5A079576CB298F6A7DE8BC24D0EF58D8AC0B4C147661F4295670D07C9E7EB6A7F53E8048B1AFF694FE96DC01B6977DB5A774FD98E449934CF5C9AD5DC5AEAC2C7421C961CC5E176ED78D117B0D78621B6960BDBE0F31AA5700312ECD416241967D4FD2D5AF41F62808AE91E232B35E6E53A23579B0DE8873EA469253BFC6E8C3963CBA21F52E4B68454F21513887C364E27B7281E7E9497A1E93B77DC4A5AA94EA80CF64F935D9E6D58CEF73BE14302A27B7A00DF02CA639AC85964B94651718A26439B515C71E20D99E36B1C5B215625F273392558E8EDD8732D089D362005394DB8561EB323B59E6E1378116FBEAB0FC92A0323198700EC6DDA1C24287DCE1A229DB0F643CED987859F48CD4F9BC63AC9F357815F0E5DEFDBAC2BB6032FC6E1297EFDA36E8145FDCF8E19F501379E2D386D59911B1CEBC74FDED38B55CDEE98BEF9EC56B87FF11B1405D91FD40A10E296D870465E99E761E3B9B97CBACF63E16E748ECBBCB3641594636C6CD94E70A47EAECEC018B7558720B202A8BEEC200EC7A4CD1DF8149F9CCFB93A818D54777AD9037F79B29CF542B981F7777ADA071F6EE5AA12EDA8F56DCE4419ACB48E63E0FAF136788ECD66FC8CC403C14E212C69A2CEFF0A494B98A7B382AAAAF81687142A42CBA0B96BFFB6C6687110478C07787927C07883BA62C68F4032EA807DDD73305CBF2D299FBEC48AB3817058855DF270928653C43174095B7BF7403948286FA980167C7DFBE852BF14047487238D2A9CAFC8684995E23A12FD84E001CAA388E2ED8282EBBE9060D9844BFC6A2BE4B8DA7A2BA5F6DD49D7E1A10E267AFBFBE2AA9DD6EBFB2F4FF8F49C76576862294CB3B28F4B3EB92135A71BA9DA4C7D86E9D85D9260A9EE4932B20D995B6D883ECAB83B271F7F836144E73BFEF684AC75D12D65DE5D8A562EE0AA729DB8FBAC9F871850C30639ADA64C9F718D871FC731BFB86EADFBEFBA61922056FAD36AEF3B0DD436D14B2DD2D254160E258A1BEB143169655577184E03B5C3926DAF1A7BCC6B63594B44C31FFD1CBECC3368A8EE7F74194290E03C0568BA172CE50515FE961BBBB2897B4DD4C043AC5746147CB9E11A979808FE9B2924961C8DCFECE4012AB8042691DFD87411ACE7EDE40DF99EBE9D88B3DE0CD82E76E3A325DE44941BF62163AD6565FE86F1AF45B05DC3622810BC190B8DE42205915FC2B46E09659E6B37A83034FDF9F306ED77B24C3DECD3FA2D32824A8A319AE8238BC47597E9B7C45F1F1FC70FFE0703E3B89C2202B43B3ABD8E2B7E2B5DF56C1C607AF49B0315AAD176271F790654225CB560DD7236E1657E34E11A97BF41B92106173D9FBD1422C780400B3BC123824722D14FE1784BB9D84F65C0739361A319BFDCD67047BC45B96E26FA125CFAF7CCB6A56F8EFBCB88FDA91141FED5B92FA16A477C5C7DFAD831FBFE70916F7B81BE8B188DF925A8CC92D1F83B4153116F4EB81187F46C5F58D7475FD255ED9FE389EFF7751ECEDECF23FBFB092AF661F53AC1F6F67FBB3FF71AD9F0B1476AB9E16B4AF9D9F996B55C330F4BC6848ED45E00180F4FC56CD900D992A8299139E5B79305ED943F380C3B626552BB04B549AA0BF0A7EBC47F143FE783C3F38FCD9D9049461421E1ADB88D26CD14CAEBCCF0636633DBBE18C7A3DBB59ABAA58174BC94786B686B910A828F6915ACE5646019A137647FB972E68B0B6F9DA00C5212CBEB75EF068E5B9C8460FE68105377A20C6E21B3D10ABA21C7D4CF58420C78E1D5085396A1072F8E627E7215708772CA9DF85B9337BCD20470D976FF6F7DD47E446B4A39F5E66D18E5A663B50F6225739C0B13D2D20A2B1DB10284632B6670D085D0447362B622C68D175C8A94B169F5FCD2EB3CF71F88F2D4EB8C5D210061C41DD00CEAC471C203CEC6565C1C725B60756CB51C6BAEB14D17D3BDB7D3BB04AE967E2DD457F55317E3B8B028F4ADCE7B6921D0385FB8AA70D323EBAB0FD0035392573423A1CDDB7B3589F5C678CAE434E680023FC76160CDE8C051F2FD87EF6D28C12EC36599F2C88407F939D4590C7A1D362437CD81D7A1AEBD71DD1FCCE708BDDCBD1671582CB4E8B11A3416180A11B76D1D9593DE3E20627A2668D98C1677020AB9C187998F2EED481001C45B8B39AE37371E76BBA34B949B835380CE1803B8B920EDDE0267B3BCCB2F0441FA7D35C7C627F5B8386B83E3B6418C2F88012D7BA303D203F1C1566C69FC099E7A3CB462B3CD31E13DAADD023873FD8814715F0076485A3FACC28989699A85BD1829BB2E8103DAB0C11DCD9B1628A330A2EDCB0FD6AB50E356C4F01882DF4D038165EE881181F5FD8467168F101665B8A68BF9DD51C6F88186C36D3762C9A884B543BBF00D2E8FEFD02B82009610B40FD041EFF549DFC3241D9CEF2313D39F56A1BE5215931615E8EE7FB7B7B0792F4B827275990004F96FFDCA4F707891806034A098A02E27394E56910CA3193D769182FC34D10295B2594B0441B113BA52DA69CA10DB1A8712E37D6A636DDF54E470B4A5C40BF491C8DA8194B9C50BF59254AC02040F84553478474C49F27BC68BD37FB414B3B886A1FB2190435D47745FF0EE920E0617E343C4DEEEB73018BEE2A71B9328B1BBA06860AE4ECC4BA51EE42B0FBA68704FB2E19B4FF9557AB0F3BAC78E9F49D183E06C341BB41637C3C941E5013183138A73B9E28FFF9B98C19DA0708A63B687070819F31F4A7FD5304CA282B1747A84C66E5529CE64EC3A850FF46112D2CE1391916CD5B0FD3362D1436631B97B100339A817182CC244C4CE5A53A3A546A6F591129F4FBF3028AFAF19029E204B81D6D8411A876876D40847E7C36238FFA390FB92EE3AD7043A2635413320238C6311E2EF018DD7228EFC91BDE7C502F5F9E24FBF85CCC87E6DD13E3A4753C1B4271D2B70D9920484631234E3019DD8C58DC8D38BC4199C6B6ECCE9CEA4DC6D6D44EEB131893A8FF7C0339F4E3B3018CFA9DA2E96EA4483607786AC70F487408919DE879C240EA73C18CCD83440E7EBD6320067A4B6758C494CEF52AC454A9CF1131AA1787268E18E07181610153F830AAF052263E47B8281E6198185A38F778EA5B3C924352EDA52F3AA2D0EFCF0525FA378F806811CD9334C3CC6ECBCB59A730B97D591F75581F8D0FA017C7EA093B567377828F82123EE8A188575383A4111F01CD2DA079C5D4272C86E79BA6315B513D48D80342CE9B8F5450940A0F4A4820A99E32A96D20A7DAE7341204D0FBF2618AE3F9EA8E5C495BC693F0E912769AF5009649AA0EC803D50A6433542E8DAA724BA54785808ACB14435D95BBA95443F51DA25B251908735EC712712E4DC5B85D25BC239A540B9F0855C3A75BD4C33C52C09A58B2AA2E96C3A236EAD5005646535575D10CA6AAE801A85C0F4D022BA1A9861AD8D98854034B826A60A9265DA5DBA1B28AD224503369AAA10660174DAA0AC803DAA06CF301E53453E65477B51FA3ADBBCAA3AFBBDA7872A9BB1C2AB5559759F435971B1836C683AE1441F34153550684663054C5C262E58AB834A81A2ED9529006219A04080BAFF940942EFC71C665154C977A366F9ACFD3B65A0C75CAD93B4744334A0B6F61B9888145F7E984A08801D4AF8779DEC5344DF33B8AB28530D4416B804C2C23DC7A118D3477A0EF43C3D3822EA2A828EA040006B6C8413C1CAB209B0337518840D2C1BE8F060E0F6F4D888DA2F1360139BD005C9EB796EFC7AA67A49DC4417D66F472805D6B7AB3FF4309411923A15109734C456FB810D7195428CAF54347B198D0610816E8151F430943727257C842EF0CDFAB2884152195846AA9D74210D01BD7801C4CCEDEFDA886B0526D3EB8EDB1F15A08E83D99FBEBFF9E1BAF7EAD5A96819DC76E2F1810F7129AEF677B14830E03064FD4DE30D077E36D9E9796A5E1EA78F94C96524A1741404476EE84FD0846D81A2BE5A1DAF6F280996A4FCB022780B35C2F2250EEDD15948054EF42A937DBCC4281FCC17A174A635351124A95EA5D28D55696592680C753EF22E1B7E9248994895E7620245F1DC53684DEA7A7B72D18610796EE50A8B6565B9851852F0A64456DDC569ECBE8027858E844F22CB76E01070240062637038DA301A0DF906E0F6225EA7B0DE9F9384D3B5A94E701D507FC334F52AC7D57C90A4559F1F568F1691B932B63CB5F67280B1F1889234C3346CBC6793CCD7319DF27B55B80C0519D45B805F20AE5C12AC88393340FEF31FA7032797F328C1FE6B3E2264DF234EA1D5A5DC61FB7F9669BE326A3F55DD4B8509BB817E8EA3F5A483C1F7D2C5E87C87C3401B319925B763FC6EFB661B4A27C5F00F74D2A4810BF85EA2E53D29739B9D3F4E18952FA8055C88E50253EEA6E718BD69B0813CB3EC6370179BFD19D370CBFF7E821583EB11BC65544CC1DD114FBD159183CA4C13AAB68B0F2F827C6F06AFDE32FFF07B05C8DD1B9690100 , N'6.1.3-40302')

GO
SET IDENTITY_INSERT [dbo].[Parameter] ON 

GO
INSERT [dbo].[Parameter] ([Id], [Name], [Value]) VALUES (1, N'ContactType', N'Telefon, E-mail')
GO
SET IDENTITY_INSERT [dbo].[Parameter] OFF
GO