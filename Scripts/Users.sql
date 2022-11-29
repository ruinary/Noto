SELECT * FROM ALL_USERS;

DROP USER DBNoto;

ALTER SESSION SET "_ORACLE_SCRIPT" = TRUE;

---------------------- schema (tables' owner)
CREATE USER DBNoto
IDENTIFIED BY Pa$$w0rd 
DEFAULT TABLESPACE Users 
QUOTA UNLIMITED ON Users;
COMMIT;
GRANT CONNECT TO DBNoto;
GRANT CREATE TABLE TO DBNoto;
GRANT CREATE SEQUENCE TO DBNoto;
GRANT CREATE VIEW TO DBNoto;
GRANT CREATE INDEXTYPE TO DBNoto;
GRANT CREATE PROCEDURE TO DBNoto;
GRANT CREATE TRIGGER TO DBNoto;
GRANT CREATE SESSION TO DBNoto;
GRANT CREATE JOB TO DBNoto;
COMMIT;

GRANT EXECUTE ON SYS.dbms_crypto TO dbnoto;

COMMIT;