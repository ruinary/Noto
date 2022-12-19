------------------------------EXPORT XML-------------------------
---------------------- as sys/system to orcl:
--drop directory cw_dir;
create directory cw_dir as '/opt/oracle/CW';
select * from dba_directories where directory_name='CW_DIR';
grant read, write on directory cw_dir to DBNoto;

------------------------------TEAM EXPORT XML-------------------------
CREATE OR REPLACE PROCEDURE teams_export
IS
    rc sys_refcursor;
    doc DBMS_XMLDOM.DOMDocument;
BEGIN
    OPEN rc FOR SELECT TeamID, TeamName, TeamIcon FROM TeamTable;
    doc := DBMS_XMLDOM.NewDOMDocument(XMLTYPE(rc));
    DBMS_XMLDOM.WRITETOFILE(doc, 'CW_DIR/teams_export.xml');
END teams_export;

begin DBNOTO.teams_export(); end;

------------------------------USER EXPORT XML-------------------------
--drop PROCEDURE users_export;
CREATE OR REPLACE PROCEDURE users_export
IS
    rc sys_refcursor;
    doc DBMS_XMLDOM.DOMDocument;
BEGIN
    open rc for select USERLOGIN,USERPASSWORD,USERNAME,USERLASTNAME,USERPHONENUMBER,USEREMAIL,USERROLE, USERICON from UserTable;
    doc := DBMS_XMLDOM.NewDOMDocument(XMLTYPE(rc));
    DBMS_XMLDOM.WRITETOFILE(doc, 'CW_DIR/users_export.xml');
END users_export;

BEGIN
    users_export();
END;
-------------------------IMPORT XML-------------------------
CREATE OR REPLACE PROCEDURE teams_import
IS
BEGIN
    INSERT INTO TeamTable(TEAMNAME)
    SELECT ExtractValue(VALUE(Teams), '//TEAMNAME') AS TeamName
    FROM TABLE(XMLSequence(EXTRACT(XMLTYPE(bfilename('CW_DIR', 'teams_export.xml'),
    nls_charset_id('UTF-8')),'/ROWSET/ROW'))) Teams;
END teams_import;

CREATE OR REPLACE PROCEDURE teams_with_icon_import
IS
BEGIN
    INSERT INTO TeamTable(TEAMNAME,TEAMICON)
    SELECT ExtractValue(VALUE(Teams), '//TEAMNAME') AS TeamName,
           ExtractValue(VALUE(Teams), '//TEAMICON') AS TeamIcon
    FROM TABLE(XMLSequence(EXTRACT(XMLTYPE(bfilename('CW_DIR', 'teams_export.xml'),
    nls_charset_id('UTF-8')),'/ROWSET/ROW'))) Teams;
END teams_with_icon_import;

BEGIN
    teams_import();
    --teams_with_icon_import();
    commit;
END;

CREATE OR REPLACE PROCEDURE users_import
IS
BEGIN
    INSERT INTO UserTable(USERLOGIN,USERPASSWORD,USERNAME,USERLASTNAME,USERPHONENUMBER,USEREMAIL,USERROLE)
    SELECT ExtractValue(VALUE(Users), '//LOGIN') AS UserLogin,
           ExtractValue(VALUE(Users), '//PASSWORD') AS UserPassword,
           ExtractValue(VALUE(Users), '//NAME') AS UserName,
           ExtractValue(VALUE(Users), '//LASTNAME') AS UserLastName,
           ExtractValue(VALUE(Users), '//PHONENUMBER') AS UserPhoneNumber,
           ExtractValue(VALUE(Users), '//EMAIL') AS UserEmail,
           ExtractValue(VALUE(Users), '//ROLE') AS UserRole
    FROM TABLE(XMLSequence(EXTRACT(XMLTYPE(bfilename('CW_DIR', 'users_export.xml'),
    nls_charset_id('UTF-8')),'/ROWSET/ROW'))) Users;
END users_import;
