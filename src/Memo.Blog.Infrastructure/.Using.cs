global using System.Linq;
global using System.Security.Claims;
global using System.Security.Cryptography;
global using System.Text;

global using Microsoft.Extensions.Configuration;

global using Memo.Blog.Domain.Entities;
global using Memo.Blog.Application.Common.Interfaces.Security;
global using Memo.Blog.Application.Common.Models;
global using Memo.Blog.Application.Common.Request;
global using Memo.Blog.Infrastructure.Security.CurrentUserProvider;
global using Memo.Blog.Infrastructure.Security.GenerateToken;
global using Memo.Blog.Infrastructure.Persistence;
global using Memo.Blog.Domain.Enums;
global using Memo.Blog.Infrastructure.Security.TokenValidation;
global using Memo.Blog.Infrastructure.Security;
