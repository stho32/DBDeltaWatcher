/*
    Set up the test database table and the base structure
    for the application
*/

IF (NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'DBDeltaWatcher'))
BEGIN
    CREATE DATABASE DBDeltaWatcher;
END
GO

USE DBDeltaWatcher;
GO

IF (EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'DBDeltaWatcher_Task')) 
BEGIN
    DROP TABLE DBDeltaWatcher_Task
END

GO

IF (EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'DBDeltaWatcher_ConnectionType')) 
BEGIN
    DROP TABLE DBDeltaWatcher_ConnectionType
END

GO

IF (EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ExampleSource')) 
BEGIN
    DROP TABLE ExampleSource
END

GO

IF (EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ExampleStatistic')) 
BEGIN
    DROP TABLE ExampleStatistic
END

GO



IF (EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'DBDeltaWatcher_RegisterProcess')) 
BEGIN
    DROP PROCEDURE DBDeltaWatcher_RegisterProcess
END

GO

IF (EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'DBDeltaWatcher_RegisterProcess')) 
BEGIN
    DROP PROCEDURE DBDeltaWatcher_RegisterProcess
END

IF (EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'UpdateExampleStatistic'))
BEGIN
    DROP PROCEDURE UpdateExampleStatistic
END

GO

CREATE TABLE DBDeltaWatcher_ConnectionType (
    Id INT NOT NULL PRIMARY KEY IDENTITY,
    [Name] VARCHAR(200) NOT NULL,
    TechnicalIdentifier VARCHAR(200) NOT NULL,
    HasConnectionStringName BIT NOT NULL DEFAULT 1,
    HasSQL BIT NOT NULL DEFAULT 1,
    IsActive BIT NOT NULL DEFAULT 1
);

GO

INSERT INTO DBDeltaWatcher_ConnectionType 
    ([Name], TechnicalIdentifier, HasConnectionStringName, HasSQL) 
VALUES 
    ('SQL Server Connection', 'SqlServer', 1, 1);

INSERT INTO DBDeltaWatcher_ConnectionType 
    ([Name], TechnicalIdentifier, HasConnectionStringName, HasSQL) 
VALUES 
    ('MySQL Connection', 'MySQL', 1, 1);

GO


CREATE TABLE DBDeltaWatcher_Task (
    Id INT NOT NULL PRIMARY KEY IDENTITY,
    ProcessName VARCHAR(200) NOT NULL DEFAULT '',
    ProcessDescription VARCHAR(MAX),
    IsActive BIT NOT NULL DEFAULT 1,

    SourceConnectionTypeId INT REFERENCES DBDeltaWatcher_ConnectionType(Id),
    SourceConnectionStringName VARCHAR(200),
    SourceName VARCHAR(200),                    -- like a table name for the source
    SourceSQL VARCHAR(MAX),
    SourceChecksumColumn VARCHAR(200),
    
    DestinationConnectionTypeId INT REFERENCES DBDeltaWatcher_ConnectionType(Id),
    DestinationConnectionStringName VARCHAR(200),
    DestinationOnDeletedRow VARCHAR(MAX),       -- this sql shall be executed for each deleted row
    DestinationOnAddedRow   VARCHAR(MAX),       -- this sql shall be executed for each added row
    DestinationOnChangedRow VARCHAR(MAX),       -- this sql shall be executed for each changed row

    SourceMirrorTableName VARCHAR(200),         -- Name of the mirror which we use to compare
    IsSourceMirrorTableLocationInSource BIT,    -- Mirror table can be created in source and in destination database

    IsExecutionExplicitlyRequested BIT NOT NULL DEFAULT 0, 
    LastExecutionTime DATETIME
);

GO


CREATE TABLE ExampleSource (
    Id INT NOT NULL PRIMARY KEY IDENTITY,
    [Timestamp] DATETIME NOT NULL DEFAULT GETDATE(),
    [Name] VARCHAR(200) NOT NULL,
    [Customer] VARCHAR(200) NOT NULL,
    Hours DECIMAL(15,2) NOT NULL DEFAULT 0,
    IsActive BIT NOT NULL DEFAULT 1
);

GO

INSERT INTO ExampleSource ([Timestamp], Name, Customer, [Hours], IsActive) VALUES (GETDATE(), 'Stefan', 'Ralf', 1, 1);
INSERT INTO ExampleSource ([Timestamp], Name, Customer, [Hours], IsActive) VALUES (GETDATE(), 'Ingo', 'Ralf', 2.5, 1);
INSERT INTO ExampleSource ([Timestamp], Name, Customer, [Hours], IsActive) VALUES (GETDATE(), 'Ingo', 'Ralf', 1, 1);
INSERT INTO ExampleSource ([Timestamp], Name, Customer, [Hours], IsActive) VALUES (GETDATE(), 'Ingo', 'Ralf', 3, 1);

