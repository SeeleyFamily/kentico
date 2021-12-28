/* Temp_FormDefinition table serves for (alt)form definition modifications and is removed by HotfixProcedure */
IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Temp_FormDefinition'))
BEGIN
    CREATE TABLE [Temp_FormDefinition]
    (
        [TempID] [int] NOT NULL IDENTITY (1, 1),
        [ObjectName] [nvarchar] (200) NOT NULL,
        [FormDefinition] [nvarchar] (max) NULL,
        [IsAltForm] [bit] NULL,
        CONSTRAINT [PK_Temp_FormDefinition] PRIMARY KEY CLUSTERED ([TempID])
    ) ON [PRIMARY];
END
ELSE
BEGIN
    DELETE FROM [Temp_FormDefinition];
END
GO

DECLARE @HOTFIXVERSION INT;
SET @HOTFIXVERSION = (SELECT [KeyValue] FROM [CMS_SettingsKey] WHERE [KeyName] = N'CMSHotfixVersion')
IF @HOTFIXVERSION < 4
BEGIN

INSERT INTO [Temp_FormDefinition] ([ObjectName], [FormDefinition], [IsAltForm])
VALUES ('media.library',
        '<form version="2"><field column="LibraryID" columntype="integer" guid="00fdb6b0-5f2e-4ef9-8648-1d3c7af8b721" isPK="true" system="true"><properties><fieldcaption>LibraryID</fieldcaption></properties><settings><controlname>labelcontrol</controlname></settings></field><field column="LibraryDisplayName" columnsize="250" columntype="text" guid="9b4aa82c-02df-4712-a5b9-3b9dee377b45" system="true" translatefield="true" visible="true"><properties><fieldcaption>{$general.displayname$}</fieldcaption><fielddescription>{$medialibrary.librarydisplayname.description$}</fielddescription></properties><settings><controlname>localizabletextbox</controlname><ValueIsContent>False</ValueIsContent></settings></field><field column="LibraryName" columnsize="250" columntype="text" guid="e4081abf-652e-47bd-82d0-313752f01873" isunique="true" system="true" visible="true"><properties><fieldcaption>{$general.codename$}</fieldcaption><fielddescription>{$medialibrary.libraryname.description$}</fielddescription></properties><settings><controlname>codename</controlname></settings></field><field allowempty="true" column="LibraryDescription" columntype="longtext" guid="ac965989-ec7a-446f-81bb-ccb5043abf0b" system="true" translatefield="true" visible="true"><properties><fieldcaption>{$general.description$}</fieldcaption><fielddescription>{$medialibrary.librarydescription.description$}</fielddescription></properties><settings><controlname>LocalizableTextArea</controlname><ValueIsContent>False</ValueIsContent></settings></field><field allowempty="true" column="LibraryTeaserGUID" columntype="guid" guid="1cf86270-1d15-4a07-a3fb-4d97ba972c46" system="true" visible="true"><properties><fieldcaption>{$media.general.teaser$}</fieldcaption></properties><settings><controlname>metafileuploadercontrol</controlname><ObjectCategory>Thumbnail</ObjectCategory></settings></field><field column="LibraryFolder" columnsize="250" columntype="text" guid="06f6b6a9-08ca-4735-8732-20cc75d11802" system="true" visible="true"><properties><enabledmacro ismacro="true">{%FormMode == FormModeEnum.Insert|(user)administrator|(hash)0a89b968eb862d72bdc749e4095716028feb4b47d8cca3d195452ef627679385%}</enabledmacro><fieldcaption>{$general.foldername$}</fieldcaption><fielddescription>{$medialibrary.libraryfolder.description$}</fielddescription></properties><settings><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim></settings></field><field allowempty="true" column="LibraryAccess" columntype="integer" guid="720d2865-0be4-43d3-8ed8-412b269b1d00" system="true"><properties><fieldcaption>LibraryAccess</fieldcaption></properties><settings><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim></settings></field><field column="LibrarySiteID" columntype="integer" guid="99746a1e-3514-4c13-b878-7bb7b39ddb3d" system="true"><properties><defaultvalue ismacro="true">{%CurrentSite.SiteID|(user)administrator|(hash)f1f36b57555be68f4a774a617ef7f4b296d321c24bd971a578ae7443c86b1480%}</defaultvalue><fieldcaption>LibrarySiteID</fieldcaption></properties><settings><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim></settings></field><field allowempty="true" column="LibraryGUID" columntype="guid" guid="512c11dd-a325-4bf2-b1be-4f7c54397132" system="true"><properties><fieldcaption>LibraryGUID</fieldcaption></properties><settings><controlname>labelcontrol</controlname></settings></field><field allowempty="true" column="LibraryLastModified" columntype="datetime" guid="54f9c352-5ac2-4e4b-ab98-65d41240e9e4" system="true"><properties><fieldcaption>LibraryLastModified</fieldcaption></properties><settings><controlname>calendarcontrol</controlname></settings></field><field allowempty="true" column="LibraryTeaserPath" columnsize="450" columntype="text" guid="5de9f8d9-3e2d-4d83-8a63-6c2bdfb76629" system="true"><properties><fieldcaption>LibraryTeaserPath</fieldcaption></properties><settings><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim></settings></field><field allowempty="true" column="LibraryUseDirectPathForContent" columntype="boolean" guid="b84913d8-82f6-438e-abc2-f6e6c7e9e7b2" system="true" visible="true"><properties><fieldcaption>{$medialibrary.libraryusedirectpathforcontent.name$}</fieldcaption><fielddescription>{$medialibrary.libraryusedirectpathforcontent.description$}</fielddescription></properties><settings><controlname>CheckBoxControl</controlname></settings></field></form>',
        0);

END
GO

DECLARE @HOTFIXVERSION INT;
SET @HOTFIXVERSION = (SELECT [KeyValue] FROM [CMS_SettingsKey] WHERE [KeyName] = N'CMSHotfixVersion')
IF @HOTFIXVERSION < 10
BEGIN

UPDATE [CMS_UIElement] SET
		[ElementVisibilityCondition] = '{%EditedObject.ClassHasURL || EditedObject.ClassIsCoupledClass|(identity)GlobalAdministrator|(hash)7510bbac5902b136c8ecfa300cba0e08e2c1b3457819de41eb44e239583bc13a%}'
	WHERE [ElementGUID]  = '39931a03-68ce-46fd-9ee3-1e019ff18826'

END
GO

DECLARE @HOTFIXVERSION INT;
SET @HOTFIXVERSION = (SELECT [KeyValue] FROM [CMS_SettingsKey] WHERE [KeyName] = N'CMSHotfixVersion')
IF @HOTFIXVERSION < 13
BEGIN

INSERT INTO [Temp_FormDefinition] ([ObjectName], [FormDefinition], [IsAltForm])
VALUES ('newsletter.emailtemplate.EditTemplateCode',
		'<form version="2"><field column="TemplateID" guid="1fc8d3e4-5b6f-4a8b-9266-42cfdd739d2d" /><field column="TemplateDisplayName" guid="d3505546-d312-4dda-aacc-cf1dfcb7bab3" visible="" /><field column="TemplateName" guid="d138a78c-5095-4778-a5a1-6c0e9d9c226e" visible="" /><field column="TemplateDescription" guid="96044a64-f381-440a-b613-673b845278a5" visible="" /><field column="TemplateInlineCSS" guid="8208c709-9f19-487e-b0a7-eb912316b1dd" visible="" /><field column="TemplateThumbnailGUID" guid="0f72ba1b-8b84-44a8-85a6-27107135071d" /><field column="TemplateIconClass" guid="b885071c-c113-44f1-8a94-d77a2f019cd9" /><field column="TemplateCode" guid="55b5079a-5163-4606-baeb-40e59c8f8aae" visible="true"><settings><AutoSize>False</AutoSize><controlname>MacroEditor</controlname><EnablePositionMember>False</EnablePositionMember><EnableSections>False</EnableSections><EnableViewState>False</EnableViewState><Height>600</Height><Language>7</Language><ResolverName ismacro="true">{%
if (TemplateType == "I") /* using I as email Issue, this is defined in EmailTemplateTypeEnum */
  { "NewsletterResolver" }
else
  { "NewsletterConfirmationResolver" }
|(identity)GlobalAdministrator|(hash)7c7c10639461cfc02ce1352aaf617fc835cae60a9df72d7d6b97d8b2d220fe8b%}</ResolverName><ShowBookmarks>False</ShowBookmarks><ShowLineNumbers>True</ShowLineNumbers><SingleLineMode>False</SingleLineMode><SingleMacroMode>False</SingleMacroMode><SupportPasteImages>False</SupportPasteImages><Width>100%</Width></settings></field><field column="TemplateSiteID" guid="e8548e32-9ba1-4093-b00e-b6ab09d374e2" /><field column="TemplateType" guid="721c9b4a-60e2-4e9e-beef-8ca0e5612a89" visible="" /><field column="TemplateGUID" guid="4bf16188-eaa3-4b2a-bc44-897706226bae" /><field column="TemplateLastModified" guid="951b6ddb-4c52-4d94-a7dd-601f67b3658c" /><field column="TemplateSubject" guid="0eaddd8f-5a59-4873-afb1-80a7156918a6" /></form>',
		1);

END
GO

DECLARE @REFRESHVERSION INT = 16;

