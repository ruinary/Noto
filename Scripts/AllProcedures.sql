ALTER SESSION SET "_ORACLE_SCRIPT" = TRUE;
SELECT * FROM user_procedures;

EXPLAIN PLAN FOR select * from UserTable;
SELECT * FROM TABLE(DBMS_XPLAN.DISPLAY());
SELECT * FROM (select UserID, UserLogin, UserTeamPrivName, row_number() over (order by UserLogin) rn from DBNoto.UserTeam_view) where rn between :n and :m ORDER BY UserLogin ASC;

----------------------------------------------------------------------

BEGIN
   REGISTER_USER('remeral', 'qwerty', 'Elizabeth', 'A.','+375291477513', 'nelope@mail.ru');
   REGISTER_USER('qusest', 'qwerty', 'Elizabeth', 'A.','+375291477513', 'nelope@mail.ru');
   REGISTER_USER('ruinary', 'qwerty', 'Elizabeth', 'A.','+375291477513', 'nelope@mail.ru');
END;
commit;
UPDATE UserTable SET UserRole  = 2 WHERE UserID = 1; commit;
select count(*) from UserTable;
commit;

-------------------------ENCRYPTION_PASSWORD-------------------------
CREATE OR REPLACE FUNCTION encryption_password
    (p_user_password IN UserTable.UserPassword%TYPE)
	RETURN UserTable.UserPassword%TYPE
IS
    l_key VARCHAR2(2000) := '2201200314775132';
    l_in_val VARCHAR2(2000) := p_user_password;
    l_mod NUMBER := DBMS_CRYPTO.encrypt_aes128 + DBMS_CRYPTO.chain_cbc + DBMS_CRYPTO.pad_pkcs5;
    l_enc RAW(2000);
BEGIN
    l_enc := DBMS_CRYPTO.encrypt(utl_i18n.string_to_raw(l_in_val, 'AL32UTF8'), l_mod, utl_i18n.string_to_raw(l_key, 'AL32UTF8'));
RETURN RAWTOHEX(l_enc);
END encryption_password;

-------------------------DECRYPTION_PASSWORD-------------------------
CREATE OR REPLACE FUNCTION decryption_password
    (p_user_password IN UserTable.UserPassword%TYPE)
	RETURN UserTable.UserPassword%TYPE
IS
    l_key VARCHAR2(2000) := '2201200314775132';
    l_in_val RAW(2000) := HEXTORAW(p_user_password);
    l_mod NUMBER := DBMS_CRYPTO.encrypt_aes128 + DBMS_CRYPTO.chain_cbc + DBMS_CRYPTO.pad_pkcs5;
    l_dec RAW(2000);
BEGIN
    l_dec := DBMS_CRYPTO.decrypt(l_in_val, l_mod, utl_i18n.string_to_raw(l_key, 'AL32UTF8'));
RETURN utl_i18n.raw_to_char(l_dec);
END decryption_password;

-------------------------REGISTER_USER-------------------------
CREATE OR REPLACE PROCEDURE register_user
    (p_user_login IN UserTable.UserLogin%TYPE,
     p_user_password IN UserTable.UserPassword%TYPE,
     p_user_name IN UserTable.UserName%TYPE,
     p_user_lastname IN UserTable.UserLastName%TYPE,
     p_user_phonenumber IN UserTable.UserPhoneNumber%TYPE,
     p_user_email IN UserTable.UserEmail%TYPE)
IS
    cnt NUMBER;
BEGIN
    SELECT COUNT(*) INTO cnt FROM UserTable WHERE UPPER(UserLogin) = UPPER(p_user_login);
    IF (cnt = 0) THEN
        INSERT INTO UserTable(UserLogin, UserPassword, UserName, UserLastName, UserPhoneNumber, UserEmail, UserRole) 
	VALUES (UPPER(p_user_login), encryption_password(UPPER(p_user_password)), p_user_name,  p_user_lastname, p_user_phonenumber, p_user_email, 1);
        COMMIT;
    ELSE
        RAISE_APPLICATION_ERROR(-20001, 'This login is already taken');
    END IF;
END register_user;

