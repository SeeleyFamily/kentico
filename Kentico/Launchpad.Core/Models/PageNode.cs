using System;
using System.Collections;
using System.Collections.Generic;


namespace Launchpad.Core.Models
{

	public class PageNode
	{
		public int AclID { get; set; }
		public IEnumerable<string> CategoryCodeNamePaths { get; set; }
		public IEnumerable<string> CategoryDisplayNames { get; set; }
		public IEnumerable<string> CategoryNames { get; set; }
		public IEnumerable<Category> Categories { get; set; }
		public DateTime DatePublished { get; set; }
		public int DocumentID { get; set; }
		public DateTime DocumentModifiedWhen { get; set; }
		public string DocumentName { get; set; }
		public string DocumentUrlPath { get; set; }
		public string DocumentCulture { get; set; }
		public int FeatureOrder { get; set; }
		public Dictionary<string, object> Fields { get; set; }
		public bool IsContentOnly { get; set; }
		public PageMetadata Metadata { get; set; }
		public string NodeAliasPath { get; set; }
		public string NodeClassName { get; set; }
		public Guid NodeGuid { get; set; }
		public int NodeID { get; set; }
		public int NodeLevel { get; set; }
		public int NodeOrder { get; set; }
		public int NodeParentID { get; set; }
		public int NodeSiteID { get; set; }
		public double RelatedRating { get; set; }
		public Preview Preview { get; set; }
		public Hashtable CustomData { get; set; }
		public PageBuilderWidgets PageBuilderWidgets { get; set; }


		public PageNode( )
		{

		}


		public PageNode( PageNode pageNode )
		{
			AclID = pageNode.AclID;
			DocumentName = pageNode.DocumentName;
			DocumentID = pageNode.DocumentID;
			DocumentUrlPath = pageNode.DocumentUrlPath;
			DocumentCulture = pageNode.DocumentCulture;
			Fields = pageNode.Fields;
			Metadata = pageNode.Metadata;
			NodeAliasPath = pageNode.NodeAliasPath;
			NodeClassName = pageNode.NodeClassName;
			NodeGuid = pageNode.NodeGuid;
			NodeID = pageNode.NodeID;
			NodeLevel = pageNode.NodeLevel;
			NodeOrder = pageNode.NodeOrder;
			NodeParentID = pageNode.NodeParentID;
			NodeSiteID = pageNode.NodeSiteID;
			CategoryNames = pageNode.CategoryNames;
			CategoryDisplayNames = pageNode.CategoryDisplayNames;
			CategoryCodeNamePaths = pageNode.CategoryCodeNamePaths;
			Categories = pageNode.Categories;
			FeatureOrder = pageNode.FeatureOrder;
			IsContentOnly = pageNode.IsContentOnly;
			Preview = pageNode.Preview;
			CustomData = pageNode.CustomData;
			PageBuilderWidgets = pageNode.PageBuilderWidgets;
		}

	}

}