GO
CREATE TABLE ExampleStatistic (
    Id INT NOT NULL PRIMARY KEY IDENTITY,
    [Name] VARCHAR(200) NOT NULL,
    HoursTotal DECIMAL(15,2) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1
)

GO

CREATE PROCEDURE DBDeltaWatcher_RegisterProcess( 
        @ProcessName VARCHAR(200), 
        @ProcessDescription VARCHAR(MAX), 

        @SourceConnectionTypeId INT,
        @SourceConnectionStringName VARCHAR(200),
        @SourceSQL VARCHAR(MAX),
        @SourceChecksumColumn VARCHAR(200),

        @DestinationConnectionTypeId INT, 
        @DestinationConnectionStringName VARCHAR(200), 
        @DestinationOnDeletedRow VARCHAR(MAX), 
        @DestinationOnAddedRow VARCHAR(MAX), 
        @DestinationOnChangedRow VARCHAR(MAX),

        @SourceMirrorTableName VARCHAR(200), 
        @IsSourceMirrorTableLocationInSource BIT
    )
   AS
BEGIN
    /*
        Registers a new process
    */
    INSERT INTO DBDeltaWatcher_Task (
        ProcessName, ProcessDescription, 
        SourceConnectionTypeId, SourceConnectionStringName, SourceSQL, SourceChecksumColumn,
        DestinationConnectionTypeId, DestinationConnectionStringName, DestinationOnDeletedRow, DestinationOnAddedRow, DestinationOnChangedRow,
        SourceMirrorTableName, IsSourceMirrorTableLocationInSource
    ) VALUES (
        @ProcessName, @ProcessDescription,

        @SourceConnectionTypeId, @SourceConnectionStringName, @SourceSQL, @SourceChecksumColumn,

        @DestinationConnectionTypeId, @DestinationConnectionStringName, @DestinationOnDeletedRow, @DestinationOnAddedRow, @DestinationOnChangedRow,

        @SourceMirrorTableName, @IsSourceMirrorTableLocationInSource
    )

END

GO

CREATE PROCEDURE UpdateExampleStatistic(
    @Name VARCHAR(200),
    @IncrementByHours DECIMAL(15,2))
    AS
BEGIN
    /*
        Updates the example statistic
        adds new rows if needed
    */
    IF (EXISTS(SELECT 1 FROM ExampleStatistic WHERE [Name] = @Name))
    BEGIN
        UPDATE ExampleStatistic
           SET HoursTotal = HoursTotal + @IncrementByHours
         WHERE [Name] = @Name
    END 
    ELSE
    BEGIN
        INSERT INTO ExampleStatistic ( [Name], [HoursTotal] )
        VALUES (@Name, @IncrementByHours)
    END
END

GO

DECLARE @ProcessName varchar(200) = 'ExampleSourceTransfer'
DECLARE @ProcessDescription varchar(max) = 'Passivly create a statistic from example source'
DECLARE @SourceConnectionTypeId int = 1
DECLARE @SourceConnectionStringName varchar(200) = 'LocalhostSQL'
DECLARE @SourceSQL varchar(max) = 'SELECT *, CHECKSUM(Id) AS CheckSumColumn FROM ExampleSource'
DECLARE @SourceChecksumColumn varchar(200) = 'CheckSumColumn'
DECLARE @DestinationConnectionTypeId int = 1
DECLARE @DestinationConnectionStringName varchar(200) = 'LocalhostSQL'
DECLARE @DestinationOnDeletedRow varchar(max) = 'EXEC UpdateExampleStatistic @Old_Name, @Old_Hours * -1'
DECLARE @DestinationOnAddedRow varchar(max) = 'EXEC UpdateExampleStatistic @New_Name, @New_Hours'
DECLARE @DestinationOnChangedRow varchar(max) = '
    EXEC UpdateExampleStatistic @Old_Name, @Old_Hours * -1 ; 
    EXEC UpdateExampleStatistic @New_Name, @New_Hours;
'
DECLARE @SourceMirrorTableName varchar(200) = 'DBDeltaWatcher_Mirror_ExampleSourceTransfer'
DECLARE @IsSourceMirrorTableLocationInSource bit = 0

EXECUTE [dbo].[DBDeltaWatcher_RegisterProcess] 
   @ProcessName
  ,@ProcessDescription
  ,@SourceConnectionTypeId
  ,@SourceConnectionStringName
  ,@SourceSQL
  ,@SourceChecksumColumn
  ,@DestinationConnectionTypeId
  ,@DestinationConnectionStringName
  ,@DestinationOnDeletedRow
  ,@DestinationOnAddedRow
  ,@DestinationOnChangedRow
  ,@SourceMirrorTableName
  ,@IsSourceMirrorTableLocationInSource
GO
