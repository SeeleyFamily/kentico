using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CMS.DocumentEngine;


namespace Launchpad.Infrastructure.Kentico.Web.Attributes
{

	[AttributeUsage( AttributeTargets.Class )]
	public class RegisterForPageTypeAttribute : Attribute
	{
		#region Properties
		public IEnumerable<string> ClassNames { get; protected set; }
		#endregion


		public RegisterForPageTypeAttribute( params Type[] pageTypes )
		{
			List<string> classNames = new List<string>();

			foreach( Type pageType in pageTypes )
			{
				if( !( pageType == typeof( TreeNode ) ) )
				{
					throw new ArgumentException( "Implementing class must be of type TreeNode." );
				}


				PropertyInfo property = pageType.GetProperty( "CLASSNAME", BindingFlags.Static );

				if( property == null )
				{
					throw new ArgumentException( $"Type {pageType.Name} does not include class name property." );
				}


				classNames.Add( ( string ) property.GetValue( null ) );
			}

			ClassNames = classNames;
		}


		public RegisterForPageTypeAttribute( params string[] classNames )
		{
			ClassNames = classNames.ToArray();
		}
	}

}
