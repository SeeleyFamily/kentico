-- Checks for missing metadata
-- page description and page title
select  documentpagetitle,documentpagedescription, Documenturlpath,* from view_cms_tree_joined where
((documentpagetitle is null OR documentpagetitle ='') OR (DocumentPageDescription is null OR DocumentPageDescription =''))
and nodealiaspath like '%locations/%' 
and nodealiaspath not like '%/location-specific-global-content%'
and documentworkflowstepid != 256
and documentworkflowstepid != 20
and nodealias !='military-appreciation-program'
and nodealias !='new-patient-forms'
and documenturlpath !=''
and documenturlpath is not null
and nodesiteid=1