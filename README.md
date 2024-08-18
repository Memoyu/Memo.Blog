<div align="center"  style="margin-bottom: 40px">
 <img src="https://raw.githubusercontent.com/Memoyu/Memo.Blog/main/docs/images/logo.png" alt="memo blog" />
</div>
<div align="center">
 <h3>基于.NET8开发的Blog系统接口服务</h3>
 <a href="https://dotnet.microsoft.com/zh-cn/download"><img src="https://img.shields.io/badge/.net8.0.0-3963bc.svg"/></a>
 <a href="LICENSE"><img src="https://img.shields.io/badge/license-MIT-3963bc.svg"/></a>
 <a href="https://github.com/Memoyu"><img src="https://img.shields.io/badge/developer-memoyu-blue"/></a>
</div>




## 简介
本项目为自用Blog系统的后端接口服务，基于.NET 8实现，遵循[CleanArchitecture](https://github.com/amantinband/clean-architecture)设计理念(Ctrl C + V)

| 管理端 | 客户端 |
| :------------------------------: | :-------------------------------: |
| ![预览](https://raw.githubusercontent.com/Memoyu/Memo.Blog.Admin/main/docs/images/admin-ld.png) | ![预览](https://raw.githubusercontent.com/Memoyu/Memo.Blog.Client/main/docs/images/client-ld.png) |

**线上地址：**

管理端：<a href="http://blog.admin.memoyu.com/" target="_blank">管理端（使用游客登录即可）</a>

移动管理端：Coming soon

客户端：<a href="http://blog.memoyu.com/" target="_blank">管理端</a>

> Tips：小霸王，稍微等一下，轻点！



**相关项目：**

管理端：[Memo.Blog.Admin: 管理端 ](https://github.com/Memoyu/Memo.Blog.Admin)

移动管理端：[Memo.Blog.App: 移动管理端](https://github.com/Memoyu/Memo.Blog.App)

客户端：[Memo.Blog.Client: 客户端](https://github.com/Memoyu/Memo.Blog.Client)



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
├─Memo.Blog.Application -- 应用服务模块
│  ├─Articles -- 服务名称（具体服务实现，例如：文章管理、权限管理等）
│  │  ├─Commands -- 增删改命令操作（对数据造成变更的处理）
│  │  ├─Common -- 当前服务公有实体，例如：响应、请求
│  │  ├─Events -- 事件触发EventHandler
│  │  └─Queries -- 查询操作（对数据进行读取）
│  │      ├─Anlyanis -- 统计分析数据
│  │      ├─Get -- 获取详情
│  │      ├─Page -- 获取分页
│  │      └─Ranking -- 获取排名
│  └─Common -- 服务公有模块
│     ├─Behaviours -- MediatR管道切面行为（AOP）
│     ├─Exceptions -- 自定义异常
│     ├─Extensions -- 扩展方法
│     ├─Interfaces -- 接口定义
│     │  ├─Persistence -- 数据相关定义
│     │  │  └─Repositories -- 仓储定义（基础仓储定义）等
│     │  ├─Region -- ip2region进一步封装接口定义
│     │  ├─Security -- 安全相关接口定义
│     │  └─Services -- 服务接口定义
│     ├─Mappings -- Mapster实体映射IRegister
│     ├─Models -- 公共模型定义，例如：配置、响应、请求等基础实体
│     ├─Request -- 进程内通信基础定义
│     ├─Security -- 安全相关的实体定义
│     ├─Services -- 服务实现，例如后台任务服务
│     └─Utils -- 工具类
├─Memo.Blog.Domain -- 服务领域模块
│  ├─Common -- 公共实体定义
│  ├─Constants -- 常量定义
│  ├─Entities -- 数据库实体定义
│  │  └─Mongo -- MongoDB实体定义
│  ├─Enums -- 枚举定义
│  ├─Events -- 事件定义
│  └─ValueObjects -- 值对象定义
├─Memo.Blog.Infrastructure -- 基础设施模块
│  ├─Persistence -- 数据持久化实现
│  │  └─Repositories -- 数据库仓储实现
│  ├─Region -- ip2region进一步封装实现
│  └─Security -- 安全相关实现，例如JWT、当前用户、权限验证
└─Memo.Blog.Api -- 外放接口模块
    ├─Controllers -- 客户端接口定义
    │  └─Admin -- 管理端接口定于
    └─wwwroot -- 静态文件
        └─Assets -- 资源文件
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
|实时通信|[SignalR/SignalR](https://github.com/SignalR/SignalR)|
|整体设计参考| [CleanArchitecture](https://github.com/amantinband/clean-architecture) |
|容器| [Docker](https://www.docker.com/) |
|DevOps|[Azure](https://dev.azure.com/)|



## 参考项目
- [amantinband/clean-architecture](https://github.com/amantinband/clean-architecture)
- [luoyunchong/lin-cms-dotnetcore](https://github.com/luoyunchong/lin-cms-dotnetcore)

**在此，感谢各位大佬的开源**



## 感谢授权
🙏🏻 感谢JetBrains对本项目授予的开发工具开源许可 [JetBrains Open Source  Support](https://www.jetbrains.com/community/opensource/#support)



## 开源协议
MIT License. See [License here](./LICENSE) for details.