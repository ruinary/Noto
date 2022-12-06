ALTER SESSION SET "_ORACLE_SCRIPT" = TRUE;

SELECT * FROM UserTable;

-------------------------ROLE-------------------------

--DROP TABLE AppRoleTable;
CREATE TABLE AppRoleTable (
    RoleID NUMBER(10) GENERATED AS IDENTITY(START WITH 1 INCREMENT BY 1),
    RoleName VARCHAR2(30) NOT NULL,
    CONSTRAINT Role_pk PRIMARY KEY (RoleID)
);
commit;
INSERT INTO AppRoleTable(RoleName) VALUES('USER');
INSERT INTO AppRoleTable(RoleName) VALUES('ADMIN');
commit;
SELECT * FROM AppRoleTable;

-------------------------USER-------------------------

--DROP TABLE UserTable;
CREATE TABLE UserTable(
    UserID NUMBER(10) GENERATED AS IDENTITY(START WITH 1 INCREMENT BY 1),
    UserLogin VARCHAR2(30) NOT NULL,
    UserPassword VARCHAR2(200) NOT NULL,
    UserName VARCHAR2(30) NOT NULL,
    UserLastName VARCHAR2(30) NOT NULL,
    UserPhoneNumber VARCHAR2(13) NOT NULL,
    UserEmail VARCHAR2(30) NOT NULL,
    UserRole NUMBER(10) NOT NULL,
    UserIcon BLOB DEFAULT EMPTY_BLOB(),
    CONSTRAINT User_pk PRIMARY KEY (UserID),
    CONSTRAINT UserRole_fk FOREIGN KEY (UserRole) REFERENCES AppRoleTable(RoleID)
);
commit;
SELECT * FROM UserTable;
select count(*) from UserTable;
-------------------------TASK STATUSES-------------------------

--DROP TABLE TaskStatuses;
CREATE TABLE TaskStatuses (
    TaskStatusID NUMBER(10) GENERATED AS IDENTITY(START WITH 1 INCREMENT BY 1),
    TaskStatusName VARCHAR2(30) NOT NULL,
    CONSTRAINT TaskStatus_pk PRIMARY KEY (TaskStatusID)
);
commit;
INSERT INTO TaskStatuses (TaskStatusName) VALUES('NOT STARTED');
INSERT INTO TaskStatuses (TaskStatusName) VALUES('STARTED');
INSERT INTO TaskStatuses (TaskStatusName) VALUES('IN PROCESS');
INSERT INTO TaskStatuses (TaskStatusName) VALUES('READY');
commit;
SELECT * FROM TaskStatuses;

-------------------------TASK PRIORITIES-------------------------
--DROP TABLE TaskPriorities;
CREATE TABLE TaskPriorities (
    TaskPriorityID NUMBER(10) GENERATED AS IDENTITY(START WITH 1 INCREMENT BY 1),
    TaskPriorityName VARCHAR2(30) NOT NULL,
    CONSTRAINT TaskPriority_pk PRIMARY KEY (TaskPriorityID)
);
commit;
INSERT INTO TaskPriorities (TaskPriorityName) VALUES('HIGH');
INSERT INTO TaskPriorities (TaskPriorityName) VALUES('MEDIUM');
INSERT INTO TaskPriorities (TaskPriorityName) VALUES('LOW');
commit;
SELECT * FROM TaskPriorities;

-------------------------TEAM-------------------------
--DROP TABLE TeamTable;
CREATE TABLE TeamTable (
    TeamID NUMBER(10) GENERATED AS IDENTITY(START WITH 1 INCREMENT BY 1),
    TeamName VARCHAR2(30) NOT NULL,
    TeamIcon BLOB DEFAULT EMPTY_BLOB(),
    CONSTRAINT TeamTable_pk PRIMARY KEY (TeamID)
);
commit;
INSERT INTO TeamTable (TeamName) VALUES('team_demo');
SELECT * FROM TeamTable;

