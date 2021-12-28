using System;
using System.Collections;
using Launchpad.Core.Extensions;

namespace Launchpad.Infrastructure.Extensions
{

	public static class IDictionaryExtensions
	{
		public static string GetStringValue(this IDictionary dictionary, string key)
		{
			if (dictionary.Contains(key))
			{
				var value = dictionary[key];
				if (value != null)
				{
					return value.ToString();
				}
			}
			return string.Empty;
		}


		public static DateTime? GetDateTimeValue(this IDictionary dictionary, string key)
		{
			if (dictionary.Contains(key))
			{
				var value = dictionary[key];
				if (value != null)
				{
					if (DateTime.TryParse(value.ToString(), out var datetime))
					{
						return datetime;
					}
				}
			}
			return (DateTime?)null;
		}


		public static bool GetBoolValue(this IDictionary dictionary, string key)
		{
			if (dictionary.Contains(key))
			{
				var value = dictionary[key];
				if (value != null)
				{
					if (bool.TryParse(value.ToString(), out bool boolValue))
					{
						return boolValue;
					}
				}
			}
			return false;
		}


		public static Guid[] GetGuidArray(this IDictionary dictionary, string key, char delimiter = ',' )
		{
			if( !dictionary.Contains( key ) || !( dictionary[key] is string value )  )
			{
				return null;
			}

			return value.ToGuidArray( delimiter );
		}


		public static int GetIntValue(this IDictionary dictionary, string key)
		{
			if (dictionary.Contains(key))
			{
				var value = dictionary[key];
				if (value != null)
				{
					if (int.TryParse(value.ToString(), out int intValue))
					{
						return intValue;
					}
				}
			}
			return 0;
		}
	}

}
