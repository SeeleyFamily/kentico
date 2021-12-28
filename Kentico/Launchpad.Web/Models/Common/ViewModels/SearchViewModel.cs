/*
 * Built with Common Launchpad 2.0.2
 */

using CMS.DocumentEngine.Types.Common;
using Launchpad.Core.Abstractions.Providers;
using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Models;
using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;
using Launchpad.Infrastructure.Abstractions.Services;
using Launchpad.Infrastructure.Kentico.Web.Attributes;
using System.Collections.Generic;
using System.Configuration;


namespace Launchpad.Web.Models.Common.ViewModels
{

	[RegisterForPageType(Search.CLASS_NAME)]
	public class SearchViewModel : BaseViewModel
	{
		#region Properties
		public virtual string ApiRoute { get; protected set; } = "search";
		public virtual IEnumerable<Filter> Filters { get; protected set; } = new List<Filter>();
		public virtual PagedResult<SummaryItem> Result { get; protected set; }
		public virtual ISearchIndexSpecification Specification { get; protected set; }
		public override string ViewPath { get; protected set; } = "Search/Index";

		protected virtual string IndexName { get; set; } = ConfigurationManager.AppSettings["Search:Index:Global"];
		protected virtual int PageSize { get; set; } = 25;
		protected virtual IAzureSearchService Service { get; set; }
		#endregion


		#region Fields
		#endregion


		public SearchViewModel
		(
			IAzureSearchService service,

			ILayoutProvider layoutProvider
		)
		: base(layoutProvider)
		{
			Service = service;
		}



		protected override void Populate()
		{
			base.Populate();

			PopulateSpecification();

			PopulateListItems();
			PopulateFilters();
		}


		protected virtual void PopulateFilters()
		{
			// Override this method to implement Filters
		}


		protected virtual void PopulateListItems()
		{
			Result = Service.Search(Specification);
		}


		protected virtual void PopulateSpecification()
		{
			Specification = new SearchIndexSpecification(HttpContext.Request.QueryString)
			{
				IndexName = IndexName,
				PageSize = PageSize
			};
		}

	}

}