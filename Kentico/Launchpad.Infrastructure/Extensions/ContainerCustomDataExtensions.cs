using CMS.Helpers;
using System;

namespace Launchpad.Infrastructure.Extensions
{
	public static class ContainerCustomDataExtensions
	{
		public static string GetStringValue(this ContainerCustomData customData, string customDataKey)
		{
			if (customData.TryGetValue(customDataKey, out var currentStringValue))
			{
				return currentStringValue.ToString();
			}
			return string.Empty;
		}

		public static bool UpdateCustomDataStringValue(this ContainerCustomData customData, string customDataKey, string newStringValue)
		{
			var doUpdate = false;
			string currentStringValue = customData.GetStringValue(customDataKey);
			if (currentStringValue != newStringValue)
			{
				doUpdate = true;
			}

			if (doUpdate)
			{
				customData.SetValue(customDataKey, newStringValue);
			}

			return doUpdate;
		}

		public static DateTime? GetDateTimeValue(this ContainerCustomData customData, string customDataKey)
		{
			if (customData.TryGetValue(customDataKey, out var currentDateTimeValue))
			{
				if (DateTime.TryParse(currentDateTimeValue.ToString(), out var datetime))
				{
					return datetime;
				}
			}
			return (DateTime?)null;
		}

		public static bool UpdateCustomDataDateTimeValue(this ContainerCustomData customData, string customDataKey, DateTime? newDateTimeValue)
		{
			var doUpdate = false;
			DateTime? currentDateTimeValue = customData.GetDateTimeValue(customDataKey);
			if (currentDateTimeValue != newDateTimeValue)
			{
				doUpdate = true;
			}

			if (doUpdate)
			{
				customData.SetValue(customDataKey, newDateTimeValue);
			}

			return doUpdate;
		}

		public static bool GetBooleanValue(this ContainerCustomData customData, string customDataKey)
		{
			if (customData.TryGetValue(customDataKey, out var currentBoolValue))
			{
				if (bool.TryParse(currentBoolValue.ToString(), out var boolValue))
				{
					return boolValue;
				}
			}
			return false;
		}

		public static bool UpdateCustomDataBoolValue(this ContainerCustomData customData, string customDataKey, bool newBoolValue)
		{
			var doUpdate = false;
			bool? currentBoolValue = customData.GetBooleanValue(customDataKey);
			if (currentBoolValue != newBoolValue)
			{
				doUpdate = true;
			}

			if (doUpdate)
			{
				customData.SetValue(customDataKey, newBoolValue);
			}

			return doUpdate;
		}


	}
}
