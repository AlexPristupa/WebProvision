DO $do$ BEGIN
if not exists(select 1 from pg_namespace where nspname = 'dbo') then 
begin
CREATE SCHEMA dbo;
end ;
END IF;END $do$;

ALTER DEFAULT PRIVILEGES IN SCHEMA dbo GRANT SELECT ON TABLES TO "lk";
ALTER DEFAULT PRIVILEGES IN SCHEMA dbo GRANT INSERT ON TABLES TO "lk";
ALTER DEFAULT PRIVILEGES IN SCHEMA dbo GRANT UPDATE ON TABLES TO "lk";
ALTER DEFAULT PRIVILEGES IN SCHEMA dbo GRANT DELETE ON TABLES TO "lk";
ALTER DEFAULT PRIVILEGES IN SCHEMA dbo GRANT EXECUTE ON FUNCTIONS TO "lk";


--CREATE SCHEMA dbo
--  AUTHORIZATION postgres;

GRANT ALL ON SCHEMA dbo TO postgres;
GRANT ALL ON SCHEMA dbo TO lk; 
GRANT ALL ON SCHEMA dbo TO public;
GRANT dbo TO lk; 

ALTER DEFAULT PRIVILEGES IN SCHEMA dbo GRANT ALL ON TABLES TO lk;
ALTER DEFAULT PRIVILEGES IN SCHEMA dbo GRANT ALL ON FUNCTIONS TO lk;