SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[View_Custom_Acl_Items_Expanded]
--WITH SCHEMABINDING
AS

SELECT Item.ACLID, Item.UserID, Item.RoleID, Item.Allowed, Item.Denied
FROM dbo.CMS_ACLItem Item

UNION

SELECT Acl.ACLID, Item.UserID, Item.RoleID, Item.Allowed, Item.Denied
FROM dbo.CMS_ACL Acl
	LEFT JOIN dbo.CMS_ACLItem Item ON Item.ACLID IN ( SELECT * FROM string_split( Acl.ACLInheritedACLs, ',' ) )
WHERE ACLInheritedACLs IS NOT NULL
AND ACLInheritedACLs <> ''

GO