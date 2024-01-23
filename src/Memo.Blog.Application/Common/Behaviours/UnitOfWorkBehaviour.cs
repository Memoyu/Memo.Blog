using System.Reflection;
using MediatR;
using Memo.Blog.Application.Common.UnitOfWorks;
using FreeSql;
using Microsoft.Extensions.Logging;

namespace Memo.Blog.Application.Common.Behaviours;

public class UnitOfWorkBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly UnitOfWorkManager _unitOfWorkManager;

    public UnitOfWorkBehaviour(ILogger<UnitOfWorkBehaviour<TRequest, TResponse>> logger, UnitOfWorkManager unitOfWorkManager)
    {
        _logger = logger;
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var transactionAttribute = request.GetType().GetCustomAttributes<TransactionalAttribute>(true)?.FirstOrDefault();

        TResponse result = default!;
        if (transactionAttribute is not null)
        {
            var unitOfWork = _unitOfWorkManager.Begin(transactionAttribute.Propagation, transactionAttribute.IsolationLevel);

            int hashCode = unitOfWork.GetHashCode();
            _logger.LogInformation("Request: 事务请求开始 请求：{Name}；Hash：{Hash}", requestName, hashCode);

            try
            {
                result = await next();//获取执行结果
                unitOfWork.Commit();
                _logger.LogInformation("Request: 事务请求提交 请求：{Name}；Hash：{Hash}", requestName, hashCode);
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
                _logger.LogError(ex, "Request: 事务请求异常 请求：{Name}；参数：{@Request}", requestName, request);
                throw;
            }
            finally
            {
                unitOfWork.Dispose();
            }
        }
        else
        {
            result = await next();
        }

        return result;
    }
}
