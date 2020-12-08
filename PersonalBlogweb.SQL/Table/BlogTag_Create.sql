use PersonalBlogweb
go
create table BlogTag
(
Id	INT PRIMARY KEY IDENTITY(1,1),--	自增ID			
BlogNo	NVARCHAR(50) NOT NULL,--	博客编号			
EnumNo	NVARCHAR(50) NOT NULL,--	枚举编号			
DataState	TINYINT NOT NULL DEFAULT(0),-- 	逻辑删除标记，0-正常，1-删除			
CreateTime	DATETIME NOT NULL DEFAULT(GETDATE()),--	创建时间			
UpdateTime	DATETIME NOT NULL DEFAULT(GETDATE())	,--更新时间			
)
go