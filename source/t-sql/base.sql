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
    IsActive BIT NOT NULL DEFAULT 1,

    /* Process Information */
    ProcessName VARCHAR(200) NOT NULL DEFAULT '',
    ProcessDescription VARCHAR(MAX),
    IsExecutionExplicitlyRequested BIT NOT NULL DEFAULT 0, 
    LastExecutionTime DATETIME,

    /* Source Connection */
    SourceConnectionTypeId INT REFERENCES DBDeltaWatcher_ConnectionType(Id),
    SourceConnectionStringName VARCHAR(200),

    /* Source Table Description */
    SourceTableName VARCHAR(200),
    /* Mirror Table Description */
    MirrorTableName VARCHAR(200),
    
    /* Transformation Target Connection */
    TransformationTargetConnectionTypeId INT REFERENCES DBDeltaWatcher_ConnectionType(Id),
    TransformationTargetConnectionStringName VARCHAR(200),

    /* Transformation Description */
    OnDeletedRow VARCHAR(MAX),       -- this sql shall be executed for each deleted row
    OnAddedRow   VARCHAR(MAX),       -- this sql shall be executed for each added row
    OnChangedRow VARCHAR(MAX)        -- this sql shall be executed for each changed row
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

INSERT INTO DBDeltaWatcher_Task (
    /* Process Information */
    ProcessName,
    ProcessDescription,

    /* Source Connection */
    SourceConnectionTypeId,
    SourceConnectionStringName,

    /* Source Table Description */
    SourceTableName,
    /* Mirror Table Description */
    MirrorTableName,
    
    /* Transformation Target Connection */
    TransformationTargetConnectionTypeId,
    TransformationTargetConnectionStringName,

    /* Transformation Description */
    OnDeletedRow,
    OnAddedRow,
    OnChangedRow
) VALUES (
    'ExampleSourceTransfer',
    'Perform a delayed update of a statistic from example source',
    1, 
    'LocalhostSQL',
    'ExampleSource',
    'DBDeltaWatcher_Mirror_ExampleSourceTransfer',
    1, 
    'LocalhostSQL',
    'EXEC UpdateExampleStatistic @Old_Name, @Old_Hours * -1',
    'EXEC UpdateExampleStatistic @New_Name, @New_Hours',
    '
    EXEC UpdateExampleStatistic @Old_Name, @Old_Hours * -1; 
    EXEC UpdateExampleStatistic @New_Name, @New_Hours;'
)