-------------------------CHECK_ROLE-------------------------
CREATE OR REPLACE PROCEDURE check_role
    (p_user_login IN UserTable.UserLogin%TYPE,
    o_user_role OUT AppRoleTable.RoleName%TYPE)
IS
    CURSOR role_cursor IS SELECT RoleName FROM AppRoleTable_view WHERE UPPER(UserLogin) = UPPER(p_user_login);
BEGIN
    OPEN role_cursor;
        FETCH role_cursor INTO o_user_role;
        IF role_cursor%NOTFOUND THEN
            RAISE_APPLICATION_ERROR(-20004, 'App role error');
        END IF;
    CLOSE role_cursor;
END check_role;

select * from AppRoleTable_view;

-------------------------LOG_IN_USER-------------------------
CREATE OR REPLACE PROCEDURE log_in_user
    (p_user_login IN UserTable.UserLogin%TYPE,
    p_user_password IN UserTable.UserPassword%TYPE,
    o_user_id OUT UserTable.UserID%TYPE,
    o_user_login OUT UserTable.UserLogin%TYPE,
    o_user_role OUT AppRoleTable.RoleName%TYPE,
    o_user_name OUT UserTable.UserName%TYPE,
    o_user_lastname OUT UserTable.UserLastName%TYPE,
    o_user_phonenumber OUT UserTable.UserPhoneNumber%TYPE,
    o_user_email OUT UserTable.UserEmail%TYPE)
IS
    cnt NUMBER;
    uri AppRoleTable.RoleID%TYPE;
    --CURSOR user_cursor IS SELECT UserID, UserLogin, UserName, UserLastName, UserPhoneNumber, UserEmail FROM UserTable WHERE UPPER(UserLogin) = UPPER(p_user_login) AND UserPassword = encryption_password(UPPER(p_user_password));
BEGIN
    --OPEN user_cursor;
        --FETCH user_cursor INTO o_user_id, o_user_login,o_user_name, o_user_lastname, o_user_phonenumber, o_user_email;
        --IF user_cursor%NOTFOUND THEN
            --RAISE_APPLICATION_ERROR(-20002, 'Incorrect login or password');
       -- END IF;
    --CLOSE user_cursor;
    SELECT COUNT(*) INTO cnt FROM UserTable WHERE UPPER(UserLogin) = UPPER(p_user_login) AND UserPassword = encryption_password(UPPER(p_user_password));
    IF (cnt != 0) THEN
       SELECT UserID INTO o_user_id FROM UserTable WHERE UPPER(UserLogin) = UPPER(p_user_login);
       SELECT UserLogin INTO o_user_login FROM UserTable WHERE UPPER(UserLogin) = UPPER(p_user_login);
       SELECT UserRole INTO uri FROM UserTable WHERE UPPER(UserLogin) = UPPER(p_user_login);
       SELECT RoleName INTO o_user_role FROM AppRoleTable WHERE RoleID = uri;
       SELECT UserName INTO o_user_name FROM UserTable WHERE UPPER(UserLogin) = UPPER(p_user_login);
       SELECT UserLastName INTO o_user_lastname FROM UserTable WHERE UPPER(UserLogin) = UPPER(p_user_login);
       SELECT UserEmail INTO o_user_email FROM UserTable WHERE UPPER(UserLogin) = UPPER(p_user_login);
       SELECT UserPhoneNumber INTO o_user_phonenumber FROM UserTable WHERE UPPER(UserLogin) = UPPER(p_user_login);
    ELSE
        RAISE_APPLICATION_ERROR(-20002, 'User is not found');
    END IF;
END log_in_user;

-------------------------SEARCH_USER_ROLE_IN_TEAM-------------------------
CREATE OR REPLACE PROCEDURE search_user_role_in_team
    (p_user_id IN UserTable.UserID%TYPE,
    p_team_id IN TeamTable.TeamID%TYPE,
    o_user_role OUT UserTeamPrivs.UserTeamPrivName%TYPE)
IS
    CURSOR user_cursor IS SELECT UserTeamPrivName FROM UserTeam_view WHERE UserID = p_user_id AND TeamID = p_team_id;
