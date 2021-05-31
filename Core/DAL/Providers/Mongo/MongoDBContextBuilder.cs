using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.Markdown.Core.DAL.Providers.Mongo
{
    public class MongoDBContextBuilder
    {
        public MongoDBOptions Options { get; set; }

        public MongoDBContextBuilder()
        {

        }

        public MongoDBContextBuilder(MongoDBOptions options)
        {
            this.Options = options;
        }

        /// <summary>
        /// Find the all instances of the IRegisterIndex interface within the current domains assemblies and run the execute function for each to configure the mappings.
        /// </summary>
        public void ExecuteIndexRegistrations()
        {
            List<Type> _registerIndexesTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x => typeof(IRegisterIndexes).IsAssignableFrom(x) && !x.ContainsGenericParameters && !x.IsInterface).ToList();

            foreach (Type registerIndexesType in _registerIndexesTypes)
            {
                var _registerIndexesInstance = (IRegisterIndexes)Activator.CreateInstance(registerIndexesType);

                if (_registerIndexesInstance != null)
                {
                    _registerIndexesInstance.Execute();
                }
            }
        }

        /// <summary>
        /// Find the all instances of the IRegisterMap interface within the current domains assemblies and run the execute function for each to configure the mappings.
        /// </summary>
        public void ExecuteMappingRegistrations()
        {
            List<Type> _registerMapTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x => typeof(IRegisterMap).IsAssignableFrom(x) && !x.ContainsGenericParameters && !x.IsInterface).ToList();

            foreach (Type registerMapType in _registerMapTypes)
            {
                var _registerMapInstance = (IRegisterMap)Activator.CreateInstance(registerMapType);

                if (_registerMapInstance != null)
                {
                    _registerMapInstance.Execute();
                }
            }
        }
    }
}
