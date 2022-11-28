--drop table Client;
--drop table Employee;
--drop table Vacancy;
--drop table OrderStatus
--drop table Equipment
--drop table Makers
--drop table ComponentsOrder
--drop table ShopOfComponents
--drop table COrder


------EMp Login Must be unique-------------------------
 create table Client
(
  Id_Client NUMBER GENERATED ALWAYS AS IDENTITY,
  CONSTRAINT Client_pk PRIMARY KEY (Id_Client),
  FullName nvarchar2(50) not null,
  Adress nvarchar2(200) not null,
  PhoneNumber nvarchar2(40) not null,
  Login nvarchar2(150 ) not null UNIQUE,
  Passw nvarchar2(150) not null
);
 create table Client1
(
  Id_Client NUMBER GENERATED ALWAYS AS IDENTITY,
  CONSTRAINT Client_pk1 PRIMARY KEY (Id_Client),
  FullName nvarchar2(50) not null,
  Adress nvarchar2(200) not null,
  PhoneNumber nvarchar2(40) not null,
  Login nvarchar2(150 ) not null ,
  Passw nvarchar2(150) not null
);
create table Vacancy
(
  Id_vac NUMBER GENERATED ALWAYS AS IDENTITY,
  VacancyName nvarchar2(200) not null,
  CONSTRAINT Vac_pk PRIMARY KEY (Id_vac)
);
 create table Employee
 (
    Id_emp NUMBER GENERATED ALWAYS AS IDENTITY,
      CONSTRAINT Emp_pk PRIMARY KEY (Id_emp),
       FullName nvarchar2(50) not null,
       Id_vac number not null,
  CONSTRAINT fk_Id_vac FOREIGN KEY (Id_vac) REFERENCES Vacancy(Id_vac),
  PassportSeria nvarchar2(60) not null,
   PassportNumber nvarchar2(60) not null,
  Adress nvarchar2(200) not null,
      PhoneNumber nvarchar2(40) not null,
      StartWorkDate DATE,
        Login nvarchar2(150) not null UNIQUE,
  Passw nvarchar2(150) not null
    
 );
create table OrderStatus
(
  Id_Order NUMBER GENERATED ALWAYS AS IDENTITY,
      CONSTRAINT IdOrder_pk PRIMARY KEY (Id_Order),
      StatusName nvarchar2(150) not null
);
create table Equipment
(
    Id_eqp NUMBER GENERATED ALWAYS AS IDENTITY,
      CONSTRAINT Eqp_pk PRIMARY KEY (Id_eqp),
      Ename nvarchar2(150) not null,
      SeriaNumber nvarchar2(150) not null,
      Description nvarchar2(300) not null,
      Maker nvarchar2(100) not null,
      EModel nvarchar2(100) not null
);
create table Makers
(
   Id_make NUMBER GENERATED ALWAYS AS IDENTITY,
      CONSTRAINT Make_pk PRIMARY KEY (Id_make),
      TypeOfRepair nvarchar2(200) not null,
      Costs number not null,
      DateOfRepair date not null
      
);
create table ComponentsOrder
(
  Id_Com NUMBER not null,
    CONSTRAINT fk_Id_Com FOREIGN KEY (Id_Com) REFERENCES ShopOfComponents(Id_Com),
     Id_make number not null,
  CONSTRAINT fk_Id_make FOREIGN KEY (Id_make) REFERENCES Makers(Id_make)
);
create table ShopOfComponents
(
    Id_Com NUMBER GENERATED ALWAYS AS IDENTITY,
    CONSTRAINT Com_pk PRIMARY KEY (Id_Com),
    ComName nvarchar2(200) not null,
    Price number not null
    
);
create table COrder
(
   Id_Or NUMBER GENERATED ALWAYS AS IDENTITY,
    CONSTRAINT Or_pk PRIMARY KEY (Id_Or),
     Id_eqp number not null,
  CONSTRAINT fk_Id_Eq FOREIGN KEY (Id_eqp) REFERENCES Equipment(Id_eqp),
  Id_Client number not null,
  CONSTRAINT fk_Id_Cl FOREIGN KEY (Id_Client) REFERENCES Client(Id_Client),
  Id_emp number not null,
  CONSTRAINT fk_Id_Emp FOREIGN KEY (Id_emp) REFERENCES Employee(Id_emp),
   Id_Status number not null,
  CONSTRAINT fk_Id_OrStatus FOREIGN KEY (Id_Status) REFERENCES OrderStatus(Id_Order),
   Id_make number default null,
  CONSTRAINT fk_Id_mk FOREIGN KEY (Id_make) REFERENCES Makers(Id_make),
  OrderDate date not null
);

