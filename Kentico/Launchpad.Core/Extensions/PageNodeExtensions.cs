using Launchpad.Core.Abstractions.Models.Summary;
using Launchpad.Core.Attributes;
using Launchpad.Core.Constants;
using Launchpad.Core.Enums;
using Launchpad.Core.Models;
using Launchpad.Core.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace Launchpad.Core.Extensions
{
	public static class PageNodeExtensions
	{
		public static TSummaryItem ToPreviewSummary<TSummaryItem>(this PageNode pageNode)
			where TSummaryItem : ISummaryItem, new()
		{
			var title = pageNode.Preview.PreviewTitle;
			var summary = pageNode.Preview.PreviewText;
			if (pageNode.Preview.HidePreviewText)
			{
				summary = string.Empty;
			}
			var image = pageNode.Preview.PreviewImage;
			var url = CoalesceUtility.CoalesceWithoutWhitespace(pageNode.Preview.PreviewUrl, pageNode.DocumentUrlPath);
			var ctaLabel = CoalesceUtility.CoalesceWithoutWhitespace(pageNode.Preview.PreviewCtaLabel, LabelConstants.ReadMore);

			var cta = new Cta()
			{
				Text = ctaLabel,
				Url = url,
				Type = new CodeDisplayNameType()
				{

					CodeName = CtaType.Default.GetAttribute<CodeDisplayNameTypeAttribute>().CodeName,
					DisplayName = CtaType.Default.GetAttribute<CodeDisplayNameTypeAttribute>().DisplayName,
				}
			};

			var tags = pageNode.GetCategoryBasedTags();


			var summaryItem = new TSummaryItem()
			{
				Id = pageNode.NodeID.ToString(),
				Guid = pageNode.NodeGuid,
				Type = pageNode.NodeClassName,
				Title = title,
				Summary = summary,
				Image = image.SanitizeMediaUrl(),
				Url = url,
				Cta = cta,
				Tags = tags,
			};

			return summaryItem;
		}

		public static IEnumerable<Tag> GetCategoryBasedTags(this PageNode pageNode)
		{
			if (pageNode.Categories.IsNullOrEmpty())
			{
				return Enumerable.Empty<Tag>();
			}
			var tags = pageNode.Categories.Select(x => new Tag()
			{
				Text = x.DisplayName,
				Type = new CodeDisplayNameType()
				{
					DisplayName = x.CodeName,
					CodeName = x.CodeNamePath
				}
			});

			return tags;
		}

		/// <summary>
		/// This method will return content from all rich text widgets concatenated, having HTML tags stripped. It is useful
		/// for word counting operations.
		/// </summary>
		/// <param name="pageNode"></param>
		/// <returns></returns>
		public static string GetConcatenatedPageBuilderRichTextContents(this PageNode pageNode)
		{
			var richTextContents = pageNode.PageBuilderWidgets?.GetWidgets()
				.Where(x => x.Type == "Kentico.Widget.RichText")
				.Select(x => x.Variants.FirstOrDefault().PropertiesDictionary["content"].ToString());

			if (richTextContents != null && richTextContents.Count() > 0)
			{
				return string.Join(" ", richTextContents).StripHTML();
			}

			return string.Empty;
		}
	}
}
