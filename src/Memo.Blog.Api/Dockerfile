#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Memo.Blog.Api/Memo.Blog.Api.csproj", "src/Memo.Blog.Api/"]
COPY ["src/Memo.Blog.Application/Memo.Blog.Application.csproj", "src/Memo.Blog.Application/"]
COPY ["src/Memo.Blog.Domain/Memo.Blog.Domain.csproj", "src/Memo.Blog.Domain/"]
COPY ["src/Memo.Blog.Infrastructure/Memo.Blog.Infrastructure.csproj", "src/Memo.Blog.Infrastructure/"]
RUN dotnet restore "./src/Memo.Blog.Api/Memo.Blog.Api.csproj"
COPY . .
WORKDIR "/src/src/Memo.Blog.Api"
RUN dotnet build "./Memo.Blog.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Memo.Blog.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Memo.Blog.Api.dll"]