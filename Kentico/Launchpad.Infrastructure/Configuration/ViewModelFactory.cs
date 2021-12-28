using System;
using Launchpad.Core.Abstractions.Models;
using Launchpad.Core.Abstractions.Providers;
using Launchpad.Core.Models;


namespace Launchpad.Infrastructure.Configuration
{

	public static class ViewModelFactory
	{

		/// <summary>
		/// Creates and initializes an appropriate <see cref="IViewModel"/> for the current request's <see cref="PageNode.NodeClassName"/>.
		/// </summary>
		public static IViewModel CreateViewModel( ICurrentNodeProvider currentNodeProvider, Func<string, IViewModel> getByClassNameFunction )
		{
			PageNode node = currentNodeProvider.GetCurrentNode();

			if( node == null )
			{
				// No viewmodel registered for the given class name
				return null;
			}


			// Create and initialize a registered view model for the given class name of the current node
			IViewModel viewModel = getByClassNameFunction( node.NodeClassName );

			if( viewModel != null )
			{
				viewModel.InitPopulate();
			}


			return viewModel;
		}

	}

}