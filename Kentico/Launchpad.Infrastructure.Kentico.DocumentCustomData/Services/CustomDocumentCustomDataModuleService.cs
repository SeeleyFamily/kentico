using CMS.DocumentEngine;
using CMSAppCustom.Models;
using Launchpad.Infrastructure.Extensions;

namespace Launchpad.Infrastructure.Kentico.DocumentCustomData.Services
{
	public class CustomDocumentCustomDataModuleService
	{
		public bool UpdateCustomDocumentCustomData(ref TreeNode node, ref CustomDataObject customDataObject)
		{
			bool doUpdate = false;

			// Use this class for custom implementations and custom page types.
			//< !-- ================ CUSTOM =============== -->
			//< !-- ======================================= -->
			//< !--Add Custom Models Below This Line -->
			//< !-- ======================================= -->
			//< !-- ======================================= -->

			switch (node.ClassName)
			{				
				default:

					break;
			}

			return doUpdate;
		}
	}
}
