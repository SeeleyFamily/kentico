using Launchpad.Core.Abstractions.Specifications;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;


namespace Launchpad.Core.Extensions
{

	public static class SpecificationExtensions
	{

		public static void Parse(this ISpecification specification, NameValueCollection keyValues, string propertyName)
		{
			var newValue = System.Net.WebUtility.UrlDecode(keyValues[propertyName]);
			// No value found in the KV Collection
			if (newValue == null || string.IsNullOrWhiteSpace(newValue))
			{
				return;
			}

			var property = specification.GetType().GetProperty(propertyName);
			var propertyValue = property.GetValue(specification);

			bool isUnset = false;
			// Object Null Check
			if (propertyValue == null)
			{
				isUnset = true;
			}


			if (property.PropertyType == typeof(int[]))
			{
				List<int> list = new List<int>();
				string[] values = newValue.Split(',');

				foreach (string value in values)
				{
					if (int.TryParse(value, out int integer))
					{
						list.Add(integer);
					}
				}

				property.SetValue(specification, list.ToArray());
				return;
			}


			else if (property.PropertyType == typeof(string[]))
			{
				property.SetValue(specification, newValue.Split(','));
				return;
			}


			else if (property.PropertyType == typeof(Guid[]))
			{
				List<Guid> list = new List<Guid>();
				string[] values = newValue.Split(',');

				foreach (string value in values)
				{
					if (Guid.TryParse(value, out Guid guid))
					{
						list.Add(guid);
					}
				}

				property.SetValue(specification, list.ToArray());
				return;
			}


			else if (property.PropertyType == typeof(string))
			{
				if (!isUnset)
				{
					// Convert the current value object to the type
					var stringPropertyValue = propertyValue.ToString();
					// Check if the values are the same
					if (stringPropertyValue.Equals(newValue, StringComparison.InvariantCultureIgnoreCase))
					{
						return;
					}
				}
				// Assign the new value 
				property.SetValue(specification, newValue);
			}


			else if (property.PropertyType == typeof(int?) || property.PropertyType == typeof(int))
			{
				// return if there is a pase error on the new value
				if (!int.TryParse(newValue, out int intNewValue)) { return; }
				if (!isUnset)
				{
					// Convert the current value object to the type
					var stringPropertyValue = propertyValue.ToString();
					// Check if parsing the values failed or the parsed values are the same
					// Check each try parse explcitly..
					if (!int.TryParse(stringPropertyValue, out int intPropertyValue)
						|| intNewValue == intPropertyValue) { return; }
				}
				// Assign the new value 
				property.SetValue(specification, intNewValue);
			}


			else if (property.PropertyType == typeof(bool?) || property.PropertyType == typeof(bool))
			{
				if (!bool.TryParse(newValue, out bool boolNewValue)) { return; }
				if (!isUnset)
				{
					// Convert the current value object to the type
					var stringPropertyValue = propertyValue.ToString();
					// Check if parsing the values failed or the parsed values are the same
					// Check each explicitly otherwise parse may not run...
					if (!bool.TryParse(stringPropertyValue, out bool boolPropertyValue)
						|| boolPropertyValue != boolNewValue) { return; }
				}
				// Assign the new value 
				property.SetValue(specification, boolNewValue);
			}
		}

	}

}
