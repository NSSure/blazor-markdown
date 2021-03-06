using Blazor.Markdown.Core.DAL.Mongo;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.Markdown.Core.DAL.Providers.Mongo
{
    public class MongoDBContextBuilder<TContext> where TContext : MongoDBContext
    {
        public MongoDBOptions Options { get; set; }

        public TContext Context { get; set; }

        public MongoDBContextBuilder()
        {

        }

        public MongoDBContextBuilder(MongoDBOptions options)
        {
            this.Options = options;
        }

        public void Run()
        {
            this.Context = Activator.CreateInstance<TContext>();

            IMongoDatabase _existingDatabase = this.Context.Client.GetDatabase(typeof(MarkdownDBContext).Name);

            if (_existingDatabase != null)
            {
                if (this.Options.DropDatabaseOnLoad)
                {
                    this.Context.Client.DropDatabase(typeof(MarkdownDBContext).Name);
                }
            }

            // Indexes are idempotent so should this just run every time the application starts?
            this.ExecuteMapRegistrations();
            this.ExecuteIndexRegistrations();
            this.ExecuteSeedRegistrations();
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
                    _registerIndexesInstance.Execute(this.Context);
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
            MarkdownDBContext _context = (MarkdownDBContext)Activator.CreateInstance(typeof(TContext));

            List<Type> _registerSeedTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x => typeof(IRegisterSeed).IsAssignableFrom(x) && !x.ContainsGenericParameters && !x.IsInterface).ToList();

            foreach (Type registerSeedType in _registerSeedTypes)
            {
                var _registerSeedInstance = (IRegisterSeed)Activator.CreateInstance(registerSeedType);

                if (_registerSeedInstance != null)
                {
                    // TODO: Grab full seeding table instead of query individually.
                    if (_context.Seed.CountDocuments(x => x.Name == registerSeedType.FullName) == 0)
                    {
                        _registerSeedInstance.Execute(_context);

                        _context.Seed.InsertOne(new Seed()
                        {
                            Name = registerSeedType.FullName,
                            DateAdded = DateTime.UtcNow
                        });
                    }
                    else
                    {
                        // Seed already executed continue to next iteration.
                        continue;
                    }
                }
            }
        }
    }
}
