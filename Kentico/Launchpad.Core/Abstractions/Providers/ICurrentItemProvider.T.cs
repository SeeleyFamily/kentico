using Launchpad.Core.Abstractions.Configuration;


namespace Launchpad.Core.Abstractions.Providers
{

	/// <summary>
	/// An interface for getting and setting the current <typeparamref name="T"/>, using a default DI scope of per web request.
	/// </summary>
	public interface ICurrentItemProvider<T> : IPerScopeService
	{
		/// <summary>
		/// Retrieves the current <typeparamref name="T"/>.
		/// </summary>
		T GetCurrent( );


		/// <summary>
		/// Sets the current <typeparamref name="T"/>.
		/// </summary>
		void SetCurrent( T item );
	}

}