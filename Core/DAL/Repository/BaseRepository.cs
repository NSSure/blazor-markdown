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
        /// <summary>
        /// The current DB context.
        /// </summary>
        public readonly MarkdownDBContext Context;

        /// <summary>
        /// The collection for the given document of type TDocument.
        /// </summary>
        public IMongoCollection<TDocument> Collection
        {
            get
            {
                return this.Context.Database.GetCollection<TDocument>(typeof(TDocument).Name);
            }
        }

        /// <summary>
        /// Default context constructor.
        /// </summary>
        /// <param name="context"></param>
        public BaseRepository(MarkdownDBContext context)
        {
            this.Context = context;
        }

        /// <summary>
        /// Adds the given document instance into the collection.
        /// </summary>
        /// <param name="document">The document instance to add into the collection.</param>
        /// <returns></returns>
        public async Task AddAsync(TDocument document, InsertOneOptions insertOneOptions = null)
        {
            await this.Collection.InsertOneAsync(document, insertOneOptions);
        }

        /// <summary>
        /// Adds the given document instance into the collection.
        /// </summary>
        /// <param name="document">The document instance to add into the collection.</param>
        /// <returns></returns>
        public async Task AddManyAsync(List<TDocument> documents, InsertManyOptions insertManyOptions = null)
        {
            await this.Collection.InsertManyAsync(documents, insertManyOptions);
        }

        /// <summary>
        /// Update the collection document using a LINQ expression along with an update definition.
        /// </summary>
        /// <param name="expression">The expression to query for a corresponding record by.</param>
        /// <param name="updateDefinition">The update definition to perform.</param>
        /// <param name="updateOptions">Optional update options.</param>
        /// <returns></returns>
        public async Task UpdateExpressionAsync(Expression<Func<TDocument, bool>> expression, UpdateDefinition<TDocument> updateDefinition, UpdateOptions updateOptions = null)
        {
            await this.Collection.UpdateOneAsync<TDocument>(expression, updateDefinition, updateOptions);
        }

        /// <summary>
        /// Updates the collection document using a filter definition along with an update definition
        /// </summary>
        /// <param name="filter">The filter definition to query for a corresponding record by.</param>
        /// <param name="updateDefinition">The update definition to perform.</param>
        /// <param name="updateOptions">Optional update options.</param>
        /// <returns></returns>
        public async Task UpdateFilterAsync(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition, UpdateOptions updateOptions = null)
        {

            await this.Collection.UpdateOneAsync(filter, updateDefinition, updateOptions);
        }

        /// <summary>
        /// Updates many collection documents using a LINQ expression along with an update definition.
        /// </summary>
        /// <param name="expression">The expression to query for a corresponding record by.</param>
        /// <param name="updateDefinition">The update definition to perform.</param>
        /// <param name="updateOptions">Optional update options.</param>
        /// <returns></returns>
        public async Task UpdateManyExpressionAsync(Expression<Func<TDocument, bool>> expression, UpdateDefinition<TDocument> updateDefinition, UpdateOptions updateOptions = null)
        {
            await this.Collection.UpdateManyAsync<TDocument>(expression, updateDefinition, updateOptions);
        }

        /// <summary>
        /// Updates many collection documents using a filter definition along with an update definition
        /// </summary>
        /// <param name="filter">The filter definition to query for a corresponding record by.</param>
        /// <param name="updateDefinition">The update definition to perform.</param>
        /// <param name="updateOptions">Optional update options.</param>
        /// <returns></returns>
        public async Task UpdateManyFilterAsync(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition, UpdateOptions updateOptions = null)
        {
            await this.Collection.UpdateManyAsync(filter, updateDefinition, updateOptions);
        }

        /// <summary>
        /// Deletes a specific document from the collection using a filter.
        /// </summary>
        /// <param name="isAtomic">Should delete run independently of other processes.</param>
        /// <returns></returns>
        public async Task DeleteFilterAsync(FilterDefinition<TDocument> filter, bool isAtomic = false)
        {
            // TODO: Create separate delete functions for instead of this if statement?
            if (isAtomic)
            {
                await this.Collection.DeleteOneAsync(filter);
            }
            else
            {
                await this.Collection.FindOneAndDeleteAsync(filter);
            }
        }

        /// <summary>
        /// Deletes a specific document from the collection using an expression.
        /// </summary>
        /// <param name="isAtomic">Should delete run independently of other processes.</param>
        /// <returns></returns>
        public async Task DeleteExpressionAsync(Expression<Func<TDocument, bool>> expression, bool isAtomic = false)
        {
            // TODO: Create separate delete functions for instead of this if statement?
            if (isAtomic)
            {
                await this.Collection.DeleteOneAsync(expression);
            }
            else
            {
                await this.Collection.FindOneAndDeleteAsync(expression);
            }
        }

        /// <summary>
        /// Query documents within the collection by the given expression.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        public async Task<List<TDocument>> Where(Expression<Func<TDocument, bool>> expression, FindOptions findOptions = null)
        {
            return await this.Collection.Find(expression, findOptions).ToListAsync();
        }

        /// <summary>
        /// Query documents within the collection by the given expression.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        public async Task<List<TDocument>> Where(FilterDefinition<TDocument> filter, FindOptions findOptions = null)
        {
            return await this.Collection.Find(filter, findOptions).ToListAsync();
        }

        /// <summary>
        /// List all of the documents with the collection.
        /// </summary>
        /// <returns>All of the documents within the collection.</returns>
        public async Task<List<TDocument>> ListAll(FindOptions findOptions = null)
        {
            return await this.Collection.Find(new BsonDocument(), options: findOptions).ToListAsync();
        }

        /// <summary>
        /// Lists all of the documents within the collection and applies a projection using a projection definition.
        /// </summary>
        /// 
        /// <typeparam name="TProjection">The type of the projection return.</typeparam>
        /// 
        /// <param name="projectionDefinition">The project definition that contains the field to include/exclude in the final project.</param>
        /// <param name="findOptions">Options for the find statement.</param>
        /// <returns></returns>
        public async Task<List<TProjection>> ListAll<TProjection>(ProjectionDefinition<TDocument, TProjection> projectionDefinition, FindOptions findOptions = null)
        {
            return await this.Collection.Find(new BsonDocument(), options: findOptions).Project(projectionDefinition).ToListAsync();
        }

        /// <summary>
        /// Lists all of the documents within the collection and applies a projection using a project expression.
        /// </summary>
        /// 
        /// <typeparam name="TProjection">The type of the projection return.</typeparam>
        /// 
        /// <param name="projectionExpression">The project expression that contains the field to include/exclude in the final project.</param>
        /// <param name="findOptions">Options for the find statement.</param>
        /// <returns></returns>
        public async Task<List<TProjection>> ListAll<TProjection>(Expression<Func<TDocument, TProjection>> projectionExpression, FindOptions findOptions = null)
        {
            return await this.Collection.Find(new BsonDocument()).Project(projectionExpression).ToListAsync();
        }

        /// <summary>
        /// Adds an item to a field array using a filter definition and field definition.
        /// </summary>
        /// 
        /// <typeparam name="TItem">The type of the items within the field array.</typeparam>
        /// 
        /// <param name="filter">The filter to find a specific document.</param>
        /// <param name="targetField">The field array to target.</param>
        /// <param name="value">The value to delete from the field array.</param>
        /// <returns></returns>
        public async Task AddArrayItem<TItem>(FilterDefinition<TDocument> filter, FieldDefinition<TDocument> targetField, TItem value)
        {
            UpdateDefinition<TDocument> _updateDefinition = Builders<TDocument>.Update.Push(targetField, value);
            await this.UpdateFilterAsync(filter, _updateDefinition);
        }

        /// <summary>
        /// Adds an item to a field array using a filter expression and field expression.
        /// </summary>
        /// 
        /// <typeparam name="TItem">The type of the items within the field array.</typeparam>
        /// 
        /// <param name="expression">The expression to find a specific document.</param>
        /// <param name="targetField">The field array to target.</param>
        /// <param name="value">The value to delete from the field array.</param>
        /// <returns></returns>
        public async Task AddArrayItem<TItem>(Expression<Func<TDocument, bool>> expression, Expression<Func<TDocument, IEnumerable<TItem>>> targetField, TItem value)
        {
            UpdateDefinition<TDocument> _updateDefinition = Builders<TDocument>.Update.Push(targetField, value);
            await this.UpdateExpressionAsync(expression, _updateDefinition);
        }

        /// <summary>
        /// Adds an item to a field array using a filter expression and field definition.
        /// </summary>
        /// 
        /// <typeparam name="TItem">The type of the items within the field array.</typeparam>
        /// 
        /// <param name="filter">The filter to find a specific document.</param>
        /// <param name="targetField">The field array to target.</param>
        /// <param name="value">The value to delete from the field array.</param>
        /// <returns></returns>
        public async Task AddArrayItem<TItem>(Expression<Func<TDocument, bool>> filter, FieldDefinition<TDocument> targetField, TItem value)
        {
            UpdateDefinition<TDocument> _updateDefinition = Builders<TDocument>.Update.Push(targetField, value);
            await this.UpdateExpressionAsync(filter, _updateDefinition);
        }

        /// <summary>
        /// Adds an item to a field array a filter definition and field expression.
        /// </summary>
        /// 
        /// <typeparam name="TItem">The type of the items within the field array.</typeparam>
        /// 
        /// <param name="filter">The filter to find a specific document.</param>
        /// <param name="targetField">The field array to target.</param>
        /// <param name="value">The value to delete from the field array.</param>
        /// <returns></returns>
        public async Task AddArrayItem<TItem>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, IEnumerable<TItem>>> targetField, TItem value)
        {
            UpdateDefinition<TDocument> _updateDefinition = Builders<TDocument>.Update.Push(targetField, value);
            await this.UpdateFilterAsync(filter, _updateDefinition);
        }

        /// <summary>
        /// Updates an item in a field array using a filter definition and field definition.
        /// </summary>
        /// 
        /// <typeparam name="TField">The type of the items within the field array.</typeparam>
        /// 
        /// <param name="filter">The filter to find a specific document.</param>
        /// <param name="targetField">The field array to target.</param>
        /// <param name="value">The value to delete from the field array.</param>
        /// <returns></returns>
        public async Task UpdateArrayItem<TField>(FilterDefinition<TDocument> filter, FieldDefinition<TDocument, TField> targetField, TField value)
        {
            UpdateDefinition<TDocument> _updateDefinition = Builders<TDocument>.Update.Set(targetField, value);
            await this.UpdateFilterAsync(filter, _updateDefinition);
        }

        /// <summary>
        /// Update an item in a field array using a filter expression and field expression.
        /// </summary>
        /// 
        /// <typeparam name="TField">The type of the items within the field array.</typeparam>
        /// 
        /// <param name="filter">The filter to find a specific document.</param>
        /// <param name="targetField">The field array to target.</param>
        /// <param name="value">The value to delete from the field array.</param>
        /// <returns></returns>
        public async Task UpdateArrayItem<TField>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> targetField, TField value)
        {
            UpdateDefinition<TDocument> _updateDefinition = Builders<TDocument>.Update.Set(targetField, value);
            await this.UpdateExpressionAsync(filter, _updateDefinition);
        }

        /// <summary>
        /// Update an item in a field array using a filter expression and field definition.
        /// </summary>
        /// 
        /// <typeparam name="TField">The type of the items within the field array.</typeparam>
        /// 
        /// <param name="filter">The filter to find a specific document.</param>
        /// <param name="targetField">The field array to target.</param>
        /// <param name="value">The value to delete from the field array.</param>
        /// <returns></returns>
        public async Task UpdateArrayItem<TField>(Expression<Func<TDocument, bool>> filter, FieldDefinition<TDocument, TField> targetField, TField value)
        {
            UpdateDefinition<TDocument> _updateDefinition = Builders<TDocument>.Update.Set(targetField, value);
            await this.UpdateExpressionAsync(filter, _updateDefinition);
        }

        /// <summary>
        /// Updates an item in a field array a filter definition and field expression.
        /// </summary>
        /// 
        /// <typeparam name="TItem">The type of the items within the field array.</typeparam>
        /// 
        /// <param name="filter">The filter to find a specific document.</param>
        /// <param name="targetField">The field array to target.</param>
        /// <param name="value">The value to delete from the field array.</param>
        /// <returns></returns>
        public async Task UpdateArrayItem<TItem>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, IEnumerable<TItem>>> targetField, TItem value)
        {
            UpdateDefinition<TDocument> _updateDefinition = Builders<TDocument>.Update.Push(targetField, value);
            await this.UpdateFilterAsync(filter, _updateDefinition);
        }

        /// <summary>
        /// Deletes an item from a field array.
        /// </summary>
        /// 
        /// <typeparam name="TItem"></typeparam>
        /// 
        /// <param name="filter">The filter to find a specific document.</param>
        /// <param name="targetField">The field array to target.</param>
        /// <param name="value">The value to delete from the field array.</param>
        /// <returns></returns>
        public async Task DeleteArrayItem<TItem>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, IEnumerable<TItem>>> targetField, TItem value)
        {
            UpdateDefinition<TDocument> _updateDefinition = Builders<TDocument>.Update.Pull(targetField, value);
            await this.Collection.UpdateOneAsync(filter, _updateDefinition);
        }
    }
}