--Package----
----------------------------
create or replace package cwPack1
as
procedure addClient(fullName nvarchar2, Adress nvarchar2, PhoneNumber nvarchar2, Login nvarchar2, Passw nvarchar2);
procedure getCountOfClientWithSameLogin(checklogin nvarchar2, results OUT number);
procedure checkClientAccount(lg nvarchar2, ps nvarchar2, results out number);
procedure getClienIdAndName(lgin nvarchar2, psd nvarchar2, id_ret out number, fio out nvarchar2);
procedure getNameAndIdEmp(p_cursor IN OUT NOCOPY SYS_REFCURSOR); 
procedure addEquipment(eqname nvarchar2, sernum nvarchar2, descr nvarchar2, eqmakers nvarchar2, eqmodel nvarchar2);
procedure makeOrder(eq_Id number,client_Id number, empl_Id number, status_id number, dateO date);
procedure getEpuipmentIdForOrder(results out number);
procedure showCurrentClientOrders(cl_id number, p_cursor IN OUT NOCOPY SYS_REFCURSOR);
procedure addComponentsOrder(idcomp number, idmaker number);
procedure showHistoryClientOrders(cl_id number, p_cursor IN OUT NOCOPY SYS_REFCURSOR);
end cwPack1;

--grant execute on System.cwPack2 to C##Employee

create or replace package cwPack2
as
procedure checkEmpAccount(login1 nvarchar2, pass1 nvarchar2, results out number);
procedure getCurrentEmplIdAndName(lgine nvarchar2, psde nvarchar2, id_rete out number, fioe out nvarchar2);
procedure getComponentsNameAndId(p_cursor IN OUT NOCOPY SYS_REFCURSOR);
procedure getClientOrdersForEmployeeToDo(eplid number, p_cursor IN OUT NOCOPY SYS_REFCURSOR);
procedure changeOrderStatus(idor number);
procedure getClientOrdersForEmployee(eplid number, p_cursor IN OUT NOCOPY SYS_REFCURSOR);
procedure addMakers(repairtype nvarchar2, price number, repairdate date);
procedure getLastMakers(idmk out number);
procedure changeStatusAndMakers(ior number, idmk number);
procedure getOrdersHistoryForEmployee(eplid number, p_cursor IN OUT NOCOPY SYS_REFCURSOR);
procedure addComponentsOrder(idcomp number, idmaker number);
end cwPack2;

create or replace package body cwPack2
as
procedure checkEmpAccount(login1 nvarchar2, pass1 nvarchar2, results out number)
  as
 begin 
results:=0;
select count(*) into results from employee where (Login=login1 and Passw=pass1);
  exception
when others then
   raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
end checkEmpAccount;

procedure getCurrentEmplIdAndName(lgine nvarchar2, psde nvarchar2, id_rete out number, fioe out nvarchar2)
as
begin
select Id_Emp  into id_rete from Employee where Login=lgine and Passw=psde;
select FullName into fioe from Employee where Login=lgine and Passw=psde;
exception
when others then
   raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
end getCurrentEmplIdAndName;

procedure getComponentsNameAndId(p_cursor IN OUT NOCOPY SYS_REFCURSOR)
as
begin
OPEN p_cursor FOR 
select Id_com, comname, price  from ShopOfComponents;
exception
when others then
   raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
end getComponentsNameAndId;