BEGIN
    OPEN user_cursor;
        FETCH user_cursor INTO o_user_role;
        IF user_cursor%NOTFOUND THEN
            RAISE_APPLICATION_ERROR(-20003, 'User is not found');
        END IF;
    CLOSE user_cursor;
END search_user_role_in_team;

declare
    o_user_role UserTeamPrivs.UserTeamPrivName%TYPE;
begin search_user_role_in_team(30,59,o_user_role); end;

-------------------------SEARCH_USER-------------------------
CREATE OR REPLACE PROCEDURE search_user
    (p_user_login IN UserTable.UserLogin%TYPE,
    o_user_id OUT UserTable.UserID%TYPE,
    o_user_name OUT UserTable.UserName%TYPE,
    o_user_lastname OUT UserTable.UserLastName%TYPE,
    o_user_phonenumber OUT UserTable.UserPhoneNumber%TYPE,
    o_user_email OUT UserTable.UserEmail%TYPE)
IS
    CURSOR user_cursor IS SELECT UserID,UserName,UserLastName,UserPhonenumber,UserEmail FROM UserTable WHERE UPPER(UserLogin) = UPPER(p_user_login);
BEGIN
    OPEN user_cursor;
        FETCH user_cursor INTO o_user_id, o_user_name,o_user_lastname,o_user_phonenumber,o_user_email;
        IF user_cursor%NOTFOUND THEN
            RAISE_APPLICATION_ERROR(-20003, 'User is not found');
        END IF;
    CLOSE user_cursor;
END search_user;

-------------------------UPDATE_USER_LOGIN-------------------------
CREATE OR REPLACE PROCEDURE update_user_login
    (p_user_login IN UserTable.UserLogin%TYPE,
    p_new_user_login IN UserTable.UserLogin%TYPE)
IS
    cnt NUMBER;
BEGIN
    SELECT COUNT(*) INTO cnt FROM UserTable WHERE UPPER(UserLogin) = UPPER(p_user_login);
    IF (cnt != 0) THEN
        SELECT COUNT(*) INTO cnt FROM UserTable WHERE UPPER(UserLogin) = UPPER(p_new_user_login);
        IF (cnt = 0) THEN
            UPDATE UserTable SET UserLogin = UPPER(p_new_user_login) WHERE UPPER(UserLogin) = UPPER(p_user_login);
            COMMIT;
        ELSE
            RAISE_APPLICATION_ERROR(-20001, 'This login is already taken');
        END IF;
    ELSE
        RAISE_APPLICATION_ERROR(-20002, 'User is not found');
    END IF;
END update_user_login;

-------------------------UPDATE_USER_PASSWORD-------------------------
CREATE OR REPLACE PROCEDURE update_user_password
    (p_user_login IN UserTable.UserLogin%TYPE,
    p_new_user_password IN UserTable.UserPassword%TYPE)
IS
    cnt NUMBER;
BEGIN
    SELECT COUNT(*) INTO cnt FROM UserTable WHERE UPPER(UserLogin) = UPPER(p_user_login);
    IF (cnt != 0) THEN
        UPDATE UserTable SET UserPassword = encryption_password(UPPER(p_new_user_password)) WHERE UPPER(UserLogin) = UPPER(p_user_login);
        COMMIT;
    ELSE
        RAISE_APPLICATION_ERROR(-20002, 'User is not found');
    END IF;
END update_user_password;

-------------------------UPDATE_USER_EMAIL-------------------------
CREATE OR REPLACE PROCEDURE update_user_email
    (p_user_login IN UserTable.UserLogin%TYPE,
    p_new_user_email IN UserTable.UserEmail%TYPE)
IS
    cnt NUMBER;
BEGIN
    SELECT COUNT(*) INTO cnt FROM UserTable WHERE UPPER(UserLogin) = UPPER(p_user_login);
    IF (cnt != 0) THEN
        SELECT COUNT(*) INTO cnt FROM UserTable WHERE UserEmail = p_new_user_email;
        IF (cnt = 0) THEN
            UPDATE UserTable SET UserEmail = p_new_user_email WHERE UPPER(UserLogin) = UPPER(p_user_login);
            COMMIT;
        ELSE
            RAISE_APPLICATION_ERROR(-20001, 'This email is already taken');
        END IF;
    ELSE
        RAISE_APPLICATION_ERROR(-20002, 'User is not found');
    END IF;
