SET QUOTED_IDENTIFIER ON
GO
--не стираем то что выше, вносим изменения ниже
if exists (select 1
          from sysobjects
          where  id = object_id('dbo.api_get_devices_phones')
          and type in ('P','PC'))
   drop procedure dbo.api_get_devices_phones
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.api_get_devices_phones_selection')
          and type in ('P','PC'))
   drop procedure dbo.api_get_devices_phones_selection
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.api_get_devices_phones_totalcount')
          and type in ('P','PC'))
   drop procedure dbo.api_get_devices_phones_totalcount
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.api_get_security_users_selection')
          and type in ('P','PC'))
   drop procedure dbo.api_get_security_users_selection
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.api_get_security_users_totalcount')
          and type in ('P','PC'))
   drop procedure dbo.api_get_security_users_totalcount
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.api_get_servers_selection')
          and type in ('P','PC'))
   drop procedure dbo.api_get_servers_selection
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.api_get_servers_totalcount')
          and type in ('P','PC'))
   drop procedure dbo.api_get_servers_totalcount
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.api_get_tasks')
          and type in ('P','PC'))
   drop procedure dbo.api_get_tasks
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.api_get_tasks_completed')
          and type in ('P','PC'))
   drop procedure dbo.api_get_tasks_completed
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.api_get_tasks_completed_selection')
          and type in ('P','PC'))
   drop procedure dbo.api_get_tasks_completed_selection
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.api_get_tasks_completed_selectiondate')
          and type in ('P','PC'))
   drop procedure dbo.api_get_tasks_completed_selectiondate
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.api_get_tasks_completed_totalcount')
          and type in ('P','PC'))
   drop procedure dbo.api_get_tasks_completed_totalcount
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.api_get_tasks_completed_totalcountdate')
          and type in ('P','PC'))
   drop procedure dbo.api_get_tasks_completed_totalcountdate
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.api_get_tasks_selection')
          and type in ('P','PC'))
   drop procedure dbo.api_get_tasks_selection
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.api_get_tasks_totalcount')
          and type in ('P','PC'))
   drop procedure dbo.api_get_tasks_totalcount
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Devices') and o.name = 'FK_devices_userscucm')
alter table dbo.Devices
   drop constraint FK_devices_userscucm
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.DevicesToLines') and o.name = 'FK_devicestolines_devices')
alter table dbo.DevicesToLines
   drop constraint FK_devicestolines_devices
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.DevicesToLines') and o.name = 'FK_devicestolines_lines')
alter table dbo.DevicesToLines
   drop constraint FK_devicestolines_lines
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.DevicesToLinesUsers') and o.name = 'FK_devicestolinesusers_devicestolines')
alter table dbo.DevicesToLinesUsers
   drop constraint FK_devicestolinesusers_devicestolines
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.DevicesToLinesUsers') and o.name = 'FK_devicestolinesusers_userscucm')
alter table dbo.DevicesToLinesUsers
   drop constraint FK_devicestolinesusers_userscucm
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Lines') and o.name = 'FK_lines_devices')
alter table dbo.Lines
   drop constraint FK_lines_devices
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Logs') and o.name = 'FK_logs_logstype')
alter table dbo.Logs
   drop constraint FK_logs_logstype
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Nodes') and o.name = 'FK_nodes_servers')
alter table dbo.Nodes
   drop constraint FK_nodes_servers
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.PageToRole') and o.name = 'FK_pagetorole_pages')
alter table dbo.PageToRole
   drop constraint FK_pagetorole_pages
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.PageToRole') and o.name = 'FK_pagetorole_role')
alter table dbo.PageToRole
   drop constraint FK_pagetorole_role
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.RefreshLog') and o.name = 'FK_refreshlog_services')
alter table dbo.RefreshLog
   drop constraint FK_refreshlog_services
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.RoleAction') and o.name = 'FK_roleaction_objectlist')
alter table dbo.RoleAction
   drop constraint FK_roleaction_objectlist
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.RoleAction') and o.name = 'FK_roleaction_role')
alter table dbo.RoleAction
   drop constraint FK_roleaction_role
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Servers') and o.name = 'FK_servers_vendormodels')
alter table dbo.Servers
   drop constraint FK_servers_vendormodels
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Servers') and o.name = 'FK_servers_vendors')
alter table dbo.Servers
   drop constraint FK_servers_vendors
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Tasks') and o.name = 'FK_tasks_devices')
alter table dbo.Tasks
   drop constraint FK_tasks_devices
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Tasks') and o.name = 'FK_tasks_lines')
alter table dbo.Tasks
   drop constraint FK_tasks_lines
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Tasks') and o.name = 'FK_tasks_servers')
alter table dbo.Tasks
   drop constraint FK_tasks_servers
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Tasks') and o.name = 'FK_tasks_server_testserverid')
alter table dbo.Tasks
   drop constraint FK_tasks_server_testserverid
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Tasks') and o.name = 'FK_tasks_task_canceltaskid')
alter table dbo.Tasks
   drop constraint FK_tasks_task_canceltaskid
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Tasks') and o.name = 'FK_tasks_tasklist')
alter table dbo.Tasks
   drop constraint FK_tasks_tasklist
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Tasks') and o.name = 'FK_tasks_users')
alter table dbo.Tasks
   drop constraint FK_tasks_users
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.TasksHistory') and o.name = 'FK_taskshistory_tasks')
alter table dbo.TasksHistory
   drop constraint FK_taskshistory_tasks
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.TasksPool') and o.name = 'FK_taskspool_tasks')
alter table dbo.TasksPool
   drop constraint FK_taskspool_tasks
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.UserToRole') and o.name = 'FK_usertorole_role')
alter table dbo.UserToRole
   drop constraint FK_usertorole_role
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.UserToRole') and o.name = 'FK_usertorole_users')
alter table dbo.UserToRole
   drop constraint FK_usertorole_users
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.VendorModels') and o.name = 'FK_vendormodels_vendors')
alter table dbo.VendorModels
   drop constraint FK_vendormodels_vendors
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Version') and o.name = 'FK_version_servers')
alter table dbo.Version
   drop constraint FK_version_servers
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.vProducts')
            and   type = 'V')
   drop view dbo.vProducts
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.ActionList')
            and   type = 'U')
   drop table dbo.ActionList
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Devices')
            and   type = 'U')
   drop table dbo.Devices
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.DevicesToLines')
            and   type = 'U')
   drop table dbo.DevicesToLines
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.DevicesToLinesUsers')
            and   type = 'U')
   drop table dbo.DevicesToLinesUsers
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Lines')
            and   type = 'U')
   drop table dbo.Lines
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Logs')
            and   type = 'U')
   drop table dbo.Logs
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.LogsType')
            and   type = 'U')
   drop table dbo.LogsType
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Nodes')
            and   type = 'U')
   drop table dbo.Nodes
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.ObjectList')
            and   type = 'U')
   drop table dbo.ObjectList
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.ObjectToAction')
            and   type = 'U')
   drop table dbo.ObjectToAction
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.PageToRole')
            and   type = 'U')
   drop table dbo.PageToRole
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Pages')
            and   type = 'U')
   drop table dbo.Pages
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.RefreshLog')
            and   type = 'U')
   drop table dbo.RefreshLog
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Role')
            and   type = 'U')
   drop table dbo.Role
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.RoleAction')
            and   type = 'U')
   drop table dbo.RoleAction
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Servers')
            and   type = 'U')
   drop table dbo.Servers
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Services')
            and   type = 'U')
   drop table dbo.Services
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Tasks')
            and   type = 'U')
   drop table dbo.Tasks
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.TasksHistory')
            and   type = 'U')
   drop table dbo.TasksHistory
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.TasksList')
            and   type = 'U')
   drop table dbo.TasksList
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.TasksPool')
            and   type = 'U')
   drop table dbo.TasksPool
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.UserToRole')
            and   type = 'U')
   drop table dbo.UserToRole
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Users')
            and   type = 'U')
   drop table dbo.Users
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.VendorModels')
            and   type = 'U')
   drop table dbo.VendorModels
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Vendors')
            and   type = 'U')
   drop table dbo.Vendors
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Version')
            and   type = 'U')
   drop table dbo.Version
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.rlsLinkRoleToObject')
            and   type = 'U')
   drop table dbo.rlsLinkRoleToObject
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.rlsPermissionToTasks')
            and   type = 'U')
   drop table dbo.rlsPermissionToTasks
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.rlsSettingList')
            and   type = 'U')
   drop table dbo.rlsSettingList
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.rlsSettingObjects')
            and   type = 'U')
   drop table dbo.rlsSettingObjects
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.rlsTasks')
            and   type = 'U')
   drop table dbo.rlsTasks
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.usersCUCM')
            and   type = 'U')
   drop table dbo.usersCUCM
go

/*==============================================================*/
/* Table: ActionList                                            */
/*==============================================================*/
create table dbo.ActionList (
   Idr                  integer              identity,
   ViewName             varchar(256)         not null,
   PrivateName          varchar(100)         not null,
   constraint PK_ACTIONLIST primary key (Idr)
)
go

/*==============================================================*/
/* Table: Devices                                               */
/*==============================================================*/
create table dbo.Devices (
   Idr                  int                  identity,
   Name                 varchar(100)         not null,
   Description          varchar(512)         null,
   IPAddress            varchar(16)          null,
   IsDeleted            bit                  not null,
   OwnerUserId          int                  null,
   constraint PK_DEVICES primary key (Idr)
)
go

/*==============================================================*/
/* Table: DevicesToLines                                        */
/*==============================================================*/
create table dbo.DevicesToLines (
   Idr                  Int                  identity,
   DevicesId            Int                  not null,
   LinesId              int                  not null,
   constraint PK_DEVICESTOLINES primary key (Idr)
)
go

/*==============================================================*/
/* Table: DevicesToLinesUsers                                   */
/*==============================================================*/
create table dbo.DevicesToLinesUsers (
   UserId               int                  not null,
   DevicesToLinesId     int                  not null,
   constraint PK_DEVICESTOLINESUSERS primary key (UserId, DevicesToLinesId)
)
go

/*==============================================================*/
/* Table: Lines                                                 */
/*==============================================================*/
create table dbo.Lines (
   Idr                  int                  identity,
   PhoneNumber          varchar(16)          null,
   Description          varchar(512)         null,
   AlertingName         varchar(100)         null,
   ASCIIAlertingName    varchar(100)         null,
   DisplayCallerID      varchar(100)         null,
   ASCIIDisplayCallerID varchar(100)         null,
   UserAssociatedLine   varchar(100)         null,
   DeviceId             int                  null,
   LineUUID             uniqueidentifier     null,
   constraint PK_LINES primary key (Idr)
)
go

