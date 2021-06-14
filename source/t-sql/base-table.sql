CREATE TABLE DBDeltaWatcherConnectionType (
    Id INT NOT NULL PRIMARY KEY IDENTITY,
    [Name] VARCHAR(200) NOT NULL,
    TechnicalIdentifier VARCHAR(200) NOT NULL,
    HasConnectionStringName BIT NOT NULL DEFAULT 1,
    IsActive BIT NOT NULL DEFAULT 1
)

CREATE TABLE DBDeltaWatcherTask (
    Id INT NOT NULL PRIMARY KEY IDENTITY,
    SourceConnectionTypeId INT REFERENCES DBDeltaWatcherConnectionType(Id),
    SourceConnectionStringName VARCHAR(200),
    SourceSQL
)
