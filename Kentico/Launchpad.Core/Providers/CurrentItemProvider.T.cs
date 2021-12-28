using Launchpad.Core.Abstractions.Providers;


namespace Launchpad.Core.Providers
{

	/// <summary>
	/// Provides the current <typeparamref name="T"/>.
	/// </summary>
	public class CurrentItemProvider<T> : ICurrentItemProvider<T>
	{
		#region Properties
		protected T Item { get; set; }
		#endregion


		public CurrentItemProvider
		(

		)
		{

		}



		/// <summary>
		/// Retrieves the current <typeparamref name="T"/>.
		/// </summary>
		public virtual T GetCurrent( )
		{
			if( Item == null )
			{
				Item = SetItemInternal();
			}


			return Item;
		}


		/// <summary>
		/// Sets the <typeparamref name="T"/> for the current context and request.
		/// </summary>
		public virtual void SetCurrent( T item )
		{
			Item = item;
		}



		protected virtual T SetItemInternal( )
		{
			// This can be overridden in subclasses, otherwise it just sets the item to null
			Item = default;

			return Item;
		}

	}

}