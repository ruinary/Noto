ALTER SESSION SET "_ORACLE_SCRIPT" = TRUE;
select * from user_views;
SELECT * FROM DBNoto.TaskTeam_view WHERE TeamID = 1;
-------------------------USER & ROLE-------------------------

CREATE VIEW AppRoleTable_view AS SELECT UserTable.UserLogin, UserTable.UserPassword, AppRoleTable.RoleName
FROM UserTable LEFT JOIN AppRoleTable ON UserTable.UserRole = AppRoleTable.RoleID;

SELECT * FROM AppRoleTable_view;

DROP VIEW AppRoleTable_view;

-------------------------USER & ROLE FULL-------------------------
--DROP VIEW user_role_full_view;
CREATE VIEW UserRole_full_view AS SELECT UserTable.UserID, UserTable.UserLogin, decryption_password(UserTable.UserPassword) as decr, AppRoleTable.RoleName
FROM UserTable LEFT JOIN AppRoleTable ON UserTable.UserRole = AppRoleTable.RoleID;

SELECT * FROM UserRole_full_view;

-------------------------USER & TEAM & PRIVELEGY-------------------------
--DROP VIEW UserTeam_view;
CREATE VIEW UserTeam_view AS SELECT TeamTable.TeamID, TeamTable.TeamName,TeamTable.TeamIcon,
         UserTable.UserID,UserTable.UserLogin,USerTable.UserIcon,UserTeamPrivs.UserTeamPrivName 
FROM UserTeamPrivTable 
    JOIN UserTeamPrivs ON UserTeamPrivTable.Privelegy = UserTeamPrivs.UserTeamPrivID
    JOIN TeamTable ON UserTeamPrivTable.PrivTeam = TeamTable.TeamID
    JOIN UserTable ON UserTeamPrivTable.PrivUser = UserTable.UserID;

SELECT * FROM DBNoto.UserTeam_view ORDER BY TeamName ASC;
SELECT * FROM UserTeam_view;
commit;
SELECT * FROM DBNoto.UserTeam_view WHERE TeamID = 1 ORDER BY UserLogin ASC;
--SELECT UserIcon FROM DBNoto.UserTable WHERE UserID = 1;
--SELECT UserIcon, UserLogin FROM DBNoto.UserTeam_view WHERE TeamID = 49 ORDER BY UserID ASC FETCH FIRST 1 ROWS ONLY;
-------------------------TEAM & TASK & LAST COMMENT------------------------- 

--DROP VIEW TaskTeam_view;
CREATE VIEW TaskTeam_view 
AS SELECT   TaskTable.TaskID, TaskTable.TaskTitle,
            TaskTable.CreationDate,TaskTable.DeadlineDate, 
            TaskPriorities.TaskPriorityName, TaskStatuses.TaskStatusName,
            TaskTable.TaskDescription,
            TeamTable.TeamID, TeamTable.TeamName
FROM TaskTable 
    JOIN TeamTable ON TeamTable.TeamID = TaskTable.TaskTeamID
    JOIN TaskPriorities ON TaskTable.TaskPriority = TaskPriorities.TaskPriorityID
    JOIN TaskStatuses ON TaskTable.TaskStatus = TaskStatuses.TaskStatusID;
commit;
SELECT * FROM DBNoto.TaskTeam_view WHERE TeamID = 1 ORDER BY TaskPriorityName ASC;
select * from TaskTeam_view;


 --JOIN TaskComments ON TaskTable.TaskID = TaskComments.ComTask
-----------------------------