/*
 * Built with Common Launchpad 2.0.2
 */

using Kentico.Forms.Web.Mvc;
using Launchpad.Core.Models;
using Launchpad.Web.Models.Common.FormComponents;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

[assembly: RegisterFormComponent(NameListComponent.IDENTIFIER, typeof(NameListComponent), "List of Text Inputs with Underlying GUID", IconClass = "icon-list")]
namespace Launchpad.Web.Models.Common.FormComponents
{
	public class NameListComponent : FormComponent<NameListComponentProperties, string>
	{
		public const string IDENTIFIER = nameof(NameListComponent);

		[BindableProperty]
		public string Value { get; set; }

		public override string GetValue()
		{
			// ensures GUID is generated for each name
			Value = JsonConvert.SerializeObject(GetNameAndGuids());

			return Value;
		}

		public override void SetValue(string value)
		{
			Value = value;
		}

		public List<NameAndGuid> GetNameAndGuids()
		{
			List<NameAndGuid> nameAndGuids = null;

			try
			{
				nameAndGuids = JsonConvert.DeserializeObject<List<NameAndGuid>>(Value);
			}
			catch (Exception)
			{
			}

			nameAndGuids?.ForEach(x =>
			{
				if (x.Guid == default)
				{
					x.Guid = Guid.NewGuid();
				}
			});

			return nameAndGuids ?? new List<NameAndGuid>();
		}
	}
}
