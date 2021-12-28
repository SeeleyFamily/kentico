using System.Collections.Generic;
using System.Linq;
using CMS.DocumentEngine;
using CMS.Search.Azure;
using Launchpad.Core.Constants;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Extensions;
using Launchpad.Infrastructure.Services;
using Microsoft.Azure.Search.Models;


namespace Launchpad.Infrastructure.Kentico.CMS.Services
{

	public class AzureSearchModuleService
	{
		#region Fields
		protected HashSet<string> excludedFields = new HashSet<string> { "issecurednode", "requiresssl" };
		protected HashSet<string> excludedFieldPrefixes = new HashSet<string> { "document", "node", "sku" };
		private HashSet<string> facetedFields = new HashSet<string> { "documentcategories", "documentcategoryids" };
		private Dictionary<string, Field> FieldMap { get; set; }
		private const string publishSource = "Kentico SmartSearch";
		protected HashSet<string> requiredFields = new HashSet<string> {
			"classname",
			"documentid",
			"documentname", 
			//"documenturlpath", // documenturlpath no longer exists in k13 and needs to be added manually
			"documentcategories",
			"documentcategoryids",
			"documentpublishfrom",
			"documentpublishto",
			"nodealiaspath",
			"nodealiaspathprefixes",
			"nodeguid",
			"nodeid",
		};


		private readonly CustomCmsModuleLoggingService customCmsModuleLoggingService;
		#endregion


		public AzureSearchModuleService()
		{
			this.customCmsModuleLoggingService = new CustomCmsModuleLoggingService();
			FieldMap = new Dictionary<string, Field>();
		}



		internal virtual void OnCreatingDocumentAfter(object sender, CreateDocumentEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("AzureSearchModuleService", "OnCreatingDocumentAfter");

			RemoveFields(e.Document);


			if (e.Searchable is TreeNode node)
			{
				PopulateFields(node, e.Document);
			}

			if (e.Searchable != null)
			{
				foreach (var field in e.Fields)
				{
					if (FieldMap.TryGetValue(field.Name, out Field mappedField))
					{
						field.IsSearchable = mappedField.IsSearchable;
						field.IsRetrievable = mappedField.IsRetrievable;
					}
					else
					{
						FieldMap.Add(field.Name, field);
					}

				}
			}
		}


		internal void OnCreatingFieldsAfter( object sender, CreateFieldsEventArgs e )
		{
			var fields = e.Fields;


			// Update the facetable fields
			foreach( Field field in fields.Where( f => facetedFields.Contains( f.Name.ToLower() ) ) )
			{
				field.IsFacetable = true;
				field.IsSearchable = true;
			}
		}


		internal virtual void OnCreatingOrUpdatingIndex(object sender, CreateOrUpdateIndexEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("AzureSearchModuleService", "OnCreatingOrUpdatingIndex");

			RemoveFields(e.Index);
			AddFields(e.Index.Fields);
		}



		protected virtual void AddField(Field field, IList<Field> fields)
		{
			if (fields.Any(f => f.Name == field.Name))
			{
				return;
			}

			fields.Add(field);
		}


		protected virtual void AddFields(IList<Field> fields)
		{
			// Page Builder for searching against text, etc
			AddField(new Field("search_pagebuilder", DataType.String) { IsRetrievable = false, IsSearchable = true }, fields);

			// Summary items for populating summary objects in search results
			AddField(new Field("summary_date", DataType.DateTimeOffset) { IsRetrievable = true, IsSearchable = false, IsSortable = true }, fields);
			AddField(new Field("summary_image", DataType.String) { IsRetrievable = true, IsSearchable = false }, fields);
			AddField(new Field("summary_text", DataType.String) { IsRetrievable = true, IsSearchable = true }, fields);
			AddField(new Field("summary_title", DataType.String) { IsRetrievable = true, IsSearchable = true }, fields);
			AddField(new Field("summary_url", DataType.String) { IsRetrievable = true, IsSearchable = false }, fields);

			// DocumentUrlPath
			AddField(new Field("documenturlpath", DataType.String) { IsRetrievable = true, IsSearchable = true }, fields);

			// Breadcrumbs
			AddField(new Field("breadcrumbs", DataType.String){ IsRetrievable = true, IsSearchable = false },fields);

			// Publish source
			AddField( new Field( "publish_source", DataType.String ) { IsRetrievable = true, IsSearchable = true }, fields );
		}


		protected virtual bool IsFieldIncluded(string fieldName)
		{
			if (requiredFields.Contains(fieldName))
			{
				return true;
			}

			if (excludedFields.Contains(fieldName))
			{
				return false;
			}

			if (excludedFieldPrefixes.Any(prefix => fieldName.StartsWith(prefix)))
			{
				return false;
			}

			return true;
		}


		protected virtual void PopulateFields(TreeNode node, Document document)
		{
			// Insert value of Page Builder widgets
			document["search_pagebuilder"] = node["DocumentPageBuilderWidgets"];

			// Populate summary information
			document["summary_date"] = node.DocumentCustomData.GetDateTimeValue(nameof(Preview.PreviewDate));
			document["summary_image"] = node.DocumentCustomData.GetStringValue(nameof(Preview.PreviewImage));
			document["summary_text"] = node.DocumentCustomData.GetStringValue(nameof(Preview.PreviewText));
			document["summary_title"] = node.DocumentCustomData.GetStringValue(nameof(Preview.PreviewTitle));
			document["summary_url"] = node.DocumentCustomData.GetStringValue(nameof(Preview.PreviewUrl));

			// DocumentUrlPath
			document["documenturlpath"] = node.DocumentCustomData[Constants.DocumentUrlPath]?.ToString();

			// Breadcrumbs
			document["breadcrumbs"] = node.DocumentCustomData.GetStringValue(nameof(Breadcrumbs));

			// Publish Source: Kentico
			document["publish_source"] = publishSource;
		}


		protected virtual void RemoveFields(Document document)
		{
			// Remove fields that aren't included in the index
			string[] keys = document.Keys.Where(k => !IsFieldIncluded(k)).ToArray();

			foreach (string key in keys)
			{
				document.Remove(key);
			}

			string[] nullKeys = document.Keys.Where(k => document[k] == null).ToArray();
			// Remove Null Values that are not required
			foreach (string key in nullKeys)
			{
				if (!IsFieldIncluded(key))
				{
					document.Remove(key);
				}
			}

		}


		protected virtual void RemoveFields(Index index)
		{
			// Remove index fields that aren't necessary to store in the index
			index.Fields = index.Fields.Where(f => IsFieldIncluded(f.Name)).ToList();
		}

	}

}
