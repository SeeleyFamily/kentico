/*
 * Built with Common Launchpad 2.0.2
 */

using Launchpad.Core.Abstractions.Providers;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;
using Launchpad.Infrastructure.Kentico.Web.Models.ViewModels;


namespace Launchpad.Web.Models.Common.ViewModels
{

	public abstract class BlogAuthorViewModell<TSummaryItem, TSpecification, TService> : ListingViewModel<TSummaryItem, TSpecification, TService>
		where TSummaryItem : BlogSummaryItem
		where TSpecification : BlogSpecification
		where TService : ISearchableSummaryDocumentService<TSummaryItem, TSpecification>
	{


		#region KenticoProperties
		public string LastName { get; protected set; }
		public string FirstName { get; protected set; }
		public string Image { get; protected set; }
		public string Title { get; protected set; }
		public string FacebookLink { get; protected set; }
		public string TwitterLink { get; protected set; }
		public string LinkedInLink { get; protected set; }
		public string InstagramLink { get; protected set; }
		public string Content { get; protected set; }
		#endregion


		#region Properties				
		public override string ViewPath { get; protected set; } = $"BlogAuthor/Index";
		public override string ApiRoute { get; protected set; } = "BlogAuthor";
		public WysiwygViewModel WysiwygViewModel { get; protected set; }
		public PageBuilderViewModel PageBuilderViewModel { get; protected set; }
		public string FullName { get; protected set; }
		#endregion


		#region fields
		#endregion


		public BlogAuthorViewModell
		(
			ILayoutProvider layoutProvider,
			TService searchableService
		)
			: base(layoutProvider)
		{
			this.SearchableService = searchableService;
		}


		protected override void Populate()
		{
			base.Populate();

			PopulateFullName();
			PopulateWysiywg();
			PopulatePageBuilder();
		}


		protected virtual void PopulateFullName()
		{
			FullName = $"{FirstName} {LastName}".Trim();
		}


		protected virtual void PopulateWysiywg()
		{
			WysiwygViewModel = new WysiwygViewModel()
			{
				Content = Content
			};
		}


		protected virtual void PopulatePageBuilder()
		{
			PageBuilderViewModel = new PageBuilderViewModel(Node.PageBuilderWidgets);
		}


	}

}