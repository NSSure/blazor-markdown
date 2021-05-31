using Blazor.Markdown.Core.DAL.Mongo;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blazor.Markdown.Core.DAL.Repository
{
    public class BaseRepository<TDocument>
    {
        public readonly MongoDBContext Context;

        public IMongoCollection<TDocument> Collection
        {
            get
            {
                return this.Context.Database.GetCollection<TDocument>(typeof(TDocument).Name);
            }
        }

        public BaseRepository(MongoDBContext context)
        {
            this.Context = context;
        }

        public async Task AddAsync(TDocument document)
        {
            await this.Collection.InsertOneAsync(document);
        }

        public async Task<List<TDocument>> ListAll()
        {
            return await this.Collection.Find(new BsonDocument()).ToListAsync();
        }
    }
}
