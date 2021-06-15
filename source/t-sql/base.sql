DROP DATABASE DBDeltaWatcher;
GO
CREATE DATABASE DBDeltaWatcher;
GO
USE DBDeltaWatcher;
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
    IsActive BIT,

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

