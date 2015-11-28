using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using flight.library.Attributes;
using Microsoft.Practices.Unity;

namespace flight.api
{
    public class UnityConfig
    {
        #region Unity Container
        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }
        #endregion


        public static IEnumerable<Type> GetTypesWithCustomAttribute<T>(Assembly[] assemblies)
        {
            return from assembly in assemblies from type in assembly.GetTypes() where type.GetCustomAttributes(typeof(T), true).Length > 0 select type;
        }

        public static void RegisterTypes(IUnityContainer container)
        {


            // Add your register logic here...
            var myAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("flight") && !a.FullName.StartsWith("flight.api")).ToArray();

            container.RegisterType(typeof(Startup));

            container.RegisterTypes(
                 UnityConfig.GetTypesWithCustomAttribute<UnityIoCSingletonLifetimedAttribute>(myAssemblies),
                 WithMappings.FromMatchingInterface,
                 WithName.Default,
                 WithLifetime.Hierarchical,
                 null
                ).RegisterTypes(
                        UnityConfig.GetTypesWithCustomAttribute<UnityIoCTransientLifetimedAttribute>(myAssemblies),
                        WithMappings.FromMatchingInterface,
                        WithName.Default,
                        WithLifetime.Transient);

        }

    }
}