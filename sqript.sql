
CREATE TABLE Department
(
	DepartmentID int primary key IDENTITY(1,1) not null,
	DepartmentName varchar(255) not null
);

CREATE TABLE Position
(
	PositionID int primary key IDENTITY(1,1) not null,
	PositionName varchar(255) 	
);

CREATE TABLE Teacher
(	
	TeacherID int primary key IDENTITY(1,1) not null,
	FirstName varchar(255) not null,
	LastName varchar(255) not null,
	Patronym varchar(255) not null,
	DepartmentID int NOT NULL,
	PositionID int NOT NULL,
	foreign key (DepartmentID) references Department(DepartmentID ),
	foreign key (PositionID) references Position(PositionID)
);


CREATE TABLE Discipline
(	
	DisciplineID int primary key IDENTITY(1,1) not null,
	DisciplineName varchar(255) 
);


CREATE TABLE Speciality
(
	Code int primary key IDENTITY(1,1) NOT NULL,
	SpecialityName varchar(255) NOT NULL
);

CREATE TABLE StudyGroup
(
	GroupNumber int primary key NOT NULL,
	SpecialityCode int NOT NULL,
	fulltime bit NOT NULL, --1 for full, 0 for evening
	semester varchar(30) NOT NULL,
	foreign key (SpecialityCode)  references Speciality(Code)
);

CREATE TABLE GroupStudies
(
	ID int primary key IDENTITY(1,1) not null,
	ClassType varchar(255) NOT NULL	
);

CREATE TABLE GroupLoad
(
	ID int primary key IDENTITY(1,1) NOT NULL,
	TeacherID int NOT NULL,
	GroupNumber int NOT NULL,
	DisciplineID int NOT NULL,
	ClassTypeID int NOT NULL,
	VolumeHours int NOT NULL,
	foreign key (TeacherID) references Teacher(TeacherID),
	foreign key (DisciplineID) references Discipline(DisciplineID),
	foreign key (GroupNumber) references StudyGroup(GroupNumber),
	foreign key (ClassTypeID) references GroupStudies(ID)
);

CREATE TABLE IndividualStudies
(
	ID int primary key IDENTITY(1,1) not null,
	IndividualClassType varchar(255) NOT NULL,
	VolumeByPerson int not null
);

CREATE TABLE PersonalLoad
(
	ID int primary key IDENTITY(1,1) NOT NULL,
	TeacherID int NOT NULL,
	IndividualClassTypeID int NOT NULL,	
	StudentsCount int NOT NULL,
	foreign key (TeacherID) references Teacher(TeacherID),
	foreign key (IndividualClassTypeID) references IndividualStudies(ID)
);

insert into Discipline values ('Базы Данных')








