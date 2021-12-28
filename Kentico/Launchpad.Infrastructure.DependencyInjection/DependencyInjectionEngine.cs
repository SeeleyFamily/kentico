using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Models;
using Launchpad.Core.Abstractions.Providers;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Attributes;
using Launchpad.Core.Enums;
using Launchpad.Core.Extensions;
using Launchpad.Infrastructure.Abstractions.Services;
using Launchpad.Infrastructure.Configuration;
using Launchpad.Infrastructure.DependencyInjection.Models;
using Launchpad.Infrastructure.Kentico.Web.Attributes;
using Launchpad.Infrastructure.Kentico.Web.Configuration;
using Launchpad.Infrastructure.Services;
using LightInject;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;


namespace Launchpad.Infrastructure.DependencyInjection
{

	public class DependencyInjectionEngine
	{
		#region Properties
		public IServiceContainer Container { get; protected set; }
		#endregion

		/// <summary>
		/// Configures and returns the service container. Call this in your web application startup method.
		/// </summary>
		public IServiceContainer Configure(HttpConfiguration httpConfiguration, params Assembly[] assemblies)
		{
			Container = new ServiceContainer(new ContainerOptions
			{
				EnablePropertyInjection = false
			});


			// Register controllers
			Container.RegisterApiControllers(assemblies);
			Container.RegisterControllers(assemblies);


			// Enable web related functionality
			Container.EnablePerWebRequestScope();
			Container.SetDefaultLifetime<PerScopeLifetime>();
			Container.EnableMvc();
			Container.EnableWebApi(httpConfiguration);


			// Scannable, discoverable service registrations
			RegisterDiscoverableServices<IPerApplicationService, PerContainerLifetime>(assemblies);
			RegisterDiscoverableServices<IPerScopeService, PerScopeLifetime>(assemblies);
			RegisterDiscoverableServices<IPerRequestService, PerRequestLifeTime>(assemblies);


			// Specific services
			RegisterServices();

			// Register Generic Document Services
			RegisterDocumentServices();

			// Register Generic Global Content Document Services
			RegisterGlobalContentDocumentServices();

			// Register Generic Related Services
			RegisterRelatedServices();

			// Register Custom Table Services
			RegisterCustomTableServices();

			return Container;
		}


		public void OverrideRegistration<TService, TImplementation, TLifetimeScope>()
			where TImplementation : TService
			where TLifetimeScope : ILifetimeScope
		{
			switch (typeof(TLifetimeScope))
			{
				case IPerApplicationService appScope:
					Container.Register<TService, TImplementation>(new PerContainerLifetime());
					break;

				case IPerScopeService webRequestScope:
					Container.Register<TService, TImplementation>(new PerScopeLifetime());
					break;

				default:
					Container.Register<TService, TImplementation>();
					break;
			}
		}

		public void OverrideRegistration(Type serviceType, Type implementingType, ILifetime lifetime)
		{
			Container.Register(serviceType, implementingType, lifetime);
		}


		public void RegisterViewModelFactory()
		{
			Container.Register(
				(factory) => ViewModelFactory.CreateViewModel(factory.GetInstance<ICurrentNodeProvider>(), className =>
				{
					// Try to return a viewmodel registered under the class name, otherwise send back null and avoid an error
					if (!(factory.TryGetInstance<IViewModel>(className) is IViewModel viewModel))
					{
						return null;
					}

					return viewModel;
				})
			);
		}



		private void RegisterServices()
		{
			// Register HttpContext / HttpContextBase
			Container.Register<HttpContextBase>(
				factory => new HttpContextWrapper(HttpContext.Current),
				new PerScopeLifetime()
			);

			// Register the application's HttpClient
			Container.Register(
				factory => new HttpClient
				{
					Timeout = TimeSpan.FromSeconds(10)
				},
				new PerContainerLifetime()
			);

			// Register site context factory;
			Container.Register(
				(factory) => SiteContextConfigurationFactory.CreateConfiguration(),
				new PerScopeLifetime()
			);

			// Register document query configuration factory; DI passes it the current HttpContext specific to each web request
			Container.Register(
				(factory) => DocumentQueryConfigurationFactory.CreateConfiguration(factory.GetInstance<HttpContextBase>()),
				new PerScopeLifetime()
			);

			// Register cache configuration factory; DI passes it the current HttpContext specific to each web request
			Container.Register(
				(factory) => CacheConfigurationFactory.CreateConfiguration(factory.GetInstance<HttpContextBase>()),
				new PerScopeLifetime()
			);
		}


