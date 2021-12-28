using Launchpad.Core.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Launchpad.Core.Models
{
	public partial class PageBuilderWidgets : WidgetParent
	{
		public IEnumerable<EditableArea> EditableAreas { get; set; }
		public override IEnumerable<Widget> GetWidgets()
		{
			return EditableAreas.SelectMany(x => x.GetWidgets());
		}
		public override bool HasWidgets()
		{
			return EditableAreas.Any(x => x.HasWidgets());
		}
		public EditableArea GetEditableArea(string identifier)
		{
			return EditableAreas.Where(x => x.Identifier.Equals(identifier)).FirstOrDefault();
		}

		public IEnumerable<Variant> GetWidgetVariants(string widgetIdentifier = null)
		{
			return EditableAreas?
				.SelectMany(x => x.Sections)
				.SelectMany(x => x.Zones)
				.SelectMany(x => x.Widgets)
				.Where(x => string.IsNullOrEmpty(widgetIdentifier) || x.Type == widgetIdentifier)
				.Select(x => x.Variants.FirstOrDefault())
				.ToList();
		}
	}

	public partial class EditableArea : WidgetParent
	{
		public string Identifier { get; set; }
		public IEnumerable<Section> Sections { get; set; }
		public override IEnumerable<Widget> GetWidgets()
		{
			return Sections.SelectMany(x => x.GetWidgets());
		}
		public override bool HasWidgets()
		{
			return Sections.Any(x => x.HasWidgets());
		}
	}

	public partial class Section : WidgetParent
	{
		public Guid Identifier { get; set; }
		public string Type { get; set; }
		public object Properties { get; set; }
		[JsonIgnore] public Dictionary<string, object> PropertiesDictionary { get; set; }
		public IEnumerable<Zone> Zones { get; set; }
		public override IEnumerable<Widget> GetWidgets()
		{
			return Zones.SelectMany(x => x.Widgets);
		}
		public override bool HasWidgets()
		{
			return Zones.Any(x => x.HasWidgets());
		}
	}

	public partial class Zone : WidgetParent
	{
		public Guid Identifier { get; set; }
		public IEnumerable<Widget> Widgets { get; set; }

		public override IEnumerable<Widget> GetWidgets()
		{
			if (Widgets.IsNullOrEmpty())
			{
				return new List<Widget>();
			}
			else
			{
				return Widgets;
			}
		}

		public override bool HasWidgets()
		{
			return GetWidgets().Any();
		}
	}

	public abstract class WidgetParent
	{
		public abstract IEnumerable<Widget> GetWidgets();
		public abstract bool HasWidgets();
		public Dictionary<string, object> GetWidgetProperties(string widgetType)
		{
			var widget = GetWidgets().Where(x => x.Type.Equals(widgetType, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
			if (widget != null)
			{
				return widget.GetWidgetProperties();
			}
			return new Dictionary<string, object>();
		}
	}

	public partial class Widget
	{
		public Guid Identifier { get; set; }
		public string Type { get; set; }
		public IEnumerable<Variant> Variants { get; set; }
		public Dictionary<string, object> GetWidgetProperties()
		{
			if (Variants.IsNullOrEmpty())
			{
				return new Dictionary<string, object>();
			}
			return Variants.FirstOrDefault().PropertiesDictionary;
		}
	}

	public partial class Variant
	{
		public Guid Identifier { get; set; }
		public object Properties { get; set; }
		[JsonIgnore] public Dictionary<string, object> PropertiesDictionary { get; set; }
	}
}
