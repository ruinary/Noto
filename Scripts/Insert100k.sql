------------------------INSERT 100 000 -------------------------
CREATE OR REPLACE PROCEDURE insert_100k_users
IS
BEGIN
    FOR i IN 1 .. 100000 LOOP
         REGISTER_USER('user' || i, 'pass' || i, 'UserName'|| i, 'UserLastName'|| i, '+375291234567', 'user@mail.ru'); commit;
    END LOOP;
END insert_100k_users;

BEGIN
insert_100k_users();
commit;
END;

CREATE OR REPLACE PROCEDURE insert_100k_tasks
IS
BEGIN    
    BEGIN  create_team('remeral','team_demo'); commit; END;
    FOR i IN 1 .. 100000 LOOP
         create_task('remeral','team_demo','task_title'||i,1,1,'task_desc'||i, TO_CHAR(SYSDATE, 'DD.MM.YYYY')); commit;
    END LOOP;
END insert_100k_tasks;

CREATE OR REPLACE PROCEDURE insert_100k_teams
IS
BEGIN
    FOR i IN 1 .. 100000 LOOP
         create_team('remeral', 'team' || i); commit;
    END LOOP;
END insert_100k_teams;