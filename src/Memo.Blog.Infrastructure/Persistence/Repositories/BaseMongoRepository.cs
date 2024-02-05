using Memo.Blog.Application.Common.Interfaces.Persistence.Repositories;
using Memo.Blog.Domain.Common;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Memo.Blog.Infrastructure.Persistence.Repositories;

public class BaseMongoRepository<TEntity> : IBaseMongoRepository<TEntity>
    where TEntity : class, new()
{
    private readonly IMongoDatabase _database;
    private readonly IMongoCollection<TEntity> _collection;

    public BaseMongoRepository(IOptions<MongoOptions> options, MongoClient client)
    {
        var mongoOptions = options.Value;
        _database = client.GetDatabase(mongoOptions.Database);
        _collection = GetCollection();
    }

    public async Task<bool> InsertOneAsync(TEntity t)
    {
        await _collection.InsertOneAsync(t);
        return true;
    }

    public async Task<bool> InsertManyAsync(List<TEntity> t)
    {
        await _collection.InsertManyAsync(t);
        return true;
    }

    public async Task<ReplaceOneResult> ReplaceOneAsync(TEntity replacement, FilterDefinition<TEntity> filter)
    {
        return await _collection.ReplaceOneAsync(filter, replacement);
    }

    public async Task<UpdateResult> UpdateOneAsync(UpdateDefinition<TEntity> update, FilterDefinition<TEntity> filter)
    {
        return await _collection.UpdateOneAsync(filter, update);
    }

    public async Task<UpdateResult> UpdateManayAsync(UpdateDefinition<TEntity> update, FilterDefinition<TEntity> filter)
    {
        return await _collection.UpdateManyAsync(filter, update);
    }

    public async Task<DeleteResult> DeleteOneAsync(FilterDefinition<TEntity> filter)
    {
        return await _collection.DeleteOneAsync(filter);
    }

    public async Task<DeleteResult> DeleteManyAsync(FilterDefinition<TEntity> filter)
    {
        return await _collection.DeleteManyAsync(filter);
    }

    public async Task<TEntity> FindOneAsync(long id, bool isObjectId = true, string[]? field = null)
    {
        FilterDefinition<TEntity> filter;
        if (isObjectId)
        {
            filter = Builders<TEntity>.Filter.Eq("_id", new ObjectId(id.ToString()));
        }
        else
        {
            filter = Builders<TEntity>.Filter.Eq("_id", id);
        }

        //不指定查询字段
        if (field == null || field.Length == 0)
        {
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        //制定查询字段
        var fieldList = new List<ProjectionDefinition<TEntity>>();
        for (int i = 0; i < field.Length; i++)
        {
            fieldList.Add(Builders<TEntity>.Projection.Include(field[i].ToString()));
        }
        var projection = Builders<TEntity>.Projection.Combine(fieldList);
        fieldList?.Clear();
        return await _collection.Find(filter).Project<TEntity>(projection).FirstOrDefaultAsync();
    }

    public async Task<List<TEntity>> FindListAsync(FilterDefinition<TEntity> filter, string[]? field = null, SortDefinition<TEntity>? sort = null)
    {
        //不指定查询字段
        if (field == null || field.Length == 0)
        {
            //return await client.Find(new BsonDocument()).ToListAsync();
            if (sort == null) return await _collection.Find(filter).ToListAsync();
            return await _collection.Find(filter).Sort(sort).ToListAsync();
        }

        //制定查询字段
        var fieldList = new List<ProjectionDefinition<TEntity>>();
        for (int i = 0; i < field.Length; i++)
        {
            fieldList.Add(Builders<TEntity>.Projection.Include(field[i].ToString()));
        }
        var projection = Builders<TEntity>.Projection.Combine(fieldList);
        fieldList?.Clear();
        if (sort == null) return await _collection.Find(filter).Project<TEntity>(projection).ToListAsync();
        //排序查询
        return await _collection.Find(filter).Sort(sort).Project<TEntity>(projection).ToListAsync();
    }

    public async Task<List<TEntity>> FindListByPageAsync(FilterDefinition<TEntity> filter, int pageInd, int pageSize, string[]? field = null, SortDefinition<TEntity>? sort = null)
    {
        //不指定查询字段
        if (field == null || field.Length == 0)
        {
            if (sort == null) return await _collection.Find(filter).Skip((pageInd - 1) * pageSize).Limit(pageSize).ToListAsync();
            //进行排序
            return await _collection.Find(filter).Sort(sort).Skip((pageInd - 1) * pageSize).Limit(pageSize).ToListAsync();
        }

        //制定查询字段
        var fieldList = new List<ProjectionDefinition<TEntity>>();
        for (int i = 0; i < field.Length; i++)
        {
            fieldList.Add(Builders<TEntity>.Projection.Include(field[i].ToString()));
        }
        var projection = Builders<TEntity>.Projection.Combine(fieldList);
        fieldList?.Clear();

        //不排序
        if (sort == null) return await _collection.Find(filter).Project<TEntity>(projection).Skip((pageInd - 1) * pageSize).Limit(pageSize).ToListAsync();

        //排序查询
        return await _collection.Find(filter).Sort(sort).Project<TEntity>(projection).Skip((pageInd - 1) * pageSize).Limit(pageSize).ToListAsync();
    }

    public async Task<long> CountAsync(FilterDefinition<TEntity> filter)
    {
        return await _collection.CountDocumentsAsync(filter);
    }

    private IMongoCollection<TEntity> GetCollection()
    {
        var type = typeof(TEntity);
        var collectionName = type.Name;
        var att = type.GetCustomAttributes(typeof(MongoCollectionAttribute), true).FirstOrDefault() as MongoCollectionAttribute;
        if (att is not null && !string.IsNullOrWhiteSpace(att.Name))
        {
            collectionName = att.Name;
        }

        return _database.GetCollection<TEntity>(collectionName) ?? throw new ArgumentNullException($"The MongoDB collection named '{collectionName}' is null "); ;
    }
}
