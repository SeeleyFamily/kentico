/*
 * Built with Common Launchpad 2.0.2
 */

using CMS.DataEngine;
using Kentico.Forms.Web.Mvc;
using Launchpad.Core.Extensions;
using System.Collections.Generic;

namespace Launchpad.Web.Models.Common.FormComponents
{
    public class DynamicDropdownComponentProperties : FormComponentProperties<string>
    {
        [DefaultValueEditingComponent(TextInputComponent.IDENTIFIER)]
        public override string DefaultValue
        {
            get;
            set;
        }

        [EditingComponent(TextInputComponent.IDENTIFIER, Label = "Placeholder")]
        public override string Placeholder
        {
            get;
            set;
        }

        [EditingComponent(TextAreaComponent.IDENTIFIER)]
        public string Options { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Label = "Custom Tooltip")]
        public string CustomTooltip { get; set; }

        [EditingComponent(CheckBoxComponent.IDENTIFIER, Label = "Allow Multiple")]
        public bool AllowMultiple { get; set; } = false;

        public DynamicDropdownComponentProperties()
            : base(FieldDataType.Text, 250)
        {
        }

        public Dictionary<string, string> GetOptions()
        {
            return Options.GenerateOptionsDictionary();
        }
    }
}