/* Clear out sync tasks first using the BEFORE CUTBACK SCRIPT */

/* Update Staging Server Name and URL */
DELETE FROM [Staging_Server]
;

/* Update Site Domain and Presentation URL */
UPDATE CMS_Site
SET SiteDomainName = 'cx-lp-kentico-cms-as-test.azurewebsites.net', -- CHANGE THIS VALUE
SitePresentationURL = 'https://cx-lp-launchpad-kentico-mvc-as-test.azurewebsites.net', -- CHANGE THIS VALUE
SiteDescription = '' -- CHANGE THIS VALUE
WHERE SiteID=1;
 
/* Update Search Index, will need to manually kick off rebuild though - Also the encrypted keys are available from the database */ 
 
UPDATE [CMS_SearchIndex]
SET 
IndexName = 'cxlp-kentico-sqki-lp-global-test', -- CHANGE THIS VALUE
IndexDisplayName = 'cxlp-kentico-sqki-lp-global-test', -- CHANGE THIS VALUE
IndexAdminKey = 'o4taTtuZ9vMwDrZejQ5QiQ9wW0fda/YC2EWfFci9CgJabAxwZiMiPpptaYoHNFaa', -- CHANGE THIS VALUE
IndexQueryKey = '2PRUhPysohO5UGbYwnYw0daPxvm0RQ5JLDawn85nJSkkek4TD/ilOVuEcFem4qlc' -- CHANGE THIS VALUE
WHERE IndexID = 2 -- CHANGE THIS VALUE
;