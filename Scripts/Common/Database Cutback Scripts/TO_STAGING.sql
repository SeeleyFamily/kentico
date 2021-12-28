/* Clear out sync tasks first using the BEFORE CUTBACK SCRIPT */

/* Update Staging Server Name and URL */
DELETE FROM [Staging_Server]
WHERE ServerDisplayName = 'DEV_TO_TEST';

UPDATE [Staging_Server]
SET ServerDisplayName = 'STAGING_TO_PROD',
ServerName = 'PROD', 
ServerURL = 'https://cx-lp-kentico-cms-as-prod.azurewebsites.net' -- CHANGE THIS VALUE
WHERE ServerSiteID = 1;

/* Update Site Domain and Presentation URL */
UPDATE CMS_Site
SET SiteDomainName = 'cx-lp-kentico-cms-as-staging.azurewebsites.net', -- CHANGE THIS VALUE
SitePresentationURL = 'https://cx-lp-launchpad-kentico-mvc-as-staging.azurewebsites.net', -- CHANGE THIS VALUE
SiteDescription = '' -- CHANGE THIS VALUE
WHERE SiteID=1;
 
/* Update Search Index, will need to manually kick off rebuild though - Also the encrypted keys are available from the database */ 
 
UPDATE [CMS_SearchIndex]
SET 
IndexName = 'cxlp-kentico-sqki-lp-global-staging', -- CHANGE THIS VALUE
IndexDisplayName = 'cxlp-kentico-sqki-lp-global-staging', -- CHANGE THIS VALUE
IndexAdminKey = 'o4taTtuZ9vMwDrZejQ5QiQ9wW0fda/YC2EWfFci9CgJabAxwZiMiPpptaYoHNFaa', -- CHANGE THIS VALUE
IndexQueryKey = 'eP69qDc/FDrJ2c/nAhtTgQqQ9nBg7sXTp33hNWv4cfosNT9nFVzvR3vwDmQoCv3V' -- CHANGE THIS VALUE
WHERE IndexID = 2 -- CHANGE THIS VALUE
;