		/// <summary>
		/// Registers the Generic Type for <see cref="CustomTableService{T}"/>
		/// </summary>
		private void RegisterCustomTableServices()
		{
			// Register Generic Custom Table Services
			Container.Register(typeof(ICustomTableService<>), typeof(CustomTableService<>), new PerScopeLifetime());
		}

		/// <summary>
		/// Registers the Generic Type for <see cref="DocumentService{T}"/>
		/// </summary>
		private void RegisterDocumentServices()
		{
			// Register Generic Document Services
			Container.Register(typeof(IDocumentService<>), typeof(DocumentService<>), new PerScopeLifetime());
		}

		/// <summary>
		/// Registers the Generic Type for <see cref="GlobalContentDocumentService{T}"/>
		/// </summary>
		private void RegisterGlobalContentDocumentServices()
		{
			// Register Generic Document Services
			Container.Register(typeof(IGlobalContentDocumentService<>), typeof(GlobalContentDocumentService<>), new PerScopeLifetime());
		}

		/// <summary>
		/// Registers the Generic Type for <see cref="RelatedService{T}"/>
		/// </summary>
		private void RegisterRelatedServices()
		{
			// Register Generic Related Services
			Container.Register(typeof(IRelatedService<>), typeof(RelatedService<>), new PerScopeLifetime());
		}

		/// <summary>
		/// Scans assemblies for service classes marked with the <typeparamref name="T"/> marker interface, 
		/// and adds them to the container registrations.
		/// </summary>
		private void RegisterDiscoverableServices<T, TLifetime>(Assembly[] assemblies)
			where TLifetime : ILifetime, new()
		{
			Type markerInterface = typeof(T);
			List<PageTypeRegistration> pageTypeRegistrationList = new List<PageTypeRegistration>();

			// Common namespaced service types should only be implmented if there is no existing custom registration type
			// This works only if we register all the custom assemblies first
			bool ShouldRegisterCommon(Type serviceType, Type implementingType, string serviceName)
			{
				var instanceExists = Container.CanGetInstance(serviceType, serviceName);
				var isCommon = implementingType.Namespace.ToLower().Contains($".{NamespacePath.Common.GetAttribute<CodeDisplayNameTypeAttribute>().CodeName.ToLower()}.");
				var commonServiceNameSpace = typeof(DocumentService<>).Assembly.GetName().Name;
				var isCommonService = implementingType.Namespace.ToLower().Contains($"{commonServiceNameSpace.ToLower()}");
				if (instanceExists && (isCommon || isCommonService))
				{
					return false;
				}
				return true;
			}

			bool ShouldRegister(Type serviceType, Type implementingType)
			{

				var shouldRegister = serviceType.IsInterface
					&& serviceType != markerInterface
					&& implementingType.IsClass
					&& !implementingType.IsAbstract
					&& !implementingType.IsGenericType
					&& markerInterface.IsAssignableFrom(implementingType);

				// Exclude pageType registration here
				// PageTypes will be registered after the services seperately
				var shouldRegisterPageType = true;
				if (shouldRegister)
				{
					if (implementingType.GetCustomAttribute<RegisterForPageTypeAttribute>() is RegisterForPageTypeAttribute attribute)
					{
						shouldRegisterPageType = false;

						foreach (var className in attribute.ClassNames)
						{
							var pageTypeRegistration = new PageTypeRegistration()
							{
								ServiceType = serviceType,
								ImplementingType = implementingType,
								ServiceName = className,
							};
							pageTypeRegistrationList.Add(pageTypeRegistration);
						}
					}
				}

				var serviceName = GetServiceName(serviceType, implementingType);
				var shouldRegisterCommon = ShouldRegisterCommon(serviceType, implementingType, serviceName);

				return shouldRegister && shouldRegisterPageType && shouldRegisterCommon;
			};

			string GetServiceName(Type serviceType, Type implementingType)
			{
				return string.Empty;
			}

			foreach (Assembly assembly in assemblies)
			{
				Container.RegisterAssembly(assembly, () => new TLifetime(), shouldRegister: ShouldRegister, serviceNameProvider: GetServiceName);
			}

			// The following block makes a huge assumption that all Custom Assemblies are registered before Common ones.
			// I think it is actually in the order they are added to the project (with most recent first)
			// By nature of custom projects, customs versions will always be added after common ones...
			foreach (var pageTypeRegistration in pageTypeRegistrationList)
			{
				var serviceType = pageTypeRegistration.ServiceType;
				var serviceName = pageTypeRegistration.ServiceName;
				var implementingType = pageTypeRegistration.ImplementingType;

				var shouldRegisterCommon = ShouldRegisterCommon(serviceType, implementingType, serviceName);

				if (shouldRegisterCommon)
				{
					Container.Register(serviceType, implementingType, serviceName, new TLifetime());
				}
			}
		}
	}

}