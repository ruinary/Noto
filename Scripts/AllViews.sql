ALTER SESSION SET "_ORACLE_SCRIPT" = TRUE;
select * from user_views;

-------------------------USER & ROLE-------------------------

CREATE VIEW AppRoleTable_view AS SELECT UserTable.UserLogin, UserTable.UserPassword, AppRoleTable.RoleName
FROM UserTable LEFT JOIN AppRoleTable ON UserTable.UserRole = AppRoleTable.RoleID;

SELECT * FROM AppRoleTable_view;

DROP VIEW AppRoleTable_view;

-------------------------USER & ROLE FULL-------------------------

CREATE VIEW user_role_full_view AS SELECT user_table.user_id, user_table.user_login, decryption_password(user_table.user_password) as decr, role_table.role_name
FROM user_table LEFT JOIN role_table ON user_table.user_role = role_table.role_id;

SELECT * FROM user_role_full_view;

DROP VIEW user_role_full_view;

-------------------------ARTIST & ALBUM-------------------------

CREATE VIEW artist_album_view AS SELECT album_table.album_id, artist_table.artist_name, album_table.album_name, album_table.album_released, album_table.album_blob
FROM artist_table JOIN album_table ON artist_table.artist_id = album_table.album_artist;

SELECT * FROM artist_album_view;

DROP VIEW artist_album_view;

-------------------------ARTIST & ALBUM & SONG-------------------------

CREATE VIEW artist_album_song_view AS SELECT song_table.song_id, artist_table.artist_name, 
    album_table.album_name, song_table.song_name, album_table.album_released, 
    album_table.album_blob, song_table.song_blob
FROM artist_table JOIN album_table ON artist_table.artist_id = album_table.album_artist
    JOIN song_table on song_table.song_album = album_table.album_id;

SELECT * FROM DBMOONYFM.artist_album_song_view ORDER BY song_name ASC;
SELECT * FROM artist_album_song_view;
commit;
DROP VIEW artist_album_song_view;

------------------------- 

CREATE VIEW artist_album_song_user_view AS SELECT user_table.user_id, song_table.song_id, artist_table.artist_name, 
    album_table.album_name, song_table.song_name, album_table.album_released, 
    album_table.album_blob, song_table.song_blob
FROM artist_table JOIN album_table ON artist_table.artist_id = album_table.album_artist
    JOIN song_table on song_table.song_album = album_table.album_id JOIN saved_table on song_table.song_id = saved_table.saved_song
    JOIN user_table on saved_table.saved_user = user_table.user_id;
    
select * from artist_album_song_user_view;
drop view artist_album_song_user_view;

-----------------------------