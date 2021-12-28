using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.Common;
using CMS.Membership;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Attributes;
using Launchpad.Core.Enums;
using Launchpad.Core.Extensions;
using Launchpad.Core.Models;
using Launchpad.Core.Specifications;
using System;
using System.Linq;


namespace Launchpad.Infrastructure.Extensions.Query
{


    public static class PeopleQueryExtensions
    {
        public static DocumentQuery<PeopleProfile> ApplyPeopleSpecification(this DocumentQuery<PeopleProfile> query, PeopleSpecification specification)
        {
            if (specification == null)
            {
                return query;
            }

            // Sub Specifications
            query.ApplyPathSpecification(specification);

            // Sorting
            if (specification.Sort.Equals(SortType.AZ.GetAttribute<CodeDisplayNameTypeAttribute>().CodeName))
            {
                query.OrderByAscending(nameof(PeopleProfile.LastName));
            }
            if (specification.Sort.Equals(SortType.NodeOrder.GetAttribute<CodeDisplayNameTypeAttribute>().CodeName))
            {
                query.OrderByAscending(nameof(TreeNode.NodeOrder));
            }

            return query;
        }
    }
}
