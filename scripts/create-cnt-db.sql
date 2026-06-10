# Создание БД и пользователя для Sample Project (PostgreSQL)
# Запуск: psql -U postgres -f scripts/create-cnt-db.sql

DO $$
BEGIN
  IF NOT EXISTS (SELECT FROM pg_roles WHERE rolname = 'cnt_sp_db_user') THEN
    CREATE ROLE cnt_sp_db_user WITH LOGIN PASSWORD 'ChangeMe_StrongPassword_123!';
  END IF;
END
$$;

SELECT 'CREATE DATABASE cnt_sp_db OWNER cnt_sp_db_user'
WHERE NOT EXISTS (SELECT FROM pg_database WHERE datname = 'cnt_sp_db')\gexec

GRANT ALL PRIVILEGES ON DATABASE cnt_sp_db TO cnt_sp_db_user;
