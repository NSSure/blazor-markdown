using Blazor.Markdown.Core.DAL.Mongo;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.Markdown.Core.DAL.Providers.Mongo
{
    public class MongoDBContextBuilder<TContext>
    {
        public MongoDBOptions Options { get; set; }

        public MongoDBContextBuilder()
        {

        }

        public MongoDBContextBuilder(MongoDBOptions options)
        {
            this.Options = options;
        }

        public void Run()
        {
            // Inject to builder through DI container?
            MongoDBContext _context = (MongoDBContext)Activator.CreateInstance(typeof(TContext));

            if (this.Options.EnsureCreated)
            {
                // Indexes are idempotent so should this just run every time the application starts?
                this.ExecuteMapRegistrations();
                this.ExecuteIndexRegistrations();

                IMongoDatabase _existingDatabase = _context.Client.GetDatabase(typeof(MongoDBContext).Name);

                this.ExecuteSeedRegistrations();
            }
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
        public void ExecuteMapRegistrations()
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

        public void ExecuteSeedRegistrations()
        {
            // How to inject context into the constructor instead of creating an instance.
            MongoDBContext _context = (MongoDBContext)Activator.CreateInstance(typeof(TContext));

            List<Type> _registerSeedTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x => typeof(IRegisterSeed).IsAssignableFrom(x) && !x.ContainsGenericParameters && !x.IsInterface).ToList();

            foreach (Type registerSeedType in _registerSeedTypes)
            {
                var _registerSeedInstance = (IRegisterSeed)Activator.CreateInstance(registerSeedType);

                if (_registerSeedInstance != null)
                {
                    _registerSeedInstance.Execute(_context);

                    _context.Seed.InsertOne(new Seed()
                    {
                        Name = registerSeedType.FullName,
                        DateAdded = DateTime.UtcNow
                    });
                }
            }
        }
    }
}