/*==============================================================*/
/* Table: Logs                                                  */
/*==============================================================*/
create table dbo.Logs (
   Idr                  int                  identity,
   TypeId               int                  not null,
   UserName             varchar(250)         null,
   DateRecord           datetime             null,
   Action               varchar(256)         null,
   Description          varchar(1500)        null,
   IP                   varchar(50)          null,
   ProductId            varchar(50)          null,
   LevelId              varchar(50)          null,
   ObjectId             int                  null,
   PropertyId           smallint             null,
   constraint PK_LOGS primary key (Idr)
)
go

/*==============================================================*/
/* Table: LogsType                                              */
/*==============================================================*/
create table dbo.LogsType (
   Idr                  integer              identity,
   Name                 varchar(150)         null,
   PrivateName          varchar(128)         null,
   constraint PK_LOGSTYPE primary key (Idr)
)
go

/*==============================================================*/
/* Table: Nodes                                                 */
/*==============================================================*/
create table dbo.Nodes (
   Idr                  int                  identity,
   FQDN                 varchar(100)         null,
   IpAddress            varchar(64)          null,
   ServersId            int                  null,
   Priority             int                  null,
   constraint PK_NODES primary key (Idr)
)
go

/*==============================================================*/
/* Table: ObjectList                                            */
/*==============================================================*/
create table dbo.ObjectList (
   Idr                  integer              identity,
   ViewName             varchar(256)         not null,
   PrivateName          varchar(100)         not null,
   constraint PK_OBJECTLIST primary key (Idr)
)
go

/*==============================================================*/
/* Table: ObjectToAction                                        */
/*==============================================================*/
create table dbo.ObjectToAction (
   ActionId             integer              not null,
   ObjectId             integer              not null,
   Mask                 integer              not null,
   constraint PK_OBJECTTOACTION primary key (ActionId, ObjectId)
)
go

/*==============================================================*/
/* Table: PageToRole                                            */
/*==============================================================*/
create table dbo.PageToRole (
   Idr                  integer              identity,
   PageId               integer              not null,
   RoleId               integer              not null,
   constraint PK_PAGETOROLE primary key (Idr)
)
go

/*==============================================================*/
/* Table: Pages                                                 */
/*==============================================================*/
create table dbo.Pages (
   Idr                  integer              identity,
   ViewName             varchar(256)         not null,
   ParentId             integer              null,
   PrivateName          varchar(256)         not null,
   action               varchar(512)         null,
   constraint PK_PAGES primary key (Idr)
)
go

/*==============================================================*/
/* Table: RefreshLog                                            */
/*==============================================================*/
create table dbo.RefreshLog (
   idr                  int                  identity,
   Info                 varchar(2048)        null,
   UploadDate           datetime             null,
   ServicesId           int                  not null,
   SitesIds             varchar(1000)        null,
   Mode                 int                  not null default 0,
   constraint PK_REFRESHLOG primary key (idr)
)
go

/*==============================================================*/
/* Table: Role                                                  */
/*==============================================================*/
create table dbo.Role (
   Idr                  integer              identity,
   Name                 varchar(15)          not null,
   Description          varchar(256)         null,
   constraint PK_ROLE primary key (Idr)
)
go

/*==============================================================*/
/* Table: RoleAction                                            */
/*==============================================================*/
create table dbo.RoleAction (
   Idr                  integer              identity,
   RoleId               integer              not null,
   ObjectId             integer              not null,
   ActionId             integer              null,
   constraint PK_ROLEACTION primary key (Idr)
)
go

/*==============================================================*/
/* Table: Servers                                               */
/*==============================================================*/
create table dbo.Servers (
   Idr                  int                  identity,
   FQDN                 varchar(100)         not null,
   IpAddress            varchar(64)          null,
   Port                 int                  null,
   Mode                 varchar(6)           null,
   Enable               bit                  null,
   TestBench            bit                  null,
   DefStartTime         datetime             null,
   Timezone             int                  null,
   Description          varchar(512)         null,
   Login                varchar(100)         null,
   Password             varchar(100)         null,
   VendorId             int                  null,
   ModelId              int                  null,
   IsDeleted            bit                  null,
   constraint PK_SERVERS primary key (Idr)
)
go

/*==============================================================*/
/* Table: Services                                              */
/*==============================================================*/
create table dbo.Services (
   id                   int                  identity,
   name                 varchar(100)         not null,
   conffilename         varchar(100)         not null,
   pathexe              varchar(100)         null,
   description          varchar(250)         null,
   constraint PK_SERVICES primary key (id)
)
go

/*==============================================================*/
/* Table: Tasks                                                 */
/*==============================================================*/
create table dbo.Tasks (
   Idr                  int                  identity,
   TaskListId           int                  not null,
   LinesId              int                  null,
   Description          varchar(512)         null,
   JSONOld              varchar(Max)         null,
   JSONNew              varchar(Max)         not null,
   ServerId             int                  null,
   TestServerId         int                  null,
   UsersId              int                  not null,
   DevicesId            int                  null,
   CancelTaskId         int                  null,
   constraint PK_TASKS primary key (Idr)
)
go

/*==============================================================*/
/* Table: TasksHistory                                          */
/*==============================================================*/
create table dbo.TasksHistory (
   Idr                  int                  identity,
   DateCreated          datetime             not null,
   DateRun              datetime             not null,
   TaskId               int                  not null,
   Status               varchar(512)         not null,
   StatusDesc           varchar(512)         null,
   DateEnd              datetime             not null,
   constraint PK_TASKSHISTORY primary key (Idr)
)
go

/*==============================================================*/
/* Table: TasksList                                             */
/*==============================================================*/
create table dbo.TasksList (
   Idr                  integer              not null,
   Name                 varchar(256)         null,
   PrivateName          varchar(150)         not null,
   constraint PK_TASKSLIST primary key (Idr)
)
go

/*==============================================================*/
/* Table: TasksPool                                             */
/*==============================================================*/
create table dbo.TasksPool (
   Idr                  int                  identity,
   DateCreated          datetime             not null,
   DateRun              datetime             null,
   TaskId               int                  not null,
   constraint PK_TASKSPOOL primary key (Idr)
)
go

/*==============================================================*/
/* Table: UserToRole                                            */
/*==============================================================*/
create table dbo.UserToRole (
   RoleId               integer              not null,
   UserId               integer              not null,
   constraint PK_USERTOROLE primary key (RoleId, UserId)
)
go

/*==============================================================*/
/* Table: Users                                                 */
/*==============================================================*/
create table dbo.Users (
   Idr                  int                  identity,
   Login                varchar(256)         not null,
   Password             varchar(256)         not null,
   DisplayName          varchar(256)         null,
   Email                varchar(256)         null,
   SID                  varchar(256)         null,
   Provider             varchar(50)          null,
   AccessToken          varchar(512)         null,
   Position             varchar(256)         null,
   IsDeleted            bit                  null,
   constraint PK_USERS primary key (Idr)
)
go

/*==============================================================*/
/* Table: VendorModels                                          */
/*==============================================================*/
create table dbo.VendorModels (
   Idr                  int                  not null,
   VendorId             int                  not null,
   Name                 varchar(150)         not null,
   Description          varchar(512)         null,
   constraint PK_VENDORMODELS primary key (Idr)
)
go

/*==============================================================*/
/* Table: Vendors                                               */
/*==============================================================*/
create table dbo.Vendors (
   Idr                  int                  not null,
   Name                 varchar(150)         not null,
   Description          varchar(512)         null,
   constraint PK_VENDORS primary key (Idr)
)
go

/*==============================================================*/
/* Table: Version                                               */
/*==============================================================*/
create table dbo.Version (
   Idr                  int                  not null,
   Version              varchar(100)         not null,
   Type                 varchar(100)         null,
   Description          varchar(250)         null,
   DateRecord           datetime             null,
   LastRecord           bit                  null,
   ServerId             int                  null,
   NodeId               int                  null,
   constraint PK_VERSION primary key (Idr)
)
go

/*==============================================================*/
/* Table: rlsLinkRoleToObject                                   */
/*==============================================================*/
create table dbo.rlsLinkRoleToObject (
   Idr                  integer              identity,
   RoleId               integer              null,
   SettingObjectId      integer              null,
   constraint PK_RLSLINKROLETOOBJECT primary key (Idr)
)
go

/*==============================================================*/
/* Table: rlsPermissionToTasks                                  */
/*==============================================================*/
create table dbo.rlsPermissionToTasks (
   Idr                  integer              identity,
   LinkId               integer              not null,
   TaskId               integer              not null,
   constraint PK_RLSPERMISSIONTOTASKS primary key (Idr)
)
go

/*==============================================================*/
/* Table: rlsSettingList                                        */
/*==============================================================*/
create table dbo.rlsSettingList (
   Idr                  integer              not null,
   Name                 varchar(64)          null,
   PrivateName          varchar(32)          not null,
   constraint PK_RLSSETTINGLIST primary key (Idr)
)
go

/*==============================================================*/
/* Table: rlsSettingObjects                                     */
/*==============================================================*/
create table dbo.rlsSettingObjects (
   Idr                  integer              not null,
   Name                 varchar(128)         null,
   PrivateName          varchar(32)          not null,
   SettingListId        integer              null,
   constraint PK_RLSSETTINGOBJECTS primary key (Idr)
)
go

/*==============================================================*/
/* Table: rlsTasks                                              */
/*==============================================================*/
create table dbo.rlsTasks (
   RoleId               integer              not null,
   TaskId               integer              not null
)
go

/*==============================================================*/
/* Table: usersCUCM                                             */
/*==============================================================*/
create table dbo.usersCUCM (
   idr                  int                  identity,
   UserId               varchar(100)         null,
   constraint PK_USERSCUCM primary key (idr)
)
go

/*==============================================================*/
/* View: vProducts                                              */
/*==============================================================*/
create view dbo.vProducts as
 select 
 cast(1 as int) as Idr,
 cast('MON' as varchar(32)) as Product,
 cast('Автоматизированная система мониторинга «Mentol Pro»' as varchar(512)) as Name,
 cast('5' as varchar(32)) as Version,
 cast('ТУ 4250-001-05223940-2017' as varchar(128)) as DNumber, 
 cast('info@inlinepro.ru' as varchar(1024)) as Contacts,
 cast('' as varchar(1024)) as AddInfo
 union
 select 
 cast(2 as int) as Idr,
 cast('TAR' as varchar(32)) as Product,
 cast('Автоматизированная система расчета «Mentol Pro»' as varchar(512)) as Name,
 cast('5' as varchar(32)) as Version,
 cast('ТУ 4251-001-05223940-2017' as varchar(128)) as DNumber, 
 cast('info@inlinepro.ru' as varchar(1024)) as Contacts,
 cast('' as varchar(1024)) as AddInfo
