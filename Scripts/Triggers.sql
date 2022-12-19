DROP PUBLIC DATABASE LINK CWCON;
CREATE PUBLIC DATABASE LINK CWCON
CONNECT TO DBNoto
IDENTIFIED BY Pa$$w0rd
USING '25.49.158.45:1521/xe';

select * from UserTable;
select * from UserTable@cwcon;
--delete UserTable;

----------------------------------------------------------------------------------------------------------------
create or replace trigger AppRoleTableTrigger
AFTER
INSERT OR DELETE OR UPDATE
ON AppRoleTable
FOR EACH ROW
DECLARE
BEGIN
IF(inserting) THEN
INSERT INTO AppRoleTable@cwcon(ROLENAME) VALUES(:NEW.ROLENAME);
END IF;
IF(updating) THEN
UPDATE AppRoleTable@cwcon SET ROLENAME = :NEW.ROLENAME WHERE ROLEID = :OLD.ROLEID;
END IF;
IF(deleting) THEN
DELETE AppRoleTable@cwcon WHERE ROLEID = :OLD.ROLEID;
END IF;
END;

create or replace trigger UserTableTrigger
AFTER
INSERT OR DELETE OR UPDATE
ON UserTable FOR EACH ROW
BEGIN
IF(inserting) THEN
INSERT INTO UserTable@cwcon(USERLOGIN,USERPASSWORD,USERNAME,USERLASTNAME,USERPHONENUMBER,USEREMAIL,USERROLE,USERICON) VALUES(:NEW.USERLOGIN,:NEW.USERPASSWORD,:NEW.USERNAME,:NEW.USERLASTNAME,:NEW.USERPHONENUMBER,:NEW.USEREMAIL,:NEW.USERROLE,:NEW.USERICON);
END IF;
IF(updating) THEN
UPDATE UserTable@cwcon SET USERLOGIN = :NEW.USERLOGIN WHERE USERLOGIN = :OLD.USERLOGIN;
UPDATE UserTable@cwcon SET USERPASSWORD = :NEW.USERPASSWORD WHERE USERLOGIN = :OLD.USERLOGIN;
UPDATE UserTable@cwcon SET USERNAME = :NEW.USERNAME WHERE USERLOGIN = :OLD.USERLOGIN;
UPDATE UserTable@cwcon SET USERLASTNAME = :NEW.USERLASTNAME WHERE USERLOGIN = :OLD.USERLOGIN;
UPDATE UserTable@cwcon SET USERPHONENUMBER = :NEW.USERPHONENUMBER WHERE USERLOGIN = :OLD.USERLOGIN;
UPDATE UserTable@cwcon SET USEREMAIL = :NEW.USEREMAIL WHERE USERLOGIN = :OLD.USERLOGIN;
UPDATE UserTable@cwcon SET USERROLE = :NEW.USERROLE WHERE USERLOGIN = :OLD.USERLOGIN;
UPDATE UserTable@cwcon SET USERICON = :NEW.USERICON WHERE USERLOGIN = :OLD.USERLOGIN;
END IF;
IF(deleting) THEN
DELETE UserTable@cwcon WHERE USERLOGIN = :OLD.USERLOGIN;
END IF;
END;

create or replace trigger TaskTableTrigger
AFTER
INSERT OR DELETE OR UPDATE
ON TaskTable FOR EACH ROW
BEGIN
IF(inserting) THEN
INSERT INTO TaskTable@cwcon(TASKTEAMID,TASKSTATUS,TASKPRIORITY,TASKTITLE,TASKDESCRIPTION,CREATIONDATE,DEADLINEDATE) VALUES(:NEW.TASKTEAMID,:NEW.TASKSTATUS,:NEW.TASKPRIORITY,:NEW.TASKTITLE,:NEW.TASKDESCRIPTION,:NEW.CREATIONDATE,:NEW.DEADLINEDATE);
END IF;
IF(updating) THEN
UPDATE TaskTable@cwcon SET TASKTEAMID = :NEW.TASKTEAMID WHERE TASKID = :OLD.TASKID;
UPDATE TaskTable@cwcon SET TASKSTATUS = :NEW.TASKSTATUS WHERE TASKID = :OLD.TASKID;
UPDATE TaskTable@cwcon SET TASKPRIORITY = :NEW.TASKPRIORITY WHERE TASKID = :OLD.TASKID;
UPDATE TaskTable@cwcon SET TASKTITLE = :NEW.TASKTITLE WHERE TASKID = :OLD.TASKID;
UPDATE TaskTable@cwcon SET TASKDESCRIPTION = :NEW.TASKDESCRIPTION WHERE TASKID = :OLD.TASKID;
UPDATE TaskTable@cwcon SET CREATIONDATE = :NEW.CREATIONDATE WHERE TASKID = :OLD.TASKID;
UPDATE TaskTable@cwcon SET DEADLINEDATE = :NEW.DEADLINEDATE WHERE TASKID = :OLD.TASKID;
END IF;
IF(deleting) THEN
DELETE TaskTable@cwcon WHERE TASKID = :OLD.TASKID;
END IF;
END;

