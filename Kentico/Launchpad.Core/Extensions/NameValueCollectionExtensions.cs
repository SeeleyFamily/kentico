using System.Collections.Specialized;

namespace Launchpad.Core.Extensions
{
	public static class NameValueCollectionExtensions
	{
		public static string GetStringValue(this NameValueCollection nameValueCollection, string key)
		{
			var currentStringValue = nameValueCollection[key];

			if (!string.IsNullOrWhiteSpace(currentStringValue))
			{
				return currentStringValue.ToString();
			}

			return string.Empty;
		}

		public static bool GetBoolValue(this NameValueCollection nameValueCollection, string key)
		{
			bool value = false;
			var currentStringValue = nameValueCollection[key];

			if (!string.IsNullOrWhiteSpace(currentStringValue))
			{
				bool.TryParse(currentStringValue, out value);
			}

			return value;
		}

		public static int GetIntValue(this NameValueCollection nameValueCollection, string key)
		{
			var currentStringValue = nameValueCollection[key];

			if (!string.IsNullOrWhiteSpace(currentStringValue))
			{
				if (currentStringValue != null)
				{
					if (int.TryParse(currentStringValue.ToString(), out int intValue))
					{
						return intValue;
					}
				}
			}

			return 0;
		}
	}
}
