using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Core.DAL.Mongo.Map;
using MongoDB.Driver;

namespace Blazor.Markdown.Core.DAL.Mongo
{
    public class MongoDBContext
    {
        private MongoClient _client { get; set; }

        /// <summary>
        /// Typically you only create one MongoClient instance for a given cluster and use it across your application. Creating multiple MongoClients will, however, still share the same pool of connections if and only if the connection strings are identical.
        /// 
        /// Read more here
        /// https://mongodb.github.io/mongo-csharp-driver/2.12/getting_started/quick_tour/
        /// </summary>
        public MongoClient Client
        {
            get
            {
                if (this._client == null)
                {
                    // Ensure the connection to the client is made after any mapping of classes.
                    this._client = new MongoClient("mongodb://localhost:27017");
                }

                return this._client;
            }
        }

        public IMongoDatabase Database
        {
            get
            {
                return this.Client.GetDatabase("Markdown");
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

        public MongoDBContext()
        {

        }
    }
}
