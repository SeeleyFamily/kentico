


namespace Launchpad.Core.Abstractions.Services
{
	
	public interface ISettingsService
	{
		/// <summary>
		/// Returns a string setting.
		/// </summary>
		string GetSetting( string settingCodeName );


		/// <summary>
		/// Returns a setting of type <typeparamref name="T"/>, if possible. Otherwise returns the type's default.
		/// </summary>
		T GetSetting<T>( string settingCodeName );
	}

}
