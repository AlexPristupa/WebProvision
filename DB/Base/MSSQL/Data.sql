SET QUOTED_IDENTIFIER ON
go
SET NOCOUNT ON
go

--Pages
if not exists (select 1 from dbo.Pages where PrivateName = 'PRV_CONFIGURATION')
insert into dbo.Pages(ViewName, ParentId, PrivateName) values ('Конфигурация', NULL, 'PRV_CONFIGURATION')
go
if not exists (select 1 from dbo.Pages where PrivateName = 'PRV_DEVICES')
insert into dbo.Pages(ViewName, ParentId, PrivateName) values ('Устройства', NULL, 'PRV_DEVICES')
go
if not exists (select 1 from dbo.Pages where PrivateName = 'PRV_TASKS')
insert into dbo.Pages(ViewName, ParentId, PrivateName) values ('Задания', NULL, 'PRV_TASKS')
go
if not exists (select 1 from dbo.Pages where PrivateName = 'PRV_SECURITY')
insert into dbo.Pages(ViewName, ParentId, PrivateName) values ('Безопасность', NULL, 'PRV_SECURITY')
go
if not exists (select 1 from dbo.Pages where PrivateName = 'PRV_CONFIGURATION_SERVERS')
insert into dbo.Pages(ViewName, ParentId, PrivateName) values ('Сервера', (select Idr from dbo.Pages where PrivateName = 'PRV_CONFIGURATION'), 'PRV_CONFIGURATION_SERVERS')
go
if not exists (select 1 from dbo.Pages where PrivateName = 'PRV_DEVICES_PHONES')
insert into dbo.Pages(ViewName, ParentId, PrivateName) values ('Телефоны', (select Idr from dbo.Pages where PrivateName = 'PRV_DEVICES'), 'PRV_DEVICES_PHONES')
go
if not exists (select 1 from dbo.Pages where PrivateName = 'PRV_TASKS_CURRENT')
insert into dbo.Pages(ViewName, ParentId, PrivateName) values ('Текущие', (select Idr from dbo.Pages where PrivateName = 'PRV_TASKS'), 'PRV_TASKS_CURRENT')
go
if not exists (select 1 from dbo.Pages where PrivateName = 'PRV_TASKS_HISTORY')
insert into dbo.Pages(ViewName, ParentId, PrivateName) values ('История', (select Idr from dbo.Pages where PrivateName = 'PRV_TASKS'), 'PRV_TASKS_HISTORY')
go
if not exists (select 1 from dbo.Pages where PrivateName = 'PRV_SECURITY_USERS')
insert into dbo.Pages(ViewName, ParentId, PrivateName) values ('Пользователи', (select Idr from dbo.Pages where PrivateName = 'PRV_SECURITY'), 'PRV_SECURITY_USERS')
go
if not exists (select 1 from dbo.Pages where PrivateName = 'PRV_SECURITY_ROLES')
insert into dbo.Pages(ViewName, ParentId, PrivateName) values ('Роли', (select Idr from dbo.Pages where PrivateName = 'PRV_SECURITY'), 'PRV_SECURITY_ROLES')
go
if not exists (select 1 from dbo.Pages where PrivateName = 'PRV_SECURITY_LOGS')
insert into dbo.Pages(ViewName, ParentId, PrivateName) values ('Журнал', (select Idr from dbo.Pages where PrivateName = 'PRV_SECURITY'), 'PRV_SECURITY_LOGS')
go

--TaskList
if not exists (select 1 from dbo.TasksList where PrivateName = 'CHANGE_OWNER_PHONE')
insert into dbo.TasksList(Idr, Name, PrivateName) values (1, 'Смена владельца на ТА','CHANGE_OWNER_PHONE')
go

--ActionList
if not exists (select 1 from dbo.ActionList where PrivateName = 'READONLY')
insert into dbo.ActionList(ViewName, PrivateName) values ('Только чтение','READONLY')
go
if not exists (select 1 from dbo.ActionList where PrivateName = 'FULLACCESS')
insert into dbo.ActionList(ViewName, PrivateName) values ('Полный доступ','FULLACCESS')
go
if not exists (select 1 from dbo.ActionList where PrivateName = 'RUNTYPICALTASKONLY')
insert into dbo.ActionList(ViewName, PrivateName) values ('Только выполнение типовых заданий','RUNTYPICALTASKONLY')
go

--ObjectList
if not exists (select 1 from dbo.ObjectList where PrivateName = 'SERVERS')
insert into dbo.ObjectList(ViewName, PrivateName) values ('Сервера','SERVERS')
go
if not exists (select 1 from dbo.ObjectList where PrivateName = 'PHONES')
insert into dbo.ObjectList(ViewName, PrivateName) values ('Телефоны','PHONES')
go

