using Kentico.Content.Web.Mvc;
using Kentico.Web.Mvc;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Kentico.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;


namespace Launchpad.Infrastructure.Kentico.Web.HtmlHelpers
{

	public static class HtmlHelperExtensions
	{

		public static string GenerateUniqueId(this HtmlHelper html, string originalIdentifier) =>
			$"{originalIdentifier}{Path.GetRandomFileName().Replace(".", string.Empty)}";


		public static MvcHtmlString PageBuilderFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
			where TProperty : PageBuilderViewModel
		{
			ModelMetadata modelMetadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

			return html.Partial("Components/PageBuilder/_PageBuilder", modelMetadata.Model);
		}


		public static MvcHtmlString Properties<TModel>( this HtmlHelper<TModel> html, TModel model, bool includeBaseProperties = false )
		{
			Type type = typeof( TModel );
			Dictionary<string, string> properties = new Dictionary<string, string>();
			BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | ( includeBaseProperties ? 0 : BindingFlags.DeclaredOnly );


			foreach( PropertyInfo property in type.GetProperties( flags ) )
			{
				properties.Add( property.Name, property.GetValue( model )?.ToString() );
			}


			return html.Partial( "Components/_Properties", properties );
		}


		public static MvcHtmlString WysiwygFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
			where TProperty : WysiwygViewModel
		{
			ModelMetadata modelMetadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

			return html.Partial("Components/Wysiwyg/_Wysiwyg", modelMetadata.Model);
		}


		public static MvcHtmlString HeroFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
			where TProperty : HeroViewModel
		{
			ModelMetadata modelMetadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

			return html.Partial("Components/Hero/_Hero", modelMetadata.Model);
		}


		public static MvcHtmlString CardFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
			where TProperty : CardViewModel
		{
			ModelMetadata modelMetadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

			return html.Partial("Components/Card/_Card", modelMetadata.Model);
		}


		public static MvcHtmlString SideNavFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
		where TProperty : MenuItem
		{
			ModelMetadata modelMetadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

			return html.Partial("Components/SideNav/_SideNav", modelMetadata.Model);
		}


		public static string PreviewSrcAttribute(this HtmlHelper html, string src)
		{
			if (!HttpContext.Current.Kentico().Preview().Enabled)
			{
				return string.Empty;
			}
			return !string.IsNullOrWhiteSpace(src) ? $"src={src}" : string.Empty;
		}


		public static string PreviewBackgroundImageStyle(this HtmlHelper html, string src)
		{
			if (!HttpContext.Current.Kentico().Preview().Enabled)
			{
				return string.Empty;
			}
			return !string.IsNullOrWhiteSpace(src) ? $"style=background-image:url({src})" : string.Empty;
		}


		public static string BackgroundPositionClass(this HtmlHelper html, string position)
		{
			if (string.IsNullOrWhiteSpace(position))
			{
				return string.Empty;
			}
			if (position.Equals("none", System.StringComparison.InvariantCultureIgnoreCase))
			{
				return string.Empty;
			}
			return $"bg-position-{position}";
		}


		public static MvcHtmlString TabsFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
			where TProperty : IEnumerable<PageBuilderTabComponentViewModel>
		{
			var htmlString = string.Empty;

			if (ModelMetadata.FromLambdaExpression(expression, html.ViewData).Model is TProperty model)
            {
				htmlString = string.Join("\n", model.Select(x => html.Partial("Components/PageBuilderTabs/_PageBuilderTabs", x)).Select(x => x.ToHtmlString()));
			}

			return new MvcHtmlString(htmlString);
		}
	}

}