CREATE TABLE Teachers
	(
	Id      				UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	Name                    NVARCHAR(255) NOT NULL,
    Surname                 NVARCHAR(255) NOT NULL,
    Description             NVARCHAR(255),
	Rank					INT NOT NULL,
    Faculty                 NVARCHAR(255)
	);

CREATE TABLE Students
	(
	Id      				UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	Name                    NVARCHAR(255) NOT NULL,
    Surname                 NVARCHAR(255) NOT NULL,
    Studies                 NVARCHAR(255) NOT NULL,
    Faculty                 NVARCHAR(255) NOT NULL,
    Description             NVARCHAR(255)
	);

CREATE TABLE TeacherActivities 
	(
	Id				UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	Date			DATETIME NOT NULL,
	CourseId		UNIQUEIDENTIFIER NOT NULL,
	TeacherId		UNIQUEIDENTIFIER NOT NULL,
	Type			INT NOT NULL
	);

CREATE TABLE Courses
	(
	Id				UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	Name			NVARCHAR(255) NOT NULL,
	Credits			INT NOT NULL,
	Type			INT NOT NULL,
	Faculty			NVARCHAR(255) NOT NULL
	);

CREATE TABLE Ratings
	(
	Id						UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	StudentId				UNIQUEIDENTIFIER NOT NULL,
	TeacherID				UNIQUEIDENTIFIER NOT NULL,
	OverallMark				INT NOT NULL,
	LevelOfDifficulty		INT NOT NULL,
	WouldTakeTeacherAgain	BIT NOT NULL,
	Tags					NVARCHAR(255),
	Comment					NVARCHAR(255),
	DateCreated				DATETIME NOT NULL,
	CourseId				UNIQUEIDENTIFIER NOT NULL
	);

CREATE TABLE Comments
	(
	Id						UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	Comment					NVARCHAR(255) NOT NULL,
	Likes					INT NOT NULL,
	Dislikes				INT NOT NULL,
	DateCreated				DATETIME NOT NULL,
	CreatedBy				UNIQUEIDENTIFIER NOT NULL,
	CommentOnId				UNIQUEIDENTIFIER NOT NULL,
	CommentOnType			INT NOT NULL
	);

CREATE TABLE CommentLikes
	(
	CommentId				UNIQUEIDENTIFIER NOT NULL,
	PersonId				UNIQUEIDENTIFIER NOT NULL
	);