go

alter table dbo.Devices
   add constraint FK_devices_userscucm foreign key (OwnerUserId)
      references dbo.usersCUCM (idr)
go

alter table dbo.DevicesToLines
   add constraint FK_devicestolines_devices foreign key (DevicesId)
      references dbo.Devices (Idr)
go

alter table dbo.DevicesToLines
   add constraint FK_devicestolines_lines foreign key (LinesId)
      references dbo.Lines (Idr)
go

alter table dbo.DevicesToLinesUsers
   add constraint FK_devicestolinesusers_devicestolines foreign key (DevicesToLinesId)
      references dbo.DevicesToLines (Idr)
go

alter table dbo.DevicesToLinesUsers
   add constraint FK_devicestolinesusers_userscucm foreign key (UserId)
      references dbo.usersCUCM (idr)
go

alter table dbo.Lines
   add constraint FK_lines_devices foreign key (DeviceId)
      references dbo.Devices (Idr)
go

alter table dbo.Logs
   add constraint FK_logs_logstype foreign key (TypeId)
      references dbo.LogsType (Idr)
go

alter table dbo.Nodes
   add constraint FK_nodes_servers foreign key (ServersId)
      references dbo.Servers (Idr)
go

alter table dbo.PageToRole
   add constraint FK_pagetorole_pages foreign key (PageId)
      references dbo.Pages (Idr)
go

alter table dbo.PageToRole
   add constraint FK_pagetorole_role foreign key (RoleId)
      references dbo.Role (Idr)
go

alter table dbo.RefreshLog
   add constraint FK_refreshlog_services foreign key (ServicesId)
      references dbo.Services (id)
go

alter table dbo.RoleAction
   add constraint FK_roleaction_objectlist foreign key (ObjectId)
      references dbo.ObjectList (Idr)
go

alter table dbo.RoleAction
   add constraint FK_roleaction_role foreign key (RoleId)
      references dbo.Role (Idr)
go

alter table dbo.Servers
   add constraint FK_servers_vendormodels foreign key (ModelId)
      references dbo.VendorModels (Idr)
go

alter table dbo.Servers
   add constraint FK_servers_vendors foreign key (VendorId)
      references dbo.Vendors (Idr)
go

alter table dbo.Tasks
   add constraint FK_tasks_devices foreign key (DevicesId)
      references dbo.Devices (Idr)
go

alter table dbo.Tasks
   add constraint FK_tasks_lines foreign key (LinesId)
      references dbo.Lines (Idr)
go

alter table dbo.Tasks
   add constraint FK_tasks_servers foreign key (ServerId)
      references dbo.Servers (Idr)
go

alter table dbo.Tasks
   add constraint FK_tasks_server_testserverid foreign key (TestServerId)
      references dbo.Servers (Idr)
go

alter table dbo.Tasks
   add constraint FK_tasks_task_canceltaskid foreign key (CancelTaskId)
      references dbo.Tasks (Idr)
go

alter table dbo.Tasks
   add constraint FK_tasks_tasklist foreign key (TaskListId)
      references dbo.TasksList (Idr)
go

alter table dbo.Tasks
   add constraint FK_tasks_users foreign key (UsersId)
      references dbo.Users (Idr)
go

alter table dbo.TasksHistory
   add constraint FK_taskshistory_tasks foreign key (TaskId)
      references dbo.Tasks (Idr)
go

alter table dbo.TasksPool
   add constraint FK_taskspool_tasks foreign key (TaskId)
      references dbo.Tasks (Idr)
go

alter table dbo.UserToRole
   add constraint FK_usertorole_role foreign key (RoleId)
      references dbo.Role (Idr)
go

alter table dbo.UserToRole
   add constraint FK_usertorole_users foreign key (UserId)
      references dbo.Users (Idr)
go

alter table dbo.VendorModels
   add constraint FK_vendormodels_vendors foreign key (VendorId)
      references dbo.Vendors (Idr)
go

alter table dbo.Version
   add constraint FK_version_servers foreign key (ServerId)
      references dbo.Servers (Idr)
         on delete cascade
go


create procedure dbo.api_get_devices_phones 
(  @__p_1 int                           --| Offset
  ,@__p_2 int                           --| Limit
)
as
/*
exec dbo.api_get_devices_phones
     @__p_1 = 0                         --| Offset
    ,@__p_2 = 10                        --| Limit
*/

begin
  set nocount on;

  declare 
    @query nvarchar(max) = '';

  set @query = '
    SELECT 
         d.Idr                    As phoneId
        ,d.Name                   AS phoneName
        ,d.Description            AS phoneDescription
        ,d.IPAddress              AS phoneIpAddress
        ,COALESCE(STUFF
        (
            (
                SELECT '', '' + l.PhoneNumber
                FROM dbo.Lines AS l 
                WHERE l.DeviceId = d.Idr
                ORDER BY l.PhoneNumber
                FOR XML PATH('''')
            ), 1, 2, N''''
            ), N'''')                as linePhoneNumber
    FROM dbo.Devices AS d
          ORDER BY phoneId ASC
          OFFSET '+cast(@__p_1 as varchar)+' ROWS
          FETCH NEXT '+cast(@__p_2 as varchar)+' ROWS ONLY
            ';
  /*print @query;*/
  exec sp_executesql @query;
end
go


create procedure dbo.api_get_devices_phones_selection 
(    @__filter_0  nvarchar(max)                       --| Search
    ,@__filter_1  varchar(128)                        --| phoneId|phoneName|phoneDescription| etc...
    ,@__p_1 int                                       --| Offset
    ,@__p_2 int                                       --| Limit
    ,@__sort_direction_p_3 nvarchar(5) = 'asc'        --| Sort direction asc-desc 
)    
AS
/*
exec api_get_devices_phones_selection 
     @__filter_0 = '7929'
    ,@__filter_1 = ''
    ,@__p_1 = 0
    ,@__p_2 = 100
    ,@__sort_direction_p_3 = 'asc'
*/
BEGIN
    set nocount on;
    set @__sort_direction_p_3 = ISNULL(@__sort_direction_p_3,'')
    declare @query nvarchar(max) = '';
    set @query = '
        ;with cte_res 
        as
        (
            SELECT distinct
                 d.Idr                                AS phoneId
                ,d.Name                               AS phoneName
                ,d.Description                        AS phoneDescription
                ,d.IpAddress                          AS phoneIpAddress
                ,COALESCE(STUFF
                (
                    (
                        SELECT '', '' + l.PhoneNumber
                        FROM dbo.Lines AS l 
                        WHERE l.DeviceId = d.Idr
                        ORDER BY l.PhoneNumber
                        FOR XML PATH('''')
                    ), 1, 2, N''''
                    ), N'''')                          as linePhoneNumber
            FROM
            (
                SELECT distinct 
                     d.Idr
                    ,d.Name 
                    ,d.Description
                    ,d.IpAddress
                FROM dbo.Devices d
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'linePhoneNumber', 'JOIN dbo.Lines lt ON lt.deviceId = d.Idr and lt.PhoneNumber like ''%'+@__filter_0+'%'' ', '')+'
                WHERE 1=1 
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'phoneId',                  ' and d.Idr like ''%'+@__filter_0+'%'' ', '')+' 
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'phoneName',                ' and d.Name like ''%'+@__filter_0+'%'' ', '')+' 
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'phoneDescription',         ' and d.Description like ''%'+@__filter_0+'%'' ', '')+' 
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'phoneIpAddress',           ' and d.IpAddress like ''%'+@__filter_0+'%'' ', '')+' 
            )  AS d
        )
        select 
             c.phoneId
            ,c.phoneName
            ,c.phoneDescription
            ,c.phoneIpAddress
            ,c.linePhoneNumber
        from cte_res as c
        where 1=1 
        /*добавляем поиск по всем полям если не задано поле для поиска */
        '+case when coalesce(@__filter_1,'')='' and coalesce(@__filter_0,'')!='' then ' and (
               (c.phoneId                    like ''%'+@__filter_0+'%'')
            or (c.phoneName                    like ''%'+@__filter_0+'%'')
            or (c.phoneDescription            like ''%'+@__filter_0+'%'')
            or (c.phoneIpAddress            like ''%'+@__filter_0+'%'')
            or (c.linePhoneNumber            like ''%'+@__filter_0+'%'') )' else '' end +'
    ORDER BY 1 '+@__sort_direction_p_3+', 2 '+@__sort_direction_p_3+', 3 '+@__sort_direction_p_3+', 4 '+@__sort_direction_p_3+', 5 '+@__sort_direction_p_3+'
    OFFSET '+cast(@__p_1 as varchar)+' ROWS
    FETCH NEXT '+cast(@__p_2 as varchar)+' ROWS ONLY';
    /*print @query;*/
    exec sp_executesql @query;
END
go


