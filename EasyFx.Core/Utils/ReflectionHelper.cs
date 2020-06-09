using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace EasyFx.Core.Utils
{
    public class ReflectionHelper
    {

       public static List<Type> GetApplicationTypes()
        {
            var context = DependencyContext.Default;
            return context.CompileLibraries
                .Where(lib => !lib.Serviceable && lib.Type != "package")
                .SelectMany(lib =>
                    AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name)).GetTypes())
                .ToList();

        }


        public static List<Assembly> GetApplicationAssemblies()
        {
            var context = DependencyContext.Default;
            return context.CompileLibraries
                .Where(lib => !lib.Serviceable && lib.Type != "package")
                .Select(lib => AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name)))
                .ToList();

        }
    }
}