procedure getClientOrdersForEmployeeToDo(eplid number, p_cursor IN OUT NOCOPY SYS_REFCURSOR)
as
begin
OPEN p_cursor FOR 
 select distinct Corder.id_or, corder.orderdate as Дата_Заказа, 
 equipment.ename as Наименование_оборудования,
 equipment.Description as Описание_поломки, 
 OrderStatus.statusname as Статус_заказа, 
 Client.Fullname as Имя_Клиента
 from Corder
 inner join Client on Corder.Id_client = Client.Id_client
 inner join equipment on  corder.id_eqp = equipment.Id_eqp
 inner join Employee on Corder.Id_emp = 3
 inner join OrderStatus on  Corder.Id_Status= OrderStatus.ID_order
 where Corder.Id_Status=1;
 exception
when others then
   raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
end getClientOrdersForEmployeeToDo;

procedure changeOrderStatus(idor number)
as
begin
update  COrder set Id_Status=2 where Id_Or=idor;
exception
when others then
   raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
end changeOrderStatus;

procedure getClientOrdersForEmployee(eplid number, p_cursor IN OUT NOCOPY SYS_REFCURSOR)
as
begin
OPEN p_cursor FOR 
 select distinct Corder.id_or, corder.orderdate as Дата_Заказа, 
 equipment.ename as Наименование_оборудования,
 equipment.Description as Описание_поломки, 
 OrderStatus.statusname as Статус_заказа, 
 Client.Fullname as Имя_Клиента
 from Corder
 inner join Client on Corder.Id_client = Client.Id_client
 inner join equipment on  corder.id_eqp = equipment.Id_eqp
 inner join Employee on Corder.Id_emp = eplid
 inner join OrderStatus on  Corder.Id_Status= OrderStatus.ID_order
 where Corder.Id_Status=2;
 exception
when others then
   raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
end getClientOrdersForEmployee;

procedure addMakers(repairtype nvarchar2, price number, repairdate date)
as
begin
insert into Makers(TypeOfRepair, Costs, DateOfRepair) values(repairtype,price, repairdate);
commit;
exception
when others then
   raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
end addMakers;

procedure getLastMakers(idmk out number)
as
begin 
idmk:=0;
select max(id_make) into idmk from Makers;
exception
when others then
   raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
end getLastMakers;

procedure changeStatusAndMakers(ior number, idmk number)
as
begin
update  COrder set ID_MAKE=idmk, Id_Status=3 where Id_Or=ior;  
exception
when others then
   raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
end changeStatusAndMakers;

procedure getOrdersHistoryForEmployee(eplid number, p_cursor IN OUT NOCOPY SYS_REFCURSOR)
as
begin
OPEN p_cursor FOR 
  select distinct Corder.id_or, corder.orderdate as Дата_Заказа, 
 equipment.ename as Наименование_оборудования, 
 equipment.Description as Описание_поломки, 
 OrderStatus.statusname as Статус_заказа, 
 Client.Fullname as Имя_Клиента, 
 makers.typeofrepair as Тип_Ремонта, 
 makers.COSTS as Стоимость, 
 ShopOfComponents.comname as Детали  
 from Corder
 inner join Client on Corder.Id_client = Client.Id_client
 inner join equipment on  corder.id_eqp = equipment.Id_eqp
 inner join Employee on Corder.Id_emp = eplid
 inner join OrderStatus on  Corder.Id_Status= OrderStatus.ID_order
 inner join Makers on COrder.ID_MAKE = makers.ID_MAKE
 left join componentsorder on COrder.ID_MAKE = componentsorder.ID_MAKE
 left join ShopOfComponents on ShopOfComponents.ID_Com = componentsorder.ID_Com
 where Corder.Id_Status=3;
                                                  
 exception
when others then
   raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
end getOrdersHistoryForEmployee;

procedure addComponentsOrder(idcomp number, idmaker number)
as
begin 
insert into ComponentsOrder(Id_Com, Id_make) values(idcomp, idmaker);
commit;
exception
when others then
   raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
end addComponentsOrder;
end cwPack2;

SET SERVEROUTPUT ON;

declare
ct2 number;
begin
System.cwpack2.checkEmpAccount('Sotr4','Password1',ct2);
dbms_output.put_line('count = '||ct2);
end;


declare 
fio nvarchar2(50);
idd number;
begin
SYSTEM.cwpack2.getCurrentEmplIdAndName('Sotr4','Password1', idd, fio);
dbms_output.put_line('id = '||idd||' fio '|| fio);
end;


