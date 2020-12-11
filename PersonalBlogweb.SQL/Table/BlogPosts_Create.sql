go
use PersonalBlogweb

go
create table BlogPosts
(
Id	INT PRIMARY KEY IDENTITY(1,1),--	自增ID			
BlogNo	NVARCHAR(50) NOT NULL,--	博客编号			
BlogTitle	NVARCHAR(255) NOT NULL DEFAULT(''),--	博客标题			
CreateTime	DATETIME NOT NULL DEFAULT(GETDATE()),--	创建时间			
UpdateTime	DATETIME NOT NULL DEFAULT(GETDATE()),--	更新时间			
DataState	TINYINT NOT NULL DEFAULT(0) 	,--逻辑删除标记，0-正常，1-删除			
BlogBody	NVARCHAR(MAX) NOT NULL DEFAULT(''),--	博客内容			
BlogBanner	NVARCHAR(50) NOT NULL DEFAULT('')	,--博客封面图			
BlogTypeENo	NVARCHAR(50) NOT NULL DEFAULT(''),--	博客类型编号			
BlogPageView	BIGINT NOT NULL DEFAULT(0) ,--	博客PV			
)
go