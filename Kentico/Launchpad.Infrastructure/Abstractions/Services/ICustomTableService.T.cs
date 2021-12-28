using System.Collections.Generic;
using CMS.CustomTables;


namespace Launchpad.Infrastructure.Abstractions.Services
{

	public interface ICustomTableService<T>
		where T : CustomTableItem, new()
	{
		/// <summary>
		/// Returns an <see cref="IEnumerable{T}"/> based on the CustomTableItem passed in as the Generic Type.
		/// </summary>
		/// <returns></returns>
		IEnumerable<T> GetItems( );
	}

}
