/*
The following statements ensure that all permanent and temporary redirects start with a /
*/

select * from Redirects_PermanentRedirects where LEFT(MatchURL,1) != '/';
select * from Redirects_TemporaryRedirects where LEFT(MatchURL,1) != '/';

UPDATE Redirects_PermanentRedirects SET MatchUrl = '/'+ MatchUrl where LEFT(MatchURL,1) != '/';
UPDATE Redirects_TemporaryRedirects SET MatchUrl = '/'+ MatchUrl where LEFT(MatchURL,1) != '/';