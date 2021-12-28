using Launchpad.Core.Models;
using System;
using System.Collections.Generic;

namespace Launchpad.Core.Abstractions.Services
{
	public interface ICategoryService
	{
		/// <summary>
		/// Gets a dictionary of all the assigned categoryIds per documentId
		/// </summary>
		Dictionary<int, int[]> GetDocumentCategories();


		/// <summary>
		/// Gets all <see cref="Category"/> assigned to the current <see cref="PageNode"/>
		/// </summary>
		IEnumerable<Category> GetDocumentCategories(PageNode pageNode);


		/// <summary>
		/// Gets all <see cref="Category"/> assigned to the current <see cref="PageNode"/> using the <see cref="PageNode.DocumentID"/>
		/// </summary>
		IEnumerable<Category> GetDocumentCategories(int documentId);


		/// <summary>
		/// Gets a delimted string of all the <see cref="Category.Guid"/>
		/// </summary>
		string GetDelimitedCategoryGuids(IEnumerable<Category> categories);


		/// <summary>
		/// Gets a delimted string of all the <see cref="Category.DisplayName"/>
		/// </summary>
		string GetDelimetedCategoryDisplayNames(IEnumerable<Category> categories);


		/// <summary>
		/// Gets a delimted string of all the <see cref="Category.CodeNamePath"/>
		/// </summary>
		/// <returns></returns>
		string GetDelimitedCategoryCodeNamePaths(IEnumerable<Category> categories);


		/// <summary>
		/// Gets all <see cref="Category"/> for thie site.
		/// </summary>
		IEnumerable<Category> GetCategories();


		IEnumerable<Category> GetCategories(IEnumerable<string> codeNames);


		IEnumerable<Category> GetCategories(IEnumerable<Guid> guids);


		Category GetCategory(string codeName);


		Category GetCategory(Guid categoryGuid);


		IEnumerable<Category> GetCategoriesByParentCategory(string parentCategoryCodeName);


		IEnumerable<Category> GetCategoriesByParentCategory(Guid parentCategoryGuid);


		double GetCategoryRelatedRanking(IEnumerable<string> categoryCodeNames1, IEnumerable<string> categoryCodeNames2);


	}
}
