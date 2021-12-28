/* Clear out sync tasks first using the BEFORE CUTBACK SCRIPT */
/* Cutting from any environment to DEV will require a manual entry for the DEV_TO_TEST Staging Server Configuration */

/* Update Staging Server Name and URL */
UPDATE [Staging_Server]
SET ServerDisplayName = 'DEV_TO_QA',
ServerName = 'QA', 
ServerURL = 'https://cx-lp-kentico-cms-as-qa.azurewebsites.net' -- CHANGE THIS VALUE
WHERE ServerSiteID = 1;

/* Update Site Domain and Presentation URL */
UPDATE CMS_Site
SET SiteDomainName = 'cx-lp-kentico-cms-as-dev.azurewebsites.net', -- CHANGE THIS VALUE
SitePresentationURL = 'https://cx-lp-launchpad-kentico-mvc-as-dev.azurewebsites.net', -- CHANGE THIS VALUE
SiteDescription = 'Presentation URL:  https://localhost:44324  https://cx-lp-launchpad-kentico-mvc-as-dev.azurewebsites.net    Administration domain name:  localhost:44347  cx-lp-kentico-cms-as-dev.azurewebsites.net' -- CHANGE THIS VALUE
WHERE SiteID=1;
 
/* Update Search Index, will need to manually kick off rebuild though - Also the encrypted keys are available from the database */ 
 
UPDATE [CMS_SearchIndex]
SET 
IndexName = 'cxlp-kentico-sqki-lp-global-dev', -- CHANGE THIS VALUE
IndexDisplayName = 'cxlp-kentico-sqki-lp-global-dev', -- CHANGE THIS VALUE
IndexAdminKey = 'o4taTtuZ9vMwDrZejQ5QiQ9wW0fda/YC2EWfFci9CgJabAxwZiMiPpptaYoHNFaa', -- CHANGE THIS VALUE
IndexQueryKey = 'YY7qave+XCGzleDTxD8F+FMBzGGWYC6ArJGDJq5aopl4+4/X/hheNqBlk9V7DL0S' -- CHANGE THIS VALUE
WHERE IndexID = 2 -- CHANGE THIS VALUE
;