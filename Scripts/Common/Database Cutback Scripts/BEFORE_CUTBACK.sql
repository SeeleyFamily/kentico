/*
Before Cutting the database, clear out all staging tasks.
*/
DELETE FROM [Staging_Synchronization];

DELETE FROM [Staging_TaskUser];

DELETE FROM [staging_TaskGroupTask];

DELETE FROM [Staging_Task];

SELECT * FROM [Staging_Synchronization];

SELECT * FROM [Staging_TaskUser];

SELECT * FROM [Staging_Task];

DELETE FROM [CMS_EventLog];

SELECT * FROM [CMS_EventLog];