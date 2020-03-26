using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MediatR.Extensions.FluentBuilder
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<T> GetRequestModulesAs<T>(this Assembly assembly)
        {
            return assembly
                .GetTypes()
                .Where(x => !x.IsAbstract && x.IsClass && ImplementsRequestModule(x))
                .Select(Activator.CreateInstance)
                .Cast<T>();
        }

        public static IEnumerable<T> GetNotificationModulesAs<T>(this Assembly assembly)
        {
            return assembly
                .GetTypes()
                .Where(x => !x.IsAbstract && x.IsClass && ImplementsNotificationModule(x))
                .Select(Activator.CreateInstance)
                .Cast<T>();
        }

        private static bool ImplementsRequestModule(Type typeToCheck)
        {
            var genericInterface = typeof(IRequestModule<,>);
            return ImplementsGenericInterface(typeToCheck, genericInterface);
        }

        private static bool ImplementsNotificationModule(Type typeToCheck)
        {
            var genericInterface = typeof(INotificationModule<>);
            return ImplementsGenericInterface(typeToCheck, genericInterface);
        }

        private static bool ImplementsGenericInterface(Type typeToCheck, Type genericInterface)
        {
            foreach (var @interface in typeToCheck.GetInterfaces())
            {
                if (!@interface.IsGenericType)
                {
                    continue;
                }

                if (@interface.GetGenericTypeDefinition() == genericInterface)
                {
                    return true;
                }
            }

            return false;
        }
    }
}