CREATE DATABASE CodeHub
GO
USE CodeHub
GO
CREATE TABLE Users(
	ID INT IDENTITY PRIMARY KEY,
	Username NVARCHAR(50) NOT NULL UNIQUE,
	PasswordHash VARCHAR(255) NOT NULL,
	Avatar VARCHAR(255),
	FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
	Email NVARCHAR(255) NOT NULL UNIQUE,
	Gender BIT,
	DateOfBirth DATE,
	Currency INT DEFAULT 0,
	IsActive BIT DEFAULT 1
);
GO
CREATE TABLE DepositHistory (
    ID INT IDENTITY PRIMARY KEY,
    UserID INT FOREIGN KEY REFERENCES Users(ID),
    Amount INT NOT NULL,
	TransactionType BIT, 
	Note NVARCHAR(255),
    TransactionDate DATETIME DEFAULT GETDATE()
);
GO
CREATE TABLE Managers (
    ID INT IDENTITY PRIMARY KEY,
	Username NVARCHAR(50) NOT NULL UNIQUE,
	PasswordHash VARCHAR(255) NOT NULL,
	Avatar VARCHAR(255),
	FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
	Email NVARCHAR(255) NOT NULL UNIQUE,
    Gender BIT,
	DateOfBirth DATE,
    Role BIT DEFAULT 1,
	IsActive BIT DEFAULT 1
);
GO
CREATE TABLE Types (
    ID INT IDENTITY PRIMARY KEY,
	Name NVARCHAR(255) NOT NULL
);
GO
CREATE TABLE Languages (
    ID INT IDENTITY PRIMARY KEY,
	Name NVARCHAR(255) NOT NULL
);
GO
CREATE TABLE SourceCodes (
    ID INT IDENTITY PRIMARY KEY,
	Coder INT NOT NULL,
	Name NVARCHAR(255) NOT NULL,
	LanguageID INT FOREIGN KEY REFERENCES Languages(ID),
	TypeID INT FOREIGN KEY REFERENCES Types(ID),
	LinkVideo VARCHAR(255),
	Description NVARCHAR(MAX),
	SourceLink VARCHAR(MAX),
	Fee INT NOT NULL,
	IsShow BIT DEFAULT 1,
	IsDelete BIT DEFAULT 0,
	FOREIGN KEY (Coder) REFERENCES Managers(ID)
);
GO
CREATE TABLE DetailCodes (
	ID INT IDENTITY PRIMARY KEY,
	Source INT FOREIGN KEY REFERENCES SourceCodes(ID) NOT NULL,
	Views INT DEFAULT 0,
    Purchases INT DEFAULT 0
);
GO
CREATE TABLE ImageUrls (
	ID INT IDENTITY PRIMARY KEY,
    Source INT FOREIGN KEY REFERENCES SourceCodes(ID) NOT NULL,
	Url VARCHAR(255)
);
GO
CREATE TABLE ReportTypes (
    ID INT IDENTITY PRIMARY KEY,
	Name NVARCHAR(255) NOT NULL
);
GO
CREATE TABLE Reports (
    ID INT IDENTITY PRIMARY KEY,
	ReportTypeID INT FOREIGN KEY REFERENCES ReportTypes(ID),
	Reporter INT FOREIGN KEY REFERENCES Users(ID),
	Source INT FOREIGN KEY REFERENCES SourceCodes(ID),
	CreatedDate DATETIME DEFAULT GETDATE()
);
GO
CREATE TABLE Requests (
    ID INT IDENTITY PRIMARY KEY,
	Name NVARCHAR(255),
	Description TEXT,
	Requester INT FOREIGN KEY REFERENCES Users(ID),
	Coder INT FOREIGN KEY REFERENCES Managers(ID),
	CreatedDate DATETIME DEFAULT GETDATE()
	Status INT
);
GO
CREATE TABLE Feedbacks (
    ID INT IDENTITY PRIMARY KEY,
	Email VARCHAR(255),
	Username NVARCHAR(255),
	Description NVARCHAR(255),
);
GO
CREATE TABLE Carts(
	ID INT IDENTITY PRIMARY KEY,
	UserID INT FOREIGN KEY REFERENCES Users(ID),
	CodeID INT FOREIGN KEY REFERENCES SourceCodes(ID),
);
GO
CREATE TABLE Orders(
	ID INT IDENTITY PRIMARY KEY,
	UserID INT FOREIGN KEY REFERENCES Users(ID),
	CodeID INT FOREIGN KEY REFERENCES SourceCodes(ID),
	Fee INT NOT NULL,
	DateCreated DATETIME DEFAULT GETDATE()
);
GO
CREATE TABLE Reviews (
	ID INT IDENTITY PRIMARY KEY,
	UserID INT FOREIGN KEY REFERENCES Users(ID),
	CodeID INT FOREIGN KEY REFERENCES SourceCodes(ID),
	Description NVARCHAR(255),
	StarRating INT,
	DateCreated DATETIME DEFAULT GETDATE()
);
GO
CREATE TABLE Notifications (
	ID INT IDENTITY PRIMARY KEY,
	UserID INT FOREIGN KEY REFERENCES Users(ID),
	Coder INT FOREIGN KEY REFERENCES Managers(ID),
	Description NVARCHAR(255),
	DateCreated DATETIME DEFAULT GETDATE()
);