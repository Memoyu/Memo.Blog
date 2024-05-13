<h1 align="center">
  <img src="https://raw.githubusercontent.com/Memoyu/Memoyu/main/logo.png" alt="memoyu" width="128" />
  <br>
  Memoyu's Blog
  <br>
</h1>
<div align="center">
 <h3>基于.NET8开发的Blog系统接口服务</h3>
 <a href="https://dotnet.microsoft.com/zh-cn/download"><img src="https://img.shields.io/badge/.net8.0.0-3963bc.svg"/></a>
 <a href="LICENSE"><img src="https://img.shields.io/badge/license-MIT-3963bc.svg"/></a>
  <a href="https://github.com/Memoyu"><img src="https://img.shields.io/badge/developer-memoyu-blue"/></a>
</div>

## 简介
本项目为自用Blog系统的后端接口服务，基于.NET 8实现，遵循[CleanArchitecture](https://github.com/amantinband/clean-architecture)设计理念(Ctrl C + V)

| 管理端                           | 客户端                            |
| -------------------------------- | --------------------------------- |
| [![预览](https://raw.githubusercontent.com/Memoyu/Memo.Blog.Admin/main/docs/images/article-list.png)](http://blog.admin.memoyu.com/) | [![预览](https://raw.githubusercontent.com/Memoyu/Memo.Blog.Admin/main/docs/images/article-list.png)](http://blog.memoyu.com/) |
> Tips：小霸王，稍微等一下，轻点！

## 功能实现

- Blog系统数据统计；
- 文章、分类、标签、评论管理；
- 开源项目同步/管理；
- 系统、访问日志写入/查询；
- 动态、友链、关于个人等管理；
- 用户、访客管理；
- 角色、权限管理；

## 分层结构
```powershell
src
├─Memo.Blog.Application -- 应用服务
│  ├─Articles -- 服务名称
│  │  ├─Commands -- 增删改命令（对数据造成变更的处理）
│  │  ├─Common 
│  │  ├─Events
│  │  └─Queries
│  │      ├─Anlyanis
│  │      ├─Get
│  │      ├─Page
│  │      └─Ranking
│  └─Common
│     ├─Behaviours
│     ├─Exceptions
│     ├─Extensions
│     ├─Interfaces
│     │  ├─Persistence
│     │  │  └─Repositories
│     │  ├─Region
│     │  ├─Security
│     │  └─Services
│     │      └─GitHubs
│     ├─Mappings
│     ├─Models
│     │  ├─GitHub
│     │  └─Settings
│     ├─Request
│     ├─Security
│     ├─Services
│     │  ├─Background
│     │  └─GitHubs
│     └─Utils
├─Memo.Blog.Domain
│  ├─Common
│  ├─Constants
│  ├─Entities
│  │  └─Mongo
│  ├─Enums
│  ├─Events
│  └─ValueObjects
├─Memo.Blog.Infrastructure
│  ├─Logger
│  ├─Persistence
│  │  └─Repositories
│  ├─Region
│  ├─Security
│  └─Services
└─Memo.Blog.WebApi
    ├─Controllers
    │  └─Admin
    └─wwwroot
        └─Assets
```





## 相关技术
|                模块                 |                           开源地址                           |
| :---------------------------------: | :----------------------------------------------------------: |
|API文档|    [RicoSuter/NSwag](https://github.com/RicoSuter/NSwag)     |
|数据库| [dotnetcore/FreeSql](https://github.com/dotnetcore/FreeSql) + [MySQL](https://www.mysql.com/cn/) + [MongoDB](https://www.mongodb.com/) + [MongoDB C# Driver](https://www.mongodb.com/docs/drivers/csharp/current/) |
|对象存储|[七牛云](https://www.qiniu.com/) + [qiniu/csharp-sdk](https://github.com/qiniu/csharp-sdk)|
|缓存| [Redis](https://redis.io/) + [dotnetcore/EasyCaching](https://github.com/dotnetcore/EasyCaching) |
| 进程内通信 |[jbogard/MediatR](https://github.com/jbogard/MediatR)|
| 身份认证 |[Authentication(内置)](https://learn.microsoft.com/zh-cn/aspnet/core/security/authentication/?view=aspnetcore-8.0) + [jwt](https://jwt.io/)|
|参数验证|[FluentValidation/FluentValidation](https://github.com/FluentValidation/FluentValidation)     |
|日志|[serilog/serilog](https://github.com/serilog/serilog)     |
|限流| [stefanprodan/AspNetCoreRateLimit](https://github.com/stefanprodan/AspNetCoreRateLimit) |
|IP解析|[lionsoul2014/ip2region](https://github.com/lionsoul2014/ip2region/)|
|雪花ID|[yitter/idgenerator](https://github.com/yitter/idgenerator)|
|对象映射| [MapsterMapper/Mapster](https://github.com/MapsterMapper/Mapster) |
| Json序列化/反序列化 | [System.Text.Json(内置)](https://learn.microsoft.com/zh-cn/dotnet/api/system.text.json) |
|后台任务调度| [BackgroundService(内置)](https://learn.microsoft.com/zh-cn/dotnet/architecture/microservices/multi-container-microservice-net-applications/background-tasks-with-ihostedservice) |
|整体设计参考| [CleanArchitecture](https://github.com/amantinband/clean-architecture) |
|容器| [Docker](https://www.docker.com/) |
|DevOps|[Azure](https://dev.azure.com/)|

## 开源协议

MIT License. See [License here](./LICENSE) for details.