END update_user_email;

-------------------------UPDATE_USER_PHONENUMBER-------------------------
CREATE OR REPLACE PROCEDURE update_user_phonenumber
    (p_user_login IN UserTable.UserLogin%TYPE,
    p_new_user_phonenumber IN UserTable.UserPhoneNumber%TYPE)
IS
    cnt NUMBER;
BEGIN
    SELECT COUNT(*) INTO cnt FROM UserTable WHERE UPPER(UserLogin) = UPPER(p_user_login);
    IF (cnt != 0) THEN
        SELECT COUNT(*) INTO cnt FROM UserTable WHERE UserPhoneNumber = p_new_user_phonenumber;
        IF (cnt = 0) THEN
            UPDATE UserTable SET UserPhoneNumber = p_new_user_phonenumber WHERE UPPER(UserLogin) = UPPER(p_user_login);
            COMMIT;
        ELSE
            RAISE_APPLICATION_ERROR(-20001, 'This phone number is already taken');
        END IF;
    ELSE
        RAISE_APPLICATION_ERROR(-20002, 'User is not found');
    END IF;
END update_user_phonenumber;

-------------------------UPDATE_USER_NAME-------------------------
CREATE OR REPLACE PROCEDURE update_user_name
    (p_user_login IN UserTable.UserLogin%TYPE,
    p_new_user_name IN UserTable.UserName%TYPE)
IS
    cnt NUMBER;
BEGIN
    SELECT COUNT(*) INTO cnt FROM UserTable WHERE UPPER(UserLogin) = UPPER(p_user_login);
    IF (cnt != 0) THEN
        UPDATE UserTable SET UserName = p_new_user_name WHERE UPPER(UserLogin) = UPPER(p_user_login);
        COMMIT;
    ELSE
        RAISE_APPLICATION_ERROR(-20002, 'User is not found');
    END IF;
END update_user_name;

-------------------------UPDATE_USER_LAST_NAME-------------------------
CREATE OR REPLACE PROCEDURE update_user_last_name
    (p_user_login IN UserTable.UserLogin%TYPE,
    p_new_user_last_name IN UserTable.UserLastName%TYPE)
IS
    cnt NUMBER;
BEGIN
    SELECT COUNT(*) INTO cnt FROM UserTable WHERE UPPER(UserLogin) = UPPER(p_user_login);
    IF (cnt != 0) THEN
        UPDATE UserTable SET UserLastName = p_new_user_last_name WHERE UPPER(UserLogin) = UPPER(p_user_login);
        COMMIT;
    ELSE
        RAISE_APPLICATION_ERROR(-20002, 'User is not found');
    END IF;
END update_user_last_name;

-------------------------DELETE_USER-------------------------
CREATE OR REPLACE PROCEDURE delete_user
    (p_user_login IN UserTable.UserLogin%TYPE)
IS
    cnt NUMBER;
    usr_id UserTable.UserID%TYPE;
BEGIN
    SELECT COUNT(*) INTO cnt FROM UserTable WHERE UPPER(UserLogin) = UPPER(p_user_login);
    IF (cnt != 0) THEN
        SELECT UserID INTO usr_id FROM UserTable WHERE UPPER(UserLogin) = UPPER(p_user_login);
        DELETE FROM UserTable WHERE UPPER(UserTable.UserLogin) = UPPER(p_user_login);
        COMMIT;
    ELSE
        RAISE_APPLICATION_ERROR(-20002, 'User is not found');
    END IF;
END delete_user;

-------------------------CREATE_TEAM (without blob)-------------------------
CREATE OR REPLACE PROCEDURE create_team
    (p_user_login IN UserTable.UserLogin%TYPE,
     p_team_name IN TeamTable.TeamName%TYPE)
IS
     cnt NUMBER;
     usr_id UserTable.UserID%TYPE;
     tm_id TeamTable.TeamID%TYPE;
