using System;

namespace Launchpad.Infrastructure.DependencyInjection.Models
{
	public class PageTypeRegistration
	{
		public Type ServiceType { get; set; }
		public Type ImplementingType { get; set; }
		public string ServiceName { get; set; }
	}
}
