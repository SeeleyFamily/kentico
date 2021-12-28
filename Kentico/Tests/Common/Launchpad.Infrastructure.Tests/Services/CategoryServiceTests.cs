using System.Collections.Generic;
using System.Linq;
using CMS.DocumentEngine;
using CMS.Taxonomy;
using CMS.Tests;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Infrastructure.Utilities;
using NUnit.Framework;

namespace Launchpad.Infrastructure.Tests.Services
{
	public class CategoryServiceTests : IntegrationTests
	{

		#region Fields
		private readonly ICategoryService service;
		#endregion

		public CategoryServiceTests()
		{
			// Default service setup
			service = ServiceCreatorUtility.CreateCategoryService();
		}

		[Test]
		public void GetCategories()
		{
			// Get Categories from Info Provider
			var cmsCategories = CategoryInfo.Provider.Get();

			// Get Categories from Service
			var serviceCategories = service.GetCategories();

			Assert.AreEqual(cmsCategories.Count, serviceCategories.Count());
		}


		[Test]
		public void GetCategory()
		{

		}

		[Test]
		public void GetDocumentCategories()
		{

			//var d = "";
			// TODO - WRITE A PROPER TEST
		}

		[Test]
		public void GetAllDocumentCategories()
		{
			var maxDocumentCategories = DocumentCategoryInfo.Provider.Get().GroupBy(x => x.DocumentID).OrderByDescending(x => x.Count()).FirstOrDefault();
			var documentId = maxDocumentCategories.Key;
			var count = maxDocumentCategories.Count();

			// Arrange
			//List<Launchpad.Core.Models.Category> example = service.GetCategoriesByPageNode(documentId).ToList();

			// Act
			Dictionary<int, int[]> categories = service.GetDocumentCategories();

			// Assert
			Assert.IsNotNull(categories);
			//Assert.AreEqual(categories[documentId].Count(), example.Count());
			Assert.AreEqual(categories[documentId].Count(), count);
		}

		[Test]
		public void GetCategoriesFromCache()
		{
			var maxDocumentCategories = DocumentCategoryInfo.Provider.Get().GroupBy(x => x.DocumentID).OrderByDescending(x => x.Count()).FirstOrDefault();
			var documentId = maxDocumentCategories.Key;
			var count = maxDocumentCategories.Count();

			// Arrange
			//List<Launchpad.Core.Models.Category> example = service.GetCategoriesByPageNode(documentId).ToList();

			//// Act
			//List<Launchpad.Core.Models.Category> categoryNames = service.GetCategoriesByDocument(documentId).ToList();

			//// Assert
			//Assert.IsNotNull(categoryNames);
			//Assert.AreEqual(categoryNames.Count(), example.Count());
			//Assert.AreEqual(categoryNames.Count(), count);
		}
	}
}
