
# Change Log
All notable changes to this project will be documented in this file.
 
# Notes
The format is based on [Keep a Changelog](httpkeepachangelog.com)
and this project adheres to [Semantic Versioning](httpsemver.org).
See [example](https://gist.githubusercontent.com/juampynr/4c18214a8eb554084e21d6e288a18a2c/raw/6d61b1ced1c66349cf9ef6ce5eb84546ebf6e79d/CHANGELOG.md)

## [Unreleased] - yyyy-mm-dd

## [v2.0.2] - 2021-12-21

### Changed
- [Kentico v13.0.56](https://devnet.kentico.com/download/hotfixes)
  Use Scripts -> Common -> Hotfix_default.sql
  
## [v2.0.1] - 2021-12-04

### Changed
- [Kentico v13.0.54](https://devnet.kentico.com/download/hotfixes)
  Use Scripts -> Common -> Hotfix_default.sql
- Separated Common vs Custom Front End

## [v1.3.0] - 2021-10-01

### Changed
- [Kentico v13.0.46](https://devnet.kentico.com/download/hotfixes)
  Use Scripts -> Common -> Hotfix_default.sql

## [v1.2.3] - 2021-07-29

### Changed
- [Kentico v13.0.37](https://devnet.kentico.com/download/hotfixes)
  Use Scripts -> Common -> Hotfix_default.sql

## [v1.2.2] - 2021-07-20

### Added
- Added Icon Type and Custom Table for Custom Icons

### Changed
- [Kentico v13.0.35](https://devnet.kentico.com/download/hotfixes)
  Use Scripts -> Common -> Hotfix_default.sql

## [v1.2.1] - 2021-07-09

### Changed
- Updated View_Custom_DocumentUrlPath to include NodeSiteID for multi-site compatability
  Use Scripts -> Common -> Rise - LP K12 to K13.sql.sql

## [v1.2.0] - 2021-06-15

### Changed
- [Kentico v13.0.30](https://devnet.kentico.com/download/hotfixes)
  Use Scripts -> Common -> Hotfix_default.sql


## [v1.1.8] - 2021-06-07
 
### Added
- Added new common page types that will need to be imported into the Site.
- Common.SiteSettings - Use Exports -> Common -> export_20210524_1521_Common_SiteSettings.zip
- Common.BaseContent & Common.BasePage - Use Exports -> Common -> export_20210607_1631_Common_BaseContent_BasePage.zip
- Added SiteSettingsService to LayoutProvider. Projects may need to update custom LayoutProvider to include SiteSettingService in the ctor.
### Changed
- [Kentico v13.0.29](https://devnet.kentico.com/download/hotfixes)
  Use Scripts -> Common -> Hotfix_default.sql
  
- CommonWidgetProperties has been changed to BaseWidgetPropeties, but all new Widgets should inherit from WidgetProperties

- CommonSectionProperties has been changed to BaseSectionPropeties, but all new Widgets should inherit from SectionProperties

### Fixed
- [NodeAlias and NodeName lengths](https://riseinteractive.atlassian.net/wiki/spaces/webdev/pages/2629140485/NodeAlias%2BNodeName%2BMax%2BLength)
  Use Scripts -> Common -> Database_NodeAlias_NodeName_Length.sql
  
#Example
 
## [x.y.z] - yyyy-mm-dd
 
### Added
   
### Changed
 
### Fixed
 
- [PROJECTNAME-UUUU](httptickets.projectname.combrowsePROJECTNAME-UUUU)
  MINOR Fix module foo tests
- [PROJECTNAME-RRRR](httptickets.projectname.combrowsePROJECTNAME-RRRR)
  MAJOR Module foo's timeline uses the browser timezone for date resolution 