-------------------------TASK-------------------------
--DROP TABLE TaskTable;
CREATE TABLE TaskTable (
    TaskID NUMBER(10) GENERATED AS IDENTITY(START WITH 1 INCREMENT BY 1),
    TaskTeamID NUMBER(10) NOT NULL,
    TaskStatus NUMBER(10) NOT NULL,
    TaskPriority NUMBER(10) NOT NULL,
    TaskTitle VARCHAR2(30) NOT NULL,
    TaskDescription VARCHAR2(200) NOT NULL,
    CreationDate VARCHAR2(30) NOT NULL,
    DeadlineDate VARCHAR2(30) NOT NULL,
    CONSTRAINT TaskTablePriority_fk FOREIGN KEY (TaskPriority) REFERENCES TaskPriorities(TaskPriorityID)  ON DELETE CASCADE, 
    CONSTRAINT TaskTableStatus_fk FOREIGN KEY (TaskStatus) REFERENCES TaskStatuses(TaskStatusID) ON DELETE CASCADE, 
    CONSTRAINT TaskTableTask_fk FOREIGN KEY (TaskTeamID) REFERENCES TeamTable(TeamID) ON DELETE CASCADE,
    CONSTRAINT TaskTable_pk PRIMARY KEY (TaskID)
);
commit;
--INSERT INTO TaskTable (TaskTeamID,TaskStatus,TaskPriority,TaskTitle,TaskDescription,CreationDate,DeadlineDate) VALUES(1,1,1,'loh','d','22.01.2022','23.01.2022');
SELECT * FROM TaskTable;

-------------------------TASK COMMENTS-------------------------
--DROP TABLE TaskComments;
CREATE TABLE TaskComments (
    ComID NUMBER(10) GENERATED AS IDENTITY(START WITH 1 INCREMENT BY 1),
    ComUser NUMBER(10) NOT NULL,
    ComTask  NUMBER(10) NOT NULL,
    ComDate VARCHAR2(30) NOT NULL,
    ComText VARCHAR2(100) NOT NULL,
    CONSTRAINT TaskComment_pk PRIMARY KEY (ComID),
    CONSTRAINT TaskComment_fk FOREIGN KEY (ComUser) REFERENCES UserTable(UserID) ON DELETE CASCADE,
    CONSTRAINT TaskCommentTask_fk FOREIGN KEY (ComTask) REFERENCES TaskTable(TaskID) ON DELETE CASCADE
);
commit;

--INSERT INTO TaskComments ( ComDate,ComText,ComUser) VALUES('22.01.2022','ComText',1);

SELECT * FROM TaskComments; 

-------------------------TEAM USER PRIVS-------------------------
--DROP TABLE UserTeamPrivs;
CREATE TABLE UserTeamPrivs (
    UserTeamPrivID NUMBER(10) GENERATED AS IDENTITY(START WITH 1 INCREMENT BY 1),
    UserTeamPrivName VARCHAR2(30) NOT NULL,
    CONSTRAINT UserTeamPriv_pk PRIMARY KEY (UserTeamPrivID)
);
commit;
INSERT INTO UserTeamPrivs(UserTeamPrivName) VALUES('OWNER');
INSERT INTO UserTeamPrivs(UserTeamPrivName) VALUES('TEAMMATE');
commit;
SELECT * FROM UserTeamPrivs;

-------------------------TEAM USER PRIVS TABLE-------------------------
--DROP TABLE UserTeamPrivTable;
CREATE TABLE UserTeamPrivTable (
    PrivUser NUMBER(10) NOT NULL,
    PrivTeam NUMBER(10) NOT NULL,
    Privelegy NUMBER(10) NOT NULL,
    CONSTRAINT UserToUsers_fk FOREIGN KEY (PrivUser) REFERENCES UserTable(UserID) ON DELETE CASCADE,
    CONSTRAINT TeamToTeams_fk FOREIGN KEY (PrivTeam) REFERENCES TeamTable(TeamID) ON DELETE CASCADE, 
    CONSTRAINT PrivToPrivs_fk FOREIGN KEY (Privelegy) REFERENCES UserTeamPrivs(UserTeamPrivID)
);
commit;
--INSERT INTO UserTeamPrivTable(PrivUser,PrivTeam,Privelegy) VALUES(1,1,1);
SELECT * FROM UserTeamPrivTable;