BEGIN
    SELECT COUNT(*) INTO cnt FROM TeamTable WHERE UPPER(TeamName) = UPPER(p_team_name);
    IF (cnt = 0) THEN
        INSERT INTO TeamTable (TeamName) VALUES(p_team_name);
        COMMIT;
        SELECT TeamID INTO tm_id FROM TeamTable WHERE UPPER(TeamName) = UPPER(p_team_name);
        SELECT UserID INTO usr_id FROM UserTable WHERE UPPER(UserLogin) = UPPER(p_user_login);
        INSERT INTO UserTeamPrivTable(PrivUser,PrivTeam,Privelegy) VALUES(usr_id,tm_id,1);
        COMMIT;
    ELSE
        RAISE_APPLICATION_ERROR(-20003, 'Team with this name already exists');
    END IF;
END create_team;

-------------------------CREATE_TASK (without comments)-------------------------
CREATE OR REPLACE PROCEDURE create_task
    (p_user_login IN UserTable.UserLogin%TYPE,
     p_team_name IN  TeamTable.TeamName%TYPE,
     p_task_title IN TaskTable.TaskTitle%TYPE,
     p_task_status IN TaskStatuses.TaskStatusID%TYPE,
     p_task_priority IN TaskPriorities.TaskPriorityID%TYPE,
     p_task_description IN TaskTable.TaskDescription%TYPE,
     p_task_deadlineDate  IN TaskTable.DeadlineDate%TYPE)
IS
     tm_id TeamTable.TeamID%TYPE;
BEGIN
        SELECT TeamID INTO tm_id FROM TeamTable WHERE UPPER(TeamName) = UPPER(p_team_name);
        INSERT INTO TaskTable(TaskTeamID, TaskStatus, TaskPriority, TaskTitle, TaskDescription, CreationDate, DeadlineDate) 
        VALUES(tm_id, p_task_status, p_task_priority, p_task_title, p_task_description, TO_CHAR(SYSDATE, 'DD.MM.YYYY'), p_task_deadlineDate);
        COMMIT;
END create_task;

-------------------------CREATE_COMMENTS-------------------------
CREATE OR REPLACE PROCEDURE create_comment
    (p_user_id IN UserTable.UserID%TYPE,
     p_task_id IN TaskTable.TaskID%TYPE,
     p_comment_text IN TaskComments.ComText%TYPE)
IS
     cnt NUMBER;
BEGIN 
SELECT COUNT(*) INTO cnt FROM UserTable WHERE UserID = p_user_id;
    IF (cnt != 0) THEN
        SELECT COUNT(*) INTO cnt FROM TaskTable WHERE TaskID = p_task_id;
        IF (cnt != 0) THEN
            INSERT INTO TaskComments(ComUser, ComTask, ComDate, ComText) 
            VALUES(p_user_id, p_task_id, TO_CHAR(SYSDATE, 'DD.MM.YYYY'), p_comment_text);
            COMMIT;  
        ELSE
            RAISE_APPLICATION_ERROR(-20002, 'Task is not found');
        END IF;
    ELSE
        RAISE_APPLICATION_ERROR(-20002, 'User is not found');
    END IF;
END create_comment;

-------------------------GET_LAST_COMMENT-------------------------
CREATE OR REPLACE PROCEDURE get_last_comment
    (p_task_id IN TaskTable.TaskID%TYPE,
     o_user_id OUT UserTable.UserID%TYPE,
     o_user_login OUT UserTable.UserLogin%TYPE,
     o_comment_date OUT TaskComments.ComDate%TYPE,
     o_comment_text OUT TaskComments.ComText%TYPE)
IS
BEGIN 
SELECT ComUser, ComDate, ComText INTO o_user_id, o_comment_date,o_comment_text FROM TaskComments WHERE ComTask = p_task_id ORDER BY TO_DATE(ComDate, 'DD.MM.YYYY') desc FETCH FIRST 1 ROWS ONLY;
SELECT UserLogin INTO o_user_login FROM UserTable WHERE UserID = o_user_id;
END get_last_comment;

