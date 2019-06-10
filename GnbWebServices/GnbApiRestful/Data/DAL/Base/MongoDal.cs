using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using System.Collections.Generic;
using Data.Model;
using System.Linq;
using System;

namespace Data.Base
{
    public class MongoDal
    {
        private const string idKey = "id";
        private readonly IConfiguration configuration;
        private MongoClient client;
        private IMongoDatabase database;
        private string databaseName;
        private string collectionName;

        public MongoDal(string collectionName, IConfiguration configuration, string databaseName = null)
        {
            if (databaseName == null)
            {
                this.databaseName = configuration["DefaultDatabaseName"];
            }
            else
            {
                this.databaseName = databaseName;
            }

            string serverHost = configuration["MongoDbServerHost"];
            this.collectionName = collectionName;
            this.configuration = configuration;
            client = new MongoClient(serverHost);
            database = client.GetDatabase(databaseName);
        }

        public List<T> GetList<T>() where T : IData
        {
            IMongoCollection<T> collection = database.GetCollection<T>(collectionName);
            List<T> result = collection.AsQueryable().ToList();

            return result;
        }

        public IQueryable<T> GetQueryable<T>() where T : IData
        {
            IMongoCollection<T> collection = database.GetCollection<T>(collectionName);
            IQueryable<T> queryable = collection.AsQueryable();

            return queryable;
        }

        public List<T> GetWhere<T>(Func<T, bool> condition) where T : IData
        {
            IMongoCollection<T> collection = database.GetCollection<T>(collectionName);
            List<T> result = collection.AsQueryable().Where(condition).ToList();

            return result;
        }

        public T GetObject<T>(ObjectId id) where T : IData
        {
            IMongoCollection<T> collection = database.GetCollection<T>(collectionName);
            T result = collection.AsQueryable().Where<T>(o => o.Id == id).SingleOrDefault();

            return result;
        }

        public void AddObject<T>(T value) where T : IData
        {
            IMongoCollection<T> collection = database.GetCollection<T>(collectionName);

            collection.InsertOne(value);
        }

        public void AddList<T>(IList<T> list) where T : IData
        {
            IMongoCollection<T> collection = database.GetCollection<T>(collectionName);

            collection.InsertMany(list);
        }

        public void UpadeteObject<T>(T value) where T : IData
        {
            IMongoCollection<T> collection = database.GetCollection<T>(collectionName);
            ObjectId id = ((IData)value).Id;
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(o => o.Id, id);

            collection.ReplaceOne(filter, value);
        }

        public void DeleteOject<T>(ObjectId id) where T : IData
        {
            IMongoCollection<T> collection = database.GetCollection<T>(collectionName);
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(o => o.Id, id);

            collection.DeleteOne(filter);
        }

        public void DeleteAll<T>() where T : IData
        {
            IMongoCollection<T> collection = database.GetCollection<T>(collectionName);
            IQueryable<ObjectId> ids = GetQueryable<T>().Select(o => o.Id);
            var idsFilter = Builders<T>.Filter.In(o => o.Id, ids);

            collection.DeleteMany(idsFilter);
        }
    }
}