create procedure dbo.api_get_devices_phones_totalcount 
(    @__filter_0  nvarchar(max)                       --| Search
    ,@__filter_1  varchar(128)                        --| phoneId|phoneName|phoneDescription| etc...
)    
AS
/*
exec api_get_devices_phones_totalcount
     @__filter_0 = '79'
    ,@__filter_1 = ''
*/
BEGIN
    set nocount on;
    declare @query nvarchar(max) = '';
    set @query = '
        ;with cte_res 
        as
        (
            SELECT distinct
                 d.Idr                                AS phoneId
                ,d.Name                               AS phoneName
                ,d.Description                        AS phoneDescription
                ,d.IpAddress                          AS phoneIpAddress
                ,COALESCE(STUFF
                (
                    (
                        SELECT '', '' + l.PhoneNumber
                        FROM dbo.Lines AS l 
                        WHERE l.DeviceId = d.Idr
                        ORDER BY l.PhoneNumber
                        FOR XML PATH('''')
                    ), 1, 2, N''''
                    ), N'''')                          as linePhoneNumber
            FROM
            (
                SELECT distinct 
                     d.Idr
                    ,d.Name 
                    ,d.Description
                    ,d.IpAddress
                FROM dbo.Devices d
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'linePhoneNumber', 'JOIN dbo.Lines lt ON lt.deviceId = d.Idr and lt.PhoneNumber like ''%'+@__filter_0+'%'' ', '')+'
                WHERE 1=1 
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'phoneId',                  ' and d.Idr like ''%'+@__filter_0+'%'' ', '')+' 
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'phoneName',                ' and d.Name like ''%'+@__filter_0+'%'' ', '')+' 
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'phoneDescription',         ' and d.Description like ''%'+@__filter_0+'%'' ', '')+' 
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'phoneIpAddress',           ' and d.IpAddress like ''%'+@__filter_0+'%'' ', '')+' 
            )  AS d
        )
        select 
            count(*)
        from cte_res as c
        where 1=1 
        /*добавляем поиск по всем полям если не задано поле для поиска */
        '+case when coalesce(@__filter_1,'')='' and coalesce(@__filter_0,'')!='' then ' and (
               (c.phoneId                   like ''%'+@__filter_0+'%'')
            or (c.phoneName                 like ''%'+@__filter_0+'%'')
            or (c.phoneDescription          like ''%'+@__filter_0+'%'')
            or (c.phoneIpAddress            like ''%'+@__filter_0+'%'')
            or (c.linePhoneNumber           like ''%'+@__filter_0+'%'') )' else '' end +'
        ';
    /*print @query;*/
    exec sp_executesql @query;
END
go


create procedure dbo.api_get_security_users_selection 
     @__filter_0 nvarchar(max)                                     --| Search
    ,@__filter_1 nvarchar(max)                                     --| userLogin|userDisplayName|userEmail|userSID|userProvider|userPosition
    ,@__p_1 int                                                    --| Offset
    ,@__p_2 int                                                    --| Limit
    ,@__sort_direction_p_3 nvarchar(10) = 'asc'                    --| Sort direction asc-desc 
as
/*
exec api_get_security_users_selection
     @__filter_0 = 'про'
    ,@__filter_1 = ''
    ,@__p_1 = 0    
    ,@__p_2 = 200
    ,@__sort_direction_p_3 = 'asc'
*/
begin
    set nocount on;

    declare 
        @query nvarchar(max) = '';

    set @__sort_direction_p_3 = ISNULL(@__sort_direction_p_3,'');
    set @query = ';with cte_res
        as
        (
            SELECT
               uf.userId
              ,uf.userLogin
              ,uf.userDisplayName
              ,uf.userEmail
              ,uf.userSID
              ,uf.userProvider
              ,uf.userPosition
              ,uf.userIsDeleted
             , COALESCE(STUFF
                (
                    (
                    SELECT '', '' + cast(utr.RoleId as varchar)
                    FROM dbo.UserToRole AS utr 
                    WHERE utr.UserId = uf.userId
                    ORDER BY utr.RoleId
                    FOR XML PATH('''')
                    ), 1, 2, N''''
                    ), N'''')                  as roleId
            FROM (
                SELECT
                   u.idr                        AS userId
                  ,u.Login                      AS userLogin
                  ,u.DisplayName                AS userDisplayName
                  ,u.Email                      AS userEmail
                  ,u.SID                        AS userSID
                  ,u.Provider                   AS userProvider
                  ,u.Position                   AS userPosition
                  ,u.IsDeleted                  AS userIsDeleted
                FROM dbo.Users u                            
                where u.IsDeleted = 0
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'userLogin',              ' and u.Login like ''%'+@__filter_0+'%'' ', '')+' 
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'userDisplayName',        ' and u.DisplayName like ''%'+@__filter_0+'%'' ', '')+' 
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'userEmail',              ' and u.Email like ''%'+@__filter_0+'%'' ', '')+' 
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'userSID',                ' and u.SID like ''%'+@__filter_0+'%'' ', '')+' 
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'userProvider',           ' and u.Provider like ''%'+@__filter_0+'%'' ', '')+' 
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'userPosition',           ' and u.Position like ''%'+@__filter_0+'%'' ', '')+' 
            ) AS uf
        )
        select 
             c.userId
            ,c.userLogin
            ,c.userDisplayName
            ,c.userEmail
            ,c.userSID
            ,c.userProvider
            ,c.userPosition
            ,c.userIsDeleted
            ,c.roleId
        from cte_res as c
        where 1=1 
          '+case when coalesce(@__filter_1,'')='' and coalesce(@__filter_0,'')!='' then ' and (
               (c.userLogin       like ''%'+@__filter_0+'%'')
            or (c.userDisplayName like ''%'+@__filter_0+'%'')
            or (c.userEmail       like ''%'+@__filter_0+'%'')
            or (c.userSID         like ''%'+@__filter_0+'%'')
            or (c.userProvider    like ''%'+@__filter_0+'%'')
            or (c.userPosition    like ''%'+@__filter_0+'%'') )' else '' end +'
        ORDER BY 1 '+@__sort_direction_p_3+', 2 '+@__sort_direction_p_3+', 3 '+@__sort_direction_p_3+', 4 '+@__sort_direction_p_3+', 5 '+@__sort_direction_p_3+', 6 '+@__sort_direction_p_3+', 8 '+@__sort_direction_p_3+'
        OFFSET '+cast(@__p_1 as varchar)+' ROWS
        FETCH NEXT '+cast(@__p_2 as varchar)+' ROWS ONLY
    ';
    /*print @query;*/
    exec sp_executesql @query;

end
go


create procedure dbo.api_get_security_users_totalcount 
     @__filter_0 nvarchar(max)                                     --| Search
    ,@__filter_1 nvarchar(max)                                     --| userLogin|userDisplayName|userEmail|userSID|userProvider|userPosition
as
/*
exec api_get_security_users_totalcount
     @__filter_0 = 'про'
    ,@__filter_1 = ''
*/
begin
    set nocount on;

    declare 
        @query nvarchar(max) = '';

    set @query = ';with cte_res
        as
        (
            SELECT 
               uf.userId
              ,uf.userLogin
              ,uf.userDisplayName
              ,uf.userEmail
              ,uf.userSID
              ,uf.userProvider
              ,uf.userPosition
              ,uf.userIsDeleted
            FROM (
                SELECT
                   u.idr                        AS userId
                  ,u.Login                      AS userLogin
                  ,u.DisplayName                AS userDisplayName
                  ,u.Email                      AS userEmail
                  ,u.SID                        AS userSID
                  ,u.Provider                   AS userProvider
                  ,u.Position                   AS userPosition
                  ,u.IsDeleted                  AS userIsDeleted
                FROM dbo.Users u                            
                where u.IsDeleted = 0
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'userLogin',              ' and u.Login like ''%'+@__filter_0+'%'' ', '')+' 
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'userDisplayName',        ' and u.DisplayName like ''%'+@__filter_0+'%'' ', '')+' 
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'userEmail',              ' and u.Email like ''%'+@__filter_0+'%'' ', '')+' 
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'userSID',                ' and u.SID like ''%'+@__filter_0+'%'' ', '')+' 
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'userProvider',           ' and u.Provider like ''%'+@__filter_0+'%'' ', '')+' 
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'userPosition',           ' and u.Position like ''%'+@__filter_0+'%'' ', '')+' 
            ) AS uf
        )
        select 
            count(*) as count
        from cte_res as c
        where 1=1 
          '+case when coalesce(@__filter_1,'')='' and coalesce(@__filter_0,'')!='' then ' and (
               (c.userLogin       like ''%'+@__filter_0+'%'')
            or (c.userDisplayName like ''%'+@__filter_0+'%'')
            or (c.userEmail       like ''%'+@__filter_0+'%'')
            or (c.userSID         like ''%'+@__filter_0+'%'')
            or (c.userProvider    like ''%'+@__filter_0+'%'')
            or (c.userPosition    like ''%'+@__filter_0+'%'') )' else '' end +'
    ';
    /*print @query;*/
    exec sp_executesql @query;

end
go


create procedure dbo.api_get_servers_selection 
     @__filter_0 nvarchar(max)                                     --| Search
    ,@__filter_1 nvarchar(max)                                     --| serverId|serverFQDN|serverIpAddress|serverPort|serverLogin|serverDescription|serverVendorName
    ,@__p_1 int                                                    --| Offset
    ,@__p_2 int                                                    --| Limit
    ,@__sort_direction_p_3 nvarchar(10) = 'asc'                    --| Sort direction asc-desc 