-------------------------UPDATE_TEAM_NAME-------------------------
CREATE OR REPLACE PROCEDURE update_team_name
    (p_old_team_name IN TeamTable.TeamName%TYPE,
    p_new_team_name IN TeamTable.TeamName%TYPE)
IS
    cnt NUMBER;
BEGIN
    SELECT COUNT(*) INTO cnt FROM TeamTable WHERE UPPER(TeamName) = UPPER(p_old_team_name);
    IF (cnt != 0) THEN
        SELECT COUNT(*) INTO cnt FROM TeamTable WHERE UPPER(TeamName) = UPPER(p_new_team_name);
        IF (cnt = 0) THEN
            UPDATE TeamTable SET TeamName = UPPER(p_new_team_name) WHERE UPPER(TeamName) = UPPER(p_old_team_name);
            COMMIT;
        ELSE
            RAISE_APPLICATION_ERROR(-20003, 'Team with this name already exists');
        END IF;
    ELSE
        RAISE_APPLICATION_ERROR(-20002, 'Team is not found');
    END IF;
END update_team_name;

-------------------------UPDATE_TEAM_OWNER-------------------------
CREATE OR REPLACE PROCEDURE update_team_owner
    (p_team_name IN TeamTable.TeamName%TYPE,
     p_new_owner IN UserTable.UserLogin%TYPE)
IS
    old_owner_id UserTable.UserID%TYPE;
    new_owner_id UserTable.UserID%TYPE;
    team_id TeamTable.TeamID%TYPE;
    cnt_user NUMBER;
    cnt_team NUMBER;
BEGIN 
SELECT COUNT(*) INTO cnt_user FROM UserTable WHERE UPPER(UserLogin) = UPPER(p_new_owner);
    IF (cnt_user != 0) THEN
        SELECT COUNT(*) INTO cnt_team FROM TeamTable WHERE UPPER(TeamName) = UPPER(p_team_name);
        IF (cnt_team != 0) THEN
            SELECT TeamID INTO team_id FROM TeamTable WHERE UPPER(TeamName) = UPPER(p_team_name);
            SELECT PrivUser INTO old_owner_id FROM UserTeamPrivTable WHERE Privelegy =  1 AND PrivTeam = team_id;
            SELECT UserID INTO new_owner_id FROM UserTable WHERE UPPER(UserLogin) = UPPER(p_new_owner);
            UPDATE UserTeamPrivTable SET Privelegy = 1 WHERE PrivUser = new_owner_id AND PrivTeam = team_id;
            UPDATE UserTeamPrivTable SET Privelegy = 2 WHERE PrivUser = old_owner_id AND PrivTeam = team_id;
            COMMIT;
        ELSE
            RAISE_APPLICATION_ERROR(-20002, 'Team is not found');
        END IF;
    ELSE
        RAISE_APPLICATION_ERROR(-20002, 'User is not found');
    END IF;
END update_team_owner;
 
 -------------------------UPDATE_TASK_TITLE-------------------------
CREATE OR REPLACE PROCEDURE update_task_title
    (p_task_id IN TaskTable.TaskID%TYPE,
    p_new_task_title IN TaskTable.TaskTitle%TYPE)
IS
    cnt NUMBER;
BEGIN
    SELECT COUNT(*) INTO cnt FROM TaskTable WHERE TaskID = p_task_id;
    IF (cnt != 0) THEN
        UPDATE TaskTable SET TaskTitle = p_new_task_title WHERE TaskID = p_task_id;
    ELSE
        RAISE_APPLICATION_ERROR(-20002, 'Task is not found');
    END IF;
END update_task_title;

 -------------------------UPDATE_TASK_STATUS-------------------------
CREATE OR REPLACE PROCEDURE update_task_status
    (p_task_id IN TaskTable.TaskID %TYPE,
    p_new_task_status IN TaskTable.TaskStatus%TYPE)
IS
    cnt NUMBER;
BEGIN
    SELECT COUNT(*) INTO cnt FROM TaskTable WHERE TaskID = p_task_id;
    IF (cnt != 0) THEN
        UPDATE TaskTable SET TaskStatus = p_new_task_status WHERE TaskID = p_task_id;
    ELSE
        RAISE_APPLICATION_ERROR(-20002, 'Task is not found');
    END IF;
