USE Master;
GO

CREATE LOGIN Testuser WITH PASSWORD = 'Start123!';
GO

ALTER LOGIN Testuser ENABLE;
GO

CREATE DATABASE Test;
GO

USE Test;
GO

CREATE USER Testuser FOR LOGIN Testuser WITH DEFAULT_SCHEMA=[DBO];
GO

EXEC sp_addrolemember N'db_owner', N'Testuser'
GO

CREATE TABLE TestTable (
    Id INT NOT NULL PRIMARY KEY IDENTITY,
    SomeString VARCHAR(200) NOT NULL DEFAULT '',
    LongString VARCHAR(MAX), 
    NumberColumn INT,
    DecimalColumn DECIMAL(15,4),
    BooleanColumn BIT
);



