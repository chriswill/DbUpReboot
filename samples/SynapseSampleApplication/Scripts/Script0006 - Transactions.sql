-- Settings and Statistics
create table $schema$.Foo(
	[Id] [int] identity(1,1) not null, -- primary key NOT ENFORCED,
	[Name] [nvarchar](50) not null
)
go

insert into $schema$.[Entry] values()