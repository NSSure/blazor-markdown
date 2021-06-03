using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Core.DAL.Providers.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Blazor.Markdown.Core.DAL.Mongo
{
    public class MarkdownDBContext : MongoDBContext
    {
        public IMongoCollection<Seed> Seed
        {
            get
            {
                return this.Database.GetCollection<Seed>(typeof(Seed).Name);
            }
        }

        public IMongoCollection<User> User
        {
            get
            {
                return this.Database.GetCollection<User>(typeof(User).Name);
            }
        }

        public IMongoCollection<Action> Action
        {
            get
            {
                return this.Database.GetCollection<Action>(typeof(Action).Name);
            }
        }

        public IMongoCollection<Role> Role
        {
            get
            {
                return this.Database.GetCollection<Role>(typeof(Role).Name);
            }
        }

        public IMongoCollection<Diagram> Diagram
        {
            get
            {
                return this.Database.GetCollection<Diagram>(typeof(Diagram).Name);
            }
        }

        public MarkdownDBContext()
        {

        }
    }
}
