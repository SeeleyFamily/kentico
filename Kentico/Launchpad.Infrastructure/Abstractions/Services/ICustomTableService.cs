using System.Collections.Generic;
using CMS.CustomTables;


namespace Launchpad.Infrastructure.Abstractions.Services
{

	public interface ICustomTableService
	{
		/// <summary>
		/// Returns an an <see cref="IEnumerable{CustomTableItem}" /> based on the CustomTable's <paramref name="className"/>
		/// If <paramref name="cache"/> is set, it will use return it from cache, otherwise - directly from the database.
		/// </summary>		
		IEnumerable<CustomTableItem> GetByType(string className, bool cache = true);
	}

}
