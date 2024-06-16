﻿using Memo.Blog.Application.Common.Hubs;
using Memo.Blog.Domain.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Protocol;

namespace Memo.Blog.Infrastructure.Hubs;

public static class HubEndpointRouteBuilderExtensions
{
    public static WebApplication MapHubs(this WebApplication app)
    {
        // app.MapHub<NotificationHub>(HubConst.ManagementHubEndpointRoute);
        return app;
    }
}