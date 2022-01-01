DO $do$ BEGIN
if not exists(select 1 from pg_shadow where usename = 'lk') then 
begin
create user lk
    encrypted password 'lk2017';
end ;
END IF;END $do$;

DO $do$ BEGIN
if not exists(select 1 from pg_shadow where usename = 'dbo') then 
begin
CREATE ROLE "dbo" LOGIN
  ENCRYPTED PASSWORD 'dbo';
end ;
END IF;END $do$;
