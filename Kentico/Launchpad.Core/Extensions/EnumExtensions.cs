using System;
using System.Linq;
using System.Reflection;

namespace Launchpad.Core.Extensions
{
	public static class EnumExtensions
	{
		//public static string GetDescription<T>(this T genericEnum)			
		//{
		//	Type genericEnumType = genericEnum.GetType();
		//	MemberInfo[] memberInfo = genericEnumType.GetMember(genericEnum.ToString());
		//	if ((memberInfo != null && memberInfo.Length > 0))
		//	{
		//		var _Attribs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
		//		if ((_Attribs != null && _Attribs.Count() > 0))
		//		{
		//			return ((System.ComponentModel.DescriptionAttribute)_Attribs.ElementAt(0)).Description;
		//		}
		//	}
		//	return genericEnum.ToString();
		//}

		public static TAttribute GetAttribute<TAttribute>(this Enum value)
			where TAttribute : Attribute
		{
			var type = value.GetType();
			var name = Enum.GetName(type, value);
			return type.GetField(name) // I prefer to get attributes this way
				.GetCustomAttribute<TAttribute>();
		}
	}
}
