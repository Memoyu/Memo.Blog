﻿namespace Memo.Blog.Domain.Events.Roles;

public record DeletedRoleEvent(long RoleId) : IDomainEvent;
