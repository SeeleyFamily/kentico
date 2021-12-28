using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Models;
using Launchpad.Core.Models;
using System;
using System.Collections.Generic;


namespace Launchpad.Core.Abstractions.Services
{

	/// <summary>
	/// Retrieves documents from the applicable storage mechanism and applies business logic and caching.
	/// </summary>
	public interface IDocumentService<T>
		where T : class
	{
		/// <summary>
		/// Retrieves all documents of a certain class name / page type.
		/// </summary>
		IEnumerable<T> Get(ICacheConfiguration cacheConfiguration = null);

		/// <summary>
		/// Retrieves all documents of a certain class name / page type with specified columns.
		/// </summary>
		IEnumerable<T> Get(string[] columns);

		/// <summary>
		/// Retrieves a document by its Node ID.
		/// </summary>
		T Get(int id);

		/// <summary>
		/// Retrieves a document by its Node Alias Path.
		/// </summary>
		T Get(string path);

		/// <summary>
		/// Retrieves a document by its Node Guid
		/// </summary>
		T Get(Guid guid);

		/// <summary>
		/// Retrieves documents by its Node Guids ordered by Node Guids
		/// </summary>
		IEnumerable<T> Get(IEnumerable<Guid> guids);

		/// <summary>
		/// Retrieves child documents by their parent's Node ID.
		/// </summary>
		IEnumerable<T> GetByParent(int id, int count = 0);

		/// <summary>
		/// Retrieves child documents by their parent's Node Alias Path.
		/// </summary>
		IEnumerable<T> GetByParent(string path, int count = 0);

		/// <summary>
		/// Retrieves child documents by their parent's Node Guid
		/// </summary>
		IEnumerable<T> GetByParent(Guid guid, int count = 0);

		/// <summary>
		/// Retrieves child documents from a folder of type <typeparamref name="TFolderType"/>.
		/// </summary>
		IEnumerable<T> GetFromFolder<TFolderType>( string path, int count = 0 ) where TFolderType : class;

		/// <summary>
		/// Retrieves child documents from the Global Content Tree
		/// </summary>
		IEnumerable<T> GetFromGlobalContent(int count = 0);

		/// <summary>
		/// Determines whether a given <see cref="IUser"/> is authorized to read a given <see cref="PageNode"/> based on the node's ACL.
		/// </summary>
		bool IsNodeAuthorizedForUser(PageNode node, IUser user);
	}

}
