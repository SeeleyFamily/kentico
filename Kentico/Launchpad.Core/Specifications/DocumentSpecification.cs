using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Attributes;
using Launchpad.Core.Enums;
using Launchpad.Core.Extensions;
using System;
using System.Collections.Specialized;


namespace Launchpad.Core.Specifications
{

	public class DocumentSpecification : IDocumentSpecification
	{


		#region Properties
		public int[] ExcludedNodes { get; set; }
		public int[] Nodes { get; set; }
		public Guid[] ExcludedGuids { get; set; }
		public Guid[] Guids { get; set; }
		public string[] Categories { get; set; }
		public string[] ExcludedCategories { get; set; }
		public string[] ClassNames { get; set; }
		public string[] ExcludedClassNames { get; set; }
		public int NodeLevel { get; set; }
		public virtual string Path { get; set; } = "/";
		public string SearchTerm { get; set; }	
		public virtual bool IncludeDocumentForPath { get; set; }
		public string Sort { get; set; } = SortType.NodeOrder.GetAttribute<CodeDisplayNameTypeAttribute>().CodeName;
		public int PageIndex { get; set; }
		public int PageSize { get; set; } = 10;		
		public int NestingLevel { get; set; } = 0;
		#endregion


		public DocumentSpecification()
		{

		}


		public DocumentSpecification(NameValueCollection keyValues)
		{
			// Parse Document Specification Properties
			this.Parse(keyValues, nameof(Nodes));
			this.Parse(keyValues, nameof(ExcludedNodes));
			this.Parse(keyValues, nameof(Guids));
			this.Parse(keyValues, nameof(ExcludedGuids));
			this.Parse(keyValues, nameof(Categories));
			this.Parse(keyValues, nameof(ExcludedCategories));
			this.Parse(keyValues, nameof(ClassNames));
			this.Parse(keyValues, nameof(ExcludedClassNames));

			// Node Level
			this.Parse(keyValues, nameof(NodeLevel));

			// Parse Paging
			this.Parse(keyValues, nameof(PageIndex));
			this.Parse(keyValues, nameof(PageSize));

			// Parse Path
			this.Parse(keyValues, nameof(Path));
			
			// Parse Sort
			this.Parse(keyValues, nameof(Sort));

			// Parse Search Term
			this.Parse(keyValues, nameof(SearchTerm));
		}


	}

}
