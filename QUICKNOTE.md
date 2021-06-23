SELECT * FROM ExampleSource
SELECT * FROM DBDeltaWatcher_Task

SELECT *, CHECKSUM(*) AS CheckSumColumn FROM ExampleSource

CREATE TABLE DBDeltaWatcher_Mirror_ExampleSourceTransfer (
  <allColumnsWithUnderscore, including Old_id as primary key>,
  Old_CheckSumColumn <- saved CHECKSUM
)

/* Changed */
SELECT *
  FROM ExampleSource es
  JOIN DBDeltaWatcher_Mirror_ExampleSourceTransfer dm ON es.Id = dm.Old_Id
 WHERE CHECKSUM(es.*) <> dm._CheckSumColumn

/* Added */
SELECT *
  FROM ExampleSource es
  LEFT JOIN DBDeltaWatcher_Mirror_ExampleSourceTransfer dm ON es.Id = dm.Old_Id
 WHERE dm.Old_Id IS NULL

/* Deleted */
SELECT *
  FROM DBDeltaWatcher_Mirror_ExampleSourceTransfer dm 
  LEFT JOIN ExampleSource es ON es.Id = dm.Old_Id
 WHERE es.Id IS NULL

/* Update Mirror Synchronous */
/* ADD */
BEGIN TRANSACTION
INSERT ...
-- + DestinationOnAddedRow-SQL
COMMIT
...


