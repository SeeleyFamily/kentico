-- These selects will show if the instance is bogged down by web farm tasks
SELECT TOP 10 *
FROM CMS_SearchTask
WHERE [SearchTaskStatus] IN (N'ready', N'error')
ORDER BY SearchTaskPriority DESC, SearchTaskID

select * from CMS_WebFarmServerTask
select * from CMS_WebFarmTask
select * from CMS_SearchTask
select * from CMS_SearchTask;

delete from CMS_WebFarmServerTask;
delete from CMS_WebFarmTask;
delete from CMS_WebFarmServer; 
delete from CMS_SearchTask; 