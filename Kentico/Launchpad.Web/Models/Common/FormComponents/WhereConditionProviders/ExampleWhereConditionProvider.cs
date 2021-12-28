using CMS.CustomTables.Types.Common;
using CMS.DataEngine;
using Kentico.Components.Web.Mvc.FormComponents;

namespace Launchpad.Web.Models.Common.FormComponents.WhereConditionProviders
{
    public class ExampleWhereConditionProvider : IObjectSelectorWhereConditionProvider
    {
        public WhereCondition Get()
        {
            return new WhereCondition()
                .WhereContains(nameof(IconCssClassItem.CodeName), "s")
                .WhereNotEquals(nameof(IconCssClassItem.CssClass), "icon-icon-Search")
                .WhereEqualsOrNull(nameof(IconCssClassItem.Type), "type that shouldn't be used");
        }
    }
}
