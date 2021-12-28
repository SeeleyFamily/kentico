SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW dbo.View_CMS_Tree_Joined WITH SCHEMABINDING AS 
SELECT C.ClassName, C.ClassDisplayName, T.[NodeID], T.[NodeAliasPath], T.[NodeName], T.[NodeAlias], T.[NodeClassID], T.[NodeParentID], T.[NodeLevel], T.[NodeACLID], T.[NodeSiteID], T.[NodeGUID], T.[NodeOrder], T.[IsSecuredNode], T.[NodeSKUID], T.[NodeLinkedNodeID], T.[NodeOwner], T.[NodeCustomData], T.[NodeLinkedNodeSiteID], T.[NodeHasChildren], T.[NodeHasLinks], T.[NodeOriginalNodeID], T.[NodeIsACLOwner], D.[DocumentID], D.[DocumentName], D.[DocumentModifiedWhen], D.[DocumentModifiedByUserID], D.[DocumentForeignKeyValue], D.[DocumentCreatedByUserID], D.[DocumentCreatedWhen], D.[DocumentCheckedOutByUserID], D.[DocumentCheckedOutWhen], D.[DocumentCheckedOutVersionHistoryID], D.[DocumentPublishedVersionHistoryID], D.[DocumentWorkflowStepID], D.[DocumentPublishFrom], D.[DocumentPublishTo], D.[DocumentCulture], D.[DocumentNodeID], D.[DocumentPageTitle], D.[DocumentPageKeyWords], D.[DocumentPageDescription], D.[DocumentContent], D.[DocumentCustomData], D.[DocumentTags], D.[DocumentTagGroupID], D.[DocumentLastPublished], D.[DocumentSearchExcluded], D.[DocumentLastVersionNumber], D.[DocumentIsArchived], D.[DocumentGUID], D.[DocumentWorkflowCycleGUID], D.[DocumentIsWaitingForTranslation], D.[DocumentSKUName], D.[DocumentSKUDescription], D.[DocumentSKUShortDescription], D.[DocumentWorkflowActionStatus], D.[DocumentCanBePublished], D.[DocumentPageBuilderWidgets], D.[DocumentPageTemplateConfiguration], D.[DocumentABTestConfiguration], D.[DocumentShowInMenu]
FROM dbo.CMS_Tree T INNER JOIN dbo.CMS_Document D ON T.NodeOriginalNodeID = D.DocumentNodeID INNER JOIN dbo.CMS_Class C ON T.NodeClassID = C.ClassID
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
CREATE UNIQUE CLUSTERED INDEX [IX_View_CMS_Tree_Joined_NodeSiteID_DocumentCulture_NodeID] ON [dbo].[View_CMS_Tree_Joined]
(
	[NodeSiteID] ASC,
	[DocumentCulture] ASC,
	[NodeID] ASC
)
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
CREATE NONCLUSTERED INDEX [IX_View_CMS_Tree_Joined_ClassName_NodeSiteID_DocumentForeignKeyValue_DocumentCulture] ON [dbo].[View_CMS_Tree_Joined]
(
	[ClassName] ASC,
	[NodeSiteID] ASC,
	[DocumentForeignKeyValue] ASC,
	[DocumentCulture] ASC
)
GO