as
/*
exec dbo.api_get_servers_selection
     @__filter_0 = 'cis'
    ,@__filter_1 = 'serverVendorName'
    ,@__p_1 = 0    
    ,@__p_2 = 200
    ,@__sort_direction_p_3 = 'asc'
*/
begin
    set nocount on;

    declare 
        @query nvarchar(max) = '';

    set @__sort_direction_p_3 = ISNULL(@__sort_direction_p_3,'');
    set @query = ';with cte_res
        as
        (
            SELECT
              t.serverId
             ,t.serverFQDN
             ,t.serverIpAddress
             ,t.serverPort
             ,t.serverLogin
             ,t.serverDescription
             ,t.serverVendorModelId
             ,t.serverVendorName
             ,t.serverIsEnabled
             ,t.serverIsTest
             , COALESCE(STUFF
                (
                    (
                    SELECT '', '' + cast(n.Idr as varchar)
                    FROM dbo.Nodes AS n
                    WHERE n.ServersId = t.serverId
                    ORDER BY n.Idr
                    FOR XML PATH('''')
                    ), 1, 2, N''''
                ), N'''')                  as nodeId
            FROM (
                SELECT 
                     s.Idr                 AS serverId
                    ,s.FQDN                AS serverFQDN
                    ,s.IpAddress           AS serverIpAddress
                    ,s.Port                AS serverPort
                    ,s.Login               AS serverLogin
                    ,s.Description         AS serverDescription
                    ,vm.Idr                AS serverVendorModelId
                    ,vs.Name               AS serverVendorName
                    ,s.Enable              AS serverIsEnabled
                    ,s.TestBench           AS serverIsTest
                FROM dbo.Servers AS s
                   left JOIN dbo.VendorModels AS vm ON s.ModelId = vm.Idr
                left JOIN dbo.Vendors      AS vs ON vm.VendorId = vs.Idr
                WHERE s.IsDeleted = 0
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'serverId',          ' and s.Idr like ''%'+@__filter_0+'%'' ', '')+' 
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'serverFQDN',        ' and s.FQDN like ''%'+@__filter_0+'%'' ', '')+' 
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'serverIpAddress',   ' and s.IpAddress like ''%'+@__filter_0+'%'' ', '')+' 
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'serverPort',        ' and s.Port like ''%'+@__filter_0+'%'' ', '')+' 
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'serverLogin',       ' and s.Login like ''%'+@__filter_0+'%'' ', '')+' 
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'serverDescription', ' and s.Description like ''%'+@__filter_0+'%'' ', '')+' 
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'serverVendorName',  ' and vs.Name like ''%'+@__filter_0+'%'' ', '')+' 
            ) AS t
        )
        select 
              c.serverId
             ,c.serverFQDN
             ,c.serverIpAddress
             ,c.serverPort
             ,c.serverLogin
             ,c.serverDescription
             ,c.serverVendorModelId
             ,c.serverVendorName
             ,c.serverIsEnabled
             ,c.serverIsTest
             ,c.nodeId
        from cte_res as c
        where 1=1 
          '+case when coalesce(@__filter_1,'')='' and coalesce(@__filter_0,'')!='' then ' and (
               (c.serverId          like ''%'+@__filter_0+'%'')
            or (c.serverFQDN        like ''%'+@__filter_0+'%'')
            or (c.serverIpAddress   like ''%'+@__filter_0+'%'')
            or (c.serverPort        like ''%'+@__filter_0+'%'')
            or (c.serverLogin       like ''%'+@__filter_0+'%'')
            or (c.serverDescription like ''%'+@__filter_0+'%'')
            or (c.serverVendorName  like ''%'+@__filter_0+'%'') )' else '' end +'
        ORDER BY 1 '+@__sort_direction_p_3+', 2 '+@__sort_direction_p_3+', 3 '+@__sort_direction_p_3+', 4 '+@__sort_direction_p_3+', 5 '+@__sort_direction_p_3+', 6 '+@__sort_direction_p_3+', 7 '+@__sort_direction_p_3+', 8 '+@__sort_direction_p_3+'
        OFFSET '+cast(@__p_1 as varchar)+' ROWS
        FETCH NEXT '+cast(@__p_2 as varchar)+' ROWS ONLY
    ';
    --print @query;
    exec sp_executesql @query;

end
go


create procedure dbo.api_get_servers_totalcount
     @__filter_0 nvarchar(max)                                     --| Search
    ,@__filter_1 nvarchar(max)                                     --| serverId|serverFQDN|serverIpAddress|serverPort|serverLogin|serverDescription|serverVendorName
as
/*
exec dbo.api_get_servers_totalcount
     @__filter_0 = 'cis'
    ,@__filter_1 = 'serverVendorName'
*/
begin
    set nocount on;

    declare 
        @query nvarchar(max) = '';

    set @query = ';with cte_res
        as
        (
            SELECT
              t.serverId
             ,t.serverFQDN
             ,t.serverIpAddress
             ,t.serverPort
             ,t.serverLogin
             ,t.serverDescription
             ,t.serverVendorModelId
             ,t.serverVendorName
             ,t.serverIsEnabled
             ,t.serverIsTest
            FROM (
                SELECT 
                     s.Idr                 AS serverId
                    ,s.FQDN                AS serverFQDN
                    ,s.IpAddress           AS serverIpAddress
                    ,s.Port                AS serverPort
                    ,s.Login               AS serverLogin
                    ,s.Description         AS serverDescription
                    ,vm.Idr                AS serverVendorModelId
                    ,vs.Name               AS serverVendorName
                    ,s.Enable              AS serverIsEnabled
                    ,s.TestBench           AS serverIsTest
                FROM dbo.Servers AS s
                   left JOIN dbo.VendorModels AS vm ON s.ModelId = vm.Idr
                left JOIN dbo.Vendors      AS vs ON vm.VendorId = vs.Idr
                WHERE s.IsDeleted = 0
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'serverId',          ' and s.Idr like ''%'+@__filter_0+'%'' ', '')+' 
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'serverFQDN',        ' and s.FQDN like ''%'+@__filter_0+'%'' ', '')+' 
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'serverIpAddress',   ' and s.IpAddress like ''%'+@__filter_0+'%'' ', '')+' 
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'serverPort',        ' and s.Port like ''%'+@__filter_0+'%'' ', '')+' 
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'serverLogin',       ' and s.Login like ''%'+@__filter_0+'%'' ', '')+' 
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'serverDescription', ' and s.Description like ''%'+@__filter_0+'%'' ', '')+' 
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'serverVendorName',  ' and vs.Name like ''%'+@__filter_0+'%'' ', '')+' 
            ) AS t
        )
        select 
             count(*) as count
        from cte_res as c
        where 1=1 
          '+case when coalesce(@__filter_1,'')='' and coalesce(@__filter_0,'')!='' then ' and (
               (c.serverId          like ''%'+@__filter_0+'%'')
            or (c.serverFQDN        like ''%'+@__filter_0+'%'')
            or (c.serverIpAddress   like ''%'+@__filter_0+'%'')
            or (c.serverPort        like ''%'+@__filter_0+'%'')
            or (c.serverLogin       like ''%'+@__filter_0+'%'')
            or (c.serverDescription like ''%'+@__filter_0+'%'')
            or (c.serverVendorName  like ''%'+@__filter_0+'%'') )' else '' end +'
    ';
    --print @query;
    exec sp_executesql @query;

end
go


create procedure dbo.api_get_tasks 
(
   @__p_1 int                           --| Offset
  ,@__p_2 int                           --| Limit
)
as
/*
exec dbo.api_get_tasks
     @__p_1 = 0                         --| Offset
    ,@__p_2 = 3                         --| Limit
*/
begin
    set nocount on;

  declare 
    @query nvarchar(max) = '';

  set @query = '
        select 
             tp.TaskId      AS taskId
            ,COALESCE(STUFF
                  (
                    (
                      SELECT '', '' + l.PhoneNumber
                        FROM Lines AS l 
                        WHERE l.DeviceId = t.DevicesId
                        ORDER BY l.PhoneNumber
                        FOR XML PATH('''')
                    ), 1, 2, N''''
                  ), N'''')   AS devicePhoneNumber
            ,tl.Name      AS taskType
            ,u.DisplayName    AS userName
            ,u.Login      AS userLogin
            ,t.Description    AS taskDescription
            ,tp.DateRun     AS taskDateRun
            ,t.TestServerId   AS serverTestId
            ,s.FQDN       AS serverFQDN

        from TasksPool      AS tp
            join Tasks      AS t on tp.TaskId = t.Idr
            join Users      AS u on u.Idr = t.UsersId
            join TasksList    AS tl on tl.Idr = t.TaskListId
            left join Servers AS s on s.idr = t.TestServerId
            left join Devices AS d on d.Idr = t.DevicesId

          ORDER BY taskId ASC
          OFFSET '+cast(@__p_1 as varchar)+' ROWS
          FETCH NEXT '+cast(@__p_2 as varchar)+' ROWS ONLY
  ';
  /*print @query;*/
  exec sp_executesql @query;
end
go


create procedure dbo.api_get_tasks_completed 
as
/*
exec dbo.api_get_tasks_completed
*/
begin
    set nocount on

    select 
         tc.Idr as taskCompletedId
        ,tc.TaskId as taskId
        ,COALESCE(STUFF
          (
            (
              SELECT ', ' + l.PhoneNumber
                FROM Lines AS l 
                WHERE l.DeviceId = t.DevicesId
                ORDER BY l.PhoneNumber
                FOR XML PATH('')
            ), 1, 2, N''
          ), N'') as devicePhoneNumber
        ,tl.Name as taskType
        ,u.DisplayName as userName
        ,u.Login as userLogin
        ,t.Description as taskDescription
        ,tc.DateCreated as taskDateCreate
        ,tc.DateRun as taskDateRun
        ,tc.DateEnd as taskDateEnd
        ,tc.Status as taskStatus
        ,tc.StatusDesc as taskStatusDesc
        ,t.CancelTaskId as taskCancelId
        ,t.TestServerId as serverTestId
        ,s.FQDN as serverFQDN
    from dbo.TasksHistory as tc
        join dbo.Tasks as t on tc.TaskId = t.Idr
        join dbo.Users as u on u.Idr = t.UsersId
        join dbo.TasksList as tl on tl.Idr = t.TaskListId
        left join dbo.Servers as s on s.idr = t.TestServerId
        left join dbo.Devices as d on d.Idr = t.DevicesId;
end
go


create procedure dbo.api_get_tasks_completed_selection 
(
     @__filter_0 nvarchar(max)                      --| Search
    ,@__filter_1 nvarchar(max)                      --| taskCompletedId|taskId|...etc
    ,@__p_1 int                                     --| Offset
    ,@__p_2 int                                     --| Limit
    ,@__sort_direction_p_3 nvarchar(10) = 'asc'     --| Sort direction asc-desc 
)
as
/*
    exec dbo.api_get_tasks_completed_selection @__filter_0='', @__filter_1='taskCompletedId', @__p_1 = 0, @__p_2 = 100, @__sort_direction_p_3 = 'desc';
*/
begin
    set nocount on;

    declare 
        @query nvarchar(max) = '';

    set @__sort_direction_p_3 = ISNULL(@__sort_direction_p_3,'');

    set @query = '
    ;with cte_res 
    as
    (
        SELECT distinct
            tc.idr               as taskCompletedId
          , tc.TaskId            as taskId
          , COALESCE(STUFF
              (
              (
                SELECT '', '' + l.PhoneNumber
                FROM Lines AS l 
                WHERE l.DeviceId = t.taskDevicesId
                ORDER BY l.PhoneNumber
                FOR XML PATH('''')
              ), 1, 2, N''''
              ), N'''')          as devicePhoneNumber
          , tl.Name                 as taskType
          , u.DisplayName         as userName
          , u.Login                 as userLogin
          , t.taskDescription     as taskDescription
          , tc.DateCreated         as taskDateCreate
          , tc.DateRun             as taskDateRun
          , tc.DateEnd             as taskDateEnd
          , tc.Status             as taskStatus
          , tc.StatusDesc        as taskStatusDesc
          , t.taskStatusCancel   as taskCancelId
          , t.taskTestServerId   as serverTestId
          , s.FQDN               as serverFQDN
        FROM
        (
            SELECT distinct
                t.Idr            as taskIdr
              , t.Description    as taskDescription
              , t.DevicesId        as taskDevicesId
              , t.TaskListId    as taskTaskListId
              , t.TestServerId    as taskTestServerId
              , t.UsersId        as taskUsersId
              , t.CancelTaskId  as taskStatusCancel
              , tc.Idr          as tasksHistoryIdr
            FROM dbo.Tasks AS t
            join dbo.tasksHistory AS tc on tc.TaskId = t.Idr
            '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'devicePhoneNumber',   'join dbo.Devices as d on t.DevicesId = d.Idr join dbo.Lines as l on l.DeviceId = d.Idr and l.PhoneNumber like ''%'+@__filter_0+'%'' ', '')+'
            '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'taskType',            'join dbo.TasksList AS tl on t.TaskListId = tl.idr and tl.Name like ''%'+@__filter_0+'%'' ', '')+'
            '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'userName',            'join dbo.Users AS u on t.UsersId = u.idr and u.DisplayName like ''%'+@__filter_0+'%'' ', '')+'
            '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'userLogin',           'join dbo.Users AS u on t.UsersId = u.idr and u.Login like ''%'+@__filter_0+'%'' ', '')+'
            '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'serverTestId',        'join dbo.Servers AS s on t.TestServerId = s.idr and s.idr like ''%'+@__filter_0+'%'' ', '')+'
            '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'serverFQDN',          'join dbo.Servers AS s on t.TestServerId = s.idr and s.FQDN like ''%'+@__filter_0+'%'' ', '')+'

            WHERE 1=1
            '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'taskCompletedId',            ' and tc.Idr like ''%'+@__filter_0+'%'' ', '')+' 
            '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'taskId',                     ' and tc.TaskId like ''%'+@__filter_0+'%'' ', '')+' 
            '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'taskDescription',            ' and t.Description like ''%'+@__filter_0+'%'' ', '')+' 
            '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'taskStatus',                 ' and tc.Status like ''%'+@__filter_0+'%'' ', '')+' 
            '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'taskStatusDesc',             ' and tc.StatusDesc like ''%'+@__filter_0+'%'' ', '')+' 
            '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'taskCancelId',               ' and t.CancelTaskId like ''%'+@__filter_0+'%'' ', '')+' 
        ) AS t
        JOIN dbo.tasksHistory AS tc ON t.tasksHistoryIdr = tc.Idr
        left JOIN dbo.Users AS u ON t.taskUsersId = u.Idr
        left JOIN dbo.Taskslist AS tl ON t.taskTaskListId = tl.Idr
        left JOIN dbo.Servers AS s ON t.taskTestServerId = s.Idr
    )
    select 
         c.taskCompletedId
        ,c.taskId
        ,c.devicePhoneNumber
        ,c.taskType
        ,c.userName
        ,c.userLogin
        ,c.taskDescription
        ,c.taskDateCreate
        ,c.taskDateRun
        ,c.taskDateEnd
        ,c.taskStatus
        ,c.taskStatusDesc
        ,c.taskCancelId
        ,c.serverTestId
        ,c.serverFQDN
    from cte_res as c
    where 1=1 
        /*добавляем поиск по всем полям если не задано поле для поиска */
      '+case when coalesce(@__filter_1,'')='' and coalesce(@__filter_0,'')!='' then ' and (
           (c.taskCompletedId   like ''%'+@__filter_0+'%'')
        or (c.taskId            like ''%'+@__filter_0+'%'')
        or (c.devicePhoneNumber    like ''%'+@__filter_0+'%'')
        or (c.taskType            like ''%'+@__filter_0+'%'')
        or (c.userName            like ''%'+@__filter_0+'%'')
        or (c.userLogin            like ''%'+@__filter_0+'%'')
        or (c.taskDescription    like ''%'+@__filter_0+'%'')
        or (c.taskDateCreate    like ''%'+@__filter_0+'%'')
        or (c.taskDateRun        like ''%'+@__filter_0+'%'')
        or (c.taskDateEnd        like ''%'+@__filter_0+'%'')
        or (c.taskStatus        like ''%'+@__filter_0+'%'')
        or (c.taskStatusDesc    like ''%'+@__filter_0+'%'')
        or (c.taskCancelId        like ''%'+@__filter_0+'%'')
        or (c.serverTestId        like ''%'+@__filter_0+'%'')
        or (c.serverFQDN        like ''%'+@__filter_0+'%'') )' else '' end +'
    ORDER BY 1 '+@__sort_direction_p_3+', 2 '+@__sort_direction_p_3+', 3 '+@__sort_direction_p_3+', 4 '+@__sort_direction_p_3+', 5 '+@__sort_direction_p_3+', 6 '+@__sort_direction_p_3+', 7 '+@__sort_direction_p_3+', 9 '+@__sort_direction_p_3+', 10 '+@__sort_direction_p_3+', 11 '+@__sort_direction_p_3+', 12 '+@__sort_direction_p_3+'
    OFFSET '+cast(@__p_1 as varchar)+' ROWS
    FETCH NEXT '+cast(@__p_2 as varchar)+' ROWS ONLY
    ';
    /*print @query;*/
    exec sp_executesql @query;

end
go


create procedure dbo.api_get_tasks_completed_selectiondate 
(
   @__filter_0  datetime                --| from date, required
  ,@__filter_1  datetime                --| to date, required
  ,@__p_1     int                        --| Offset, required
  ,@__p_2     int                        --| Limit, required
  ,@__filter_2  varchar(32)                --| tableColumn, required, example. taskDateCreate|taskDateRun|taskDateEnd
  ,@__sort_direction_p_3  varchar(10)    --| Sort direction asc-desc
)
as
/*
exec dbo.api_get_tasks_completed_selectiondate
     @__filter_0 = '2020-10-01 00:00:00.000'              --| from date, required
    ,@__filter_1 = '2020-11-05 23:59:59.000'              --| to date, required
    ,@__p_1 = 0                                           --| Offset, required
    ,@__p_2 = 10                                          --| Limit, required
    ,@__filter_2 = 'taskDateEnd'                          --| tableColumn, required, example. taskDateCreate|taskDateRun|taskDateEnd
    ,@__sort_direction_p_3 = 'asc'                        --| Sort direction asc-desc
*/
begin
    set nocount on;

    set @__filter_2 = ISNULL(@__filter_2, 'taskDateEnd')
    set @__sort_direction_p_3 = ISNULL(@__sort_direction_p_3,'')
    declare @query nvarchar(max) = '';
    set @query = '
      SELECT 
         taskCompletedId
        ,taskId
        ,devicePhoneNumber
        ,taskType
        ,userName
        ,userLogin
        ,taskDescription
        ,taskDateCreate
        ,taskDateRun
        ,taskDateEnd
        ,taskStatus
        ,taskStatusDesc
        ,taskCancelId
        ,serverTestId
        ,serverFQDN
      FROM (
        SELECT 
           tc.Idr                 AS taskCompletedId
          ,tc.TaskId              AS taskId
          ,COALESCE(STUFF
          (
             (
               SELECT '', '' + l.PhoneNumber
                 FROM Lines AS l 
                 WHERE l.DeviceId = t.DevicesId
                 ORDER BY l.PhoneNumber
                 FOR XML PATH('''')
             ), 1, 2, N''''
           ), N'''')              AS devicePhoneNumber
          ,tl.Name                AS taskType
          ,u.DisplayName          AS userName
          ,u.Login                AS userLogin
          ,t.Description          AS taskDescription
          ,tc.DateCreated         AS taskDateCreate
          ,tc.DateRun             AS taskDateRun
          ,tc.DateEnd             AS taskDateEnd
          ,tc.Status              AS taskStatus
          ,tc.StatusDesc          AS taskStatusDesc
          ,t.CancelTaskId         AS taskCancelId
          ,t.TestServerId         AS serverTestId
          ,s.FQDN                 AS serverFQDN

        FROM dbo.TasksHistory     AS tc
        inner JOIN dbo.Tasks      AS t ON tc.TaskId = t.Idr
        inner JOIN dbo.Users      AS u ON u.Idr = t.UsersId
        inner JOIN dbo.TasksList  AS tl ON tl.Idr = t.TaskListId
        left JOIN dbo.Servers     AS s ON s.idr = t.TestServerId
        left JOIN dbo.Devices     AS d ON d.Idr = t.DevicesId
        left JOIN dbo.Lines       AS l ON l.DeviceId = d.Idr
      ) AS r
      WHERE r.'+@__filter_2+' >= @dtFrom and r.'+@__filter_2+' <= @dtTo
      ORDER BY '+@__filter_2+' '+@__sort_direction_p_3 + '
      OFFSET '+cast(@__p_1 as varchar)+' ROWS
      FETCH NEXT '+cast(@__p_2 as varchar)+' ROWS ONLY
    ';
    /*print @query;*/
    exec sp_executesql 
         @stmt = @query
        ,@params = N'@dtFrom datetime, @dtTo datetime'
        ,@dtFrom  = @__filter_0
        ,@dtTo = @__filter_1;
end
go


create procedure dbo.api_get_tasks_completed_totalcount 
(
     @__filter_0 nvarchar(max)      --| Search, example. empty; string
    ,@__filter_1 nvarchar(128)      --| tableColumn, example. Null; 'TaskCancelId'; 'userName'...
)
as
/*
exec dbo.api_get_tasks_completed_totalcount @__filter_0 = 'search', @__filter_1 = 'serverFQDN'
*/
begin
    set nocount on;

    declare @query nvarchar(max) = '';
    if coalesce(@__filter_0,'')  = '' 
    begin
      if coalesce(@__filter_1,'')=''
        /*если, поле search не заполнено, а поле orderedColumn не выбрано - считать count для всех текущих заданий dbo.TasksHistory */
        set @query = 'SELECT count(Idr) AS count FROM dbo.TasksHistory';
      else
        /*с заполненным полем orderedColumn, поле search пустое, выводим все count без учета нулевых значений. */
        set @query = '
      SELECT 
        count(* /*res.'+@__filter_1+'*/) AS count
      FROM (
        SELECT distinct
            tc.TaskId AS taskId
          , t.Description AS taskDescription
          , null AS devicePhoneNumber
          , u.Login AS userLogin
          , u.DisplayName AS userName
          , tl.Name AS taskType
          , t.TestServerId AS serverTestId
          , s.FQDN AS serverFQDN
          , tc.Status AS taskStatus
          , tc.StatusDesc AS taskStatusDesc
          , t.CancelTaskId as TaskCancelId
        FROM dbo.TasksHistory tc
        JOIN dbo.Tasks t ON t.idr = tc.TaskId
        JOIN dbo.Users AS u ON t.UsersId = u.Idr
        JOIN Taskslist AS tl ON t.TaskListId = tl.Idr
        /*left JOIN Lines AS l ON t.DevicesId = l.DeviceId*/
        left JOIN Servers AS s ON t.TestServerId = s.Idr
        ) AS res ';
end
else begin
   set @__filter_1 = lower(@__filter_1);

   set @query = '
   select 
  count(*) AS count
   FROM (
    SELECT distinct
        tc.TaskId AS taskId
      , t.TaskDescription AS taskDescription
      /*, l.phonenumber AS devicePhoneNumber*/
      , u.Login AS userLogin
      , u.DisplayName AS userName
      , tl.Name AS taskType
      , t.TaskTestServerId AS serverTestId
      , s.FQDN AS serverFQDN
      , tc.Status AS taskStatus
      , tc.StatusDesc AS taskStatusDesc
      , t.TaskCancelId
  FROM (
        SELECT 
        t.Idr AS taskIdr
      , t.Description AS taskDescription
      , t.DevicesId AS taskDevicesId
      , t.TaskListId AS taskTaskListId
      , t.TestServerId AS taskTestServerId
      , t.UsersId AS taskUsersId
      , t.CancelTaskId AS taskCancelId
        FROM dbo.Tasks AS t
        JOIN dbo.TasksHistory AS tc on tc.TaskId = t.Idr
        WHERE tc.TaskId like ''%'+@__filter_0+'%''
      '+case when isnull(@__filter_1,'')!='' then +'and '''+ @__filter_1 +''' = ''Taskid'' ' else '' end +'
    union
        SELECT 
        t.Idr AS taskIdr
      , t.Description AS taskDescription
      , t.DevicesId AS taskDevicesId
      , t.TaskListId AS taskTaskListId
      , t.TestServerId AS taskTestServerId
      , t.UsersId AS taskUsersId
      , t.CancelTaskId AS taskCancelId
        FROM dbo.Tasks AS t
        WHERE t.Description like ''%'+@__filter_0+'%''
      '+case when isnull(@__filter_1,'')!='' then +' and '''+ @__filter_1 +''' = ''Taskdescription'' ' else '' end +'
    union
        SELECT distinct 
        t.Idr AS taskIdr
      , t.Description AS taskDescription
      , t.DevicesId AS taskDevicesId
      , t.TaskListId AS taskTaskListId
      , t.TestServerId AS taskTestServerId
      , t.UsersId AS taskUsersId
      , t.CancelTaskId AS taskCancelId
        FROM dbo.Tasks AS t
        JOIN dbo.Devices AS d on t.DevicesId = d.Idr 
        JOIN dbo.Lines AS l on l.DeviceId = d.Idr and l.PhoneNumber like ''%'+@__filter_0+'%''
    where 1=1
      '+case when isnull(@__filter_1,'')!='' then +' and '''+ @__filter_1 +''' = ''devicephonenumber'' ' else '' end +'
    union
        SELECT 
        t.Idr AS taskIdr
      , t.Description AS taskDescription
      , t.DevicesId AS taskDevicesId
      , t.TaskListId AS taskTaskListId
      , t.TestServerId AS taskTestServerId
      , t.UsersId AS taskUsersId
      , t.CancelTaskId AS taskCancelId
        FROM dbo.Tasks AS t
        JOIN dbo.Users AS u on t.UsersId = u.idr
        WHERE u.Login like ''%'+@__filter_0+'%''
      '+case when isnull(@__filter_1,'')!='' then +' and '''+ @__filter_1 +''' = ''userlogin'' ' else '' end +'
    union
        SELECT 
        t.Idr AS taskIdr
      , t.Description AS taskDescription
      , t.DevicesId AS taskDevicesId
      , t.TaskListId AS taskTaskListId
      , t.TestServerId AS taskTestServerId
      , t.UsersId AS taskUsersId
      , t.CancelTaskId AS taskCancelId
        FROM dbo.Tasks AS t
        JOIN dbo.Users AS u on t.UsersId = u.idr
        WHERE u.DisplayName like ''%'+@__filter_0+'%''
      '+case when isnull(@__filter_1,'')!='' then +' and '''+ @__filter_1 +''' = ''username'' ' else '' end +'
    union
        SELECT 
        t.Idr AS taskIdr
      , t.Description AS taskDescription
      , t.DevicesId AS taskDevicesId
      , t.TaskListId AS taskTaskListId
      , t.TestServerId AS taskTestServerId
      , t.UsersId AS taskUsersId
      , t.CancelTaskId AS taskCancelId
        FROM dbo.Tasks AS t
        JOIN dbo.TasksList AS tl on t.TaskListId = tl.idr
        WHERE tl.Name like ''%'+@__filter_0+'%''
      '+case when isnull(@__filter_1,'')!='' then +' and '''+ @__filter_1 +''' = ''Tasktype'' ' else '' end +'
    union
        SELECT 
        t.Idr AS taskIdr
      , t.Description AS taskDescription
      , t.DevicesId AS taskDevicesId
      , t.TaskListId AS taskTaskListId
      , t.TestServerId AS taskTestServerId
      , t.UsersId AS taskUsersId
      , t.CancelTaskId AS taskCancelId
        FROM dbo.Tasks AS t
        WHERE t.TestServerId like ''%'+@__filter_0+'%''
      '+case when isnull(@__filter_1,'')!='' then +' and '''+ @__filter_1 +''' = ''servertestid'' ' else '' end +'
    union
        SELECT 
        t.Idr AS taskIdr
      , t.Description AS taskDescription
      , t.DevicesId AS taskDevicesId
      , t.TaskListId AS taskTaskListId
      , t.TestServerId AS taskTestServerId
      , t.UsersId AS taskUsersId
      , t.CancelTaskId AS taskCancelId
        FROM dbo.Tasks AS t
        WHERE t.CancelTaskId like ''%'+@__filter_0+'%''
      '+case when isnull(@__filter_1,'')!='' then +' and '''+ @__filter_1 +''' = ''TaskCancelId'' ' else '' end +'
    union
        SELECT 
        t.Idr AS taskIdr
      , t.Description AS taskDescription
      , t.DevicesId AS taskDevicesId
      , t.TaskListId AS taskTaskListId
      , t.TestServerId AS taskTestServerId
      , t.UsersId AS taskUsersId
      , t.CancelTaskId AS taskCancelId
        FROM dbo.Tasks AS t
        JOIN dbo.Servers AS s on t.TestServerId = s.idr
        WHERE s.FQDN like ''%'+@__filter_0+'%''
      '+case when isnull(@__filter_1,'')!='' then +' and '''+ @__filter_1 +''' = ''serverfqdn'' ' else '' end +'
  union
        SELECT 
        t.Idr AS taskIdr
      , t.Description AS taskDescription
      , t.DevicesId AS taskDevicesId
      , t.TaskListId AS taskTaskListId
      , t.TestServerId AS taskTestServerId
      , t.UsersId AS taskUsersId
      , t.CancelTaskId AS taskCancelId
        FROM dbo.Tasks AS t
        JOIN dbo.TasksHistory AS tc on t.Idr = tc.TaskId
        WHERE tc.Status like ''%'+@__filter_0+'%''
      '+case when isnull(@__filter_1,'')!='' then +' and '''+ @__filter_1 +''' = ''Taskstatus'' ' else '' end +'
  union
        SELECT 
        t.Idr AS taskIdr
      , t.Description AS taskDescription
      , t.DevicesId AS taskDevicesId
      , t.TaskListId AS taskTaskListId
      , t.TestServerId AS taskTestServerId
      , t.UsersId AS taskUsersId
      , t.CancelTaskId AS taskCancelId
        FROM dbo.Tasks AS t
        JOIN dbo.TasksHistory AS tc on t.Idr = tc.TaskId
        WHERE tc.StatusDesc like ''%'+@__filter_0+'%''
      '+case when isnull(@__filter_1,'')!='' then +' and '''+ @__filter_1 +''' = ''Taskstatusdesc'' ' else '' end +'
    ) AS t
    JOIN dbo.TasksHistory AS tc ON t.TaskIdr = tc.TaskId
    /*left JOIN Lines AS l ON t.TaskDevicesId = l.DeviceId*/
    left JOIN Users AS u ON t.TaskUsersId = u.Idr
    left JOIN Taskslist AS tl ON t.TaskTaskListId = tl.Idr
    left JOIN Servers AS s ON t.TaskTestServerId = s.Idr
  ) AS res
    ';
    end;
    --print @query;
    exec sp_executesql @query;
end
go


create procedure dbo.api_get_tasks_completed_totalcountdate 
(  @__filter_0  datetime     --| from date, required
  ,@__filter_1  datetime     --| to date, required
  ,@__filter_2  varchar(32)  --| tableColumn, required, example. taskDateCreate|taskDateRun|taskDateEnd
)
as
/*
exec dbo.api_get_tasks_completed_totalcountdate
     @__filter_0 = '2020-10-01 00:00:00.000'              --| from date, required
    ,@__filter_1 = '2020-11-05 23:59:59.000'              --| to date, required
    ,@__filter_2 = 'taskDateEnd'                          --| tableColumn, required, example. taskDateCreate|taskDateRun|taskDateEnd
*/
begin
    set nocount on;

    declare @query nvarchar(max) = '';
    set @__filter_2 = ISNULL(@__filter_2, 'taskDateEnd');
    set @query = '
      SELECT 
         count(*) AS count
      FROM (
        SELECT 
           tc.DateCreated           AS taskDateCreate
          ,tc.DateRun               AS taskDateRun
          ,tc.DateEnd               AS taskDateEnd
          ,tc.Idr                   AS taskCompletedId
          /*,t.DevicesId              AS devicePhoneNumber*/
        FROM dbo.TasksHistory       AS tc
        inner JOIN dbo.Tasks        AS t ON tc.TaskId = t.Idr
        inner JOIN dbo.Users        AS u ON u.Idr = t.UsersId
        inner JOIN dbo.TasksList    AS tl ON tl.Idr = t.TaskListId
        left JOIN dbo.Servers       AS s ON s.idr = t.TestServerId
        left JOIN dbo.Devices       AS d ON d.Idr = t.DevicesId
      )                             AS r
      WHERE r.'+@__filter_2+' >= @dtFrom and r.'+@__filter_2+' <= @dtTo
      ';
    /*print @query;*/
    exec sp_executesql 
         @stmt = @query
        ,@params = N'@dtFrom datetime, @dtTo datetime'
        ,@dtFrom  = @__filter_0
        ,@dtTo = @__filter_1;
end
go


create procedure dbo.api_get_tasks_selection 
(
     @__filter_0 nvarchar(max)                                      --| Search
    ,@__filter_1 nvarchar(max)                                      --| taskId|taskDescription|...etc
    ,@__p_1 int                                                     --| Offset
    ,@__p_2 int                                                     --| Limit
    ,@__sort_direction_p_3 nvarchar(10) = 'asc'                     --| Sort direction asc-desc 
)
as
/*
exec dbo.api_get_tasks_selection @__filter_0='1239',@__filter_1='taskDescription', @__p_1 = 0, @__p_2 = 100, @__sort_direction_p_3 = 'desc';
*/
begin
    set nocount on;

    declare 
        @query nvarchar(max) = '';

    set @__sort_direction_p_3 = ISNULL(@__sort_direction_p_3,'');

    set @query = ';with cte_res
        as
        (
          SELECT distinct
                tp.TaskId         as taskId
              , t.taskDescription as taskDescription
              , COALESCE(STUFF
                  (
                  (
                    SELECT '', '' + l.PhoneNumber
                    FROM Lines AS l 
                    WHERE l.DeviceId = t.taskDevicesId
                    ORDER BY l.PhoneNumber
                    FOR XML PATH('''')
                  ), 1, 2, N''''
                  ), N'''')       as devicePhoneNumber
            , u.Login             as userLogin
            , u.DisplayName       as userName
            , tl.Name             as taskType
            , t.taskTestServerId  as serverTestId
            , s.FQDN              as serverFQDN
            , tp.DateRun                                AS taskDateRun
          FROM
          (
                SELECT distinct
                      t.Idr as taskIdr
                    , t.Description as taskDescription
                    , t.DevicesId as taskDevicesId
                    , t.TaskListId as taskTaskListId
                    , t.TestServerId as taskTestServerId
                    , t.UsersId as taskUsersId
                    , tp.Idr as TasksPoolIdr
                FROM dbo.Tasks AS t
                  JOIN dbo.TasksPool AS tp ON tp.TaskId = t.Idr
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'devicePhoneNumber',   'join dbo.Devices as d on t.DevicesId = d.Idr join dbo.Lines as l on l.DeviceId = d.Idr and l.PhoneNumber like ''%'+@__filter_0+'%'' ', '')+'
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'userLogin',           'join dbo.Users AS u on t.UsersId = u.idr and u.Login like ''%'+@__filter_0+'%'' ', '')+'
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'userName',            'join dbo.Users AS u on t.UsersId = u.idr and u.DisplayName like ''%'+@__filter_0+'%'' ', '')+'
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'taskType',            'join dbo.TasksList AS tl on t.TaskListId = tl.idr and tl.Name like ''%'+@__filter_0+'%'' ', '')+'
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'serverFQDN',          'join dbo.Servers AS s on t.TestServerId = s.idr and s.FQDN like ''%'+@__filter_0+'%'' ', '')+'
                WHERE 1=1
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'taskId',              ' and tp.TaskId like ''%'+@__filter_0+'%'' ', '')+' 
                '+IIF(coalesce(@__filter_0,'') != '' and @__filter_1 = 'taskDescription',     ' and t.Description like ''%'+@__filter_0+'%'' ', '')+' 
              ) AS t
              JOIN dbo.TasksPool AS tp ON tp.Idr = t.TasksPoolIdr
              left JOIN dbo.Users AS u ON t.taskUsersId = u.Idr
              left JOIN dbo.Taskslist AS tl ON t.taskTaskListId = tl.Idr
              left JOIN dbo.Servers AS s ON t.taskTestServerId = s.Idr
        )
        select 
             c.taskId
            ,c.taskDescription
            ,c.devicePhoneNumber
            ,c.userLogin
            ,c.userName
            ,c.taskType
            ,c.serverTestId
            ,c.serverFQDN
            ,c.taskDateRun
        from cte_res as c
        where 1=1 
          '+case when coalesce(@__filter_1,'')='' and coalesce(@__filter_0,'')!='' then ' and (
               (c.taskId              like ''%'+@__filter_0+'%'')
            or (c.taskDescription     like ''%'+@__filter_0+'%'')
            or (c.devicePhoneNumber   like ''%'+@__filter_0+'%'')
            or (c.userLogin           like ''%'+@__filter_0+'%'')
            or (c.userName            like ''%'+@__filter_0+'%'')
            or (c.taskType            like ''%'+@__filter_0+'%'')
            or (c.serverTestId        like ''%'+@__filter_0+'%'')
            or (c.serverFQDN          like ''%'+@__filter_0+'%'') )' else '' end +'
        ORDER BY 1 '+@__sort_direction_p_3+', 2 '+@__sort_direction_p_3+', 3 '+@__sort_direction_p_3+', 4 '+@__sort_direction_p_3+', 5 '+@__sort_direction_p_3+', 6 '+@__sort_direction_p_3+', 8 '+@__sort_direction_p_3+'
        OFFSET '+cast(@__p_1 as varchar)+' ROWS
        FETCH NEXT '+cast(@__p_2 as varchar)+' ROWS ONLY
    ';
    /*print @query;*/
    exec sp_executesql @query;

end
go


create procedure dbo.api_get_tasks_totalcount 
(
     @__filter_0 nvarchar(max)            --| Search, example. empty; string
    ,@__filter_1 nvarchar(128)            --| tableColumn, example. Null; 'TaskCancelId'; 'userName'...
)
as
/*
exec dbo.api_get_tasks_totalcount
     @__filter_0 = ''                   --| Search, example. empty; string
    ,@__filter_1 = 'taskDescription'    --| tableColumn, example. Null; 'TaskCancelId'; 'userName'...
*/
begin
    set nocount on;

    declare @query nvarchar(max) = '';
    if coalesce(@__filter_0,'')  = '' 
    begin
      if coalesce(@__filter_1,'')=''
        /*если, поле search не заполнено, а поле orderedColumn не выбрано - считать count для всех текущих заданий dbo.TasksPool */
        set @query = 'select count(Idr) as count from dbo.TasksPool';
      else
        /*с заполненным полем orderedColumn, поле search пустое, выводим все count без учета нулевых значений. */
        set @query = '
            select 
                count(*) as count
            from (
                SELECT distinct
                    tp.TaskId as taskId
                  , null as devicePhoneNumber
                  , tl.Name as taskType
                  , u.DisplayName as userName
                  , u.Login as userLogin
                  , t.Description as taskDescription
                  , t.TestServerId as serverTestId
                  , s.FQDN as serverFQDN
                from dbo.TasksPool tp
                join dbo.Tasks t on t.idr = tp.TaskId
                JOIN dbo.Users AS u ON t.UsersId = u.Idr
                JOIN Taskslist AS tl ON t.TaskListId = tl.Idr
                /* left JOIN Lines AS l ON t.DevicesId = l.DeviceId */
                left JOIN Servers AS s ON t.TestServerId = s.Idr
                ) as res ';
end
else begin
   set @__filter_1 = lower(@__filter_1);

   set @query = '
   select 
    count(*) AS count
   from (
    SELECT distinct
        tp.TaskId as taskId
      , t.taskDescription as taskDescription
      /* , l.phonenumber as devicePhoneNumber */
      , u.Login as userLogin
      , u.DisplayName as userName
      , tl.Name as taskType
      , t.taskTestServerId as serverTestId
      , s.FQDN as serverFQDN
    FROM (
        SELECT 
        t.Idr as taskIdr
      , t.Description as taskDescription
      , t.DevicesId as taskDevicesId
      , t.TaskListId as taskTaskListId
      , t.TestServerId as taskTestServerId
      , t.UsersId as taskUsersId
        FROM dbo.Tasks AS t
        join dbo.TasksPool AS tp on tp.TaskId = t.Idr
        WHERE tp.TaskId like ''%'+@__filter_0+'%''
      '+case when isnull(@__filter_1,'')!='' then +'and '''+ @__filter_1 +''' = ''taskid'' ' else '' end +'
    union
        SELECT 
        t.Idr as taskIdr
      , t.Description as taskDescription
      , t.DevicesId as taskDevicesId
      , t.TaskListId as taskTaskListId
      , t.TestServerId as taskTestServerId
      , t.UsersId as taskUsersId
        FROM dbo.Tasks AS t
        WHERE t.Description like ''%'+@__filter_0+'%''
      '+case when isnull(@__filter_1,'')!='' then +'and '''+ @__filter_1 +''' = ''taskdescription'' ' else '' end +'
    union
        SELECT 
        t.Idr as taskIdr
      , t.Description as taskDescription
      , t.DevicesId as taskDevicesId
      , t.TaskListId as taskTaskListId
      , t.TestServerId as taskTestServerId
      , t.UsersId as taskUsersId
        FROM dbo.Tasks AS t
        join dbo.Devices as d on t.DevicesId = d.Idr 
        join dbo.Lines as l on l.DeviceId = d.Idr and l.PhoneNumber like ''%'+@__filter_0+'%''
        where 1=1
      '+case when isnull(@__filter_1,'')!='' then +'and '''+ @__filter_1 +''' = ''devicephonenumber'' ' else '' end +'
    union
        SELECT 
        t.Idr as taskIdr
      , t.Description as taskDescription
      , t.DevicesId as taskDevicesId
      , t.TaskListId as taskTaskListId
      , t.TestServerId as taskTestServerId
      , t.UsersId as taskUsersId
        FROM dbo.Tasks AS t
        join dbo.Users AS u on t.UsersId = u.idr
        WHERE u.Login like ''%'+@__filter_0+'%''
      '+case when isnull(@__filter_1,'')!='' then +'and '''+ @__filter_1 +''' = ''userlogin'' ' else '' end +'
    union
        SELECT 
        t.Idr as taskIdr
      , t.Description as taskDescription
      , t.DevicesId as taskDevicesId
      , t.TaskListId as taskTaskListId
      , t.TestServerId as taskTestServerId
      , t.UsersId as taskUsersId
        FROM dbo.Tasks AS t
        join dbo.Users AS u on t.UsersId = u.idr
        WHERE u.DisplayName like ''%'+@__filter_0+'%''
      '+case when isnull(@__filter_1,'')!='' then +'and '''+ @__filter_1 +''' = ''username'' ' else '' end +'
    union
        SELECT 
        t.Idr as taskIdr
      , t.Description as taskDescription
      , t.DevicesId as taskDevicesId
      , t.TaskListId as taskTaskListId
      , t.TestServerId as taskTestServerId
      , t.UsersId as taskUsersId
        FROM dbo.Tasks AS t
        join dbo.TasksList AS tl on t.TaskListId = tl.idr
        WHERE tl.Name like ''%'+@__filter_0+'%''
      '+case when isnull(@__filter_1,'')!='' then +'and '''+ @__filter_1 +''' = ''tasktype'' ' else '' end +'
    union
        SELECT 
        t.Idr as taskIdr
      , t.Description as taskDescription
      , t.DevicesId as taskDevicesId
      , t.TaskListId as taskTaskListId
      , t.TestServerId as taskTestServerId
      , t.UsersId as taskUsersId
        FROM dbo.Tasks AS t
        WHERE t.TestServerId like ''%'+@__filter_0+'%''
      '+case when isnull(@__filter_1,'')!='' then +'and '''+ @__filter_1 +''' = ''servertestid'' ' else '' end +'
    union
        SELECT 
        t.Idr as taskIdr
      , t.Description as taskDescription
      , t.DevicesId as taskDevicesId
      , t.TaskListId as taskTaskListId
      , t.TestServerId as taskTestServerId
      , t.UsersId as taskUsersId
        FROM dbo.Tasks AS t
        join dbo.Servers AS s on t.TestServerId = s.idr
        WHERE s.FQDN like ''%'+@__filter_0+'%''
      '+case when isnull(@__filter_1,'')!='' then +'and '''+ @__filter_1 +''' = ''serverFQDN'' ' else '' end +'
    ) AS t
    JOIN dbo.TasksPool AS tp ON t.taskIdr = tp.TaskId
    /* left JOIN Lines AS l ON t.taskDevicesId = l.DeviceId */
    left JOIN Users AS u ON t.taskUsersId = u.Idr
    left JOIN Taskslist AS tl ON t.taskTaskListId = tl.Idr
    left JOIN Servers AS s ON t.taskTestServerId = s.Idr
  ) as res
    ';
    end;
    --print @query;
    exec sp_executesql @query;
end
go
