using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using EasyFx.Core.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyFx.Core.DependencyInjection
{
    public class AutofacConfigure : IDisposable
    {
        public static IContainer ApplicationContainer { get; private set; }



        private static void RegisterTypes(ContainerBuilder builder, List<Type> types, ServiceLifetime lifetime, bool asSelf, bool isGeneric)
        {
            if (!types.Any())
            {
                return;
            }

            if (isGeneric)
            {
                foreach (var item in types)
                {
                    IRegistrationBuilder<object, ReflectionActivatorData, DynamicRegistrationStyle> typeBuilder;
                    var interfaceType = item.GetInterfaces().FirstOrDefault(p => p.IsGenericType);
                    if (interfaceType != null)
                    {
                        typeBuilder = builder.RegisterGeneric(item).As(interfaceType).PropertiesAutowired();
                    }
                    else
                    {
                        typeBuilder = builder.RegisterGeneric(item).PropertiesAutowired();
                    }
                    SetLifetime(typeBuilder, lifetime);
                }
            }
            else
            {
                var typeBuilder = builder.RegisterTypes(types.ToArray()).PropertiesAutowired();
                if (asSelf)
                {
                    typeBuilder.AsSelf();
                }
                else
                {
                    typeBuilder.AsImplementedInterfaces();
                }
                SetLifetime(typeBuilder, lifetime);
            }
        }

        private static void SetLifetime<TReflectionActivatorData>(IRegistrationBuilder<object, TReflectionActivatorData, DynamicRegistrationStyle> typeBuilder, ServiceLifetime lifetime)
        where TReflectionActivatorData : ReflectionActivatorData
        {

            switch (lifetime)
            {
                case ServiceLifetime.Scoped:
                    typeBuilder.InstancePerDependency();
                    break;

                case ServiceLifetime.Singleton:
                    typeBuilder.SingleInstance();
                    break;

                case ServiceLifetime.Transient:
                    typeBuilder.InstancePerLifetimeScope();
                    break;
            }
        }
        private static void RegisterTypes(ContainerBuilder builder, List<Type> types, ServiceLifetime lifetime)
        {
            if (!types.Any())
            {
                return;
            }

            var group = types.Select(it => new
            {
                AsSelf = it.GetInterfaces().Contains(typeof(IDependencyInterfaceIgnore)),
                IsGeneric = it.IsGenericType,
                Type = it
            })
            .GroupBy(it => new
            {
                it.AsSelf,
                it.IsGeneric
            });

            foreach (var item in group)
            {
                RegisterTypes(builder, item.Select(it => it.Type).ToList(), lifetime, item.Key.AsSelf, item.Key.IsGeneric);
                RegisterTypes(builder, item.Select(it => it.Type).ToList(), lifetime, item.Key.AsSelf, item.Key.IsGeneric);
            }
        }

        public static IServiceProvider BuildServiceProvider(IServiceCollection services, params IModule[] modules)
        {
            var builder = new ContainerBuilder();

            if (modules != null && modules.Any())
            {
                foreach (var module in modules)
                {
                    builder.RegisterModule(module);
                }
            }

            var types = ReflectionHelper.GetApplicationTypes();

            //singleton
            var singletonTypes = types.FindAll(t => t.GetInterfaces().Contains(typeof(ISingleton)) && !t.IsInterface);
            RegisterTypes(builder, singletonTypes, ServiceLifetime.Singleton);
            //scope
            var scopeTypes = types.FindAll(t => t.GetInterfaces().Contains(typeof(IScope)) && !t.IsInterface);
            RegisterTypes(builder, scopeTypes, ServiceLifetime.Scoped);
            //transient
            var transientTypes = types.FindAll(t => t.GetInterfaces().Contains(typeof(ITransient)) && !t.IsInterface);
            RegisterTypes(builder, transientTypes, ServiceLifetime.Transient);

            builder.Populate(services);

            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            ApplicationContainer?.Dispose();
        }
    }
}