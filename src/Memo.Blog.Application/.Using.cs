global using System.Linq;

global using FluentValidation;
global using MediatR;
global using MapsterMapper;

global using Memo.Blog.Application.Common.Interfaces.Security;
global using Memo.Blog.Application.Common.Models;
global using Memo.Blog.Application.Common.Security.Permissions;
global using Memo.Blog.Application.Common.Behaviours;
global using Memo.Blog.Domain.Constants;
global using Memo.Blog.Application.Common.Interfaces.Persistence.Repositories;
global using Memo.Blog.Application.Common.Security;
global using Memo.Blog.Domain.Entities;
global using Memo.Blog.Application.Common.Request;
global using Memo.Blog.Application.Common.Utils;

global using ApiPermission = Memo.Blog.Application.Common.Security.Permissions.Permissions;
