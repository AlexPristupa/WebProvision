use master
go

if not exists (select 1 from sysdatabases where name = 'mentolprovision')
create database mentolprovision COLLATE Cyrillic_General_CI_AS
go

alter database mentolprovision SET RECOVERY SIMPLE WITH NO_WAIT
go

alter database mentolprovision MODIFY FILE ( NAME = 'mentolprovision', FILEGROWTH = 10%)
go

alter database mentolprovision SET NEW_BROKER  with rollback immediate
go