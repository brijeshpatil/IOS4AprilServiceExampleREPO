create database IOS4AprilServiceExampleDB
use IOS4AprilServiceExampleDB

create table StateTbl
(
	StateID int primary key identity,
	StateName varchar(50)
)

create table CityTbl
(
	CityID int primary key identity,
	CityName varchar(50),
	FkStateID int references StateTbl(StateID)
)

create table UserInfo
(
	userid int primary key identity,
	fname varchar(50),
	lname varchar(50),
	fkstateid int references StateTbl(StateID),
	fkcityid int references CityTbl(CityID),
	uname varchar(50),
	pass varchar(50)
)