create or replace trigger TeamTableTrigger
AFTER
INSERT OR DELETE OR UPDATE
ON TeamTable
FOR EACH ROW
DECLARE
BEGIN
IF(inserting) THEN
INSERT INTO TeamTable@cwcon(TEAMNAME) VALUES(:NEW.TEAMNAME);
END IF;
IF(updating) THEN
UPDATE TeamTable@cwcon SET TEAMNAME = :NEW.TEAMNAME WHERE TEAMID = :OLD.TEAMID;
UPDATE TeamTable@cwcon SET TEAMICON = :NEW.TEAMICON WHERE TEAMID = :OLD.TEAMID;
END IF;
IF(deleting) THEN
DELETE TeamTable@cwcon WHERE TEAMID = :OLD.TEAMID;
END IF;
END;

create or replace trigger UserTeamPrivsTrigger
AFTER
INSERT OR DELETE OR UPDATE
ON UserTeamPrivs
FOR EACH ROW
DECLARE
BEGIN
IF(inserting) THEN
INSERT INTO UserTeamPrivs@cwcon(USERTEAMPRIVNAME) VALUES(:NEW.USERTEAMPRIVNAME);
END IF;
IF(updating) THEN
UPDATE UserTeamPrivs@cwcon SET USERTEAMPRIVNAME = :NEW.USERTEAMPRIVNAME WHERE USERTEAMPRIVID = :OLD.USERTEAMPRIVID;
END IF;
IF(deleting) THEN
DELETE UserTeamPrivs@cwcon WHERE USERTEAMPRIVID = :OLD.USERTEAMPRIVID;
END IF;
END;

create or replace trigger UserTeamPrivTableTrigger
AFTER
INSERT OR DELETE OR UPDATE
ON UserTeamPrivTable
FOR EACH ROW
DECLARE
BEGIN
IF(inserting) THEN
INSERT INTO UserTeamPrivTable@cwcon(PRIVUSER,PRIVTEAM,PRIVELEGY) VALUES(:NEW.PRIVUSER,:NEW.PRIVTEAM,:NEW.PRIVELEGY);
END IF;
IF(updating) THEN
UPDATE UserTeamPrivTable@cwcon SET PRIVELEGY = :NEW.PRIVELEGY WHERE PRIVUSER = :OLD.PRIVUSER AND PRIVTEAM = :OLD.PRIVTEAM;
END IF;
IF(deleting) THEN
DELETE UserTeamPrivTable@cwcon WHERE PRIVUSER = :OLD.PRIVUSER AND PRIVTEAM = :OLD.PRIVTEAM;
END IF;
END;

create or replace trigger TaskPrioritiesTrigger
AFTER
INSERT OR DELETE OR UPDATE
ON TaskPriorities
FOR EACH ROW
DECLARE
BEGIN
IF(inserting) THEN
INSERT INTO TaskPriorities@cwcon(TASKPRIORITYNAME) VALUES(:NEW.TASKPRIORITYNAME);
END IF;
IF(updating) THEN
UPDATE TaskPriorities@cwcon SET TASKPRIORITYNAME = :NEW.TASKPRIORITYNAME WHERE TASKPRIORITYID = :OLD.TASKPRIORITYID;
END IF;
IF(deleting) THEN
DELETE TaskPriorities@cwcon WHERE TASKPRIORITYID = :OLD.TASKPRIORITYID;
END IF;
END;

create or replace trigger TaskStatusesTrigger
AFTER
INSERT OR DELETE OR UPDATE
ON TaskStatuses
FOR EACH ROW
DECLARE
BEGIN
IF(inserting) THEN
INSERT INTO TaskStatuses@cwcon(TASKSTATUSNAME) VALUES(:NEW.TASKSTATUSNAME);
END IF;
IF(updating) THEN
UPDATE TaskStatuses@cwcon SET TASKSTATUSNAME = :NEW.TASKSTATUSNAME WHERE TASKSTATUSID = :OLD.TASKSTATUSID;
END IF;
IF(deleting) THEN
DELETE TaskStatuses@cwcon WHERE TASKSTATUSID = :OLD.TASKSTATUSID;
END IF;
END;

create or replace trigger TaskCommentsTrigger
AFTER
INSERT OR DELETE OR UPDATE
ON TaskComments
FOR EACH ROW
DECLARE
BEGIN
IF(inserting) THEN
INSERT INTO TaskComments@cwcon(COMUSER,COMTASK,COMDATE,COMTEXT) VALUES(:NEW.COMUSER,:NEW.COMTASK,:NEW.COMDATE,:NEW.COMTEXT);
END IF;
IF(updating) THEN
UPDATE TaskComments@cwcon SET COMUSER = :NEW.COMUSER WHERE COMID = :OLD.COMID;
UPDATE TaskComments@cwcon SET COMTASK = :NEW.COMTASK WHERE COMID = :OLD.COMID;
UPDATE TaskComments@cwcon SET COMDATE = :NEW.COMDATE WHERE COMID = :OLD.COMID;
UPDATE TaskComments@cwcon SET COMTEXT = :NEW.COMTEXT WHERE COMID = :OLD.COMID;
END IF;
IF(deleting) THEN
DELETE TaskComments@cwcon WHERE COMID = :OLD.COMID;
END IF;
END;