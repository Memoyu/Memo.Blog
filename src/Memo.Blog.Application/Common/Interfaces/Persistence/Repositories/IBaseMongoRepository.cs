using MongoDB.Driver;

namespace Memo.Blog.Application.Common.Interfaces.Persistence.Repositories;

public interface IBaseMongoRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// 异步添加一条数据
    /// </summary>
    /// <param name="entity">添加的实体</param>
    /// <returns></returns>
    Task<bool> InsertOneAsync(TEntity entity);

    /// <summary>
    /// 异步批量插入
    /// </summary>
    /// <param name="entity">实体集合</param>
    /// <returns></returns>
    Task<bool> InsertManyAsync(List<TEntity> entity);

    /// <summary>
    /// 异步替换修改数据
    /// </summary>
    /// <param name="replacement">要替换的实体</param>
    /// <param name="filter">替换条件</param>
    /// <returns></returns>
    Task<ReplaceOneResult> ReplaceOneAsync(TEntity replacement, FilterDefinition<TEntity> filter);

    /// <summary>
    /// 异步批量修改数据
    /// </summary>
    /// <param name="update">要修改的字段</param>
    /// <param name="filter">修改条件</param>
    /// <returns></returns>
    Task<UpdateResult> UpdateOneAsync(UpdateDefinition<TEntity> update, FilterDefinition<TEntity> filter);

    /// <summary>
    /// 异步批量修改数据
    /// </summary>
    /// <param name="update">要修改的字段</param>
    /// <param name="filter">修改条件</param>
    /// <returns></returns>
    Task<UpdateResult> UpdateManayAsync(UpdateDefinition<TEntity> update, FilterDefinition<TEntity> filter);

    /// <summary>
    /// 异步删除多条数据
    /// </summary>
    /// <param name="filter">删除的条件</param>
    /// <returns></returns>
    Task<DeleteResult> DeleteOneAsync(FilterDefinition<TEntity> filter);

    /// <summary>
    /// 异步删除多条数据
    /// </summary>
    /// <param name="filter">删除的条件</param>
    /// <returns></returns>
    Task<DeleteResult> DeleteManyAsync(FilterDefinition<TEntity> filter);

    /// <summary>
    /// 异步根据id查询一条数据
    /// </summary>
    /// <param name="id">objectid</param>
    /// <returns></returns>
    Task<TEntity> FindOneAsync(long id, bool isObjectId = true, string[]? field = null);

    /// <summary>
    /// 异步查询集合
    /// </summary>
    /// <param name="filter">查询条件</param>
    /// <param name="field">要查询的字段,不写时查询全部</param>
    /// <param name="sort">要排序的字段</param>
    /// <returns></returns>
    Task<List<TEntity>> FindListAsync(FilterDefinition<TEntity> filter, string[]? field = null, SortDefinition<TEntity>? sort = null);

    /// <summary>
    /// 异步分页查询集合
    /// </summary>
    /// <param name="filter">查询条件</param>
    /// <param name="pageIndex">当前页</param>
    /// <param name="pageSize">页容量</param>
    /// <param name="field">要查询的字段,不写时查询全部</param>
    /// <param name="sort">要排序的字段</param>
    /// <returns></returns>
    Task<List<TEntity>> FindListByPageAsync(FilterDefinition<TEntity> filter, int pageIndex, int pageSize, string[]? field = null, SortDefinition<TEntity>? sort = null);

    /// <summary>
    /// 异步根据条件获取总数
    /// </summary>
    /// <param name="filter">条件</param>
    /// <returns></returns>
    Task<long> CountAsync(FilterDefinition<TEntity> filter);
}