declare
cur sys_refcursor;
   TYPE zz1  IS RECORD(Id_com number, comname nvarchar2(150), price number);  -- обязательно надо определить, куда фетчим, это самый скользкий момент
   zz zz1;
    -- можно явно задать zz в виде записи (record)
begin 
     system.cwpack2.getComponentsNameAndId(cur);
     loop
      fetch cur into zz;
     
      EXIT when cur%notfound;
       dbms_output.put_line('Id: '||zz.Id_com||' Name: ' || zz.comname||' price: '||zz.price);
    end loop;
  if cur%isopen then
       close cur;
    end if;    
end;


--todo order for employee
declare 
cur sys_refcursor;
   TYPE zz1  IS RECORD(id_or number, orderdate date, ename nvarchar2(150), Description nvarchar2(150), statusname nvarchar2(150), Fullname nvarchar2(150));  -- РѕР±СЏР·Р°С‚РµР»СЊРЅРѕ РЅР°РґРѕ РѕРїСЂРµРґРµР»РёС‚СЊ, РєСѓРґР° С„РµС‚С‡РёРј, СЌС‚Рѕ СЃР°РјС‹Р№ СЃРєРѕР»СЊР·РєРёР№ РјРѕРјРµРЅС‚
   zz zz1;
begin
   System.cwpack2.getClientOrdersForEmployeeToDo(3, cur);
     loop
      fetch cur into zz;
      EXIT when cur%notfound;
       dbms_output.put_line('Id order: ' || zz.id_or ||' Дата Заказа: ' || zz.orderdate|| ' Наименование оборудования: ' || zz.ename || ' Имя исполнителя: ' ||zz.FULLNAME || ' Статус заказа: ' || zz.statusname|| ' Описание поломки: ' || zz.Description);
    end loop;
  if cur%isopen then
       close cur;
    end if;    
end;


--change orstatus int Corder idor
begin 
System,cwpack2.changeOrderStatus(1);
end;


declare 
cur sys_refcursor;
   TYPE zz1  IS RECORD(id_or number, orderdate date, ename nvarchar2(150), Description nvarchar2(150), statusname nvarchar2(150), Fullname nvarchar2(150));  -- обязательно надо определить, куда фетчим, это самый скользкий момент
   zz zz1;
begin
   System.cwpack2.getClientOrdersForEmployee(3, cur);
    loop
      fetch cur into zz;
      EXIT when cur%notfound;
       dbms_output.put_line('Id order: ' || zz.id_or ||' Дата Заказа: ' || zz.orderdate|| ' Наименование оборудования: ' || zz.ename || ' Имя исполнителя: ' ||zz.FULLNAME || ' Статус заказа: ' || zz.statusname|| ' Описание поломки: ' || zz.Description);
    end loop;
  if cur%isopen then
       close cur;
    end if;    
end;

begin
System.cwpack2.addMakers('repair type', 100, '10.11.2022');
end;


declare
n number;
begin
System.cwpack2.getLastMakers(n);
dbms_output.put_line('Last Id Maker: ' ||n);
end;


--change corder status and maker idor idmk
begin
System.cwpack2.changeStatusAndMakers(1,1);
end;

declare 
cur sys_refcursor;
   TYPE zz1  IS RECORD(id_or number, orderdate date, ename nvarchar2(150), Description nvarchar2(150), statusname nvarchar2(150), Fullname nvarchar2(150),typeofrepair   nvarchar2(150), COSTS number, comname nvarchar2(150));  -- обязательно надо определить, куда фетчим, это самый скользкий момент
   zz zz1;
begin
    System.cwpack2.getOrdersHistoryForEmployee(3, cur);
     loop
      fetch cur into zz;
      EXIT when cur%notfound;
       dbms_output.put_line('Id order: ' || zz.id_or ||' Дата Заказа: ' || zz.orderdate|| ' Наименование оборудования: ' || zz.ename || ' Имя исполнителя: ' ||zz.FULLNAME || ' Тип ремонта: ' || zz.typeofrepair || ' Статус заказа: ' || zz.statusname|| ' Описание поломки: ' || zz.Description || ' Стоимость: ' || zz.COSTS || ' Наименование детали: ' || zz.comname);
    end loop;
  if cur%isopen then
       close cur;
    end if;      