END update_task_status;

 -------------------------UPDATE_TASK_PRIORITY-------------------------
CREATE OR REPLACE PROCEDURE update_task_priority
    (p_task_id IN TaskTable.TaskID %TYPE,
    p_new_task_priority IN TaskTable.TaskPriority%TYPE)
IS
    cnt NUMBER;
BEGIN
    SELECT COUNT(*) INTO cnt FROM TaskTable WHERE TaskID = p_task_id;
    IF (cnt != 0) THEN
        UPDATE TaskTable SET TaskPriority = p_new_task_priority WHERE TaskID = p_task_id;
    ELSE
        RAISE_APPLICATION_ERROR(-20002, 'Task is not found');
    END IF;
END update_task_priority;

 -------------------------UPDATE_TASK_DESCRIPTION-------------------------
CREATE OR REPLACE PROCEDURE update_task_description
    (p_task_id IN TaskTable.TaskID %TYPE,
    p_new_task_description IN TaskTable.TaskDescription%TYPE)
IS
    cnt NUMBER;
BEGIN
    SELECT COUNT(*) INTO cnt FROM TaskTable WHERE TaskID = p_task_id;
    IF (cnt != 0) THEN
        UPDATE TaskTable SET TaskDescription = p_new_task_description WHERE TaskID = p_task_id;
    ELSE
        RAISE_APPLICATION_ERROR(-20002, 'Task is not found');
    END IF;
END update_task_description;

 -------------------------UPDATE_TASK_DEADLINEDATE-------------------------
CREATE OR REPLACE PROCEDURE update_task_deadlinedate
    (p_task_id IN TaskTable.TaskID %TYPE,
    p_new_task_deadlinedate IN TaskTable.DeadlineDate%TYPE)
IS
    cnt NUMBER;
BEGIN
    SELECT COUNT(*) INTO cnt FROM TaskTable WHERE TaskID = p_task_id;
    IF (cnt != 0) THEN
        UPDATE TaskTable SET DeadlineDate  = p_new_task_deadlinedate WHERE TaskID = p_task_id;
    ELSE
        RAISE_APPLICATION_ERROR(-20002, 'Task is not found');
    END IF;
END update_task_deadlinedate;

 -------------------------UPDATE_TASK_CREATIONDATE-------------------------
CREATE OR REPLACE PROCEDURE update_task_creationdate
    (p_task_id IN TaskTable.TaskID %TYPE,
    p_new_task_creationdate IN TaskTable.CreationDate%TYPE)
IS
    cnt NUMBER;
BEGIN
    SELECT COUNT(*) INTO cnt FROM TaskTable WHERE TaskID = p_task_id;
    IF (cnt != 0) THEN
        UPDATE TaskTable SET CreationDate  = p_new_task_creationdate WHERE TaskID = p_task_id;
    ELSE
        RAISE_APPLICATION_ERROR(-20002, 'Task is not found');
    END IF;
END update_task_creationdate;

------------------------------DELETE TASK------------------------------
create or replace PROCEDURE delete_task
    (p_id IN TaskTable.TaskID%TYPE)
IS
    cnt NUMBER;
BEGIN
    SELECT COUNT(*) INTO cnt FROM TaskTable WHERE TaskID = p_id;
    IF (cnt != 0) THEN
        DELETE TaskComments WHERE ComTask = p_id;
        DELETE TaskTable WHERE TaskID = p_id;
        commit;
    ELSE
        RAISE_APPLICATION_ERROR(-20008, 'Task is not found');
    END IF;
END delete_task;
select * from TaskTeam_view;
SELECT * FROM (select TaskID, TaskTitle, CreationDate, DeadlineDate, TaskPriorityName, TaskStatusName, TaskDescription, row_number() over (ORDER BY TaskID ASC) rn from DBNoto.TaskTeam_view WHERE TeamID = 44) where rn between :n and :m ORDER BY TaskID ASC;

------------------------------DELETE TEAM------------------------------
create or replace PROCEDURE delete_team
    (p_id IN TeamTable.TeamID%TYPE)
