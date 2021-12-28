/*
 * Built with Common Launchpad 2.0.2
 */

using Launchpad.Core.Abstractions.Providers;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Models;
using System.Collections.Generic;


namespace Launchpad.Web.Models.Common.ViewModels
{
	/// <summary>
	/// The listing view model is a structured approach to provided standardized results in a listing format from the infrastructure layer with the web layer.
	/// </summary>	
	/// <typeparam name="TSpecification"></typeparam>
	/// <typeparam name="TSearchableService"></typeparam>
	public abstract class ListingViewModel<T, TSpecification, TSearchableService> : BaseViewModel
		where T : class
		where TSpecification : ISpecification
		where TSearchableService : ISearchableService<T, TSpecification>
	{
		#region Properties		
		public PagedResult<T> Result { get; protected set; }
		public virtual TSpecification Specification { get; protected set; }
		public List<Filter> Filters { get; protected set; } = new List<Filter>();
		public List<string> PushStateParameters { get; protected set; } = new List<string>();
		protected virtual TSearchableService SearchableService { get; set; }
		public abstract string ApiRoute { get; protected set; }
		#endregion


		public ListingViewModel
		(
			ILayoutProvider layoutProvider
		)
			: base(layoutProvider)
		{
		}



		protected override void Populate()
		{
			base.Populate();

			// Populate the Specification First
			PopulateSpecification();
			// Populate the Push State Parameters
			PopulatePushStateParameters();
			// Populate the List Item Results
			PopulateListItems();
			// Create Filters
			// Populate Filters is after PopulateListItems
			// We may someitmes utilize the facets that come back with the result set to build filters...
			PopulateFilters();
		}


		protected virtual void PopulateListItems()
		{
			Result = SearchableService.Find(Specification);
		}


		protected abstract void PopulateSpecification();

		protected virtual void PopulatePushStateParameters()
		{
			// Potentially Add Default Specifications Here
		}

		protected virtual void PopulateFilters()
		{
			// Override this method to implement Filters
		}

	}

}