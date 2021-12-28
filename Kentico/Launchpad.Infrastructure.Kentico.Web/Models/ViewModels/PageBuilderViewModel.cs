


using Kentico.PageBuilder.Web.Mvc;
using Launchpad.Core.Extensions;
using Launchpad.Core.Models;

namespace Launchpad.Infrastructure.Kentico.Web.Models.ViewModels
{

	public class PageBuilderViewModel
	{
		public string AreaIdentifier { get; set; } = "pagecontent";
		public string DefaultSectionIdentifier { get; set; } = null;
		public string[] AllowedWidgets { get; set; } = new string[] { };
		public string[] AllowedSections { get; set; } = new string[] { };
		public PageBuilderWidgets PageBuilderWidgets { get; set; }
		
		public PageBuilderViewModel(PageBuilderWidgets pageBuilderWidgets)
		{
			PageBuilderWidgets = pageBuilderWidgets;
		}		
		
		public EditableAreaOptions GetEditableAreaOptions()
		{
			var editableAreaOptions = new EditableAreaOptions();
			if(!string.IsNullOrWhiteSpace(DefaultSectionIdentifier))
			{
				editableAreaOptions.DefaultSectionIdentifier = DefaultSectionIdentifier;
			}
			if (!AllowedWidgets.IsNullOrEmpty())
			{
				editableAreaOptions.AllowedWidgets = AllowedWidgets;
			}
			if (!AllowedSections.IsNullOrEmpty())
			{
				editableAreaOptions.AllowedSections = AllowedSections;
			}

			return editableAreaOptions;
		}

		public bool HasWidgets()
		{
			if(PageBuilderWidgets == null)
			{
				return false;
			}

			var editableArea = PageBuilderWidgets.GetEditableArea(AreaIdentifier);
			if(editableArea != null)
			{
				return editableArea.HasWidgets();
			}
			
			return false;
		}
	}

}