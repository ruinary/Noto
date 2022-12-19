------------------------INSERT 100 000 USERS-------------------------
CREATE OR REPLACE PROCEDURE insert_100k_users
IS
BEGIN
    FOR i IN 1 .. 100000 LOOP
         REGISTER_USER('user' || i, 'pass' || i, 'UserName'|| i, 'UserLastName'|| i, '+375291234567', 'user@mail.ru');
    END LOOP;
END insert_100k_users;

BEGIN
insert_100k_users();
commit;
END;

CREATE OR REPLACE PROCEDURE insert_100k_tasks
IS
BEGIN    
    BEGIN  create_team('qusest','team_demo'); commit; END;
    FOR i IN 1 .. 100000 LOOP
         create_task('qusest','team_demo','task_title'||i,1,1,'task_desc'||i, TO_CHAR(SYSDATE, 'DD.MM.YYYY'));
    END LOOP;
END insert_100k_tasks;
