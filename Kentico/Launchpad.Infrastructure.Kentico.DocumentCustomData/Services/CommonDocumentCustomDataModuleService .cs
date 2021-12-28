using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.Common;
using CMSAppCustom.Models;
using Launchpad.Core.Extensions;
using Launchpad.Core.Utilities;
using Launchpad.Infrastructure.Extensions;
using System;
using System.Collections.Generic;


namespace Launchpad.Infrastructure.Kentico.DocumentCustomData.Services
{
	public class CommonDocumentCustomDataModuleService
	{
		public bool UpdateCommonDocumentCustomData(ref TreeNode node, ref CustomDataObject customDataObject)
		{
			bool doUpdate = false;
			switch (node.ClassName)
			{
				case AssetResource.CLASS_NAME:
					customDataObject.Preview.PreviewTitle = !string.IsNullOrWhiteSpace(customDataObject.Preview.PreviewTitle) ? customDataObject.Preview.PreviewTitle : node.GetStringValue(nameof(AssetResource.AssetTitle), string.Empty);

					if (customDataObject.Preview.PreviewDate == null)
					{
						var publishDate = node.GetDateTimeValue(nameof(AssetResource.PublishDate), DateTime.MinValue);
						if (publishDate != DateTime.MinValue)
						{
							customDataObject.Preview.PreviewDate = publishDate;
						}
					}

					customDataObject.Preview.PreviewUrl = !string.IsNullOrWhiteSpace(customDataObject.Preview.PreviewUrl) ? customDataObject.Preview.PreviewUrl : node.GetStringValue(nameof(AssetResource.DownloadableAsset), string.Empty);

					// TODO READ TEXT FROM DOWNLOADABLE ASSET

					customDataObject.SearchBlobValues.AddRange(
						new List<string>
						{
							node.GetStringValue(nameof(AssetResource.AssetTitle), string.Empty)
						}
					);

					break;


				case BlogAuthor.CLASS_NAME:
					var blogAuthorfullName = $"{node.GetStringValue(nameof(BlogAuthor.FirstName), string.Empty)} {node.GetStringValue(nameof(BlogAuthor.LastName), string.Empty)}".Trim();
					customDataObject.Preview.PreviewTitle = !string.IsNullOrWhiteSpace(customDataObject.Preview.PreviewTitle) ? customDataObject.Preview.PreviewTitle : blogAuthorfullName;

					customDataObject.Preview.PreviewText = !string.IsNullOrWhiteSpace(customDataObject.Preview.PreviewText) ? customDataObject.Preview.PreviewText : node.GetStringValue(nameof(BlogAuthor.Content), string.Empty);

					customDataObject.Preview.PreviewImage = CoalesceUtility.CoalesceWithoutWhitespace(customDataObject.Preview.PreviewImage, node.GetStringValue(nameof(BlogAuthor.Image), string.Empty));
					
					// Blog Author Title
					var blogAuthorTitle = node.GetStringValue(nameof(BlogAuthor.Title), string.Empty);
					doUpdate = node.DocumentCustomData.UpdateCustomDataStringValue(nameof(BlogAuthor.Title), blogAuthorTitle) || doUpdate;

					customDataObject.SearchBlobValues.AddRange(
						new List<string>
						{
							node.GetStringValue(nameof(BlogAuthor.FirstName), string.Empty),
							node.GetStringValue(nameof(BlogAuthor.LastName), string.Empty),
							node.GetStringValue(nameof(BlogAuthor.Title), string.Empty)
						}
					);

					break;


				case BlogDetail.CLASS_NAME:
					customDataObject.Preview.PreviewTitle = !string.IsNullOrWhiteSpace(customDataObject.Preview.PreviewTitle) ? customDataObject.Preview.PreviewTitle : node.GetStringValue(nameof(BlogDetail.Headline), string.Empty);

					if (customDataObject.Preview.PreviewDate == null)
					{
						var publishDate = node.GetDateTimeValue(nameof(BlogDetail.PublishDate), DateTime.MinValue);
						if (publishDate != DateTime.MinValue)
						{
							customDataObject.Preview.PreviewDate = publishDate;
						}
					}

					customDataObject.Preview.PreviewText = !string.IsNullOrWhiteSpace(customDataObject.Preview.PreviewText) ? customDataObject.Preview.PreviewText : node.GetStringValue(nameof(BlogDetail.Content), string.Empty);
					if (string.IsNullOrWhiteSpace(customDataObject.Preview.PreviewText))
					{
						var pageBuilderWidgets = node.GetPageBuilderWidgets();
						// this only gets the first rich text widget
						var richTextWidget = pageBuilderWidgets.GetWidgetProperties("Kentico.Widget.RichText");
						if (richTextWidget.Count >= 0)
						{
							var richTextWidgetContent = richTextWidget.GetStringValue("content");
							customDataObject.Preview.PreviewText = richTextWidgetContent.StripHTML();
						}
					}

					customDataObject.Preview.PreviewImage = !string.IsNullOrWhiteSpace(customDataObject.Preview.PreviewImage) ? customDataObject.Preview.PreviewImage : node.GetStringValue(nameof(BlogDetail.HeroBackgroundImageMobile), string.Empty);
					customDataObject.Preview.PreviewImage = !string.IsNullOrWhiteSpace(customDataObject.Preview.PreviewImage) ? customDataObject.Preview.PreviewImage : node.GetStringValue(nameof(BlogDetail.HeroBackgroundImage), string.Empty);

					customDataObject.SearchBlobValues.AddRange(
						new List<string>
						{
							node.GetStringValue(nameof(BlogDetail.Headline), string.Empty),
							node.GetStringValue(nameof(BlogDetail.Content), string.Empty)
						}
					);

					break;


				case CalloutCard.CLASS_NAME:
					customDataObject.Preview.PreviewCtaLabel = CoalesceUtility.CoalesceWithoutWhitespace(customDataObject.Preview.PreviewCtaLabel, node.GetStringValue(nameof(CalloutCard.CtaLabel), string.Empty));
					customDataObject.Preview.PreviewImage = CoalesceUtility.CoalesceWithoutWhitespace(customDataObject.Preview.PreviewImage, node.GetStringValue(nameof(CalloutCard.Image), string.Empty));
					customDataObject.Preview.PreviewText = CoalesceUtility.CoalesceWithoutWhitespace(customDataObject.Preview.PreviewText, node.GetStringValue(nameof(CalloutCard.Description), string.Empty));
					customDataObject.Preview.PreviewTitle = CoalesceUtility.CoalesceWithoutWhitespace(customDataObject.Preview.PreviewTitle, node.GetStringValue(nameof(CalloutCard.Headline), string.Empty));
					customDataObject.Preview.PreviewUrl = CoalesceUtility.CoalesceWithoutWhitespace(customDataObject.Preview.PreviewUrl, node.GetStringValue(nameof(CalloutCard.CtaUrl), string.Empty));

					break;


				case ContentDetail.CLASS_NAME:
					customDataObject.Preview.PreviewTitle = !string.IsNullOrWhiteSpace(customDataObject.Preview.PreviewTitle) ? customDataObject.Preview.PreviewTitle : node.GetStringValue(nameof(ContentDetail.Headline), string.Empty);

					if (customDataObject.Preview.PreviewDate == null)
					{
						var publishDate = node.GetDateTimeValue(nameof(ContentDetail.PublishDate), DateTime.MinValue);
						if (publishDate != DateTime.MinValue)
						{
							customDataObject.Preview.PreviewDate = publishDate;
						}
					}

					customDataObject.Preview.PreviewText = !string.IsNullOrWhiteSpace(customDataObject.Preview.PreviewText) ? customDataObject.Preview.PreviewText : node.GetStringValue(nameof(ContentDetail.Content), string.Empty);
					if (string.IsNullOrWhiteSpace(customDataObject.Preview.PreviewText))
					{
						//TODO PULL FROM PAGE BUILDER
					}

					customDataObject.Preview.PreviewImage = !string.IsNullOrWhiteSpace(customDataObject.Preview.PreviewImage) ? customDataObject.Preview.PreviewImage : node.GetStringValue(nameof(ContentDetail.HeroBackgroundImageMobile), string.Empty);
					customDataObject.Preview.PreviewImage = !string.IsNullOrWhiteSpace(customDataObject.Preview.PreviewImage) ? customDataObject.Preview.PreviewImage : node.GetStringValue(nameof(ContentDetail.HeroBackgroundImage), string.Empty);

					customDataObject.SearchBlobValues.AddRange(
						new List<string>
						{
							node.GetStringValue(nameof(ContentDetail.Headline), string.Empty),
							node.GetStringValue(nameof(ContentDetail.Content), string.Empty)
						}
					);

					break;


				case EventDetail.CLASS_NAME:
					customDataObject.Preview.PreviewTitle = !string.IsNullOrWhiteSpace(customDataObject.Preview.PreviewTitle) ? customDataObject.Preview.PreviewTitle : node.GetStringValue(nameof(EventDetail.Title), string.Empty);

					if (customDataObject.Preview.PreviewDate == null)
					{
						var publishDate = node.GetDateTimeValue(nameof(EventDetail.StartDate), DateTime.MinValue);
						if (publishDate != DateTime.MinValue)
						{
							customDataObject.Preview.PreviewDate = publishDate;
						}
					}

					customDataObject.Preview.PreviewText = !string.IsNullOrWhiteSpace(customDataObject.Preview.PreviewText) ? customDataObject.Preview.PreviewText : node.GetStringValue(nameof(EventDetail.Content), string.Empty);
					if (string.IsNullOrWhiteSpace(customDataObject.Preview.PreviewText))
					{
						//TODO PULL FROM PAGE BUILDER
					}

					customDataObject.SearchBlobValues.AddRange(
						new List<string>
						{
							node.GetStringValue(nameof(EventDetail.Title), string.Empty),
							node.GetStringValue(nameof(EventDetail.Content), string.Empty)
						}
					);

					break;


				case ExternalResource.CLASS_NAME:
					customDataObject.Preview.PreviewTitle = !string.IsNullOrWhiteSpace(customDataObject.Preview.PreviewTitle) ? customDataObject.Preview.PreviewTitle : node.GetStringValue(nameof(ExternalResource.ExternalResourceTitle), string.Empty);

					if (customDataObject.Preview.PreviewDate == null)
					{
						var publishDate = node.GetDateTimeValue(nameof(ExternalResource.PublishDate), DateTime.MinValue);
						if (publishDate != DateTime.MinValue)
						{
							customDataObject.Preview.PreviewDate = publishDate;
						}
					}

					customDataObject.Preview.PreviewUrl = !string.IsNullOrWhiteSpace(customDataObject.Preview.PreviewUrl) ? customDataObject.Preview.PreviewUrl : node.GetStringValue(nameof(ExternalResource.ExternalResourceUrl), string.Empty);

					customDataObject.SearchBlobValues.AddRange(
						new List<string>
						{
							node.GetStringValue(nameof(ExternalResource.ExternalResourceTitle), string.Empty)
						}
					);

					break;


				case PeopleProfile.CLASS_NAME:
					var peopleProfileFullName = $"{node.GetStringValue(nameof(PeopleProfile.FirstName), string.Empty)} {node.GetStringValue(nameof(PeopleProfile.LastName), string.Empty)}".Trim();
					customDataObject.Preview.PreviewTitle = !string.IsNullOrWhiteSpace(customDataObject.Preview.PreviewTitle) ? customDataObject.Preview.PreviewTitle : peopleProfileFullName;

					customDataObject.Preview.PreviewText = !string.IsNullOrWhiteSpace(customDataObject.Preview.PreviewText) ? customDataObject.Preview.PreviewText : node.GetStringValue(nameof(PeopleProfile.Content), string.Empty);

					customDataObject.Preview.PreviewImage = CoalesceUtility.CoalesceWithoutWhitespace(customDataObject.Preview.PreviewImage, node.GetStringValue(nameof(PeopleProfile.Photo), string.Empty));

					// People Title
					var peopleTitle = node.GetStringValue(nameof(PeopleProfile.Title), string.Empty);
					doUpdate = node.DocumentCustomData.UpdateCustomDataStringValue(nameof(PeopleProfile.Title), peopleTitle) || doUpdate;

					customDataObject.SearchBlobValues.AddRange(
						new List<string>
						{
							node.GetStringValue(nameof(PeopleProfile.FirstName), string.Empty),
							node.GetStringValue(nameof(PeopleProfile.LastName), string.Empty),
							node.GetStringValue(nameof(PeopleProfile.Title), string.Empty)
						}
					);

					break;
			}


			return doUpdate;
		}
	}
}