end;

-----------------------End Employee Package-------------------------
--------------------------------------------------------------------
--------------------------------------------------------------------


create or replace package cwPackadm
as
procedure showAllClients;
procedure addVacancy(vacName nvarchar2);
procedure showAllVacancy;
procedure addEmp(fullName nvarchar2, Id_vac  number, PassportSeria nvarchar2, PassportNumber nvarchar2,  Adress nvarchar2,  PhoneNumber nvarchar2, StartWorkDate date,  Login nvarchar2, Passw nvarchar2);
procedure showAllEmp;
procedure addStatus(stName nvarchar2);
procedure showAllOrderStatus;
PROCEDURE GET (ConscriptsOut OUT sys_refcursor);--get all clients
procedure importXmlDataFromEmployee;
procedure importXmlDataFromComponents;
procedure exportXmlToClients;
procedure addComponents(componname nvarchar2, compcost number);
end cwPackadm;


create or replace package body cwPackadm
as
procedure showAllClients
  as
  begin
  for client in (select id_client, fullname, adress,phonenumber from Client)
loop
 dbms_output.put_line(client.id_client|| ' ' ||client. fullname|| ' ' ||client.adress|| ' ' ||client.phonenumber); 
 end loop;
 exception
when others then
   raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
end showAllClients ;
procedure addVacancy(vacName nvarchar2)
  as
  begin 
    insert into Vacancy(vacancyname) values(vacName);
    exception
when others then
   raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
    end addVacancy;

procedure showAllVacancy
  as
begin
  for vac in (select * from Vacancy)
    loop  
  dbms_output.put_line(vac.Id_Vac|| ' ' ||vac.VacancyName); 
  end loop;
  exception
when others then
   raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
  end showAllVacancy;

procedure addEmp(fullName nvarchar2, Id_vac  number, PassportSeria nvarchar2, PassportNumber nvarchar2,  Adress nvarchar2,  PhoneNumber nvarchar2, StartWorkDate date, Login nvarchar2, Passw nvarchar2)
    as
  begin 
      insert into Employee(fullname, id_vac, passportseria,passportnumber,adress,phonenumber,startworkdate, login, passw) values(fullName, Id_vac, PassportSeria, PassportNumber,  Adress,  PhoneNumber, StartWorkDate, Login, Passw); 
      commit;
    exception
when others then
   raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
    end addEmp;

procedure showAllEmp
  as
begin
  for emp in (select fullname, id_vac, passportseria,passportnumber,adress,phonenumber,startworkdate from Employee)
    loop
     dbms_output.put_line(emp.fullname|| ' ' ||emp.passportseria|| ' ' ||emp.passportnumber|| ' ' ||emp.adress|| ' ' ||emp.phonenumber|| ' ' ||to_char(emp.startworkdate,'DD-MM-YYYY')); 
  end loop;
  exception
when others then
   raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
  end showAllEmp;

procedure addStatus(stName nvarchar2)
  as
 begin
  insert into OrderStatus(StatusName) values(stName);
  commit;
  exception
when others then
   raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
  end addStatus;

procedure showAllOrderStatus
  as
begin
for status in (select StatusName from OrderStatus)
    loop
     dbms_output.put_line(status.statusName); 
  end loop;
 exception
when others then
   raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
  end showAllOrderStatus;

PROCEDURE GET (ConscriptsOut OUT sys_refcursor)
 as
BEGIN

 OPEN ConscriptsOut FOR
   SELECT   id_client, fullname, adress,phonenumber
  FROM  Client; 
 
 end GET;

