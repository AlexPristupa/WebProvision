SET QUOTED_IDENTIFIER ON
GO

SET NOCOUNT ON
go

--#16218
if not exists (select 1
                 from syscolumns
                 where name = 'isdeleted' and 
                       id = object_id('dbo.users'))
	alter table users 
		add isdeleted  bit null
go
--END VERSION 1

--№16217
--dbo.users
if not exists (select 1 from dbo.users where login = 'admin')
insert into dbo.users ([login], [passw], [lastvisit], [lastvisitdatetime], [hostname], [mustchangepass], [disable], [validatyperiod]) values ('admin', cast('admin' as varbinary), 0, NULL, NULL, 0, 0, '2200-01-01 00:00:00.000')
go
--END VERSION 2

--END VERSION

--пишем выше
insert into dbo.version (version,date) values ('dbbuild 2', getdate());
SET NOCOUNT OFF
go