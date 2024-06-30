global using System.Linq;

global using FluentValidation;
global using MediatR;
global using MapsterMapper;
global using Mapster;

global using Memo.Blog.Application.Common.Interfaces.Security;
global using Memo.Blog.Application.Common.Models;
global using Memo.Blog.Application.Common.Behaviours;
global using Memo.Blog.Domain.Constants;
global using Memo.Blog.Application.Common.Interfaces.Persistence.Repositories;
global using Memo.Blog.Application.Tokens.Common;
global using Memo.Blog.Domain.Entities;
global using Memo.Blog.Application.Common.Request;
global using Memo.Blog.Application.Common.Utils;
global using Memo.Blog.Domain.Enums;
global using Memo.Blog.Application.Common.Extensions;

global using ApiPermission = Memo.Blog.Domain.Constants.Security.Permissions.Permissions;
