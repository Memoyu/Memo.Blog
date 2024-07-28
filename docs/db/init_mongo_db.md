### 添加mongodb index

源于文章搜索需要进行mongo text search，所以需要创建mongodb index：

Category_text

Comments_text

Content_text

Description_text

Tags_text

Title_text

``` shell
# 切换当前db
use blog

# 执行索引创建
db.article.createIndex(
   {
      Category: "text",
      Comments: "text",
      Content: "text",
      Description: "text",
      Tags: "text",
      Title: "text"
   }
)
```

