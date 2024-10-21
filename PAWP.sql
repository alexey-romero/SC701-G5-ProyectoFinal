CREATE DATABASE PAWP1;
GO;
USE PAWP1;
GO;
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,
    LastName NVARCHAR(255) NOT NULL,
    SecondLastName NVARCHAR(255),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    Status NVARCHAR(18),
    CreatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE Roles (
    RoleId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL UNIQUE,
    Status NVARCHAR(18) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);


CREATE TABLE User_Roles (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    RoleId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);


CREATE TABLE Widgets (
    WidgetId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255) NULL,
    APIEndpoint NVARCHAR(255) NOT NULL,
    RequiresApiKey BIT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UserId INT, -- Propietario del Widget
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

CREATE TABLE User_Widgets (
    UserWidgetId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    WidgetId INT NOT NULL,
    PositionX INT NOT NULL,
    PositionY INT NOT NULL,
    IsFavorite BIT DEFAULT 0,
    IsVisible BIT DEFAULT 1, -- Para ocultar o mostrar widgets
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (WidgetId) REFERENCES Widgets(WidgetId)
);


CREATE TABLE Configurations (
    ConfigurationId INT PRIMARY KEY IDENTITY(1,1),
    UserWidgetId INT NOT NULL,
    ApiKey NVARCHAR(255) NULL,
    Settings NVARCHAR(MAX) NULL,
    RefreshInterval INT DEFAULT 60, 
    Width INT DEFAULT 300,
    Height INT DEFAULT 150,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserWidgetId) REFERENCES User_Widgets(UserWidgetId)
);


CREATE TABLE Widget_Categories (
    CategoryId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL UNIQUE
);


CREATE TABLE Widget_Category (
    Id INT PRIMARY KEY IDENTITY(1,1),
    WidgetId INT NOT NULL,
    CategoryId INT NOT NULL,
    FOREIGN KEY (WidgetId) REFERENCES Widgets(WidgetId),
    FOREIGN KEY (CategoryId) REFERENCES Widget_Categories(CategoryId)
);