using Blazor.Markdown.Core.DAL.Mongo;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Blazor.Markdown.Core.DAL.Providers.Mongo
{
    public interface IRegisterIndexes
    {
        public Task Execute();
    }

    public class IndexConfig<TDocument>
    {
        public IndexKeysDefinition<TDocument> Definition { get; set; }
        public IndexType Type { get; set; }
    }

    public enum IndexType
    {
        Ascending = 0,
        Combine = 1,
        Descending = 2,
        Geo2D = 3,
        Geo2DSphere = 4,
        GeoHaystack = 5,
        Hashed = 6,
        Text = 7,
        Wildcard = 8
    }

    public class IndexBuilder<TDocument>
    {
        private readonly IndexKeysDefinitionBuilder<TDocument> _Builder;

        public Dictionary<string, List<IndexConfig<TDocument>>> PendingIndexes = new Dictionary<string, List<IndexConfig<TDocument>>>();

        public IndexBuilder(IndexKeysDefinitionBuilder<TDocument> builder)
        {
            this._Builder = builder;
        }

        public PropertyInfo GetPropertyInfo<TSource, TProperty>(Expression<Func<TSource, TProperty>> propertyLambda)
        {
            Type type = typeof(TSource);

            MemberExpression expression = null;

            if (propertyLambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                expression = propertyLambda.Body as MemberExpression;
            }
            else
            {
                expression = ((UnaryExpression)propertyLambda.Body).Operand as MemberExpression;
            }

            if (expression == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    propertyLambda.ToString()));

            PropertyInfo propInfo = expression.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a property.",
                    propertyLambda.ToString()));

            if (type != propInfo.ReflectedType &&
                !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a property that is not from type {1}.",
                    propertyLambda.ToString(),
                    type));

            return propInfo;
        }

        public void Ascending(Expression<Func<TDocument, object>> indexExpression)
        {
            string _propertyName = GetPropertyInfo(indexExpression).Name;

            if (!this.PendingIndexes.ContainsKey(_propertyName))
            {
                this.PendingIndexes.Add(_propertyName, new List<IndexConfig<TDocument>>());
            }

            if (!this.PendingIndexes[_propertyName].Exists(x => x.Type == IndexType.Ascending))
            {
                this.PendingIndexes[_propertyName].Add(new IndexConfig<TDocument>()
                {
                    Type = IndexType.Ascending,
                    Definition = this._Builder.Ascending(indexExpression)
                });
            }
        }

        public void Descending(Expression<Func<TDocument, object>> indexExpression)
        {
            string _propertyName = GetPropertyInfo(indexExpression).Name;

            if (!this.PendingIndexes.ContainsKey(_propertyName))
            {
                this.PendingIndexes.Add(_propertyName, new List<IndexConfig<TDocument>>());
            }

            if (!this.PendingIndexes[_propertyName].Exists(x => x.Type == IndexType.Descending))
            {
                this.PendingIndexes[_propertyName].Add(new IndexConfig<TDocument>()
                {
                    Type = IndexType.Ascending,
                    Definition = this._Builder.Ascending(indexExpression)
                });
            }
        }

        public List<CreateIndexModel<TDocument>> GetIndexModels()
        {
            return this.PendingIndexes.SelectMany(a => a.Value.Select(x => x.Definition)).Select(a => new CreateIndexModel<TDocument>(a)).ToList();
        }
    }

    public class Index
    {
        public int v { get; set; }
        public Dictionary<string, int> key { get; set; }
        public string name { get; set; }
    }

    public abstract class RegisterIndexes<TDocument> : IRegisterIndexes
    {
        public List<Index> ExistingIndexes = new List<Index>();

        public RegisterIndexes()
        {

        }

        public async Task Execute()
        {
            MongoDBContext _context = new MongoDBContext();

            var _collection = _context.Database.GetCollection<TDocument>(typeof(TDocument).Name);

            if (_collection != null)
            {
                await _collection.Indexes.List().ForEachAsync(registeredIndex =>
                {
                    this.ExistingIndexes.Add((Index)MongoDB.Bson.Serialization.BsonSerializer.Deserialize(registeredIndex, typeof(Index)));
                });
            }

            IndexBuilder<TDocument> _builder = new IndexBuilder<TDocument>(Builders<TDocument>.IndexKeys);

            this.Configure(_builder);

            List<IndexKeysDefinition<TDocument>> _finalizedKeys = new List<IndexKeysDefinition<TDocument>>();

            foreach (KeyValuePair<string, List<IndexConfig<TDocument>>> property in _builder.PendingIndexes)
            {
                foreach (IndexConfig<TDocument> indexConfig in property.Value)
                {
                    // TODO: Figure out how to query by the key in the BsonDocument.
                    // {{ "v" : 2, "key" : { "DateAdded" : -1 }, "name" : "DateAdded_-1" }}
                    string _name = property.Key;

                    if (indexConfig.Type == IndexType.Ascending)
                    {
                        _name = $"{_name}_1";
                    }
                    else
                    {
                        _name = $"{_name}_-1";
                    }

                    Index _existingIndex = this.ExistingIndexes.FirstOrDefault(a => a.name == _name);

                    if (_existingIndex == null)
                    {
                        _finalizedKeys.Add(indexConfig.Definition);
                    }
                    else
                    {
                        this.ExistingIndexes.Remove(_existingIndex);
                    }
                }
            }

            await _collection.Indexes.CreateManyAsync(_builder.GetIndexModels());

            foreach (Index droppedIndex in this.ExistingIndexes)
            {
                // Can't drop this index.
                if (droppedIndex.name == "_id_")
                {
                    continue;
                }

                await _collection.Indexes.DropOneAsync(droppedIndex.name);
            }
        }

        public abstract void Configure(IndexBuilder<TDocument> builder);
    }
}
