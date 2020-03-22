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

        private static bool ImplementsRequestModule(Type type)
        {
            foreach (var @interface in type.GetInterfaces())
            {
                if (!@interface.IsGenericType)
                {
                    continue;
                }

                if (@interface.GetGenericTypeDefinition() == typeof(IRequestModule<,>))
                {
                    return true;
                }
            }

            return false;
        }
    }
}