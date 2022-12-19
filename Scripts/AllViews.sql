ALTER SESSION SET "_ORACLE_SCRIPT" = TRUE;
select * from user_views;

-------------------------USER & ROLE-------------------------

CREATE VIEW AppRoleTable_view AS SELECT UserTable.UserLogin, UserTable.UserPassword, AppRoleTable.RoleName
FROM UserTable LEFT JOIN AppRoleTable ON UserTable.UserRole = AppRoleTable.RoleID;

-------------------------USER & ROLE FULL-------------------------
--DROP VIEW user_role_full_view;
CREATE VIEW UserRole_full_view AS SELECT UserTable.UserID, UserTable.UserLogin, decryption_password(UserTable.UserPassword) as decr, AppRoleTable.RoleName
FROM UserTable LEFT JOIN AppRoleTable ON UserTable.UserRole = AppRoleTable.RoleID;

-------------------------USER & TEAM & PRIVELEGY-------------------------
--DROP VIEW UserTeam_view;
CREATE VIEW UserTeam_view AS SELECT TeamTable.TeamID, TeamTable.TeamName,TeamTable.TeamIcon,
         UserTable.UserID,UserTable.UserLogin,UserTable.UserIcon,UserTeamPrivs.UserTeamPrivName 
FROM UserTeamPrivTable 
    JOIN UserTeamPrivs ON UserTeamPrivTable.Privelegy = UserTeamPrivs.UserTeamPrivID
    JOIN TeamTable ON UserTeamPrivTable.PrivTeam = TeamTable.TeamID
    JOIN UserTable ON UserTeamPrivTable.PrivUser = UserTable.UserID;
commit;


-------------------------USER & COMMENT & TASK-------------------------
--DROP VIEW UserComment_view;
CREATE VIEW UserComment_view AS SELECT TaskComments.ComID, TaskComments.ComTask, TaskComments.ComText, TaskComments.ComDate,
            UserTable.UserID,UserTable.UserLogin,UserTable.UserIcon
FROM TaskComments 
    JOIN UserTable ON TaskComments.ComUser = UserTable.UserID;
commit;
-------------------------TEAM & TASK------------------------- 

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