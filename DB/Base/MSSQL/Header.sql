if not exists (select * from syslogins where name = 'lp')
if  ((select cast(right(left((SELECT @@VERSION),25),4) as int)) >= 2003)
	exec('create login lp with password = ''lp2020'', DEFAULT_DATABASE = mentolprovision, CHECK_POLICY = OFF')
else
	EXEC sp_addlogin 'lp', 'lp2020', 'mentolprovision', null
go

use mentolprovision
go

if not exists (select * from sysusers where name = 'lp' and uid < 16382)
        EXEC sp_adduser 'lp'
else
      BEGIN
        EXEC sp_dropuser lp
        EXEC sp_adduser 'lp'
      END
go

EXEC sp_addrolemember 'db_owner', 'lp'  
go