--dbo.rlsSettingList
if not exists (select 1 from  dbo.rlsSettingList where  PrivateName = 'SERVERS_LIST')
insert into dbo.rlsSettingList (Idr, Name, PrivateName) values (1, 'Доступ к серверам','SERVERS_LIST')
go
if not exists (select 1 from  dbo.rlsSettingList where  PrivateName = 'REPORTS_LIST')
insert into dbo.rlsSettingList (Idr, Name, PrivateName) values (2, 'Список отчетов пользователя','REPORTS_LIST')
go
if not exists (select 1 from  dbo.rlsSettingList where  PrivateName = 'TABLECOLUMN_LIST')
insert into dbo.rlsSettingList (Idr, Name, PrivateName) values (3, 'Список полей в таблицах','TABLECOLUMN_LIST')
go
if not exists (select 1 from  dbo.rlsSettingList where  PrivateName = 'TYPICALTASKS_LIST')
insert into dbo.rlsSettingList (Idr, Name, PrivateName) values (4, 'Список типовых заданий','TYPICALTASKS_LIST')
go

--dbo.rlsSettingObjects
if not exists (select 1 from  dbo.rlsSettingObjects so inner join dbo.rlsSettingList sl on sl.Idr = so.SettingListId and sl.PrivateName = 'SERVERS_LIST' where so.PrivateName = 'FULLACCESS')
insert into dbo.rlsSettingObjects (Idr, Name, PrivateName, SettingListId) select 1, 'Все','FULLACCESS', Idr from  dbo.rlsSettingList where  PrivateName = 'SERVERS_LIST'
go
if not exists (select 1 from  dbo.rlsSettingObjects so inner join dbo.rlsSettingList sl on sl.Idr = so.SettingListId and sl.PrivateName = 'SERVERS_LIST' where  so.PrivateName = 'LIST_ACCESS')
insert into dbo.rlsSettingObjects (Idr, Name, PrivateName, SettingListId) select 2, 'Перечень значений','LIST_ACCESS', Idr from  dbo.rlsSettingList where  PrivateName = 'SERVERS_LIST'
go
if not exists (select 1 from  dbo.rlsSettingObjects so inner join dbo.rlsSettingList sl on sl.Idr = so.SettingListId and sl.PrivateName = 'REPORTS_LIST' where so.PrivateName = 'FULLACCESS')
insert into dbo.rlsSettingObjects (Idr, Name, PrivateName, SettingListId) select 3, 'Все','FULLACCESS', Idr from  dbo.rlsSettingList where  PrivateName = 'REPORTS_LIST'
go
if not exists (select 1 from  dbo.rlsSettingObjects so inner join dbo.rlsSettingList sl on sl.Idr = so.SettingListId and sl.PrivateName = 'REPORTS_LIST' where  so.PrivateName = 'LIST_ACCESS')
insert into dbo.rlsSettingObjects (Idr, Name, PrivateName, SettingListId) select 4, 'Перечень отчетов','LIST_ACCESS', Idr from  dbo.rlsSettingList where  PrivateName = 'REPORTS_LIST'
go
if not exists (select 1 from  dbo.rlsSettingObjects so inner join dbo.rlsSettingList sl on sl.Idr = so.SettingListId and sl.PrivateName = 'TABLECOLUMN_LIST' where so.PrivateName = 'FULLACCESS')
insert into dbo.rlsSettingObjects (Idr, Name, PrivateName, SettingListId) select 5, 'Все','FULLACCESS', Idr from  dbo.rlsSettingList where  PrivateName = 'TABLECOLUMN_LIST'
go
if not exists (select 1 from  dbo.rlsSettingObjects so inner join dbo.rlsSettingList sl on sl.Idr = so.SettingListId and sl.PrivateName = 'TABLECOLUMN_LIST' where  so.PrivateName = 'LIST_ACCESS')
insert into dbo.rlsSettingObjects (Idr, Name, PrivateName, SettingListId) select 6, 'Перечень значений','LIST_ACCESS', Idr from  dbo.rlsSettingList where  PrivateName = 'TABLECOLUMN_LIST'
go
if not exists (select 1 from  dbo.rlsSettingObjects so inner join dbo.rlsSettingList sl on sl.Idr = so.SettingListId and sl.PrivateName = 'TYPICALTASKS_LIST' where so.PrivateName = 'FULLACCESS')
insert into dbo.rlsSettingObjects (Idr, Name, PrivateName, SettingListId) select 7, 'Все','FULLACCESS', Idr from  dbo.rlsSettingList where  PrivateName = 'TYPICALTASKS_LIST'
go
if not exists (select 1 from  dbo.rlsSettingObjects so inner join dbo.rlsSettingList sl on sl.Idr = so.SettingListId and sl.PrivateName = 'TYPICALTASKS_LIST' where  so.PrivateName = 'LIST_ACCESS')
insert into dbo.rlsSettingObjects (Idr, Name, PrivateName, SettingListId) select 8, 'Перечень значений','LIST_ACCESS', Idr from  dbo.rlsSettingList where  PrivateName = 'TYPICALTASKS_LIST'
go

--services
if not exists (select 1 from dbo.services where name = 'mentolciscoprovision')
insert into dbo.services (name, conffilename, pathexe, description)
values('mentolciscoprovision', 'mentolciscoprovision.exe.conf', NULL, NULL)
go

--dbo.users
if not exists (select 1 from dbo.users where login = 'admin')
insert into dbo.users ([login], [passw], [lastvisit], [lastvisitdatetime], [hostname], [mustchangepass], [disable], [validatyperiod]) values ('admin', cast('admin' as varbinary), 0, NULL, NULL, 0, 0, '2200-01-01 00:00:00.000')
go