IS
    cnt NUMBER;
    task_id TaskTable.TaskID%TYPE;
BEGIN
    SELECT COUNT(*) INTO cnt FROM TeamTable WHERE TeamID = p_id;
    IF (cnt != 0) THEN        
        DELETE TeamTable WHERE TeamID = p_id;
    ELSE
        RAISE_APPLICATION_ERROR(-20009, 'Team is not found');
    END IF;
END delete_team;

------------------------------DELETE USER------------------------------
create or replace PROCEDURE delete_user
    (p_login IN UserTable.UserLogin%TYPE)
IS
    user_id UserTable.UserID%TYPE;
    cnt NUMBER;
BEGIN
    SELECT COUNT(*) INTO cnt FROM UserTable WHERE upper(UserLogin) = upper(p_login);
    IF (cnt != 0) THEN
        SELECT UserID INTO user_id FROM UserTable WHERE upper(UserLogin) = upper(p_login);
        DELETE TaskComments WHERE ComUser = user_id;
        DELETE UserTeamPrivTable WHERE PrivUser = user_id;
        DELETE UserTable WHERE UserID = user_id;
    ELSE
        RAISE_APPLICATION_ERROR(-20003, 'User is not found');
    END IF;
END delete_user;

------------------------------ADD USER TO TEAM------------------------------
CREATE OR REPLACE PROCEDURE add_user_to_team
    (p_user_login IN UserTable.UserLogin%TYPE,
    p_team_id IN TeamTable.TeamID%TYPE)
IS
    user_id UserTable.UserID%TYPE;
    cnt NUMBER;
BEGIN
    SELECT COUNT(*) INTO CNT FROM UserTable WHERE upper(UserLogin) = upper(p_user_login);
    SELECT UserID INTO user_id FROM UserTable WHERE upper(UserLogin) = upper(p_user_login);
     IF (cnt != 0) THEN
        SELECT COUNT(*) INTO cnt FROM UserTeamPrivTable WHERE PrivUser = user_id AND PrivTeam = p_team_id;
        IF (cnt = 0) THEN
            INSERT INTO UserTeamPrivTable(PrivUser, PrivTeam, Privelegy) VALUES(user_id, p_team_id, 2);
            COMMIT;
        ELSE
            RAISE_APPLICATION_ERROR(-20011, 'This user is already add');
        END IF; 
    ELSE
        RAISE_APPLICATION_ERROR(-20011, 'This user is not find');
    END IF;
END add_user_to_team;

------------------------------REMOVE USER FROM TEAM------------------------------
CREATE OR REPLACE PROCEDURE remove_user_from_team
    (p_user_id IN UserTable.UserID%TYPE,
    p_team_id IN TeamTable.TeamID%TYPE)
IS
    cnt NUMBER;
BEGIN
    SELECT COUNT(*) INTO cnt FROM UserTeamPrivTable WHERE PrivUser = p_user_id AND PrivTeam = p_team_id;
    IF (cnt != 0) THEN
        DELETE FROM UserTeamPrivTable WHERE PrivUser = p_user_id AND PrivTeam = p_team_id;
        COMMIT;
    ELSE
        RAISE_APPLICATION_ERROR(-20012, 'This user is not found in this team');
    END IF;
END remove_user_from_team;

------------------------------COUNT USER IN TEAM------------------------------
CREATE OR REPLACE PROCEDURE count_user_in_team 
    (p_team_id IN TeamTable.TeamID%TYPE,
    o_count_users OUT NUMBER)
IS
BEGIN
    SELECT count(*)INTO o_count_users FROM UserTeam_view WHERE TeamID = p_team_id;
END count_user_in_team;

------------------------------COUNT TASK BY DATE------------------------------
CREATE OR REPLACE PROCEDURE count_task_by_date 
    (p_team_id IN TeamTable.TeamID%TYPE,
     p_deadlinedate IN TaskTable.DeadlineDate%TYPE,
     o_count_tasks OUT NUMBER)
IS
BEGIN
    SELECT count(*)INTO o_count_tasks FROM TaskTeam_view WHERE TeamID = p_team_id AND DeadlineDate = p_deadlinedate;
END count_task_by_date;
