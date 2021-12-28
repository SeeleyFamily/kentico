/*
	Launchpad K12 -> K13 upgrade script

	WARNING:  BEFORE EXECUTING THIS SCRIPT, BE SURE THAT THE DATABASE HAS BEEN UPGRADED TO K13 VIA KENTICO'S SCRIPT:
	UPGRADE 12 -> 13 UPGRADE.SQL

	IF THIS SCRIPT SHOWS RED SQUIGGLIES IN "ClassHasURL" IN THE QUERY BELOW, THE TARGET DB MAY NOT BE UPGRADED YET.
	(HIT CTRL-SHIFT-R TO REFRESH IF YOU HAVE ALREADY UPGRADED. RUN ONCE NO ERRORS STILL SHOW.)
*/


/* Create the view preprocessing the DocumentCustomData DocumentUrlPath for joining against path searches */
GO

CREATE OR ALTER VIEW [dbo].[View_Custom_DocumentUrlPath]
AS

SELECT
	  NodeID
	, NodeGUID
	, NodeSiteID
	, DocumentCulture
	, [Xml].value( '(/CustomData/DocumentUrlPath)[1]', 'nvarchar(max)' ) AS DocumentUrlPath
FROM View_CMS_Tree_Joined Tree
	CROSS APPLY( SELECT CONVERT( XML, DocumentCustomData, 1 ) ) AS [CustomData]([Xml])

WHERE DocumentCustomData IS NOT NULL


GO


/* Update the example and migrated content page types to have a url pattern so Kentico's new HasUrl() identifies them and they're included in DocumentUrlPath */

UPDATE CMS_Class
SET ClassURLPattern = '/'
	, ClassHasURL = 1
WHERE ClassName IN
(
	'Common.ExampleContent',
	'Common.MigratedContent'
)

GO


/* Update the DocumentUrlPath class URL Pattern to use DocumentCustomData */

UPDATE CMS_Class
SET ClassUrlPattern = '{%DocumentCustomData["DocumentUrlPath"]%}'
WHERE ClassUrlPattern IN
(
	'{% DocumentUrlPath %}',
	'{%DocumentUrlPath%}'
)

GO