procedure importXmlDataFromEmployee
as
F UTL_FILE.FILE_TYPE;
MYCLOB CLOB;
begin
SELECT  
DBMS_XMLGEN.GETXML('
SELECT
 ID_EMP, FULLNAME, ID_VAC,PASSPORTSERIA, PASSPORTNUMBER, ADRESS, PHONENUMBER, STARTWORKDATE LOGIN, PASSW
FROM
EMPLOYEE') INTO MYCLOB FROM DUAL;
F:= UTL_FILE.FOPEN('C:\XML','IMPORTEMP.XML','W');
UTL_FILE.PUT(F,MYCLOB);
UTL_FILE.FCLOSE(F);
exception
when others then
   raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
end importXmlDataFromEmployee;

procedure importXmlDataFromComponents
as
F UTL_FILE.FILE_TYPE;
MYCLOB CLOB;
begin
SELECT  
DBMS_XMLGEN.GETXML('
SELECT
 ID_COM, COMNAME, PRICE
FROM
ShopOfComponents') INTO MYCLOB FROM DUAL;
F:= UTL_FILE.FOPEN('C:\XML','IMPORTShopOfComponents.XML','W');
UTL_FILE.PUT(F,MYCLOB);
UTL_FILE.FCLOSE(F);
exception
when others then
   raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
end importXmlDataFromComponents;

procedure exportXmlToClients
as
begin
insert into Client (FULLNAME, ADRESS, PHONENUMBER, LOGIN, PASSW)
SELECT *
    FROM XMLTABLE('/ROWSET/ROW'
           PASSING XMLTYPE(BFILENAME('DIR','CLIENTS1.XML'),
           NLS_CHARSET_ID('CHAR_CS'))
           COLUMNS FullName nvarchar2(50) PATH 'FullName',
                    Adress nvarchar2(200) PATH 'Adress',
                    PhoneNumber nvarchar2(40) PATH 'Phone',
                    Login nvarchar2(150) PATH 'Login',
                    Passw nvarchar2(150) PATH 'Passw');
                    commit;
                    exception
when others then
   raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
      
end exportXmlToClients;

procedure addComponents(componname nvarchar2, compcost number)
as
begin 
insert into ShopOfComponents (ComName, Price) values(componname, compcost);
commit;
     exception
when others then
   raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
end addComponents;

end cwPackadm;

SET SERVEROUTPUT ON;


begin
--system.cwPackadm.showallclients;
--system.cwPackadm.addvacancy('Master');
--system.cwPackadm.showallvacancy;
----------fullname, id_vac, passportseria,passportnumber,adress,phonenumber,startworkdate--
--system.cwPackadm.addemp('Petrov Mihail Ivanovich', 1,'2827829M823181', 'HB988981218','Yakuba Kolasa 2B, 122','+37626722148', '3.12.2003', 'Login13','Pa');
--system.cwPackadm.showallemp;
--------in waiting, in processing, done 
--system.cwPackadm.addstatus('in done');
--system.cwPackadm.showallorderstatus;
end;

--show all client info
DECLARE
  C_EMP SYS_REFCURSOR;
  TYPE new_type IS RECORD(id_client number, fullname nvarchar2(150), adress nvarchar2(150),phonenumber nvarchar2(140));
  L_REC new_type; --instead of using %ROWTYPE, use the declared type
BEGIN
  system.cwPackadm.GET(C_EMP);
  LOOP
 FETCH c_emp INTO l_rec;
 EXIT WHEN c_emp%NOTFOUND;

     dbms_output.put_line(l_rec.id_client|| ' ' ||l_rec. fullname|| ' ' ||l_rec.adress|| ' ' ||l_rec.phonenumber );
 END LOOP;

CLOSE c_emp;
END;

begin
system.cwPackadm.importXmlDataFromEmployee;
system.cwPackadm.importxmldatafromcomponents;
end;

begin
system.cwPackadm.exportxmltoclients;
end;


---Id_Com Id_maker
begin
system.cwPackadm.addComponentsOrder(1,1);
end;


begin
--add componentsorder IdComp from Components and Id_Make from Makers
System.cwpack1.addComponents(1,1);
end;

---------packadminedn---------------------------------------------
---------------------------------------------------------------------
-----------------------------------------------------------------




create or replace package body cwPack1
  as 
procedure addClient(fullName nvarchar2, Adress nvarchar2, PhoneNumber nvarchar2, Login nvarchar2, Passw nvarchar2)
as
begin
insert into Client(fullname, adress,phonenumber, login, passw) values(fullName, Adress, PhoneNumber, Login, Passw);
commit;
      exception
when others then
   raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
end addClient ;

procedure getCountOfClientWithSameLogin(checklogin nvarchar2, results OUT number)
as

begin
 results:=0;
select count(*) into results from Client where Login=checklogin;
exception
when others then
   raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
end getCountOfClientWithSameLogin;

procedure checkClientAccount(lg nvarchar2, ps nvarchar2, results out number)
  as
begin 
results:=0;
select Count(*) into results from client where Login=lg and Passw=ps;
  exception
when others then
   raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
end checkClientAccount;

procedure getClienIdAndName(lgin nvarchar2, psd nvarchar2, id_ret out number, fio out nvarchar2)
as
begin
select Id_Client  into id_ret from Client where Login=lgin and Passw=psd;
select FullName into fio from Client where Login=lgin and Passw=psd;
exception
when others then
   raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
end getClienIdAndName;

procedure getNameAndIdEmp(p_cursor IN OUT NOCOPY SYS_REFCURSOR)
as  
begin
OPEN p_cursor FOR 
select Id_emp, fullName  from employee;
 end getNameAndIdEmp; 

procedure addEquipment(eqname nvarchar2, sernum nvarchar2, descr nvarchar2, eqmakers nvarchar2, eqmodel nvarchar2)
as
begin
insert into equipment(Ename, SeriaNumber, Description, Maker, EModel) values(eqname,sernum,  descr, eqmakers, eqmodel);
 commit;
    exception
when others then
   raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
end addEquipment;

procedure getEpuipmentIdForOrder(results out number)
as
begin 
results:=0;
select max(id_eqp) into results from equipment;
end getEpuipmentIdForOrder;

procedure makeOrder(eq_Id number,client_Id number, empl_Id number, status_id number,  dateO date)
as
begin
insert into corder(Id_eqp, Id_Client, Id_emp, Id_Status, OrderDate) values(eq_Id ,client_Id , empl_Id , status_id ,  dateO );
commit;
    exception
when others then
   raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
end makeOrder;

procedure showCurrentClientOrders(cl_id number, p_cursor IN OUT NOCOPY SYS_REFCURSOR)
as
begin
OPEN p_cursor FOR 
  select distinct Corder.id_or, corder.orderdate as Дата_Заказа, 
  equipment.ename as Наименование_оборудования, 
  Employee.FULLNAME as Имя_исполнителя, 
  OrderStatus.statusname as Статус_заказа
  from Corder
  inner join Client on Corder.Id_client = cl_id
  inner join  equipment on  corder.id_eqp = equipment.Id_eqp
  inner join Employee on Corder.Id_emp = Employee.Id_Emp
  inner join OrderStatus on Corder.Id_status = OrderStatus.Id_order
  where Corder.Id_status=1 or Corder.Id_status=2;
exception
when others then
   raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
end showCurrentClientOrders;

procedure showHistoryClientOrders(cl_id number, p_cursor IN OUT NOCOPY SYS_REFCURSOR)
as
begin
OPEN p_cursor FOR 
 select distinct Corder.id_or, corder.orderdate as Дата_Заказа, 
  equipment.ename as Наименование_оборудования, 
  Employee.FULLNAME as Имя_исполнителя, 
  OrderStatus.statusname as Статус_заказа, 
  makers.COSTS as Стоимость
  from Corder
  inner join Client on Corder.Id_client = cl_id
  inner join  equipment on  corder.id_eqp = equipment.Id_eqp
  inner join Employee on Corder.Id_emp = Employee.Id_Emp
  inner join OrderStatus on Corder.Id_status = OrderStatus.Id_order
  inner join Makers on COrder.ID_MAKE = makers.ID_MAKE
  where Corder.Id_status=3;
exception
when others then
   raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
end showHistoryClientOrders;

procedure addComponentsOrder(idcomp number, idmaker number)
as
begin 
insert into ComponentsOrder(Id_Com, Id_make) values(idcomp, idmaker);
commit;
exception
when others then
   raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
end addComponentsOrder;

end cwPack1;


SET SERVEROUTPUT ON;

declare
ct number;
begin
System.cwpack1.getCountOfClientWithSameLogin('Login', ct);
  dbms_output.put_line('count = '||ct);
end;

begin
--System.cwpack1.addclient('Syatkovskaya Katsiaryna Dmitrievna', 'street Prushinskih, 34', '+375297631738', 'SedSedSed','Password1');
end;


declare
ct3 number;
 BEGIN
  System.cwpack1.checkClientAccount('Sotr4', 'Password1',ct3);
  dbms_output.put_line('count = '||ct3);
end;


declare 
fio nvarchar2(50);
idd number;
begin
System.cwpack1.getClienIdAndName('Login','Passw', idd, fio);
dbms_output.put_line('id = '||idd||' fio '|| fio);
end;


declare
cur sys_refcursor;
   TYPE zz1  IS RECORD(Id_emp number, fullName nvarchar2(50));  -- обязательно надо определить, куда фетчим, это самый скользкий момент
   zz zz1;
    -- можно явно задать zz в виде записи (record)
begin  
     System.cwpack1.getNameAndIdEmp(cur);
     loop
      fetch cur into zz;
     
      EXIT when cur%notfound;
       dbms_output.put_line(zz.Id_emp|| zz.fullName);
    end loop;
  if cur%isopen then
       close cur;
    end if;    
end;


--1 - MAKERS ID
begin
System.cwpack1.addEquipment('Name', '123123','descr of toruble', 1, 'model');
end;


--last eqp id
declare
ct3 number;
 BEGIN
  System.cwpack1.getEpuipmentIdForOrder(ct3);
  dbms_output.put_line('ID_Eqp = '||ct3);
end;


---eq_id cl_id em_id stat_id date
begin
System.cwpack1.makeOrder(1,1,1,1,'10.11.2022')
end;


declare 
cur sys_refcursor;
   TYPE zz1  IS RECORD(id_or number, orderdate date, ename nvarchar2(150), FULLNAME nvarchar2(150), statusname nvarchar2(150));  -- обязательно надо определить, куда фетчим, это самый скользкий момент
   zz zz1;
begin
   System.cwpack1.showCurrentClientOrders(1, cur);
     loop
      fetch cur into zz;
      EXIT when cur%notfound;
       dbms_output.put_line('Id order: ' || zz.id_or ||' Дата Заказа: ' || zz.orderdate|| ' Наименование оборудования: ' || zz.ename || ' Имя исполнителя: ' ||zz.FULLNAME || ' Статус заказа: ' || zz.statusname);
    end loop;
  if cur%isopen then
       close cur;
    end if;    
end;


declare 
cur sys_refcursor;
   TYPE zz1  IS RECORD(id_or number, orderdate date, ename nvarchar2(150), FULLNAME nvarchar2(150), statusname nvarchar2(150), costs number);  
     zz zz1;
begin
   System.cwpack1.showHistoryClientOrders(1, cur);
     loop
      fetch cur into zz;
      EXIT when cur%notfound;
       dbms_output.put_line('Id order: ' || zz.id_or ||' Дата Заказа: ' || zz.orderdate|| ' Наименование оборудования: ' || zz.ename || ' Имя исполнителя: ' ||zz.FULLNAME || ' Статус заказа: ' || zz.statusname||' Стоимость '|| zz.costs);
    end loop;
  if cur%isopen then
       close cur;
    end if;     
end;
-------------------end Client Package-------------------------
--------------------------------------------------------------
--------------------------------------------------------------

---create dir for export--------------------
CREATE OR REPLACE DIRECTORY  DIR AS 'C:\XML\IMPORT';
select * from client

create user C##Client identified by Password123
create user C##Employee identified by Password123

grant execute on System.cwpack1 to  C##Client
grant execute on System.cwpack2 to  C##Employee

select directory_name from all_directories where directory_path = 'C:\XML\IMPORT'


---select fullname, id_vac, passportseria,passportnumber,adress,phonenumber,to_char(startworkdate, 'DD-MM-YYYY') from employee;
--------------------------end body---------------------
EXPLAIN PLAN FOR Select * from Client;
SELECT PLAN_TABLE_OUTPUT FROM TABLE(DBMS_XPLAN.DISPLAY());