DECLARE @HOTFIXVERSION INT;
SET @HOTFIXVERSION = (SELECT [KeyValue] FROM [CMS_SettingsKey] WHERE [KeyName] = N'CMSHotfixVersion')
IF @HOTFIXVERSION < @REFRESHVERSION
BEGIN
    UPDATE CMS_Class
    SET ClassFormDefinition = '<form version="2"><field column="ClassID" columntype="integer" guid="15a00747-3ff0-470a-ae7a-cc99c8a8259f" isPK="true" system="true"><properties><fieldcaption>ClassID</fieldcaption></properties><settings><controlname>labelcontrol</controlname></settings></field><field column="ClassDisplayName" columnsize="100" columntype="text" guid="b622e4bc-00e4-4ddd-bc24-952579b9812a" system="true" visible="true"><properties><fieldcaption>{$general.displayname$}</fieldcaption><validationerrormessage>{$sysdev.class_edit_gen.displayname$}</validationerrormessage></properties><settings><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim></settings></field><field column="ClassName" columnsize="100" columntype="text" guid="11b3b0bd-6161-4ea6-b714-2249e4710ff3" isunique="true" system="true" visible="true"><properties><fieldcaption>{$general.codename$}</fieldcaption><fieldcssclass>form-group</fieldcssclass><validationerrormessage>{$sysdev.class_edit_gen.name$}</validationerrormessage></properties><settings><AllowEditPrefix>True</AllowEditPrefix><controlname>codenamewithprefix</controlname><ResourcePrefix>class.edit</ResourcePrefix><ShowAdditionalInfo>True</ShowAdditionalInfo></settings></field><field column="ClassUsesVersioning" columntype="boolean" guid="2783d609-e427-4a6a-b58f-11309c662557" system="true"><properties><defaultvalue ismacro="true">false</defaultvalue><fieldcaption>ClassUsesVersioning</fieldcaption></properties><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ClassIsDocumentType" columntype="boolean" guid="9644486f-782f-4582-9b94-06d82ec99cd4" system="true"><properties><defaultvalue ismacro="true">false</defaultvalue><fieldcaption>ClassIsDocumentType</fieldcaption></properties><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ClassIsCoupledClass" columntype="boolean" guid="60b11243-38b5-40a5-b3e1-d722e717b11b" system="true"><properties><defaultvalue ismacro="true">false</defaultvalue><fieldcaption>ClassIsCoupledClass</fieldcaption></properties><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ClassXmlSchema" columntype="longtext" guid="fa1ff4fd-b445-494b-9b34-538b978011c7" system="true"><properties><fieldcaption>ClassXmlSchema</fieldcaption></properties><settings><controlname>textareacontrol</controlname><IsTextArea>True</IsTextArea></settings></field><field column="ClassFormDefinition" columntype="longtext" guid="0e36af3b-04b5-477b-86d9-e4fedbb6acfd" system="true"><properties><fieldcaption>ClassFormDefinition</fieldcaption></properties><settings><controlname>textareacontrol</controlname><IsTextArea>True</IsTextArea></settings></field><field column="ClassNodeNameSource" columnsize="100" columntype="text" guid="ce45ef8a-ba6d-40da-b437-88221a134f0a" system="true"><properties><fieldcaption>ClassNodeNameSource</fieldcaption></properties><settings><controlname>textboxcontrol</controlname></settings></field><field allowempty="true" column="ClassTableName" columnsize="100" columntype="text" guid="b211147b-2e77-4f0d-9546-e18990108532" system="true" visible="true"><properties><fieldcaption>{$general.tablename$}</fieldcaption><validationerrormessage>{$sysdev.class_edit_gen.tablename$}</validationerrormessage></properties><settings><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim></settings></field><field allowempty="true" column="ClassFormLayout" columntype="longtext" guid="32425d42-712b-48ad-92f2-ca7be3493456" system="true"><properties><fieldcaption>ClassFormLayout</fieldcaption></properties><settings><controlname>textareacontrol</controlname><IsTextArea>True</IsTextArea></settings></field><field allowempty="true" column="ClassShowAsSystemTable" columntype="boolean" guid="ad5fbd45-77ac-4ef6-8df7-823392ea0357" system="true" visible="true"><properties><defaultvalue>false</defaultvalue><fieldcaption>{$sysdev.class_edit_general.classissys$}</fieldcaption><validationerrormessage>{$sysdev.class_edit_gen.tablename$}</validationerrormessage></properties><settings><controlname>checkboxcontrol</controlname></settings></field><field allowempty="true" column="ClassUsePublishFromTo" columntype="boolean" guid="f6c7434d-b4f5-4788-878b-af4b7a011066" system="true"><properties><fieldcaption>ClassUsePublishFromTo</fieldcaption></properties><settings><controlname>checkboxcontrol</controlname></settings></field><field allowempty="true" column="ClassShowTemplateSelection" columntype="boolean" guid="9d6a6d71-0f1a-49f3-810c-40b830a5cba9" system="true"><properties><fieldcaption>ClassShowTemplateSelection</fieldcaption></properties><settings><controlname>checkboxcontrol</controlname></settings></field><field allowempty="true" column="ClassSKUMappings" columntype="longtext" guid="db0a08d0-ec66-45e4-bd0a-c3d1f86ffb6a" system="true"><properties><fieldcaption>ClassSKUMappings</fieldcaption></properties><settings><controlname>textareacontrol</controlname><IsTextArea>True</IsTextArea></settings></field><field allowempty="true" column="ClassIsMenuItemType" columntype="boolean" guid="de9eca79-f828-4a86-8df6-72d4b2e36265" system="true"><properties><fieldcaption>ClassIsMenuItemType</fieldcaption></properties><settings><controlname>checkboxcontrol</controlname></settings></field><field allowempty="true" column="ClassNodeAliasSource" columnsize="100" columntype="text" guid="9db135ca-2256-4812-96d3-c9bbc1b458d1" system="true"><properties><fieldcaption>ClassNodeAliasSource</fieldcaption></properties><settings><controlname>textboxcontrol</controlname></settings></field><field column="ClassLastModified" columntype="datetime" guid="b0efc357-062e-4db3-a772-4761673faa81" system="true"><properties><fieldcaption>ClassLastModified</fieldcaption></properties><settings><controlname>calendarcontrol</controlname></settings></field><field column="ClassGUID" columntype="guid" guid="155ef725-c6a5-434f-90e6-f9a91f70d6cb" system="true"><properties><fieldcaption>ClassGUID</fieldcaption></properties><settings><controlname>labelcontrol</controlname></settings></field><field allowempty="true" column="ClassCreateSKU" columntype="boolean" guid="087f9b36-6d29-4336-8fed-567b370935eb" system="true"><properties><fieldcaption>ClassCreateSKU</fieldcaption></properties><settings><controlname>checkboxcontrol</controlname></settings></field><field allowempty="true" column="ClassIsProduct" columntype="boolean" guid="90f0e42e-40a8-4d74-a144-482dade5a01a" system="true"><properties><fieldcaption>ClassIsProduct</fieldcaption></properties><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ClassIsCustomTable" columntype="boolean" guid="9d94c00d-128b-4ebb-b019-05c591605f79" system="true"><properties><defaultvalue ismacro="true">false</defaultvalue><fieldcaption>ClassIsCustomTable</fieldcaption></properties><settings><controlname>checkboxcontrol</controlname></settings></field><field allowempty="true" column="ClassShowColumns" columnsize="1000" columntype="text" guid="95ac260b-5d66-4bf5-a16b-af7313b642e0" system="true"><properties><fieldcaption>ClassShowColumns</fieldcaption></properties><settings><controlname>textboxcontrol</controlname></settings></field><field allowempty="true" column="ClassSearchTitleColumn" columnsize="200" columntype="text" guid="0ca33adc-5951-43e0-883b-2900618012f6" system="true"><properties><fieldcaption>ClassSearchTitleColumn</fieldcaption></properties><settings><controlname>textboxcontrol</controlname></settings></field><field allowempty="true" column="ClassSearchContentColumn" columnsize="200" columntype="text" guid="c01c8c30-6ad8-4e78-89ff-4444f30a39f2" system="true"><properties><fieldcaption>ClassSearchContentColumn</fieldcaption></properties><settings><controlname>textboxcontrol</controlname></settings></field><field allowempty="true" column="ClassSearchImageColumn" columnsize="200" columntype="text" guid="bc7d25ab-93cf-4613-ab30-1ed8fd2fb380" system="true"><properties><fieldcaption>ClassSearchImageColumn</fieldcaption></properties><settings><controlname>textboxcontrol</controlname></settings></field><field allowempty="true" column="ClassSearchCreationDateColumn" columnsize="200" columntype="text" guid="9e93f0c7-0ca3-427c-9fcd-0b268d042848" system="true"><properties><fieldcaption>ClassSearchCreationDateColumn</fieldcaption></properties><settings><controlname>textboxcontrol</controlname></settings></field><field allowempty="true" column="ClassSearchIndexDataSource" columntype="integer" guid="263618d8-1a6a-47f4-82a4-5fd4ccc5dec5" system="true" /><field allowempty="true" column="ClassSearchSettings" columntype="longtext" guid="b9ce15ef-d968-4341-b39b-569fe442cb54" system="true"><properties><fieldcaption>ClassSearchSettings</fieldcaption></properties><settings><controlname>textareacontrol</controlname><IsTextArea>True</IsTextArea></settings></field><field allowempty="true" column="ClassInheritsFromClassID" columntype="integer" guid="794296ec-5b04-40a2-a9ea-a802629e6839" system="true"><properties><fieldcaption>ClassInheritsFromClassID</fieldcaption></properties><settings><controlname>textboxcontrol</controlname></settings></field><field allowempty="true" column="ClassConnectionString" columnsize="100" columntype="text" guid="81f850e0-0dbd-4298-aebf-3a73bdc3b8e4" system="true" visible="true"><properties><fieldcaption>{$connectionstring.title$}</fieldcaption></properties><settings><controlname>connection_string_selector</controlname></settings></field><field allowempty="true" column="ClassSearchEnabled" columntype="boolean" guid="befeb58a-0af5-4775-8f92-b1a35b2a4917" system="true"><properties><fieldcaption>Search enabled</fieldcaption></properties><settings><controlname>checkboxcontrol</controlname></settings></field><field allowempty="true" column="ClassSKUDefaultDepartmentName" columnsize="200" columntype="text" guid="0d3e0544-4d03-46ca-ac2b-5121a526b5ae" system="true"><properties><fieldcaption>ClassSKUDefaultDepartmentName</fieldcaption></properties><settings><controlname>textboxcontrol</controlname></settings></field><field allowempty="true" column="ClassSKUDefaultDepartmentID" columntype="integer" guid="e3bb4e99-5d76-4936-957b-ec8379e5c6fe" system="true"><properties><fieldcaption>ClassSKUDefaultDepartmentID</fieldcaption></properties><settings><controlname>textboxcontrol</controlname></settings></field><field allowempty="true" column="ClassContactMapping" columntype="longtext" guid="fa0aa943-e3ef-42a0-ade2-179a27fc184b" system="true"><properties><fieldcaption>ClassContactMapping</fieldcaption></properties><settings><controlname>textareacontrol</controlname><IsTextArea>True</IsTextArea></settings></field><field allowempty="true" column="ClassContactOverwriteEnabled" columntype="boolean" guid="04b3aee9-b470-45b2-9c5e-90932d09cc19" system="true"><properties><fieldcaption>ClassContactOverwriteEnabled</fieldcaption></properties><settings><controlname>checkboxcontrol</controlname></settings></field><field allowempty="true" column="ClassSKUDefaultProductType" columnsize="50" columntype="text" guid="7fdff914-9f78-4512-ab6e-ce640de4cb3b" system="true"><properties><fieldcaption>ClassSKUDefaultProductType</fieldcaption></properties><settings><controlname>textboxcontrol</controlname></settings></field><field allowempty="true" column="ClassIsProductSection" columntype="boolean" guid="2ef6ca34-511f-4894-a139-9e5d74e8f82d" system="true"><properties><fieldcaption>ClassIsProductSection</fieldcaption></properties><settings><controlname>checkboxcontrol</controlname></settings></field><field allowempty="true" column="ClassFormLayoutType" columnsize="50" columntype="text" guid="1197ae53-0087-4e94-9b62-b8d4f3c46919" system="true"><properties><fieldcaption>ClassFormLayoutType</fieldcaption></properties><settings><controlname>textboxcontrol</controlname></settings></field><field allowempty="true" column="ClassVersionGUID" columnsize="50" columntype="text" guid="f27eb613-419e-48d4-9046-6828e5d8caab" system="true"><properties><fieldcaption>ClassVersionGUID</fieldcaption></properties><settings><controlname>textboxcontrol</controlname></settings></field><field allowempty="true" column="ClassDefaultObjectType" columnsize="100" columntype="text" guid="87954ccc-9d86-4dcc-8e05-90dc7bbdfeb6" system="true"><properties><fieldcaption>ClassDefaultObjectType</fieldcaption></properties><settings><controlname>textboxcontrol</controlname></settings></field><field allowempty="true" column="ClassIsForm" columntype="boolean" guid="1dd5c069-4856-48dd-9938-030db613fe0b" system="true"><properties><fieldcaption>ClassIsForm</fieldcaption></properties><settings><controlname>checkboxcontrol</controlname></settings></field><field allowempty="true" column="ClassResourceID" columntype="integer" guid="e8581cf4-a56e-4daf-9dd4-ee3f0d8a15b2" system="true" visible="true"><properties><fieldcaption>Class module</fieldcaption></properties><settings><controlname>moduleselector</controlname><DisplayAllModules>True</DisplayAllModules><DisplayNone>False</DisplayNone></settings></field><field allowempty="true" column="ClassCustomizedColumns" columnsize="400" columntype="text" guid="aac1c1dd-6965-49a8-8e30-05a652fbbbe7" system="true"><settings><controlname>textboxcontrol</controlname></settings></field><field allowempty="true" column="ClassCodeGenerationSettings" columntype="longtext" guid="5a8428f9-3212-4ad6-be4e-7fd96928f699" system="true"><settings><Autoresize_Hashtable>True</Autoresize_Hashtable><controlname>htmlareacontrol</controlname><Dialogs_Anchor_Hide>False</Dialogs_Anchor_Hide><Dialogs_Attachments_Hide>False</Dialogs_Attachments_Hide><Dialogs_Content_Hide>False</Dialogs_Content_Hide><Dialogs_Email_Hide>False</Dialogs_Email_Hide><Dialogs_Libraries_Hide>False</Dialogs_Libraries_Hide><Dialogs_Web_Hide>False</Dialogs_Web_Hide></settings></field><field allowempty="true" column="ClassIconClass" columnsize="200" columntype="text" guid="6db7bd4c-3a91-4f98-8540-a82a070677ff" system="true" /><field allowempty="true" column="ClassURLPattern" columnsize="200" columntype="text" guid="00c00c0d-1627-4bab-864c-1a6b371b8636" system="true"><properties><fieldcaption>URL pattern</fieldcaption></properties><settings><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><controlname>TextBoxControl</controlname><FilterMode>False</FilterMode><Trim>False</Trim></settings></field><field column="ClassUsesPageBuilder" columntype="boolean" guid="2462fa2a-d236-4a96-8b1e-a8b72d645290" system="true"><properties><defaultvalue>False</defaultvalue></properties></field><field column="ClassIsNavigationItem" columntype="boolean" guid="21ad165c-3970-4270-9fbc-f8ec98c2d3c2" system="true"><properties><defaultvalue>False</defaultvalue></properties></field><field column="ClassHasURL" columntype="boolean" guid="38ca89b0-abfb-482e-a0d7-be1ee50de0f9" system="true"><properties><defaultvalue>False</defaultvalue></properties></field><field column="ClassHasMetadata" columntype="boolean" guid="31ebdec9-9b2c-4d09-bd01-437195b27654" system="true"><properties><defaultvalue>False</defaultvalue></properties></field></form>',
        ClassXmlSchema = '<?xml version="1.0" encoding="utf-8"?>  <xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">    <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">      <xs:complexType>        <xs:choice minOccurs="0" maxOccurs="unbounded">          <xs:element name="CMS_Class">            <xs:complexType>              <xs:sequence>                <xs:element name="ClassID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />                <xs:element name="ClassDisplayName">                  <xs:simpleType>                    <xs:restriction base="xs:string">                      <xs:maxLength value="100" />                    </xs:restriction>                  </xs:simpleType>                </xs:element>                <xs:element name="ClassName">                  <xs:simpleType>                    <xs:restriction base="xs:string">                      <xs:maxLength value="100" />                    </xs:restriction>                  </xs:simpleType>                </xs:element>                <xs:element name="ClassUsesVersioning" type="xs:boolean" />                <xs:element name="ClassIsDocumentType" type="xs:boolean" />                <xs:element name="ClassIsCoupledClass" type="xs:boolean" />                <xs:element name="ClassXmlSchema">                  <xs:simpleType>                    <xs:restriction base="xs:string">                      <xs:maxLength value="2147483647" />                    </xs:restriction>                  </xs:simpleType>                </xs:element>                <xs:element name="ClassFormDefinition">                  <xs:simpleType>                    <xs:restriction base="xs:string">                      <xs:maxLength value="2147483647" />                    </xs:restriction>                  </xs:simpleType>                </xs:element>                <xs:element name="ClassNodeNameSource">                  <xs:simpleType>                    <xs:restriction base="xs:string">                      <xs:maxLength value="100" />                    </xs:restriction>                  </xs:simpleType>                </xs:element>                <xs:element name="ClassTableName" minOccurs="0">                  <xs:simpleType>                    <xs:restriction base="xs:string">                      <xs:maxLength value="100" />                    </xs:restriction>                  </xs:simpleType>                </xs:element>                <xs:element name="ClassFormLayout" minOccurs="0">                  <xs:simpleType>                    <xs:restriction base="xs:string">                      <xs:maxLength value="2147483647" />                    </xs:restriction>                  </xs:simpleType>                </xs:element>                <xs:element name="ClassShowAsSystemTable" type="xs:boolean" minOccurs="0" />                <xs:element name="ClassUsePublishFromTo" type="xs:boolean" minOccurs="0" />                <xs:element name="ClassShowTemplateSelection" type="xs:boolean" minOccurs="0" />                <xs:element name="ClassSKUMappings" minOccurs="0">                  <xs:simpleType>                    <xs:restriction base="xs:string">                      <xs:maxLength value="2147483647" />                    </xs:restriction>                  </xs:simpleType>                </xs:element>                <xs:element name="ClassIsMenuItemType" type="xs:boolean" minOccurs="0" />                <xs:element name="ClassNodeAliasSource" minOccurs="0">                  <xs:simpleType>                    <xs:restriction base="xs:string">                      <xs:maxLength value="100" />                    </xs:restriction>                  </xs:simpleType>                </xs:element>                <xs:element name="ClassLastModified" type="xs:dateTime" />                <xs:element name="ClassGUID" msdata:DataType="System.Guid, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />                <xs:element name="ClassCreateSKU" type="xs:boolean" minOccurs="0" />                <xs:element name="ClassIsProduct" type="xs:boolean" minOccurs="0" />                <xs:element name="ClassIsCustomTable" type="xs:boolean" />                <xs:element name="ClassShowColumns" minOccurs="0">                  <xs:simpleType>                    <xs:restriction base="xs:string">                      <xs:maxLength value="1000" />                    </xs:restriction>                  </xs:simpleType>                </xs:element>                <xs:element name="ClassSearchTitleColumn" minOccurs="0">                  <xs:simpleType>                    <xs:restriction base="xs:string">                      <xs:maxLength value="200" />                    </xs:restriction>                  </xs:simpleType>                </xs:element>                <xs:element name="ClassSearchContentColumn" minOccurs="0">                  <xs:simpleType>                    <xs:restriction base="xs:string">                      <xs:maxLength value="200" />                    </xs:restriction>                  </xs:simpleType>                </xs:element>                <xs:element name="ClassSearchImageColumn" minOccurs="0">                  <xs:simpleType>                    <xs:restriction base="xs:string">                      <xs:maxLength value="200" />                    </xs:restriction>                  </xs:simpleType>                </xs:element>                <xs:element name="ClassSearchCreationDateColumn" minOccurs="0">                  <xs:simpleType>                    <xs:restriction base="xs:string">                      <xs:maxLength value="200" />                    </xs:restriction>                  </xs:simpleType>                </xs:element>                <xs:element name="ClassSearchSettings" minOccurs="0">                  <xs:simpleType>                    <xs:restriction base="xs:string">                      <xs:maxLength value="2147483647" />                    </xs:restriction>                  </xs:simpleType>                </xs:element>                <xs:element name="ClassInheritsFromClassID" type="xs:int" minOccurs="0" />                <xs:element name="ClassSearchEnabled" type="xs:boolean" minOccurs="0" />                <xs:element name="ClassSKUDefaultDepartmentName" minOccurs="0">                  <xs:simpleType>                    <xs:restriction base="xs:string">                      <xs:maxLength value="200" />                    </xs:restriction>                  </xs:simpleType>                </xs:element>                <xs:element name="ClassSKUDefaultDepartmentID" type="xs:int" minOccurs="0" />                <xs:element name="ClassContactMapping" minOccurs="0">                  <xs:simpleType>                    <xs:restriction base="xs:string">                      <xs:maxLength value="2147483647" />                    </xs:restriction>                  </xs:simpleType>                </xs:element>                <xs:element name="ClassContactOverwriteEnabled" type="xs:boolean" minOccurs="0" />                <xs:element name="ClassSKUDefaultProductType" minOccurs="0">                  <xs:simpleType>                    <xs:restriction base="xs:string">                      <xs:maxLength value="50" />                    </xs:restriction>                  </xs:simpleType>                </xs:element>                <xs:element name="ClassConnectionString" minOccurs="0">                  <xs:simpleType>                    <xs:restriction base="xs:string">                      <xs:maxLength value="100" />                    </xs:restriction>                  </xs:simpleType>                </xs:element>                <xs:element name="ClassIsProductSection" type="xs:boolean" minOccurs="0" />                <xs:element name="ClassFormLayoutType" minOccurs="0">                  <xs:simpleType>                    <xs:restriction base="xs:string">                      <xs:maxLength value="50" />                    </xs:restriction>                  </xs:simpleType>                </xs:element>                <xs:element name="ClassVersionGUID" minOccurs="0">                  <xs:simpleType>                    <xs:restriction base="xs:string">                      <xs:maxLength value="50" />                    </xs:restriction>                  </xs:simpleType>                </xs:element>                <xs:element name="ClassDefaultObjectType" minOccurs="0">                  <xs:simpleType>                    <xs:restriction base="xs:string">                      <xs:maxLength value="100" />                    </xs:restriction>                  </xs:simpleType>                </xs:element>                <xs:element name="ClassIsForm" type="xs:boolean" minOccurs="0" />                <xs:element name="ClassResourceID" type="xs:int" minOccurs="0" />                <xs:element name="ClassCustomizedColumns" minOccurs="0">                  <xs:simpleType>                    <xs:restriction base="xs:string">                      <xs:maxLength value="400" />                    </xs:restriction>                  </xs:simpleType>                </xs:element>                <xs:element name="ClassCodeGenerationSettings" minOccurs="0">                  <xs:simpleType>                    <xs:restriction base="xs:string">                      <xs:maxLength value="2147483647" />                    </xs:restriction>                  </xs:simpleType>                </xs:element>                <xs:element name="ClassIconClass" minOccurs="0">                  <xs:simpleType>                    <xs:restriction base="xs:string">                      <xs:maxLength value="200" />                    </xs:restriction>                  </xs:simpleType>                </xs:element>                <xs:element name="ClassURLPattern" minOccurs="0">                  <xs:simpleType>                    <xs:restriction base="xs:string">                      <xs:maxLength value="200" />                    </xs:restriction>                  </xs:simpleType>                </xs:element>                <xs:element name="ClassUsesPageBuilder" type="xs:boolean" />                <xs:element name="ClassIsNavigationItem" type="xs:boolean" />                <xs:element name="ClassHasURL" type="xs:boolean" />                <xs:element name="ClassHasMetadata" type="xs:boolean" />                <xs:element name="ClassSearchIndexDataSource" type="xs:int" minOccurs="0" />              </xs:sequence>            </xs:complexType>          </xs:element>        </xs:choice>      </xs:complexType>      <xs:unique name="Constraint1" msdata:PrimaryKey="true">        <xs:selector xpath=".//CMS_Class" />        <xs:field xpath="ClassID" />      </xs:unique>    </xs:element>  </xs:schema>'
    WHERE ClassGUID = 'D7E91104-201B-4B11-9550-E93AD9A4D81F'

    ALTER TABLE CMS_Class
        ADD ClassSearchIndexDataSource INT NULL;

    EXEC('
    DECLARE @XMLSettings TABLE
    (
        Settings XML,
        IndexType NVARCHAR(100),
        SiteID INT
    )

    DECLARE @ClassNames TABLE
    (
        IndexType NVARCHAR(100),
        ClassName NVARCHAR(100)
    )

    INSERT INTO @XMLSettings
    SELECT CAST(IndexSettings AS XML) AS SettingsXML, IndexType, IndexSiteID
    FROM CMS_SearchIndex SI
    INNER JOIN CMS_SearchIndexSite SIS ON SIS.IndexID = SI.IndexID
    WHERE IndexType = ''cms.document'' OR IndexType = ''DOCUMENTS_CRAWLER_INDEX'';

    WITH classNames AS (
        SELECT IndexType, Items.classNames.value(''@classnames'', ''nvarchar(max)'') AS [classNames], SiteID
        FROM @XMLSettings AS S CROSS APPLY S.Settings.nodes(''/index/item'') AS Items(classNames)
    ),

    paths AS (
    SELECT S.IndexType, Items.classNames.value(''@path'', ''nvarchar(max)'') AS [path], SiteID
    FROM @XMLSettings AS S CROSS APPLY S.Settings.nodes(''/index/item'') AS Items(classNames)
    WHERE Items.classNames.value(''@classnames'', ''nvarchar(max)'') = '''')

    INSERT INTO @ClassNames
    SELECT DISTINCT IndexType, Split.ClassName.value(''.'', ''VARCHAR(100)'') AS ClassName
    FROM (
        SELECT IndexType, CAST (''<M>'' + REPLACE(C.classNames, '';'', ''</M><M>'') + ''</M>'' AS XML) AS XMLClassNames
        FROM classNames C
        WHERE [classNames] <> ''''
    ) AS A CROSS APPLY XMLClassNames.nodes (''/M'') AS Split(ClassName)
    UNION
    SELECT DISTINCT IndexType, C.ClassName
    FROM paths P
        INNER JOIN CMS_Tree T ON T.NodeAliasPath LIKE P.[path] AND P.SiteID = T.NodeSiteID
        INNER JOIN CMS_Class C ON T.NodeClassID = C.ClassID
    WHERE C.ClassSearchEnabled = 1;

    UPDATE CMS_Class
    SET ClassSearchIndexDataSource = 1
    WHERE ClassName IN (SELECT ClassName FROM @ClassNames WHERE IndexType = ''cms.document'')

    UPDATE CMS_Class
    SET ClassSearchIndexDataSource = 0
    WHERE ClassName IN (SELECT ClassName FROM @ClassNames WHERE IndexType = ''DOCUMENTS_CRAWLER_INDEX'')

    UPDATE CMS_Class
    SET ClassSearchIndexDataSource = 2
    WHERE ClassName IN (
        SELECT ClassName FROM @ClassNames CN
        WHERE CN.IndexType = ''cms.document'' AND EXISTS (SELECT 1 FROM @ClassNames CN2 WHERE CN2.IndexType = ''DOCUMENTS_CRAWLER_INDEX'' AND CN2.ClassName = CN.ClassName))

    -- Update page types that are not part of any index but have search feature enabled

    UPDATE CMS_Class
    SET ClassSearchIndexDataSource = 2
    WHERE ClassSearchEnabled = 1 AND ClassSearchIndexDataSource IS NULL AND ClassIsCoupledClass = 1 AND ClassUsesPageBuilder = 1 AND ClassIsDocumentType = 1

    UPDATE CMS_Class
    SET ClassSearchIndexDataSource = 1
    WHERE ClassSearchEnabled = 1 AND ClassSearchIndexDataSource IS NULL AND ClassIsCoupledClass = 1 AND ClassUsesPageBuilder = 0 AND ClassIsDocumentType = 1

    UPDATE CMS_Class
    SET ClassSearchIndexDataSource = 0
    WHERE ClassSearchEnabled = 1 AND ClassSearchIndexDataSource IS NULL AND ClassIsCoupledClass = 0 AND ClassIsDocumentType = 1')

    -- Update Pages crawler index type to Pages type

    UPDATE CMS_SearchIndex
    SET IndexType = 'cms.document'
    WHERE IndexType = 'DOCUMENTS_CRAWLER_INDEX'

    UPDATE CMS_SearchIndex
    SET IndexIsOutdated = 1
    WHERE IndexType = 'cms.document'
END

SET @HOTFIXVERSION = (SELECT [KeyValue] FROM [CMS_SettingsKey] WHERE [KeyName] = N'CMSHotfixVersion')
IF @HOTFIXVERSION < @REFRESHVERSION
BEGIN
    UPDATE CMS_Class
    SET ClassSearchSettings = '<search><item azurecontent="True" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="True" azuresortable="False" content="True" id="000919be-e176-433d-aaaf-bce66c1b6680" name="DocumentTags" searchable="True" tokenized="True" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="0325c6b3-d194-44cc-a574-6e764ec6af32" name="DocumentGUID" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="05c20f85-7283-4fcd-b946-df0f824f49aa" name="DocumentLastPublished" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="0da7a8a8-7304-4f1b-a779-48f5d8c22f3c" name="SKUReorderAt" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="0f2ecf6b-6be7-4805-aa60-15233b392127" name="DocumentSKUDescription" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="177d279c-5648-40de-a531-1cb1e26249a7" name="NodeParentID" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="19411933-6ac1-4a4c-b14c-5ae37dc9be32" name="SKUValidFor" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="198ddf0a-1ef4-4c48-a927-6d80c014bc8f" name="DocumentSKUName" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="1b21baf5-0ef6-450c-8fb6-ebc5eff6d903" name="NodeOwner" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="1e9d6b2f-0315-4584-87e6-65c23d6a8d8c" name="DocumentCreatedWhen" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="1fe06506-e12f-4e80-ac38-00802bdd62c9" name="SKUManufacturerID" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="2322f467-6d57-4514-9abe-dbbe0a13c7a0" name="SKUWidth" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="241e5e2d-3bdb-46c2-aa22-981d2c248604" name="NodeHasChildren" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="28ddd9c9-233b-4241-9881-fdd2e565b117" name="DocumentCheckedOutVersionHistoryID" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="True" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="True" azuresortable="False" content="True" id="29dc8ea6-c1c1-4ef0-b991-6173e96d1dbd" name="SKUShortDescription" searchable="False" tokenized="True" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="2e369591-a992-4d87-98b2-05fba25e0f95" name="SKULastModified" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="2e750254-277c-4200-9496-b5975a823df4" name="DocumentPageBuilderWidgets" searchable="False" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="30847e29-7ab7-47ac-a8ba-1024464bb9b0" name="SKURetailPrice" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="313fac85-bc2c-4043-a0bf-01cb828e697b" name="SKUValidUntil" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="33b41e70-df78-49ed-9bd4-4db368432efe" name="SKUInStoreFrom" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="352adc36-2fd8-41bd-98b3-cc089d283bbc" name="NodeLinkedNodeSiteID" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="357e40ff-9a0e-4637-a438-210d86a179a3" name="SKUValidity" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="387e14d4-13c5-46ec-b9c1-96f0536a3398" name="SKUCreated" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="True" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="True" azuresortable="False" content="True" id="39d13da9-82b3-47e8-990f-990d01b55d66" name="NodeCustomData" searchable="False" tokenized="True" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="42fdd1a2-bac2-4c16-bf2e-9b1bc48efb80" name="SKUPublicStatusID" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="43f0e056-3268-46b7-a4e9-e2c2a707f1a3" name="SKUID" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="4967a750-b268-4b8f-9e1a-b6ef4816aaa0" name="DocumentWorkflowCycleGUID" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="4a558b79-f4e8-4a38-8d49-21c049a8477f" name="NodeClassID" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="4d72ac6b-a1e5-41d2-8901-59170b4eced6" name="SKUWeight" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="542af074-e88e-499e-85e5-21b818080e33" name="SKUMaxItemsInOrder" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="True" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="True" id="544030da-2b4b-480a-b152-fa9f14286496" name="SKUImagePath" searchable="False" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="5880691d-0716-4aaf-8232-14d6ddfe1b83" name="DocumentCreatedByUserID" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="58932d8d-e3a5-4bb3-84a2-cc7a55a49650" name="DocumentIsArchived" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="True" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="True" id="58e9d5bb-5907-4119-a223-fb8b782df4fc" name="SKUDescription" searchable="False" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="58fae381-7dca-418f-b043-f5d0fbe6a5b5" name="DocumentWorkflowStepID" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="True" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="5930175a-83bc-4f12-bcd9-7b372a664e8f" name="NodeID" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="True" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="True" azuresortable="False" content="True" id="59673b90-ea77-46c3-843e-b81166b8c7d7" name="NodeName" searchable="False" tokenized="True" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="5c3d2228-d8f7-4ab9-908c-e12649b29645" name="SKUMinItemsInOrder" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="5e16ea49-6e58-4d7c-849e-9256fdea6941" name="DocumentLastVersionNumber" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="5f7091d1-843d-4a1e-8c98-818bd5947886" name="SKUOrder" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="60260dd6-318b-4e5d-b439-d58cd8a6d846" name="SKUTaxClassID" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="6045c62b-af75-4ce4-8a50-4ca7046661b6" name="SKUBundleItemsCount" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="64a56ab7-6a19-41c9-b333-30ef1a82d311" name="DocumentIsWaitingForTranslation" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="6a0f859c-e0d4-4574-b769-50df12a7dd13" name="NodeLevel" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="6afc7dd9-f115-4b82-acdd-1f185c5d4132" name="DocumentWorkflowActionStatus" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="True" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="True" azuresortable="False" content="True" id="6d999348-8f8c-40ea-8900-b5e7787bc2dc" name="NodeAlias" searchable="False" tokenized="True" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="6e60b8a7-5111-40f9-9ce5-a8ba7d7d6b01" name="SKUAvailableItems" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="6e68be2a-60d2-4c00-ae4d-20137f6895b0" name="SKUNeedsShipping" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="True" azuresortable="False" content="False" id="6fec5897-024a-42dd-8b87-85c146f24afe" name="DocumentCulture" searchable="True" tokenized="True" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="7208e9e6-1a4e-4110-b23b-f123b1a8af58" name="SKUTrackInventory" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="75004db4-329f-4cc0-b192-a412da04334f" name="SKUHeight" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="7677e13f-9d37-4626-bf1a-a8441d2cb112" name="NodeGUID" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="77f14e5b-af6c-4a39-898e-25c4a5f27a03" name="DocumentNodeID" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="77f38e40-012e-42b0-8c27-510d8fb896da" name="IsSecuredNode" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="True" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="79f1c993-af05-4317-b5f8-69e3b25f0182" name="NodeAliasPath" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="807c9187-231c-4fe3-80a0-2e01897a0ce5" name="DocumentCheckedOutWhen" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="80950d11-c35a-4835-a698-ab0019f907f2" name="SKUSellOnlyAvailable" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="81319199-ac4f-473d-a8d7-50862e9d5515" name="NodeSKUID" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="8396ba4f-84de-4498-916c-69d930a83382" name="DocumentPublishedVersionHistoryID" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="883db0d9-d71b-4262-a6a1-a1d14b4f0994" name="NodeHasLinks" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="8b3886fd-435f-4918-9ec9-abcf78b337ec" name="SKUMembershipGUID" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="8dba8241-4ed6-4503-8904-8b8e77c70d5f" name="NodeACLID" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="8e986ef4-8ac6-4984-8c19-5aabd80dbc5e" name="SKUNumber" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="8f104432-0fa6-4483-a34d-fa8067cb774c" name="SKUCollectionID" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="92240017-35fc-4212-963e-b4de983c4305" name="DocumentModifiedWhen" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="935b5f7b-04db-4d3d-bf72-245d162521b5" name="SKUBrandID" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="96634737-e0b2-418b-b4e0-e891c0177b2f" name="SKUProductType" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="96de9337-749e-4e75-843a-52e2332d318b" name="SKUOptionCategoryID" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="True" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="True" azuresortable="False" content="True" id="9b1cacfb-fbe8-4f1a-bd11-9b407c61d82e" name="DocumentName" searchable="False" tokenized="True" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="9b46dc04-d51c-4b45-a51c-2e9a8346b67f" name="DocumentABTestConfiguration" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="9c281449-4762-4d13-a2cb-8eab772af205" name="NodeOrder" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="True" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="True" azuresortable="False" content="True" id="9d2b93c4-6d29-443e-b84e-b76db4f475d9" name="DocumentPageKeyWords" searchable="False" tokenized="True" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="a0066448-ced6-4698-bb09-ff9f71a851b6" name="NodeSiteID" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="a05dfe5d-c1cf-426b-8561-9a2498f0c0db" name="DocumentPublishTo" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="a4f55883-0f1a-4474-8fea-5bf1e55764f7" name="DocumentTagGroupID" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="a5988e87-43c8-4b85-ae0c-484950795a8b" name="DocumentCanBePublished" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="a88e8c28-11b0-4177-a64e-36b32e381183" name="DocumentPageTemplateConfiguration" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="aef6cc97-91d8-4e03-98db-ad6f74b4e5b8" name="SKUDepth" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="b3d44239-754e-4319-b8d2-9eae2714615d" name="NodeIsACLOwner" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="b3dba69b-14c1-4deb-950f-b8ead728832c" name="DocumentCheckedOutByUserID" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="b49c26ee-5e1f-44da-9fd1-5606f4f9e43e" name="SKUEnabled" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="b79745a3-e895-4e90-9c79-ed02fa14ac9c" name="DocumentSearchExcluded" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="bdee2172-7ec1-4fda-8f4b-26e6aee04580" name="DocumentForeignKeyValue" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="True" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="True" azuresortable="False" content="True" id="c228a1b7-e510-42ec-9e4f-0958e8bc7f0e" name="DocumentPageTitle" searchable="False" tokenized="True" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="True" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="c5b1f4b4-7b8a-4756-9b92-9fde8a4464ae" name="DocumentID" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="c9618d8e-fbb1-43d4-a495-c896465e04cb" name="NodeOriginalNodeID" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="ca7096ae-710e-4b3b-8357-9349a8608a47" name="DocumentSKUShortDescription" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="cffa338a-4d92-43e1-942d-814a6859ca50" name="SKUAvailableInDays" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="d1c91684-0517-439e-a1f3-ab75a202786b" name="SKUParentSKUID" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="d1de9bdc-2387-4d8b-83f6-4f0d40ba9dba" name="SKUEproductFilesCount" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="d277395e-ecb4-4802-8b44-2feb37374dbf" name="DocumentShowInMenu" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="True" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="d3d99f72-7006-4172-a626-6a5158517651" name="NodeLinkedNodeID" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="d5010740-f570-4f1e-b2fa-8f9cf332d918" name="SKUSiteID" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="d6d7d917-d227-4152-b345-a64dd9a75ad0" name="DocumentPublishFrom" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="d96bf80e-6766-4ff4-acb7-6f9fb45bcd81" name="SKUCustomData" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="db3522eb-18dd-4a89-b681-afe59ed4c457" name="SKUDepartmentID" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="dc43b6cd-07fe-49af-a472-9db971b56941" name="SKUInternalStatusID" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="e7fb1be4-d559-49c8-835d-3cc7e56f52d1" name="SKUBundleInventoryType" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="True" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="True" azuresortable="False" content="True" id="ee87c740-8c09-4a89-948b-bf5daf01c16e" name="DocumentPageDescription" searchable="False" tokenized="True" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="False" azuresortable="False" content="False" id="ef4e23e9-2163-4146-8bee-947117a6c8ae" name="DocumentCustomData" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="f290b950-60ba-4e02-8f7d-e49adfb1017f" name="SKUPrice" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="True" azurefacetable="False" azurefilterable="False" azureretrievable="False" azuresearchable="True" azuresortable="False" content="True" id="f8dac096-16ee-4d5f-b70b-0e03ee9c88ab" name="DocumentContent" searchable="False" tokenized="True" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="f963eeca-c615-4e63-bfc4-bb18bdac20b2" name="DocumentModifiedByUserID" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="fa68ba6f-39fb-46b7-87b2-f052619a5197" name="SKUGUID" searchable="False" tokenized="False" updatetrigger="False" /><item azurecontent="True" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="True" azuresortable="True" content="True" id="fceb1f51-396b-4e9c-996d-c6ce8df0a0d2" name="SKUName" searchable="True" tokenized="False" updatetrigger="True" /><item azurecontent="False" azurefacetable="False" azurefilterable="False" azureretrievable="True" azuresearchable="False" azuresortable="False" content="False" id="fdec187d-fb7b-48fb-8861-c8839032e3ae" name="SKUSupplierID" searchable="True" tokenized="False" updatetrigger="True" /></search>'
    WHERE ClassName = 'cms.document' AND ClassCustomizedColumns NOT LIKE '%ClassSearchSettings%'
END
GO

DECLARE @REFRESHVERSION INT = 16;
DECLARE @HOTFIXVERSION INT;
SET @HOTFIXVERSION = (SELECT [KeyValue] FROM [CMS_SettingsKey] WHERE [KeyName] = N'CMSHotfixVersion')
IF @HOTFIXVERSION < @REFRESHVERSION
BEGIN
    DECLARE @keyID int;
    DECLARE @keyCategoryID int;
    SET @keyID = (SELECT TOP 1 [KeyID] FROM [CMS_SettingsKey] WHERE [KeyName] = 'CMSCMLegitimateInterestActivitiesEnabled')
    SET @keyCategoryID = (SELECT TOP 1 [CategoryID] FROM [CMS_SettingsCategory] WHERE [CategoryName] = 'CMS.OnlineMarketing.Activities.General')

    IF @keyCategoryID IS NOT NULL AND @keyID IS NULL
    BEGIN
        INSERT [CMS_SettingsKey] ([KeyName], [KeyDisplayName], [KeyDescription], [KeyValue], [KeyType], [KeyCategoryID], [SiteID], [KeyGUID], [KeyLastModified], [KeyOrder], [KeyDefaultValue], [KeyValidation], [KeyEditingControlPath], [KeyIsGlobal], [KeyIsCustom], [KeyIsHidden], [KeyFormControlSettings], [KeyExplanationText])
        VALUES ('CMSCMLegitimateInterestActivitiesEnabled', '{$settingskey.cmscmlegitimateinterestactivitiesenabled$}', '{$settingskey.cmscmlegitimateinterestactivitiesenabled.description$}', 'False', 'boolean', @keyCategoryID, NULL, '78becb3e-78f0-4246-85d8-1557e313756e', getDate(), 2, 'False', NULL, NULL, 0, NULL, 0, NULL, '')
    END
END
GO

DECLARE @REFRESHVERSION INT = 16;
DECLARE @HOTFIXVERSION INT;
SET @HOTFIXVERSION = (SELECT [KeyValue] FROM [CMS_SettingsKey] WHERE [KeyName] = N'CMSHotfixVersion')
IF @HOTFIXVERSION < @REFRESHVERSION
BEGIN
    -- KX-33 - Live site debugs
    DECLARE @elementParentID int;
    SET @elementParentID = (SELECT TOP 1 [ElementID] FROM [CMS_UIElement] WHERE [ElementGUID] = '45bfb5b2-9840-4f7a-988e-0a87458b72a1')
    IF @elementParentID IS NOT NULL BEGIN

    DECLARE @elementResourceID int;
    SET @elementResourceID = (SELECT TOP 1 [ResourceID] FROM [CMS_Resource] WHERE [ResourceGUID] = 'ce1a65a0-80dc-4c53-b0e7-bdecf0aa8c02')
    IF @elementResourceID IS NOT NULL BEGIN

    INSERT [CMS_UIElement] ([ElementDisplayName], [ElementName], [ElementCaption], [ElementTargetURL], [ElementResourceID], [ElementParentID], [ElementChildCount], [ElementOrder], [ElementLevel], [ElementIDPath], [ElementIconPath], [ElementIsCustom], [ElementLastModified], [ElementGUID], [ElementSize], [ElementDescription], [ElementFromVersion], [ElementPageTemplateID], [ElementType], [ElementProperties], [ElementIsMenu], [ElementFeature], [ElementIconClass], [ElementIsGlobalApplication], [ElementCheckModuleReadPermission], [ElementAccessCondition], [ElementVisibilityCondition], [ElementRequiresGlobalAdminPriviligeLevel])
    VALUES ('SQL queries - Live site', 'SQLQueriesLiveSite', '{$administration-system.debuglivesite$}', '~/CMSModules/System/Debug/Log.aspx?name=sqlqueries&livesitelogs=true', @elementResourceID, @elementParentID, 0, 1, 5, '', '', 0, getDate(), 'c6b39eee-1f2a-4f9a-ae1a-c4906fa6f0f1', 0, '', '13.0', NULL, 'Url', '<Data><DisplayBreadcrumbs>False</DisplayBreadcrumbs></Data>', 0, '', '', 0, 1, '', '{%Debug.IsDebugEnabled("sqlqueries")|(identity)GlobalAdministrator|(hash)6d8f9e8cd6d0ff159959f47867b2fa3de307e74711201cd8175c12406ff788ce%}', 0)

    INSERT [CMS_UIElement] ([ElementDisplayName], [ElementName], [ElementCaption], [ElementTargetURL], [ElementResourceID], [ElementParentID], [ElementChildCount], [ElementOrder], [ElementLevel], [ElementIDPath], [ElementIconPath], [ElementIsCustom], [ElementLastModified], [ElementGUID], [ElementSize], [ElementDescription], [ElementFromVersion], [ElementPageTemplateID], [ElementType], [ElementProperties], [ElementIsMenu], [ElementFeature], [ElementIconClass], [ElementIsGlobalApplication], [ElementCheckModuleReadPermission], [ElementAccessCondition], [ElementVisibilityCondition], [ElementRequiresGlobalAdminPriviligeLevel])
    VALUES ('SQL queries - Admin', 'SQLQueriesAdmin', '{$administration-system.debugadmin$}', '~/CMSModules/System/Debug/Log.aspx?name=sqlqueries', @elementResourceID, @elementParentID, 0, 2, 5, '', '', 0, getDate(), 'bf376057-c0a2-4e7d-8bed-f64ba5509b3b', 0, '', '13.0', NULL, 'Url', '<Data><DisplayBreadcrumbs>False</DisplayBreadcrumbs></Data>', 0, '', '', 0, 1, '', '{%Debug.IsDebugEnabled("sqlqueries") && (Settings.CMSDebugAllSQLQueries || Settings.CMSDebugEverythingEverywhere)|(identity)GlobalAdministrator|(hash)fbd9acdef15e46d949b5bae27c33e001aca536b6aacfa316039340f819fd17b3%}', 0)

    END
    END


    SET @elementParentID = (SELECT TOP 1 [ElementID] FROM [CMS_UIElement] WHERE [ElementGUID] = '5143d751-b421-452b-9a50-4b37c0fe89dd')
    IF @elementParentID IS NOT NULL BEGIN

    SET @elementResourceID = (SELECT TOP 1 [ResourceID] FROM [CMS_Resource] WHERE [ResourceGUID] = 'ce1a65a0-80dc-4c53-b0e7-bdecf0aa8c02')
    IF @elementResourceID IS NOT NULL BEGIN

    INSERT [CMS_UIElement] ([ElementDisplayName], [ElementName], [ElementCaption], [ElementTargetURL], [ElementResourceID], [ElementParentID], [ElementChildCount], [ElementOrder], [ElementLevel], [ElementIDPath], [ElementIconPath], [ElementIsCustom], [ElementLastModified], [ElementGUID], [ElementSize], [ElementDescription], [ElementFromVersion], [ElementPageTemplateID], [ElementType], [ElementProperties], [ElementIsMenu], [ElementFeature], [ElementIconClass], [ElementIsGlobalApplication], [ElementCheckModuleReadPermission], [ElementAccessCondition], [ElementVisibilityCondition], [ElementRequiresGlobalAdminPriviligeLevel])
    VALUES ('Cache items - Live site', 'CacheItemsLiveSite', '{$administration-system.debuglivesite$}', '~/CMSModules/System/Debug/System_DebugCacheItems.aspx?livesitelogs=true', @elementResourceID, @elementParentID, 0, 1, 5, '', '', 0, getDate(), '078fc1dc-3989-4ab3-8c4d-9dcd6a447063', 0, '', '13.0', NULL, 'Url', '<Data><DisplayBreadcrumbs>False</DisplayBreadcrumbs></Data>', 0, '', '', 0, 1, '', '', 0)

    INSERT [CMS_UIElement] ([ElementDisplayName], [ElementName], [ElementCaption], [ElementTargetURL], [ElementResourceID], [ElementParentID], [ElementChildCount], [ElementOrder], [ElementLevel], [ElementIDPath], [ElementIconPath], [ElementIsCustom], [ElementLastModified], [ElementGUID], [ElementSize], [ElementDescription], [ElementFromVersion], [ElementPageTemplateID], [ElementType], [ElementProperties], [ElementIsMenu], [ElementFeature], [ElementIconClass], [ElementIsGlobalApplication], [ElementCheckModuleReadPermission], [ElementAccessCondition], [ElementVisibilityCondition], [ElementRequiresGlobalAdminPriviligeLevel])
    VALUES ('Cache items - Admin', 'CacheItemsAdmin', '{$administration-system.debugadmin$}', '~/CMSModules/System/Debug/System_DebugCacheItems.aspx?livesitelogs=false', @elementResourceID, @elementParentID, 0, 2, 5, '', '', 0, getDate(), 'c8ff79b4-ba85-4d92-8616-13f16aa88a54', 0, '', '13.0', NULL, 'Url', '<Data><DisplayBreadcrumbs>False</DisplayBreadcrumbs></Data>', 0, '', '', 0, 1, '', '', 0)

    END
    END


    SET @elementParentID = (SELECT TOP 1 [ElementID] FROM [CMS_UIElement] WHERE [ElementGUID] = '7e2c1ae4-e39d-4903-ae63-cbf693685c02')
    IF @elementParentID IS NOT NULL BEGIN

    SET @elementResourceID = (SELECT TOP 1 [ResourceID] FROM [CMS_Resource] WHERE [ResourceGUID] = 'ce1a65a0-80dc-4c53-b0e7-bdecf0aa8c02')
    IF @elementResourceID IS NOT NULL BEGIN

    INSERT [CMS_UIElement] ([ElementDisplayName], [ElementName], [ElementCaption], [ElementTargetURL], [ElementResourceID], [ElementParentID], [ElementChildCount], [ElementOrder], [ElementLevel], [ElementIDPath], [ElementIconPath], [ElementIsCustom], [ElementLastModified], [ElementGUID], [ElementSize], [ElementDescription], [ElementFromVersion], [ElementPageTemplateID], [ElementType], [ElementProperties], [ElementIsMenu], [ElementFeature], [ElementIconClass], [ElementIsGlobalApplication], [ElementCheckModuleReadPermission], [ElementAccessCondition], [ElementVisibilityCondition], [ElementRequiresGlobalAdminPriviligeLevel])
    VALUES ('Macros (K#) - Live site', 'MacrosLiveSite', '{$administration-system.debuglivesite$}', '~/CMSModules/System/Debug/Log.aspx?name=macros&livesitelogs=true', @elementResourceID, @elementParentID, 0, 1, 5, '', '', 0, getDate(), 'fdc2ec83-ffe5-41be-8d11-edc89a8950f1', 0, '', '13.0', NULL, 'Url', '<Data><DisplayBreadcrumbs>False</DisplayBreadcrumbs></Data>', 0, '', '', 0, 1, '', '{%Debug.IsDebugEnabled("macros")|(identity)GlobalAdministrator|(hash)f8a56d7ee7223ec5dafef72e0d23ddca16726e132e815dfd792c8319376ca61b%}', 0)

    INSERT [CMS_UIElement] ([ElementDisplayName], [ElementName], [ElementCaption], [ElementTargetURL], [ElementResourceID], [ElementParentID], [ElementChildCount], [ElementOrder], [ElementLevel], [ElementIDPath], [ElementIconPath], [ElementIsCustom], [ElementLastModified], [ElementGUID], [ElementSize], [ElementDescription], [ElementFromVersion], [ElementPageTemplateID], [ElementType], [ElementProperties], [ElementIsMenu], [ElementFeature], [ElementIconClass], [ElementIsGlobalApplication], [ElementCheckModuleReadPermission], [ElementAccessCondition], [ElementVisibilityCondition], [ElementRequiresGlobalAdminPriviligeLevel])
    VALUES ('Macros (K#) - Admin', 'MacrosAdmin', '{$administration-system.debugadmin$}', '~/CMSModules/System/Debug/Log.aspx?name=macros', @elementResourceID, @elementParentID, 0, 2, 5, '', '', 0, getDate(), 'bdf7db28-1007-4d15-9454-271a96166e8a', 0, '', '13.0', NULL, 'Url', '<Data><DisplayBreadcrumbs>False</DisplayBreadcrumbs></Data>', 0, '', '', 0, 1, '', '{%Debug.IsDebugEnabled("macros") && (Settings.CMSDebugAllMacros || Settings.CMSDebugEverythingEverywhere)|(identity)GlobalAdministrator|(hash)06714ae7db6e8ae30def5cf55501dbcbb865c1523b2cc8c536267a7b6123e917%}', 0)

    END
    END


    SET @elementParentID = (SELECT TOP 1 [ElementID] FROM [CMS_UIElement] WHERE [ElementGUID] = 'f817574e-91e1-46ad-a2fb-8c7a24b049f4')
    IF @elementParentID IS NOT NULL BEGIN

    SET @elementResourceID = (SELECT TOP 1 [ResourceID] FROM [CMS_Resource] WHERE [ResourceGUID] = 'ce1a65a0-80dc-4c53-b0e7-bdecf0aa8c02')
    IF @elementResourceID IS NOT NULL BEGIN

    INSERT [CMS_UIElement] ([ElementDisplayName], [ElementName], [ElementCaption], [ElementTargetURL], [ElementResourceID], [ElementParentID], [ElementChildCount], [ElementOrder], [ElementLevel], [ElementIDPath], [ElementIconPath], [ElementIsCustom], [ElementLastModified], [ElementGUID], [ElementSize], [ElementDescription], [ElementFromVersion], [ElementPageTemplateID], [ElementType], [ElementProperties], [ElementIsMenu], [ElementFeature], [ElementIconClass], [ElementIsGlobalApplication], [ElementCheckModuleReadPermission], [ElementAccessCondition], [ElementVisibilityCondition], [ElementRequiresGlobalAdminPriviligeLevel])
    VALUES ('Cache access - Live site', 'CacheAccessLiveSite', '{$administration-system.debuglivesite$}', '~/CMSModules/System/Debug/Log.aspx?name=cache&livesitelogs=true', @elementResourceID, @elementParentID, 0, 1, 5, '', '', 0, getDate(), '8e0711ab-0b89-4b10-b848-acecc29fc4f6', 0, '', '13.0', NULL, 'Url', '<Data><DisplayBreadcrumbs>False</DisplayBreadcrumbs></Data>', 0, '', '', 0, 1, '', '{%Debug.IsDebugEnabled("cache")|(identity)GlobalAdministrator|(hash)65a903d6a974c51bba5213b58f0c3d4f3ea2248366828f158f21d803db13f5c7%}', 0)

    INSERT [CMS_UIElement] ([ElementDisplayName], [ElementName], [ElementCaption], [ElementTargetURL], [ElementResourceID], [ElementParentID], [ElementChildCount], [ElementOrder], [ElementLevel], [ElementIDPath], [ElementIconPath], [ElementIsCustom], [ElementLastModified], [ElementGUID], [ElementSize], [ElementDescription], [ElementFromVersion], [ElementPageTemplateID], [ElementType], [ElementProperties], [ElementIsMenu], [ElementFeature], [ElementIconClass], [ElementIsGlobalApplication], [ElementCheckModuleReadPermission], [ElementAccessCondition], [ElementVisibilityCondition], [ElementRequiresGlobalAdminPriviligeLevel])
    VALUES ('Cache access - Admin', 'CacheAccessAdmin', '{$administration-system.debugadmin$}', '~/CMSModules/System/Debug/Log.aspx?name=cache', @elementResourceID, @elementParentID, 0, 2, 5, '', '', 0, getDate(), 'ce714c56-6347-454f-ada8-00762ae03826', 0, '', '13.0', NULL, 'Url', '<Data><DisplayBreadcrumbs>False</DisplayBreadcrumbs></Data>', 0, '', '', 0, 1, '', '{%Debug.IsDebugEnabled("cache") && (Settings.CMSDebugAllCache || Settings.CMSDebugEverythingEverywhere)|(identity)GlobalAdministrator|(hash)af74b4832dccedf23e7fb795d974dfde10ec6f8a657b0a0bb141148252c51498%}', 0)

    END
    END


    SET @elementParentID = (SELECT TOP 1 [ElementID] FROM [CMS_UIElement] WHERE [ElementGUID] = '3abfac77-95b9-4a3b-a0be-3539abca35d8')
    IF @elementParentID IS NOT NULL BEGIN

    DECLARE @elementPageTemplateID int;
    SET @elementPageTemplateID = (SELECT TOP 1 [PageTemplateID] FROM [CMS_PageTemplate] WHERE [PageTemplateGUID] = '8136b750-a785-438f-a412-32212cd4dde6')
    IF @elementPageTemplateID IS NOT NULL BEGIN

    SET @elementResourceID = (SELECT TOP 1 [ResourceID] FROM [CMS_Resource] WHERE [ResourceGUID] = 'ce1a65a0-80dc-4c53-b0e7-bdecf0aa8c02')
    IF @elementResourceID IS NOT NULL BEGIN

    UPDATE [CMS_UIElement] SET
            [ElementTargetURL] = '',
            [ElementPageTemplateID] = @elementPageTemplateID,
            [ElementType] = 'PageTemplate',
            [ElementProperties] = '<Data><AllowSubTabs>true</AllowSubTabs><category_name_header>False</category_name_header><displaybreadcrumbs>False</displaybreadcrumbs><DisplayTitleInTabs>false</DisplayTitleInTabs><includejquery>False</includejquery></Data>'
        WHERE [ElementGUID] = '5143d751-b421-452b-9a50-4b37c0fe89dd'

    UPDATE [CMS_UIElement] SET
            [ElementTargetURL] = '',
            [ElementPageTemplateID] = @elementPageTemplateID,
            [ElementType] = 'PageTemplate',
            [ElementProperties] = '<Data><AllowSubTabs>true</AllowSubTabs><category_name_header>False</category_name_header><displaybreadcrumbs>False</displaybreadcrumbs><DisplayTitleInTabs>false</DisplayTitleInTabs><includejquery>False</includejquery></Data>'
        WHERE [ElementGUID] = 'f817574e-91e1-46ad-a2fb-8c7a24b049f4'

    UPDATE [CMS_UIElement] SET
            [ElementTargetURL] = '',
            [ElementOrder] = 6,
            [ElementPageTemplateID] = @elementPageTemplateID,
            [ElementType] = 'PageTemplate',
            [ElementProperties] = '<Data><AllowSubTabs>true</AllowSubTabs><category_name_header>False</category_name_header><displaybreadcrumbs>False</displaybreadcrumbs><DisplayTitleInTabs>false</DisplayTitleInTabs><includejquery>False</includejquery></Data>'
        WHERE [ElementGUID] = '45bfb5b2-9840-4f7a-988e-0a87458b72a1'

    END
    END
    END


    SET @elementParentID = (SELECT TOP 1 [ElementID] FROM [CMS_UIElement] WHERE [ElementGUID] = '3abfac77-95b9-4a3b-a0be-3539abca35d8')
    IF @elementParentID IS NOT NULL BEGIN

    SET @elementResourceID = (SELECT TOP 1 [ResourceID] FROM [CMS_Resource] WHERE [ResourceGUID] = 'ce1a65a0-80dc-4c53-b0e7-bdecf0aa8c02')
    IF @elementResourceID IS NOT NULL BEGIN

    UPDATE [CMS_UIElement] SET
            [ElementOrder] = 7
        WHERE [ElementGUID] = 'ea73614e-6a9a-487b-9675-bda6ae3eaed3'

    UPDATE [CMS_UIElement] SET
            [ElementOrder] = 8
        WHERE [ElementGUID] = 'e9468499-ecc6-4fb7-b34a-ae8f0621bf85'

    END
    END


    SET @elementParentID = (SELECT TOP 1 [ElementID] FROM [CMS_UIElement] WHERE [ElementGUID] = '3abfac77-95b9-4a3b-a0be-3539abca35d8')
    IF @elementParentID IS NOT NULL BEGIN

    SET @elementPageTemplateID = (SELECT TOP 1 [PageTemplateID] FROM [CMS_PageTemplate] WHERE [PageTemplateGUID] = '8136b750-a785-438f-a412-32212cd4dde6')
    IF @elementPageTemplateID IS NOT NULL BEGIN

    SET @elementResourceID = (SELECT TOP 1 [ResourceID] FROM [CMS_Resource] WHERE [ResourceGUID] = 'ce1a65a0-80dc-4c53-b0e7-bdecf0aa8c02')
    IF @elementResourceID IS NOT NULL BEGIN

    UPDATE [CMS_UIElement] SET
            [ElementTargetURL] = '',
            [ElementOrder] = 9,
            [ElementPageTemplateID] = @elementPageTemplateID,
            [ElementType] = 'PageTemplate',
            [ElementProperties] = '<Data><AllowSubTabs>true</AllowSubTabs><category_name_header>False</category_name_header><displaybreadcrumbs>False</displaybreadcrumbs><DisplayTitleInTabs>false</DisplayTitleInTabs><includejquery>False</includejquery></Data>'
        WHERE [ElementGUID] = '7e2c1ae4-e39d-4903-ae63-cbf693685c02'

    END
    END
    END


    SET @elementParentID = (SELECT TOP 1 [ElementID] FROM [CMS_UIElement] WHERE [ElementGUID] = '3abfac77-95b9-4a3b-a0be-3539abca35d8')
    IF @elementParentID IS NOT NULL BEGIN

    SET @elementResourceID = (SELECT TOP 1 [ResourceID] FROM [CMS_Resource] WHERE [ResourceGUID] = 'ce1a65a0-80dc-4c53-b0e7-bdecf0aa8c02')
    IF @elementResourceID IS NOT NULL BEGIN

    UPDATE [CMS_UIElement] SET
            [ElementOrder] = 10
        WHERE [ElementGUID] = '77862fa7-765d-47e8-8343-a29b21c252f9'

    UPDATE [CMS_UIElement] SET
            [ElementOrder] = 11
        WHERE [ElementGUID] = 'd8237e42-f77f-4ac3-a202-dd79ce0dbdb9'

    UPDATE [CMS_UIElement] SET
            [ElementOrder] = 12
        WHERE [ElementGUID] = 'fb583819-fb8a-4bc5-97e5-29e6eaa18c35'

    UPDATE [CMS_UIElement] SET
            [ElementOrder] = 13
        WHERE [ElementGUID] = 'eda92ccf-916e-461e-a573-9b4628d47a17'

    UPDATE [CMS_UIElement] SET
            [ElementOrder] = 14
        WHERE [ElementGUID] = '242ffe21-1cd0-4667-b1e3-85306b7d003a'

    UPDATE [CMS_UIElement] SET
            [ElementOrder] = 15
        WHERE [ElementGUID] = 'da7f18d9-f9ec-4b13-8f1a-157142059223'

    END
    END
END
GO

DECLARE @REFRESHVERSION INT = 16;
DECLARE @HOTFIXVERSION INT;
SET @HOTFIXVERSION = (SELECT [KeyValue] FROM [CMS_SettingsKey] WHERE [KeyName] = N'CMSHotfixVersion')
IF @HOTFIXVERSION < @REFRESHVERSION
BEGIN
    -- [LAST] Recalculate IDPath and ChildCount for UI_Element

    DECLARE @elementCursor CURSOR;
    SET @elementCursor = CURSOR FOR SELECT [ElementID], [ElementParentID] FROM [CMS_UIElement] ORDER BY [ElementLevel], [ElementID]

    DECLARE @elementID int;
    DECLARE @elementParentID int;

    OPEN @elementCursor

    FETCH NEXT FROM @elementCursor INTO @elementID, @elementParentID;
    WHILE @@FETCH_STATUS = 0
    BEGIN

    UPDATE [CMS_UIElement] SET

        [ElementChildCount] = (SELECT COUNT(*)
                                FROM [CMS_UIElement] AS [Children]
                                WHERE [Children].[ElementParentID] = @elementID),

        [ElementIDPath] = COALESCE((SELECT TOP 1 [ElementIDPath] FROM [CMS_UIElement] AS [Parent] WHERE [Parent].ElementID = @elementParentID), '')
                            + '/'
                            + REPLICATE('0', 8 - LEN([ElementID]))
                            + CAST([ElementID] AS NVARCHAR(8))

    WHERE [ElementID] = @elementID

    FETCH NEXT FROM @elementCursor INTO @elementID, @elementParentID;
    END

    CLOSE @elementCursor;
    DEALLOCATE @elementCursor;
END
GO

DECLARE @HOTFIXVERSION INT;
SET @HOTFIXVERSION = (SELECT [KeyValue] FROM [CMS_SettingsKey] WHERE [KeyName] = N'CMSHotfixVersion')
IF @HOTFIXVERSION < 19
BEGIN

INSERT INTO [Temp_FormDefinition] ([ObjectName], [FormDefinition], [IsAltForm])
VALUES ('cms.SearchIndex',
		'<form version="2"><field column="IndexID" columntype="integer" guid="11a12bb8-8daa-4d81-a93d-18b6ee6dd7fe" isPK="true" system="true"><properties><fieldcaption>IndexID</fieldcaption></properties><settings><controlname>labelcontrol</controlname></settings></field><category name="General.General"><properties><caption>{$General.General$}</caption><visible>true</visible></properties></category><field column="IndexProvider" columnsize="200" columntype="text" guid="4584a254-c55a-433f-957f-ca3420494258" system="true" /><field allowempty="true" column="IndexSearchServiceName" columnsize="200" columntype="text" guid="7a3e2eef-b4bf-4750-8133-f5cd93652acf" system="true" /><field allowempty="true" column="IndexAdminKey" columnsize="200" columntype="text" guid="5a148650-ff4b-48a5-8118-d9c748c93284" system="true" /><field allowempty="true" column="IndexQueryKey" columnsize="200" columntype="text" guid="5f5c6c0c-e170-44b4-9186-6b01e0bb5ade" system="true" /><field column="IndexDisplayName" columnsize="200" columntype="text" guid="f6062f9f-ddcb-432b-aa26-3eb19bec2136" system="true" translatefield="true" visible="true"><properties><fieldcaption>{$general.displayname$}</fieldcaption><fielddescription>{$general.displayname.description$}</fielddescription></properties><settings><controlname>localizabletextbox</controlname><ValueIsContent>False</ValueIsContent></settings></field><field column="IndexName" columnsize="200" columntype="text" guid="1e61111e-be57-4ed5-bf42-5a831ef74e02" system="true" visible="true"><properties><fieldcaption>{$general.codename$}</fieldcaption><fielddescription>{$general.codename.description$}</fielddescription></properties><settings><controlname>codename</controlname></settings></field><field column="IndexType" columnsize="200" columntype="text" guid="e196f557-21ab-4b65-8977-d5b14d33d333" system="true" visible="true"><properties><fieldcaption>{$srch.index.type$}</fieldcaption><fielddescription>{$srch.index.type.description$}</fielddescription></properties><settings><controlname>searchindextypeselector</controlname></settings></field><field allowempty="true" column="IndexAnalyzerType" columnsize="200" columntype="text" guid="38f0655c-0b92-4436-915f-8a2545ee548a" hasdependingfields="true" spellcheck="false" system="true" visible="true"><properties><fieldcaption>{$srch.index.analyzer$}</fieldcaption><fielddescription>{$srch.index.analyzer.description$}</fielddescription></properties><settings><AssemblyName>CMS.DataEngine</AssemblyName><controlname>enumselector</controlname><DisplayType>0</DisplayType><Sort>False</Sort><TypeName>CMS.DataEngine.SearchAnalyzerTypeEnum</TypeName><UseStringRepresentation>True</UseStringRepresentation></settings></field><field allowempty="true" column="IndexSettings" columntype="longtext" guid="6baa44c0-892e-4136-be2e-2e625347981f" system="true"><properties><fieldcaption>IndexSettings</fieldcaption></properties><settings><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><controlname>textareacontrol</controlname><FilterMode>False</FilterMode><IsTextArea>True</IsTextArea></settings></field><field column="IndexGUID" columntype="guid" guid="7b121874-5f4d-44e1-a652-26273df5f674" system="true"><properties><fieldcaption>IndexGUID</fieldcaption></properties><settings><controlname>labelcontrol</controlname></settings></field><field column="IndexLastModified" columntype="datetime" guid="f52606a9-543c-4554-98c5-c1a3680ef7ee" system="true"><properties><fieldcaption>IndexLastModified</fieldcaption></properties><settings><controlname>calendarcontrol</controlname><DisplayNow>True</DisplayNow><EditTime>False</EditTime><TimeZoneType>inherit</TimeZoneType></settings></field><field allowempty="true" column="IndexLastRebuildTime" columntype="datetime" guid="5a4a4036-c437-41ce-b2f7-2c1e5bf7eff1" system="true"><properties><fieldcaption>IndexLastRebuildTime</fieldcaption></properties><settings><controlname>calendarcontrol</controlname><DisplayNow>True</DisplayNow><EditTime>False</EditTime><TimeZoneType>inherit</TimeZoneType></settings></field><field allowempty="true" column="IndexStopWordsFile" columnsize="200" columntype="text" dependsonanotherfield="true" guid="16b06aff-63dc-4a32-8f0e-99bff88a0cf3" system="true" visible="true"><properties><controlcssclass>DropDownFieldSmall</controlcssclass><fieldcaption>{$srch.index.stopwords$}</fieldcaption><fielddescription>{$srch.index.stopwords.description$}</fielddescription><visiblemacro ismacro="true">{%IndexAnalyzerType.ToString() == "standard" || IndexAnalyzerType.ToString() == "stop" || IndexAnalyzerType.ToString() == "stopwithstemming"|(identity)GlobalAdministrator|(hash)fc113d0920d58431700eb23adac2227dd44428bdf2f3da7800fd38112b2acff5%}</visiblemacro></properties><settings><controlname>stopwordsselector</controlname></settings></field><field allowempty="true" column="IndexCustomAnalyzerAssemblyName" columnsize="200" columntype="text" dependsonanotherfield="true" guid="ee4cca86-033f-4e43-aa34-b969f8114ade" system="true" visible="true"><properties><fieldcaption>{$srch.index.customanalyzerassembly$}</fieldcaption><fielddescription>{$srch.index.customanalyzerassembly.description$}</fielddescription><visiblemacro ismacro="true">{%IndexAnalyzerType.ToString() == "custom"|(identity)GlobalAdministrator|(hash)6af80669e60a5a07a3a044b1c845be4b6257f46c44284b860b5a0cf09c22a01b%}</visiblemacro></properties><settings><BaseClassName>Lucene.Net.Analysis.Analyzer, Lucene.Net ; Lucene.Net.Analysis.Analyzer, Lucene.Net.v3</BaseClassName><ClassNameColumnName>IndexCustomAnalyzerClassName</ClassNameColumnName><controlname>assemblyclassselector</controlname><ShowClasses>True</ShowClasses><ShowEnumerations>False</ShowEnumerations><ShowInterfaces>False</ShowInterfaces><ValidateAssembly>True</ValidateAssembly></settings></field><field allowempty="true" column="IndexCustomAnalyzerClassName" columnsize="200" columntype="text" dependsonanotherfield="true" guid="0cee35a9-e0bc-4ab8-98b5-391e67e1ee16" spellcheck="false" system="true"><properties><fieldcaption>Class name</fieldcaption></properties><settings><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim></settings></field><field allowempty="true" column="IndexBatchSize" columntype="integer" guid="869750fe-25c4-476c-b614-cb3aa1c6f5cc" system="true" visible="true"><properties><controlcssclass>DropDownFieldSmall</controlcssclass><fieldcaption>{$srch.index.batchsize$}</fieldcaption><fielddescription>{$srch.index.batchsize.description$}</fielddescription></properties><settings><controlname>dropdownlistcontrol</controlname><EditText>True</EditText><Options>100;100
500;500
1000;1000
5000;5000
10000;10000
50000;50000
100000;100000</Options><SortItems>False</SortItems></settings></field><field allowempty="true" column="AssignToSite" columntype="boolean" dummy="mainform" guid="7e3c10ca-3f2e-4392-beaa-bd409c4601a5" system="true" visible="true"><properties><defaultvalue>true</defaultvalue><visiblemacro ismacro="true">{%FormMode == FormModeEnum.Insert|(identity)GlobalAdministrator|(hash)6da73e7e4232a90c342ee49067cdad67d10d7865620d90fd7aecb68edba902be%}</visiblemacro></properties><settings><controlname>AssignToSite</controlname></settings></field><field allowempty="true" column="IndexCrawlerUser" columnsize="200" columntype="text" guid="b623c978-b5b6-41c6-8746-33446b9eca5f" system="true" /><field allowempty="true" column="IndexStatus" columnsize="10" columntype="text" guid="4dee1752-a984-4385-b1e0-0187a0289441" system="true"><settings><controlname>checkboxlistcontrol</controlname></settings></field><field allowempty="true" column="IndexIsOutdated" columntype="boolean" guid="30e045d9-7386-4ea0-a520-f6474e0d6e32" system="true" /><field allowempty="true" column="IndexLastUpdate" columntype="datetime" guid="c83cc5f6-0453-47da-9550-9b147156d1fd" system="true"><settings><controlname>dropdownlistcontrol</controlname></settings></field></form>',
		0),
		('cms.SearchIndex.EditForm',
		'<form version="2"><field column="IndexID" guid="11a12bb8-8daa-4d81-a93d-18b6ee6dd7fe" isunique="true" /><field column="IndexProvider" guid="4584a254-c55a-433f-957f-ca3420494258" /><field column="IndexSearchServiceName" guid="7a3e2eef-b4bf-4750-8133-f5cd93652acf" /><field column="IndexAdminKey" guid="5a148650-ff4b-48a5-8118-d9c748c93284" /><field column="IndexQueryKey" guid="5f5c6c0c-e170-44b4-9186-6b01e0bb5ade" /><field column="IndexDisplayName" guid="f6062f9f-ddcb-432b-aa26-3eb19bec2136" /><field column="IndexName" guid="1e61111e-be57-4ed5-bf42-5a831ef74e02" /><field column="IndexType" guid="e196f557-21ab-4b65-8977-d5b14d33d333"><settings><controlname>labelcontrol</controlname><OutputFormat>{% GetResourceString("smartsearch.indextype." + IndexType.ToString()); |(identity)GlobalAdministrator|(hash)0949eb473c423b41d6b932a46debe92e8a2994da311fe63fbd2ba767ccd69c33%}


</OutputFormat></settings></field><field column="IndexAnalyzerType" guid="38f0655c-0b92-4436-915f-8a2545ee548a" /><field column="IndexSettings" guid="6baa44c0-892e-4136-be2e-2e625347981f" /><field column="IndexGUID" guid="7b121874-5f4d-44e1-a652-26273df5f674" /><field column="IndexLastModified" guid="f52606a9-543c-4554-98c5-c1a3680ef7ee" /><field column="IndexLastRebuildTime" guid="5a4a4036-c437-41ce-b2f7-2c1e5bf7eff1" /><field column="IndexStopWordsFile" guid="16b06aff-63dc-4a32-8f0e-99bff88a0cf3" /><field column="IndexCustomAnalyzerAssemblyName" guid="ee4cca86-033f-4e43-aa34-b969f8114ade" /><field column="IndexCustomAnalyzerClassName" guid="0cee35a9-e0bc-4ab8-98b5-391e67e1ee16" /><field column="IndexBatchSize" guid="869750fe-25c4-476c-b614-cb3aa1c6f5cc"><properties><controlcssclass>TextBoxFieldSmall</controlcssclass></properties></field><field column="AssignToSite" guid="7e3c10ca-3f2e-4392-beaa-bd409c4601a5" /><field column="IndexCrawlerUser" guid="b623c978-b5b6-41c6-8746-33446b9eca5f" visible="true"><settings><controlname>UserNameSelector</controlname></settings><properties><fieldcaption>{$srch.index.user$}</fieldcaption><fielddescription>{$srch.index.user.description$}</fielddescription><visiblemacro ismacro="true">{%IndexType == "cms.document"|(identity)GlobalAdministrator|(hash)46a00ea11817be7df6082b6b95c8f126a82755ead352b75f0abe01dc9b624ff9%}</visiblemacro></properties></field><field column="IndexStatus" guid="4dee1752-a984-4385-b1e0-0187a0289441" /><field column="IndexIsOutdated" guid="30e045d9-7386-4ea0-a520-f6474e0d6e32" /><field column="IndexLastUpdate" guid="c83cc5f6-0453-47da-9550-9b147156d1fd" /></form>',
		1),
		('cms.SearchIndex.EditAzureSearchIndexForm',
		'<form version="2"><field column="IndexID" guid="11a12bb8-8daa-4d81-a93d-18b6ee6dd7fe" isunique="true" /><field column="IndexDisplayName" guid="f6062f9f-ddcb-432b-aa26-3eb19bec2136" order="2" /><field column="IndexName" guid="1e61111e-be57-4ed5-bf42-5a831ef74e02" order="3"><settings><RequireIdentifier>False</RequireIdentifier></settings><properties><enabledmacro ismacro="true">{%false%}</enabledmacro><fielddescription>{$srch.azure.name.description$}</fielddescription></properties></field><field column="IndexType" guid="e196f557-21ab-4b65-8977-d5b14d33d333" order="4"><settings><controlname>LabelControl</controlname><OutputFormat>{% GetResourceString("smartsearch.indextype." + IndexType.ToString()); |(identity)GlobalAdministrator|(hash)0949eb473c423b41d6b932a46debe92e8a2994da311fe63fbd2ba767ccd69c33%}</OutputFormat><ResolveMacros>True</ResolveMacros></settings></field><field column="IndexAnalyzerType" guid="38f0655c-0b92-4436-915f-8a2545ee548a" visible="" order="5" /><field column="IndexProvider" guid="4584a254-c55a-433f-957f-ca3420494258" order="6" /><field column="IndexSettings" guid="6baa44c0-892e-4136-be2e-2e625347981f" order="7" /><field column="IndexGUID" guid="7b121874-5f4d-44e1-a652-26273df5f674" order="8" /><field column="IndexLastModified" guid="f52606a9-543c-4554-98c5-c1a3680ef7ee" order="9" /><field column="IndexLastRebuildTime" guid="5a4a4036-c437-41ce-b2f7-2c1e5bf7eff1" order="10" /><field column="IndexStopWordsFile" guid="16b06aff-63dc-4a32-8f0e-99bff88a0cf3" order="11" /><field column="IndexCustomAnalyzerAssemblyName" guid="ee4cca86-033f-4e43-aa34-b969f8114ade" order="12" /><field column="IndexCustomAnalyzerClassName" guid="0cee35a9-e0bc-4ab8-98b5-391e67e1ee16" order="13" /><field column="IndexBatchSize" guid="869750fe-25c4-476c-b614-cb3aa1c6f5cc" order="14"><settings><controlname>DropDownListControl</controlname><DisplayActualValueAsItem>False</DisplayActualValueAsItem><Options>100
500
1000</Options></settings></field><field column="AssignToSite" guid="7e3c10ca-3f2e-4392-beaa-bd409c4601a5" order="15" /><field column="IndexCrawlerUser" guid="b623c978-b5b6-41c6-8746-33446b9eca5f" visible="true" order="16"><settings><controlname>UserNameSelector</controlname></settings><properties><fieldcaption>{$srch.index.user$}</fieldcaption><fielddescription>{$srch.index.user.description$}</fielddescription><visiblemacro ismacro="true">{%IndexType == "cms.document"|(identity)GlobalAdministrator|(hash)46a00ea11817be7df6082b6b95c8f126a82755ead352b75f0abe01dc9b624ff9%}</visiblemacro></properties></field><category dummy="true" name="srch.azure.searchserviceapikeys" order="17"><properties><caption>{$srch.azure.searchservicesettings$}</caption><visible>True</visible></properties></category><field column="IndexSearchServiceName" guid="7a3e2eef-b4bf-4750-8133-f5cd93652acf" visible="true" allowempty="" order="18"><settings><controlname>TextBoxControl</controlname></settings><properties><fieldcaption>{$srch.azure.servicename$}</fieldcaption><fielddescription>{$srch.azure.servicename.description$}</fielddescription></properties></field><field column="IndexAdminKey" guid="5a148650-ff4b-48a5-8118-d9c748c93284" visible="true" allowempty="" order="19"><settings><controlname>TextBoxControl</controlname></settings><properties><fieldcaption>{$srch.azure.adminkey$}</fieldcaption><fielddescription>{$srch.azure.adminkey.description$}</fielddescription></properties></field><field column="IndexQueryKey" guid="5f5c6c0c-e170-44b4-9186-6b01e0bb5ade" visible="true" order="20"><settings><controlname>TextBoxControl</controlname></settings><properties><fieldcaption>{$srch.azure.querykey$}</fieldcaption><fielddescription>{$srch.azure.querykey.description$}</fielddescription></properties></field><field column="IndexStatus" guid="4dee1752-a984-4385-b1e0-0187a0289441" order="21" /><field column="IndexIsOutdated" guid="30e045d9-7386-4ea0-a520-f6474e0d6e32" order="22" /><field column="IndexLastUpdate" guid="c83cc5f6-0453-47da-9550-9b147156d1fd" order="23" /></form>',
		1);

END
GO

DECLARE @REFRESH_2_VERSION INT = 31;
DECLARE @HOTFIXVERSION INT;
SET @HOTFIXVERSION = (SELECT [KeyValue] FROM [CMS_SettingsKey] WHERE [KeyName] = N'CMSHotfixVersion')
IF @HOTFIXVERSION < @REFRESH_2_VERSION
BEGIN
	-- Add new settings categories
	DECLARE @categoryParentID int;
	SET @categoryParentID = (SELECT TOP 1 [CategoryID] FROM [CMS_SettingsCategory] WHERE [CategoryName] = 'CMS.Content')
	IF @categoryParentID IS NOT NULL BEGIN

	DECLARE @categoryResourceID int;
	SET @categoryResourceID = (SELECT TOP 1 [ResourceID] FROM [CMS_Resource] WHERE [ResourceGUID] = '98c6ee00-230a-4207-a6d3-03677b83b245')
	IF @categoryResourceID IS NOT NULL BEGIN

	INSERT [CMS_SettingsCategory] ([CategoryDisplayName], [CategoryOrder], [CategoryName], [CategoryParentID], [CategoryIDPath], [CategoryLevel], [CategoryChildCount], [CategoryIconPath], [CategoryIsGroup], [CategoryIsCustom], [CategoryResourceID])
	 VALUES ('{$settingscategory.cmstextanalytics$}', 4, 'CMS.Content.TextAnalytics', @categoryParentID, '', 2, 1, '', 0, 0, @categoryResourceID)

	END

	END

	SET @categoryParentID = (SELECT TOP 1 [CategoryID] FROM [CMS_SettingsCategory] WHERE [CategoryName] = 'CMS.Content.TextAnalytics')
	IF @categoryParentID IS NOT NULL BEGIN

	IF @categoryResourceID IS NOT NULL BEGIN

	INSERT [CMS_SettingsCategory] ([CategoryDisplayName], [CategoryOrder], [CategoryName], [CategoryParentID], [CategoryIDPath], [CategoryLevel], [CategoryChildCount], [CategoryIconPath], [CategoryIsGroup], [CategoryIsCustom], [CategoryResourceID])
	 VALUES ('{$general.general$}', 0, 'CMS.TextAnalytics.General', @categoryParentID, '', 3, 0, '', 1, 0, @categoryResourceID)

	END

	END

	SET @categoryParentID = (SELECT TOP 1 [CategoryID] FROM [CMS_SettingsCategory] WHERE [CategoryName] = 'CMS.Content')
	IF @categoryParentID IS NOT NULL BEGIN

	SET @categoryResourceID = (SELECT TOP 1 [ResourceID] FROM [CMS_Resource] WHERE [ResourceGUID] = '1ab44056-7cc1-4ad2-bc49-3adae489654f')
	IF @categoryResourceID IS NOT NULL BEGIN

	UPDATE [CMS_SettingsCategory] SET 
			[CategoryOrder] = 3
		WHERE [CategoryName] = 'CMS.Content.TranslationServices' AND [CategoryParentID] = @categoryParentID

	END

	END


	-- Recalculate IDPath and ChildCount for CMS_SettingsCategory
	DECLARE @categoryCursor CURSOR;
	SET @categoryCursor = CURSOR FOR SELECT [CategoryID], [CategoryParentID] FROM [CMS_SettingsCategory] WHERE [CategoryName] IN ('CMS.Content', 'CMS.Content.TextAnalytics', 'CMS.TextAnalytics.General') ORDER BY [CategoryLevel], [CategoryID]

	DECLARE @categoryID int;

	OPEN @categoryCursor

	FETCH NEXT FROM @categoryCursor INTO @categoryID, @categoryParentID;
	WHILE @@FETCH_STATUS = 0
	BEGIN

	UPDATE [CMS_SettingsCategory] SET

		[CategoryChildCount] = (SELECT COUNT(*)
										FROM [CMS_SettingsCategory] AS [Children]
										WHERE [Children].[CategoryParentID] = @categoryID),

		[CategoryIDPath] = COALESCE((SELECT TOP 1 [CategoryIDPath] FROM [CMS_SettingsCategory] AS [Parent] WHERE [Parent].CategoryID = @categoryParentID), '')
							  + '/'
							  + REPLICATE('0', 8 - LEN(@categoryID))
							  + CAST(@categoryID AS NVARCHAR(8))

	WHERE [CategoryID] = @categoryID

	FETCH NEXT FROM @categoryCursor INTO @categoryID, @categoryParentID;
	END

	CLOSE @categoryCursor;
	DEALLOCATE @categoryCursor;


	-- Add new settings keys
	DECLARE @keyCategoryID int;
	SET @keyCategoryID = (SELECT TOP 1 [CategoryID] FROM [CMS_SettingsCategory] WHERE [CategoryName] = 'CMS.TextAnalytics.General')
	IF @keyCategoryID IS NOT NULL BEGIN

	INSERT [CMS_SettingsKey] ([KeyName], [KeyDisplayName], [KeyDescription], [KeyValue], [KeyType], [KeyCategoryID], [SiteID], [KeyGUID], [KeyLastModified], [KeyOrder], [KeyDefaultValue], [KeyValidation], [KeyEditingControlPath], [KeyIsGlobal], [KeyIsCustom], [KeyIsHidden], [KeyFormControlSettings], [KeyExplanationText])
	 VALUES ('CMSAzureTextAnalyticsAPIEndpoint', '{$settingskey.cmsazuretextanalyticsapiendpoint$}', '{$settingskey.cmsazuretextanalyticsapiendpoint.description$}', NULL, 'string', @keyCategoryID, NULL, '4d84b483-161e-4eca-9c4b-1cf59b79c7e9', getDate(), 2, NULL, NULL, NULL, 0, 0, 0, NULL, '')

	INSERT [CMS_SettingsKey] ([KeyName], [KeyDisplayName], [KeyDescription], [KeyValue], [KeyType], [KeyCategoryID], [SiteID], [KeyGUID], [KeyLastModified], [KeyOrder], [KeyDefaultValue], [KeyValidation], [KeyEditingControlPath], [KeyIsGlobal], [KeyIsCustom], [KeyIsHidden], [KeyFormControlSettings], [KeyExplanationText])
	 VALUES ('CMSAzureTextAnalyticsAPIKey', '{$settingskey.cmsazuretextanalyticsapikey$}', '{$settingskey.cmsazuretextanalyticsapikey.description$}', NULL, 'string', @keyCategoryID, NULL, '0d4a2fda-a2d4-4dc3-914c-7cfa81cb0978', getDate(), 3, NULL, NULL, NULL, 0, 0, 0, NULL, '')

	INSERT [CMS_SettingsKey] ([KeyName], [KeyDisplayName], [KeyDescription], [KeyValue], [KeyType], [KeyCategoryID], [SiteID], [KeyGUID], [KeyLastModified], [KeyOrder], [KeyDefaultValue], [KeyValidation], [KeyEditingControlPath], [KeyIsGlobal], [KeyIsCustom], [KeyIsHidden], [KeyFormControlSettings], [KeyExplanationText])
	 VALUES ('CMSEnableSentimentAnalysis', '{$settingskey.CMSEnableSentimentAnalysis$}', '{$settingskey.cmsenablesentimentanalysis.description$}', 'False', 'boolean', @keyCategoryID, NULL, '7b28d858-a7a5-4c68-9422-2ba8fbc27486', getDate(), 1, 'False', NULL, NULL, 0, 0, 0, NULL, '')

	END
END
GO

DECLARE @REFRESH_2_VERSION INT = 31;
DECLARE @HOTFIXVERSION INT;
SET @HOTFIXVERSION = (SELECT [KeyValue] FROM [CMS_SettingsKey] WHERE [KeyName] = N'CMSHotfixVersion')
IF @HOTFIXVERSION < @REFRESH_2_VERSION
BEGIN
	DECLARE @keyCategoryID int;
	SET @keyCategoryID = (SELECT TOP 1 [CategoryID] FROM [CMS_SettingsCategory] WHERE [CategoryName] = 'CMS.URL.ContentTreeRouting')
	IF @keyCategoryID IS NOT NULL
	BEGIN
		IF NOT EXISTS(SELECT 1 FROM [CMS_SettingsKey] WHERE [KeyName] = 'CMSUseCultureAliasAsLanguagePrefixInUrl' AND [SiteID] IS NULL AND [KeyCategoryID] = @keyCategoryID)
			BEGIN
				INSERT [CMS_SettingsKey] ([KeyName], [KeyDisplayName], [KeyDescription], [KeyValue], [KeyType], [KeyCategoryID], [SiteID], [KeyGUID], [KeyLastModified], [KeyOrder], [KeyDefaultValue], [KeyValidation], [KeyEditingControlPath], [KeyIsGlobal], [KeyIsCustom], [KeyIsHidden], [KeyFormControlSettings], [KeyExplanationText])
				VALUES ('CMSUseCultureAliasAsLanguagePrefixInUrl', '{$settingskey.cmsuseculturealiasaslanguageprefixinurl$}', '{$settingskey.cmsuseculturealiasaslanguageprefixinurl.description$}', 'False', 'boolean', @keyCategoryID, NULL, '097676e8-23d6-4d42-8f04-94943483918c', getDate(), 5, 'False', NULL, NULL, 0, 0, 0, NULL, '')

				UPDATE [CMS_SettingsKey] SET [KeyOrder] = 6
				WHERE [KeyName] = 'CMSLowercaseURLs' AND [SiteID] IS NULL AND [KeyCategoryID] = @keyCategoryID

				UPDATE [CMS_SettingsKey] SET [KeyOrder] = 8
				WHERE [KeyName] = 'CMSStoreFormerUrls' AND [SiteID] IS NULL AND [KeyCategoryID] = @keyCategoryID

				UPDATE [CMS_SettingsKey] SET [KeyOrder] = 7
				WHERE [KeyName] = 'CMSUseURLsWithTrailingSlash' AND [SiteID] IS NULL AND [KeyCategoryID] = @keyCategoryID
			END
	END

    IF NOT EXISTS (SELECT 1 FROM sys.types WHERE is_table_type = 1 AND name = 'Type_CMS_PageUrlPathCultureAliasesTable')
		BEGIN
			CREATE TYPE [Type_CMS_PageUrlPathCultureAliasesTable] AS TABLE(
			[CultureCode] [nvarchar](50) NOT NULL,
			[CultureAlias] [nvarchar](100))
		END

	EXEC('ALTER PROCEDURE [Proc_CMS_PageUrlPath_ChangeUrlCultureFormat]
	@SiteID INT,
	@CultureCode NVARCHAR(50) = NULL,
	@ModifyCulturePrefix INT,
	@DefaultCultureCode NVARCHAR(50),
	@HidePrefixForDefaultCulture BIT,
	@UseCultureAliasAsCulturePrefix BIT,
	@PageUrlPathCultureAliases Type_CMS_PageUrlPathCultureAliasesTable READONLY
AS
BEGIN
    BEGIN TRANSACTION
		BEGIN TRY
			UPDATE [CMS_PageUrlPath]
			SET [PageUrlPathUrlPath] =
				-- Do not modify default culture prefix if is set to be hidden.
				CASE WHEN @CultureCode IS NULL AND [PageUrlPathCulture] = @DefaultCultureCode AND @HidePrefixForDefaultCulture = 1
					THEN [PageUrlPathUrlPath]
				ELSE
					-- Add, remove or modify language prefix.
					CASE @ModifyCulturePrefix
						WHEN 1 THEN
							-- Add prefix to URL. Use culture code or culture alias as prefix (based on @UseCultureAliasAsCulturePrefix flag and culture alias value).
							CASE WHEN @UseCultureAliasAsCulturePrefix = 1 AND (SELECT [CultureAlias] FROM @PageUrlPathCultureAliases WHERE [CultureCode] = [PageUrlPathCulture]) IS NOT NULL THEN
								(SELECT [CultureAlias] FROM @PageUrlPathCultureAliases WHERE [CultureCode] = [PageUrlPathCulture]) + ''/'' + [PageUrlPathUrlPath]
							ELSE
								[PageUrlPathCulture] + ''/'' + [PageUrlPathUrlPath]
							END
						WHEN 2 THEN
							-- Change URL language prefix to culture code or culture alias (based on @UseCultureAliasAsCulturePrefix flag and culture alias value).
							CASE WHEN @UseCultureAliasAsCulturePrefix = 1 AND (SELECT [CultureAlias] FROM @PageUrlPathCultureAliases WHERE [CultureCode] = [PageUrlPathCulture]) IS NOT NULL THEN
								-- Remove original prefix (first segment) and add culture alias as replacement.
								(SELECT [CultureAlias] FROM @PageUrlPathCultureAliases WHERE [CultureCode] = [PageUrlPathCulture]) + ''/'' + SUBSTRING([PageUrlPathUrlPath], CHARINDEX(''/'', [PageUrlPathUrlPath]) + 1, LEN([PageUrlPathUrlPath]))
							ELSE
								-- Remove original prefix (first segment) and add culture code as replacement.
								[PageUrlPathCulture] + ''/'' + SUBSTRING([PageUrlPathUrlPath], CHARINDEX(''/'', [PageUrlPathUrlPath]) + 1, LEN([PageUrlPathUrlPath]))
							END
						ELSE
							-- Remove prefix (first segment) of the URL.
							SUBSTRING([PageUrlPathUrlPath], CHARINDEX(''/'', [PageUrlPathUrlPath]) + 1, LEN([PageUrlPathUrlPath]))
					END
				END,
				[PageUrlPathUrlPathHash] =
					CASE WHEN @CultureCode IS NULL AND [PageUrlPathCulture] = @DefaultCultureCode AND @HidePrefixForDefaultCulture = 1
						THEN CONVERT(VARCHAR(64), HASHBYTES(''SHA2_256'', LOWER([PageUrlPathUrlPath])), 2)
					ELSE
						CASE @ModifyCulturePrefix
							WHEN 1 THEN
								CASE WHEN @UseCultureAliasAsCulturePrefix = 1 AND (SELECT [CultureAlias] FROM @PageUrlPathCultureAliases WHERE [CultureCode] = [PageUrlPathCulture]) IS NOT NULL THEN
									CONVERT(VARCHAR(64), HASHBYTES(''SHA2_256'', LOWER((SELECT [CultureAlias] FROM @PageUrlPathCultureAliases WHERE [CultureCode] = [PageUrlPathCulture]) + ''/'' + [PageUrlPathUrlPath])), 2)
								ELSE
									CONVERT(VARCHAR(64), HASHBYTES(''SHA2_256'', LOWER([PageUrlPathCulture] + ''/'' + [PageUrlPathUrlPath])), 2)
								END
							WHEN 2 THEN
								CASE WHEN @UseCultureAliasAsCulturePrefix = 1 AND (SELECT [CultureAlias] FROM @PageUrlPathCultureAliases WHERE [CultureCode] = [PageUrlPathCulture]) IS NOT NULL THEN
									CONVERT(VARCHAR(64), HASHBYTES(''SHA2_256'', LOWER((SELECT [CultureAlias] FROM @PageUrlPathCultureAliases WHERE [CultureCode] = [PageUrlPathCulture]) + ''/'' + SUBSTRING([PageUrlPathUrlPath], CHARINDEX(''/'', [PageUrlPathUrlPath]) + 1, LEN([PageUrlPathUrlPath])))), 2)
								ELSE
									CONVERT(VARCHAR(64), HASHBYTES(''SHA2_256'', LOWER([PageUrlPathCulture] + ''/'' + SUBSTRING([PageUrlPathUrlPath], CHARINDEX(''/'', [PageUrlPathUrlPath]) + 1, LEN([PageUrlPathUrlPath])))), 2)
								END
							ELSE
								CONVERT(VARCHAR(64), HASHBYTES(''SHA2_256'', LOWER(SUBSTRING([PageUrlPathUrlPath], CHARINDEX(''/'', [PageUrlPathUrlPath]) + 1, LEN([PageUrlPathUrlPath])))), 2)
						END
					END
			WHERE [PageUrlPathSiteID] = @SiteID AND (@CultureCode IS NULL OR [PageUrlPathCulture] = @CultureCode)
		END TRY
		BEGIN CATCH
			IF XACT_STATE() = 1
			BEGIN
				DECLARE @CollisionPaths TABLE(
					[PathID] INT NOT NULL,
					[NewPath] NVARCHAR(2000) NOT NULL,
					[OldPath] NVARCHAR(2000) NOT NULL,
					[Culture] NVARCHAR(50) NOT NULL)

				DECLARE @AltUrl TABLE(
					[AlternativeUrl] NVARCHAR(450) NOT NULL
				)

				INSERT INTO @AltUrl
					SELECT [AlternativeUrlUrl]
					FROM [CMS_AlternativeUrl] [A1]
					WHERE NOT EXISTS
						(SELECT 1 FROM [CMS_AlternativeUrl] [A2]
							WHERE [A1].[AlternativeUrlUrl] LIKE [A2].[AlternativeUrlUrl] + ''/%''
								AND [A1].[AlternativeUrlSiteID] = [A2].[AlternativeUrlSiteID])
						AND [A1].[AlternativeUrlSiteID] = @SiteID;

				INSERT INTO @CollisionPaths
					SELECT
						[PageUrlPathID] AS [PathID],
						[PageUrlPathUrlPath] + ''-'' + LOWER(CONVERT(VARCHAR(32), HASHBYTES(''MD5'', LOWER([PageUrlPathUrlPath] + [PageUrlPathCulture] + CONVERT(NVARCHAR(36), [NodeGUID]))), 2)) AS [NewPath],
						[PageUrlPathUrlPath] AS [OldPath],
						[PageUrlPathCulture] AS [Culture]
					FROM [CMS_PageUrlPath] [P]
					INNER JOIN [CMS_Tree] ON [NodeID] = [PageUrlPathNodeID]
					INNER JOIN @AltUrl [A] ON [A].[AlternativeUrl] =
						CASE WHEN @CultureCode IS NULL AND [P].[PageUrlPathCulture] = @DefaultCultureCode AND @HidePrefixForDefaultCulture = 1
							THEN [P].[PageUrlPathUrlPath]
						ELSE
							CASE @ModifyCulturePrefix
								WHEN 1 THEN
									CASE @UseCultureAliasAsCulturePrefix WHEN 1 THEN
										(SELECT [CultureAlias] FROM @PageUrlPathCultureAliases WHERE [CultureCode] = [P].[PageUrlPathCulture]) + ''/'' + [P].[PageUrlPathUrlPath]
									ELSE
										[P].[PageUrlPathCulture] + ''/'' + [P].[PageUrlPathUrlPath]
									END
								WHEN 2 THEN
									CASE WHEN @UseCultureAliasAsCulturePrefix = 1 AND (SELECT [CultureAlias] FROM @PageUrlPathCultureAliases WHERE [CultureCode] = [P].[PageUrlPathCulture]) IS NOT NULL THEN
										(SELECT [CultureAlias] FROM @PageUrlPathCultureAliases WHERE [CultureCode] = [P].[PageUrlPathCulture]) + ''/'' + SUBSTRING([P].[PageUrlPathUrlPath], CHARINDEX(''/'', [P].[PageUrlPathUrlPath]) + 1, LEN([P].[PageUrlPathUrlPath]))
									ELSE
										[P].[PageUrlPathCulture] + ''/'' + SUBSTRING([P].[PageUrlPathUrlPath], CHARINDEX(''/'', [P].[PageUrlPathUrlPath]) + 1, LEN([P].[PageUrlPathUrlPath]))
									END
								ELSE
									SUBSTRING([P].[PageUrlPathUrlPath], CHARINDEX(''/'', [P].[PageUrlPathUrlPath]) + 1, LEN([P].[PageUrlPathUrlPath]))
								END
						END
					WHERE [PageUrlPathSiteID] = @SiteID AND (@CultureCode IS NULL OR [PageUrlPathCulture] = @CultureCode)

				INSERT INTO @CollisionPaths
					SELECT
						[P].[PageUrlPathID] AS [PathID],
						[C].[NewPath] + SUBSTRING([P].[PageUrlPathUrlPath], LEN([C].[OldPath]) + 1, LEN([P].[PageUrlPathUrlPath])) AS [NewPath],
						[P].[PageUrlPathUrlPath] AS [OldPath],
						[P].[PageUrlPathCulture] AS [Culture]
					FROM @CollisionPaths [C]
					INNER JOIN [CMS_PageUrlPath] [P] ON [P].[PageUrlPathUrlPath] LIKE [C].[OldPath] + ''/%''
					WHERE [P].[PageUrlPathSiteID] = @SiteID  AND (@CultureCode IS NULL OR [P].[PageUrlPathCulture] = @CultureCode) AND [C].[Culture] = [P].[PageUrlPathCulture]

				UPDATE [P]
				SET [P].[PageUrlPathUrlPath] =
						CASE WHEN @CultureCode IS NULL AND [P].[PageUrlPathCulture] = @DefaultCultureCode AND @HidePrefixForDefaultCulture = 1
							THEN COALESCE([C].[NewPath], [P].[PageUrlPathUrlPath])
						ELSE
							CASE @ModifyCulturePrefix
							WHEN 1 THEN
								CASE WHEN @UseCultureAliasAsCulturePrefix = 1 AND (SELECT [CultureAlias] FROM @PageUrlPathCultureAliases WHERE [CultureCode] = [P].[PageUrlPathCulture]) IS NOT NULL THEN
									(SELECT [CultureAlias] FROM @PageUrlPathCultureAliases WHERE [CultureCode] = [P].[PageUrlPathCulture]) + ''/'' + COALESCE([C].[NewPath], [P].[PageUrlPathUrlPath])
								ELSE
									[P].[PageUrlPathCulture] + ''/'' + COALESCE([C].[NewPath], [P].[PageUrlPathUrlPath])
								END
							WHEN 2 THEN
								CASE WHEN @UseCultureAliasAsCulturePrefix = 1 AND (SELECT [CultureAlias] FROM @PageUrlPathCultureAliases WHERE [CultureCode] = [P].[PageUrlPathCulture]) IS NOT NULL THEN
									(SELECT [CultureAlias] FROM @PageUrlPathCultureAliases WHERE [CultureCode] = [P].[PageUrlPathCulture]) + ''/'' + SUBSTRING(COALESCE([C].[NewPath], [P].[PageUrlPathUrlPath]) , CHARINDEX(''/'', COALESCE([C].[NewPath], [P].[PageUrlPathUrlPath])) + 1, LEN(COALESCE([C].[NewPath], [P].[PageUrlPathUrlPath])))
								ELSE
									[P].[PageUrlPathCulture] + ''/'' + SUBSTRING(COALESCE([C].[NewPath], [P].[PageUrlPathUrlPath]), CHARINDEX(''/'', COALESCE([C].[NewPath], [P].[PageUrlPathUrlPath])) + 1, LEN(COALESCE([C].[NewPath], [P].[PageUrlPathUrlPath])))
								END
							ELSE
								SUBSTRING(COALESCE([C].[NewPath], [P].[PageUrlPathUrlPath]), CHARINDEX(''/'', COALESCE([C].[NewPath], [P].[PageUrlPathUrlPath])) + 1, LEN(COALESCE([C].[NewPath], [P].[PageUrlPathUrlPath])))
							END
						END,
					[P].[PageUrlPathUrlPathHash] =
						CASE WHEN @CultureCode IS NULL AND [P].[PageUrlPathCulture] = @DefaultCultureCode AND @HidePrefixForDefaultCulture = 1
							THEN CONVERT(VARCHAR(64), HASHBYTES(''SHA2_256'', LOWER(COALESCE([C].[NewPath], [P].[PageUrlPathUrlPath]))), 2)
						ELSE
							CASE @ModifyCulturePrefix
							WHEN 1 THEN
								CASE WHEN @UseCultureAliasAsCulturePrefix = 1 AND (SELECT [CultureAlias] FROM @PageUrlPathCultureAliases WHERE [CultureCode] = [P].[PageUrlPathCulture]) IS NOT NULL THEN
									CONVERT(VARCHAR(64), HASHBYTES(''SHA2_256'', LOWER((SELECT [CultureAlias] FROM @PageUrlPathCultureAliases WHERE [CultureCode] = [P].[PageUrlPathCulture]) + ''/'' + COALESCE([C].[NewPath], [P].[PageUrlPathUrlPath]))), 2)
								ELSE
									CONVERT(VARCHAR(64), HASHBYTES(''SHA2_256'', LOWER([P].[PageUrlPathCulture] + ''/'' + COALESCE([C].[NewPath], [P].[PageUrlPathUrlPath]))), 2)
								END
							WHEN 2 THEN
								CASE WHEN @UseCultureAliasAsCulturePrefix = 1 AND (SELECT [CultureAlias] FROM @PageUrlPathCultureAliases WHERE [CultureCode] = [P].[PageUrlPathCulture]) IS NOT NULL THEN
									CONVERT(VARCHAR(64), HASHBYTES(''SHA2_256'', LOWER((SELECT [CultureAlias] FROM @PageUrlPathCultureAliases WHERE [CultureCode] =[P].[PageUrlPathCulture]) + ''/'' + SUBSTRING(COALESCE([C].[NewPath], [P].[PageUrlPathUrlPath]), CHARINDEX(''/'', COALESCE([C].[NewPath], [P].[PageUrlPathUrlPath])) + 1, LEN(COALESCE([C].[NewPath], [P].[PageUrlPathUrlPath]))))), 2)
								ELSE
									CONVERT(VARCHAR(64), HASHBYTES(''SHA2_256'', LOWER([P].[PageUrlPathCulture] + ''/'' + SUBSTRING(COALESCE([C].[NewPath], [P].[PageUrlPathUrlPath]), CHARINDEX(''/'', COALESCE([C].[NewPath], [P].[PageUrlPathUrlPath])) + 1, LEN(COALESCE([C].[NewPath], [P].[PageUrlPathUrlPath]))))), 2)
								END
							ELSE
								CONVERT(VARCHAR(64), HASHBYTES(''SHA2_256'', LOWER(SUBSTRING(COALESCE([C].[NewPath], [P].[PageUrlPathUrlPath]), CHARINDEX(''/'', COALESCE([C].[NewPath], [P].[PageUrlPathUrlPath])) + 1, LEN(COALESCE([C].[NewPath], [P].[PageUrlPathUrlPath]))))), 2)
							END
						END
				FROM [CMS_PageUrlPath] [P]
				LEFT JOIN @CollisionPaths [C] ON [P].[PageUrlPathID] = [C].[PathID]
				WHERE [P].[PageUrlPathSiteID] = @SiteID AND (@CultureCode IS NULL OR [P].[PageUrlPathCulture] = @CultureCode)
			END
			ELSE
			BEGIN
				ROLLBACK TRANSACTION
			END
		END CATCH
	COMMIT TRANSACTION
END')

	EXEC('ALTER PROCEDURE [Proc_CMS_PageUrlPath_GenerateUrlPaths]
	@StartingNodeID INT = NULL,
	@SiteID INT,
	@CultureCodes Type_CMS_StringTable READONLY,
	@LastModified DATETIME,
	@UseCulturePrefix BIT,
	@DefaultCultureCode NVARCHAR(50),
	@HidePrefixForDefaultCulture BIT,
	@UseCultureAliasAsCulturePrefix BIT,
	@PageUrlPathCultureAliases Type_CMS_PageUrlPathCultureAliasesTable READONLY
AS
BEGIN
	DECLARE @PageTypes TABLE(
		[ID] INT NOT NULL,
		[HasUrl] BIT NOT NULL)

	INSERT INTO @PageTypes ([ID], [HasUrl])
		SELECT
			[ClassID],
			[ClassHasURL]
		FROM [CMS_Class]
		WHERE [ClassID] IN (SELECT [ClassID] FROM [CMS_ClassSite] WHERE [SiteID] = @SiteID)

	IF OBJECT_ID(N''tempdb..#PreparedPaths'') IS NOT NULL
	BEGIN
		DROP TABLE #PreparedPaths
	END

	CREATE TABLE #PreparedPaths (
		[ID] INT IDENTITY(1,1),
		[CultureCode] NVARCHAR(50) COLLATE DATABASE_DEFAULT NOT NULL,
		[NodeID] INT NOT NULL,
		[NodeGUID] UNIQUEIDENTIFIER NOT NULL,
		[UrlPath] NVARCHAR(2000) COLLATE DATABASE_DEFAULT NOT NULL,
		[NodeSiteID] INT NOT NULL,
		[IsRootPath] BIT NOT NULL,
		[NodeAliasPath] NVARCHAR(450) COLLATE DATABASE_DEFAULT NOT NULL,
		[NodeLevel] INT NOT NULL,
		[PathHash] NVARCHAR(64) COLLATE DATABASE_DEFAULT NOT NULL
	)

	DECLARE @PrefixPath NVARCHAR(2000) = ''''

	-- Builds parent URL path prefix from node aliases.
	-- In the prefix participate only node aliases of pages having URL
	IF @StartingNodeID IS NOT NULL
	BEGIN
		;WITH ParentPaths AS (
			SELECT
				[NodeID],
				[NodeParentID],
				[NodeSiteID],
				[NodeAliasPath],
				CAST('''' AS NVARCHAR(2000)) AS ParentPath		-- Current path does not participate in prefix
			FROM [CMS_Tree]
			WHERE
				[NodeID] = @StartingNodeID

			UNION ALL

			SELECT
				[T].[NodeID],
				[T].[NodeParentID],
				[T].[NodeSiteID],
				[T].[NodeAliasPath],
				CAST(CASE HasUrl
					 WHEN 1 THEN CASE [P].[ParentPath] WHEN '''' THEN [T].[NodeAlias] ELSE [T].[NodeAlias] + ''/'' + [P].[ParentPath] END
					 ELSE [P].[ParentPath] END AS NVARCHAR(2000))
					 AS ParentPath
			FROM
				[CMS_Tree] AS [T]
					INNER JOIN ParentPaths [P] ON [T].[NodeID] = [P].[NodeParentID]
					INNER JOIN @PageTypes ON [T].[NodeClassID] = [ID]
		)
		SELECT TOP 1 @PrefixPath = [ParentPath] FROM ParentPaths
		WHERE [NodeParentID] IS NULL
	END

	-- Use temporary table with optimized index for building the paths
	IF OBJECT_ID(N''tempdb..#Nodes'') IS NOT NULL
	BEGIN
		DROP TABLE #Nodes
	END

	CREATE TABLE #Nodes (
		[NodeID] INT NOT NULL,
		[NodeGUID] UNIQUEIDENTIFIER NOT NULL,
		[NodeParentID] INT NULL,
		[NodeSiteID] INT NOT NULL,
		[NodeAlias] NVARCHAR(50) COLLATE DATABASE_DEFAULT NOT NULL,
		[NodeAliasPath] NVARCHAR(450) COLLATE DATABASE_DEFAULT NOT NULL,
		[NodeLevel] INT NOT NULL,
		[HasUrl] BIT NOT NULL
	)

	INSERT INTO #Nodes ([NodeID], [NodeGUID], [NodeParentID], [NodeSiteID], [NodeAlias], [NodeAliasPath], [NodeLevel], [HasUrl])
	SELECT [NodeID], [NodeGUID], [NodeParentID], [NodeSiteID], [NodeAlias], [NodeAliasPath], [NodeLevel], [P].[HasUrl]
	FROM [CMS_Tree] INNER JOIN @PageTypes as [P] ON [NodeClassID] = [ID]

	CREATE NONCLUSTERED INDEX [IX_Nodes_NodeParentID] ON #Nodes ([NodeParentID])
	INCLUDE ([NodeID], [NodeGUID], [NodeSiteID], [NodeAlias], [NodeAliasPath], [NodeLevel], [HasUrl])

	-- Build culture paths for all descendants having URL
	;WITH paths AS (
		SELECT
			[NodeID],
			[NodeGUID],
			CAST(CASE HasUrl
			     WHEN 1 THEN CASE @PrefixPath WHEN '''' THEN [NodeAlias] ELSE @PrefixPath + ''/'' + [NodeAlias] END
				 ELSE @PrefixPath END AS NVARCHAR(2000))
				 AS Suffix,
			[NodeParentID],
			[NodeSiteID],
			[HasUrl],
			[HasUrl] AS IsRootPath,
			[NodeAliasPath],
			[NodeLevel]
		FROM
			#Nodes
		WHERE
			(@StartingNodeID IS NULL AND [NodeParentID] IS NULL AND [NodeSiteID] = @SiteID)
			OR (@StartingNodeID IS NOT NULL AND [NodeID] = @StartingNodeID)

		UNION ALL

		SELECT
			[T].[NodeID],
			[T].[NodeGUID],
			CAST(CASE [T].[HasUrl]
				 WHEN 1 THEN CASE [P].[Suffix] WHEN '''' THEN [T].[NodeAlias] ELSE [P].[Suffix] + ''/'' + [T].[NodeAlias] END
				 ELSE [P].[Suffix] END AS NVARCHAR(2000))
				 AS Suffix,
			[T].[NodeParentID],
			[T].[NodeSiteID],
			[T].[HasUrl],
			CAST(CASE WHEN [T].[HasUrl] = 1 AND [P].[Suffix] = '''' THEN 1 ELSE 0 END AS BIT) AS IsRootPath,
			[T].[NodeAliasPath],
			[T].[NodeLevel]
		FROM
			#Nodes AS [T] INNER JOIN paths [P] ON [P].[NodeID] = [T].[NodeParentID]
	)
	INSERT INTO #PreparedPaths ([CultureCode] , [NodeID], [NodeGUID], [UrlPath], [NodeSiteID], [IsRootPath], [NodeAliasPath], [NodeLevel], [PathHash])
		SELECT
			[CultureCode],
			[NodeID],
			[NodeGUID],
			CASE @UseCulturePrefix
			WHEN 1 THEN
				CASE WHEN [CultureCode] = @DefaultCultureCode AND @HidePrefixForDefaultCulture = 1
				THEN [Suffix]
				ELSE
					CASE WHEN @UseCultureAliasAsCulturePrefix = 1 AND [CultureAlias] IS NOT NULL
					THEN [CultureAlias] + ''/'' + [Suffix]
					ELSE [CultureCode] + ''/'' + [Suffix] END
				END
			ELSE [Suffix] END AS UrlPath,
			[NodeSiteID],
			[IsRootPath],
			[NodeAliasPath],
			[NodeLevel],
			CASE @UseCulturePrefix
			WHEN 1 THEN
				CASE WHEN [CultureCode] = @DefaultCultureCode AND @HidePrefixForDefaultCulture = 1
				THEN CONVERT(VARCHAR(64), HASHBYTES(''SHA2_256'', LOWER([Suffix])), 2)
				ELSE
					CASE WHEN @UseCultureAliasAsCulturePrefix = 1 AND [CultureAlias] IS NOT NULL
					THEN CONVERT(VARCHAR(64), HASHBYTES(''SHA2_256'', LOWER([CultureAlias] + ''/'' + [Suffix] )), 2)
					ELSE CONVERT(VARCHAR(64), HASHBYTES(''SHA2_256'', LOWER([CultureCode] + ''/'' + [Suffix] )), 2) END
				END
			ELSE CONVERT(VARCHAR(64), HASHBYTES(''SHA2_256'', LOWER([Suffix])), 2) END AS [PathHash]
		FROM
		(
			SELECT [NodeID], [NodeGUID], [Suffix], [NodeSiteID], [C].[Value] as [CultureCode], [A].[CultureAlias] as [CultureAlias], [IsRootPath], [NodeAliasPath], [NodeLevel]
			FROM paths CROSS JOIN @CultureCodes AS C
			LEFT JOIN @PageUrlPathCultureAliases AS A ON [A].[CultureCode] = [C].[Value] WHERE [HasUrl] = 1
		) AS S

	-- Index allows quickly to find collision paths inside the table with prepared data
	CREATE NONCLUSTERED INDEX [IX_PreparedPaths_PathHash_CultureCode] ON #PreparedPaths (
		[PathHash] ASC,
		[CultureCode] ASC
	)

    BEGIN TRANSACTION
		BEGIN TRY

			IF ((SELECT COUNT(*)
				FROM #PreparedPaths
				GROUP BY [PathHash], [CultureCode]
				HAVING COUNT(*) > 1) > 0
			)
				THROW 50001, ''Collision found'', 1

			INSERT INTO [CMS_PageUrlPath]([PageUrlPathGUID], [PageUrlPathCulture], [PageUrlPathNodeID], [PageUrlPathUrlPath], [PageUrlPathUrlPathHash], [PageUrlPathSiteID], [PageUrlPathLastModified])
				SELECT
					NEWID(),
					[CultureCode],
					[NodeID],
					[UrlPath],
					[PathHash],
					[NodeSiteID],
					@LastModified
				FROM #PreparedPaths
		END TRY
		BEGIN CATCH
			IF XACT_STATE() = 1
			BEGIN

				DECLARE @CollidingPathGroups TABLE (
					[CultureCode] NVARCHAR(50) NOT NULL,
					[PathHash] NVARCHAR(64) NOT NULL
				)

				-- Table for paths that are in a collision with other paths in the prepared data
				DECLARE @CollidingPaths TABLE (
					[CultureCode] NVARCHAR(50) NOT NULL,
					[NodeGUID] UNIQUEIDENTIFIER NOT NULL,
					[UrlPath] NVARCHAR(2000) NOT NULL,
					[NodeAliasPath] NVARCHAR(450) NOT NULL,
					[NodeLevel] INT NOT NULL
				)

				DECLARE @CollidingUrlPath NVARCHAR(2000)
				DECLARE @CollidingCultureCode NVARCHAR(50)
				DECLARE @CollidingNodeGUID uniqueidentifier
				DECLARE @CollidingNodeAliasPath NVARCHAR(450)

				WHILE (1 = 1)
				BEGIN
					INSERT INTO @CollidingPathGroups([PathHash], [CultureCode])
					SELECT [PathHash], [CultureCode]
						FROM #PreparedPaths
						GROUP BY [PathHash], [CultureCode]
						HAVING COUNT(*) > 1

					INSERT INTO @CollidingPaths([UrlPath], [CultureCode], [NodeGUID], [NodeAliasPath], [NodeLevel])
					SELECT [P].[UrlPath], [P].[CultureCode], [P].[NodeGUID], [P].[NodeAliasPath], [P].[NodeLevel]
						FROM #PreparedPaths [P]
						INNER JOIN @CollidingPathGroups [C] ON [P].[PathHash] = [C].[PathHash] AND [P].[CultureCode] = [C].[CultureCode]

					IF ((SELECT COUNT([UrlPath]) FROM @CollidingPaths) = 0)
						BREAK;

					-- Order colliding items by NodeLevel to ensure that parent paths will be processed first
					SELECT TOP (1) @CollidingUrlPath = [UrlPath], @CollidingCultureCode = [CultureCode], @CollidingNodeGUID = [NodeGUID], @CollidingNodeAliasPath = [NodeAliasPath]
					FROM @CollidingPaths
					ORDER BY [NodeLevel], [NodeAliasPath]

					DECLARE @nonColisionUrlPath NVARCHAR(2000)
					SET @nonColisionUrlPath = @CollidingUrlPath + ''-'' + LOWER(CONVERT(VARCHAR(32), HASHBYTES(''MD5'', LOWER(@CollidingUrlPath + @CollidingCultureCode + CONVERT(NVARCHAR(36), @CollidingNodeGUID))), 2))

					UPDATE #PreparedPaths
					SET [UrlPath] = @nonColisionUrlPath + SUBSTRING(UrlPath, LEN(@CollidingUrlPath) + 1, LEN([UrlPath])),
						[PathHash] = CONVERT(VARCHAR(64), HASHBYTES(''SHA2_256'', LOWER(@nonColisionUrlPath + SUBSTRING(UrlPath, LEN(@CollidingUrlPath) + 1, LEN([UrlPath])))), 2)
					WHERE ([NodeAliasPath] = @CollidingNodeAliasPath OR [NodeAliasPath] LIKE @CollidingNodeAliasPath + ''/%'') AND [CultureCode] = @CollidingCultureCode

					DELETE FROM @CollidingPathGroups
					DELETE FROM @CollidingPaths
				END

				DECLARE @ParentsWithUrl TABLE(
					[CultureCode] NVARCHAR(50),
					[UrlPath] NVARCHAR(2000),
					[UrlPathWithHash] NVARCHAR(2000),
					[NodeAliasPath] NVARCHAR(450) NOT NULL
				)
				INSERT INTO @ParentsWithUrl ([CultureCode], [UrlPath], [UrlPathWithHash], [NodeAliasPath])
				SELECT
					[CultureCode],
					[UrlPath],
					CASE WHEN
						EXISTS (
							SELECT TOP 1 [PageUrlPathID] FROM [CMS_PageUrlPath] WHERE ([PageUrlPathUrlPath] = [UrlPath] OR [PageUrlPathUrlPath] LIKE [UrlPath] + ''/%'') AND [PageUrlPathCulture] = [CultureCode] AND [PageUrlPathSiteID] = @SiteID
							UNION
							SELECT TOP 1 [AlternativeUrlID] FROM [CMS_AlternativeUrl] WHERE ([AlternativeUrlUrl] = [UrlPath] OR [AlternativeUrlUrl] LIKE [UrlPath] + ''/%'') AND [AlternativeUrlSiteID] = @SiteID
						)
						THEN [UrlPath] + ''-'' + LOWER(CONVERT(VARCHAR(32), HASHBYTES(''MD5'', LOWER([UrlPath] + [CultureCode] + CONVERT(NVARCHAR(36), [NodeGUID]))), 2))
						ELSE [UrlPath]
					END,
					[NodeAliasPath]
				FROM #PreparedPaths [PreparedPath]
				WHERE [IsRootPath] = 1

				INSERT INTO [CMS_PageUrlPath]([PageUrlPathGUID], [PageUrlPathCulture], [PageUrlPathNodeID], [PageUrlPathUrlPath], [PageUrlPathUrlPathHash], [PageUrlPathSiteID], [PageUrlPathLastModified])
					SELECT
						NEWID(),
						[CultureCode],
						[NodeID],
						[UrlPathWithHash],
						CONVERT(VARCHAR(64), HASHBYTES(''SHA2_256'', LOWER([UrlPathWithHash])), 2),	-- recompute hash since the UrlPath may be changed because of collisions
						[NodeSiteID],
						@LastModified
					FROM
					(
						SELECT
							[PreparedPath].[CultureCode],
							[PreparedPath].[NodeID],
							[Parent].[UrlPathWithHash] + SUBSTRING([PreparedPath].[UrlPath] , LEN([Parent].[UrlPath])  + 1, LEN([PreparedPath].[UrlPath]) - LEN([Parent].[UrlPath])) AS UrlPathWithHash,
							[PreparedPath].[NodeSiteID]
						FROM #PreparedPaths [PreparedPath]
						INNER JOIN @ParentsWithUrl [Parent]
							ON ([PreparedPath].[UrlPath] = [Parent].[UrlPath] OR [PreparedPath].[UrlPath] LIKE [Parent].[UrlPath] + ''/%'')
							AND [PreparedPath].[CultureCode] = [Parent].[CultureCode]
					) AS S
			END
			ELSE
			BEGIN
				ROLLBACK TRANSACTION
			END
		END CATCH
	COMMIT TRANSACTION
END')
END
GO

-- KX-2033
DECLARE @REFRESH_4_VERSION INT = 52;
DECLARE @HOTFIXVERSION INT;
SET @HOTFIXVERSION = (SELECT [KeyValue] FROM [CMS_SettingsKey] WHERE [KeyName] = N'CMSHotfixVersion')
IF @HOTFIXVERSION < @REFRESH_4_VERSION
BEGIN
	-- Rename settings categories
	UPDATE [CMS_SettingsCategory] SET
			[CategoryName] = 'CMS.Content.CognitiveServices',
			[CategoryDisplayName] = '{$settingscategory.cmscognitiveservices$}'
		WHERE [CategoryName] = 'CMS.Content.TextAnalytics'

	UPDATE [CMS_SettingsCategory] SET
			[CategoryName] = 'CMS.CognitiveServices.TextAnalytics',
			[CategoryDisplayName] = '{$settingscategory.cmstextanalytics$}'
		WHERE [CategoryName] = 'CMS.TextAnalytics.General'

	-- Add new settings categories
	DECLARE @categoryParentID int;
	SET @categoryParentID = (SELECT TOP 1 [CategoryID] FROM [CMS_SettingsCategory] WHERE [CategoryName] = 'CMS.Content.CognitiveServices')
	IF @categoryParentID IS NOT NULL BEGIN

	DECLARE @categoryResourceID int;
	SET @categoryResourceID = (SELECT TOP 1 [ResourceID] FROM [CMS_Resource] WHERE [ResourceGUID] = '98c6ee00-230a-4207-a6d3-03677b83b245')
	IF @categoryResourceID IS NOT NULL BEGIN

	INSERT [CMS_SettingsCategory] ([CategoryDisplayName], [CategoryOrder], [CategoryName], [CategoryParentID], [CategoryIDPath], [CategoryLevel], [CategoryChildCount], [CategoryIconPath], [CategoryIsGroup], [CategoryIsCustom], [CategoryResourceID])
	 VALUES ('{$settingscategory.cmscognitiveservicesimagerecognition$}', 1, 'CMS.CognitiveServices.ImageRecognition', @categoryParentID, '', 3, 0, '', 1, 0, @categoryResourceID)

	END

	END

	-- Recalculate IDPath and ChildCount for CMS_SettingsCategory
	DECLARE @categoryCursor CURSOR;
	SET @categoryCursor = CURSOR FOR SELECT [CategoryID], [CategoryParentID] FROM [CMS_SettingsCategory] WHERE [CategoryName] IN ('CMS.Content.CognitiveServices', 'CMS.CognitiveServices.ImageRecognition') ORDER BY [CategoryLevel], [CategoryID]

	DECLARE @categoryID int;

	OPEN @categoryCursor

	FETCH NEXT FROM @categoryCursor INTO @categoryID, @categoryParentID;
	WHILE @@FETCH_STATUS = 0
	BEGIN

	UPDATE [CMS_SettingsCategory] SET

		[CategoryChildCount] = (SELECT COUNT(*)
										FROM [CMS_SettingsCategory] AS [Children]
										WHERE [Children].[CategoryParentID] = @categoryID),

		[CategoryIDPath] = COALESCE((SELECT TOP 1 [CategoryIDPath] FROM [CMS_SettingsCategory] AS [Parent] WHERE [Parent].CategoryID = @categoryParentID), '')
							  + '/'
							  + REPLICATE('0', 8 - LEN(@categoryID))
							  + CAST(@categoryID AS NVARCHAR(8))

	WHERE [CategoryID] = @categoryID

	FETCH NEXT FROM @categoryCursor INTO @categoryID, @categoryParentID;
	END

	CLOSE @categoryCursor;
	DEALLOCATE @categoryCursor;

	-- Add new settings keys
	DECLARE @keyCategoryID int;
	SET @keyCategoryID = (SELECT TOP 1 [CategoryID] FROM [CMS_SettingsCategory] WHERE [CategoryName] = 'CMS.CognitiveServices.ImageRecognition')
	IF @keyCategoryID IS NOT NULL BEGIN

	INSERT [CMS_SettingsKey] ([KeyName], [KeyDisplayName], [KeyDescription], [KeyValue], [KeyType], [KeyCategoryID], [SiteID], [KeyGUID], [KeyLastModified], [KeyOrder], [KeyDefaultValue], [KeyValidation], [KeyEditingControlPath], [KeyIsGlobal], [KeyIsCustom], [KeyIsHidden], [KeyFormControlSettings], [KeyExplanationText])
	 VALUES ('CMSEnableImageRecognition', '{$settingskey.cmsenableimagerecognition$}', '{$settingskey.cmsenableimagerecognition.description$}', 'False', 'boolean', @keyCategoryID, NULL, 'ef4fdc8e-798c-4d48-bc9d-4271e3fec427', getDate(), 1, 'False', NULL, NULL, 0, 0, 0, NULL, '')

	INSERT [CMS_SettingsKey] ([KeyName], [KeyDisplayName], [KeyDescription], [KeyValue], [KeyType], [KeyCategoryID], [SiteID], [KeyGUID], [KeyLastModified], [KeyOrder], [KeyDefaultValue], [KeyValidation], [KeyEditingControlPath], [KeyIsGlobal], [KeyIsCustom], [KeyIsHidden], [KeyFormControlSettings], [KeyExplanationText])
	 VALUES ('CMSAzureComputerVisionAPIEndpoint', '{$settingskey.cmsazurecomputervisionendpoint$}', '{$settingskey.cmsazurecomputervisionendpoint.description$}', NULL, 'string', @keyCategoryID, NULL, '4d84b483-161e-4eca-9c4b-1cf59b79c7e9', getDate(), 2, NULL, NULL, NULL, 0, 0, 0, NULL, '')

	INSERT [CMS_SettingsKey] ([KeyName], [KeyDisplayName], [KeyDescription], [KeyValue], [KeyType], [KeyCategoryID], [SiteID], [KeyGUID], [KeyLastModified], [KeyOrder], [KeyDefaultValue], [KeyValidation], [KeyEditingControlPath], [KeyIsGlobal], [KeyIsCustom], [KeyIsHidden], [KeyFormControlSettings], [KeyExplanationText])
	 VALUES ('CMSAzureComputerVisionAPIKey', '{$settingskey.cmsazurecomputervisionapikey$}', '{$settingskey.cmsazurecomputervisionapikey.description$}', NULL, 'string', @keyCategoryID, NULL, '0d4a2fda-a2d4-4dc3-914c-7cfa81cb0978', getDate(), 3, NULL, NULL, NULL, 0, 0, 0, NULL, '')

	INSERT [CMS_SettingsKey] ([KeyName], [KeyDisplayName], [KeyDescription], [KeyValue], [KeyType], [KeyCategoryID], [SiteID], [KeyGUID], [KeyLastModified], [KeyOrder], [KeyDefaultValue], [KeyValidation], [KeyEditingControlPath], [KeyIsGlobal], [KeyIsCustom], [KeyIsHidden], [KeyFormControlSettings], [KeyExplanationText])
	 VALUES ('CMSAzureComputerVisionConfidence', '{$settingskey.cmsazurecomputervisionconfidence$}', '{$settingskey.cmsazurecomputervisionconfidence.description$}', '0.25', 'double', @keyCategoryID, NULL, '3ed09beb-7c57-4ea5-87e5-3891544e8d7a', getDate(), 4, '0.25', '0(\.\d+)?|1(\.0)?', NULL, 0, 0, 0, NULL, '')

	END
END
GO
/* ----------------------------------------------------------------------------*/
/* This SQL command must be at the end and must contain current hotfix version */
/* ----------------------------------------------------------------------------*/
UPDATE [CMS_SettingsKey] SET KeyValue = '56' WHERE KeyName = N'CMSHotfixVersion'
GO
