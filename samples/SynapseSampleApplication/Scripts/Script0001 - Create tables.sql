-- Creates the following tables:
--  * Entry
--  * Feed
--  * FeedItem
--  * Revision

create table $schema$.[Entry](
	[Id] [int] identity(1,1) , --not null constraint PK_Entry_Id primary key NOT ENFORCED,
	[Name] [nvarchar](50) not null,
	[Title] [nvarchar](200) not null,
	[Summary] [nvarchar](500) not null,
	[IsVisible] [bit] not null,
	[Published] [datetime] not null,
	[LatestRevisionId] [int] null
)
go

create table $schema$.[Feed](
	[Id] [int] identity(1,1) not null, -- constraint PK_Feed_Id primary key NOT ENFORCED,
	[Name] [nvarchar](100) not null,
	[Title] [nvarchar](255) not null,
)
go

create table $schema$.[Revision](
	[Id] [int] identity(1,1) not null, -- constraint PK_Revision_Id primary key NOT ENFORCED,
	[EntryId] [int] not null,
	[Body] [nvarchar](500) not null,
	[ChangeSummary] [nvarchar](1000) not null,
	[Reason] [nvarchar](1000) not null,
	[Revised] [datetime] not null,
	[Tags] [nvarchar](1000) not null,
	[Status] [int] not null,
	[IsVisible] [bit] not null,
	[RevisionNumber] [int] not null,
)
go

create table $schema$.[FeedItem](
	[Id] [int] identity(1,1) not null, -- constraint PK_FeedItem_Id primary key NOT ENFORCED,
	[FeedId] [int] not null,
	[ItemId] [int] not null,
	[SortDate] [datetime] not null,
)
go

create table $schema$.[Comment](
	[Id] [int] identity(1,1) not null, -- constraint PK_Comment_Id primary key NOT ENFORCED,
	[Body] [nvarchar](500) not null,
	[AuthorName] [nvarchar](100) not null,
	[AuthorCompany] [nvarchar](100) not null,
	[AuthorEmail] [nvarchar](100) not null,
	[AuthorUrl] [nvarchar](100) not null,
	[Posted] [datetime] not null,
	[EntryId] [int] not null,
	[Status] int not null
)
go
