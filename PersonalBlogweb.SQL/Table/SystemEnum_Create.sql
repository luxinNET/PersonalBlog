use PersonalBlogweb
go
create table SystemEnum
(
Id	INT PRIMARY KEY IDENTITY(1,1),--自增ID			
EnumText	NVARCHAR(255) NOT NULL DEFAULT(''),--枚举内容			
EnumValue	NVARCHAR(255) NOT NULL DEFAULT(''),--枚举值
EnumType	TINYINT NOT NULL DEFAULT(1) ,--枚举类型 0-标签 1-类型
EnumNo	NVARCHAR(50) NOT NULL,--枚举编号
EnumParentNo	NVARCHAR(50) NOT NULL DEFAULT(''),--枚举父级编号
CreateTime	DATETIME NOT NULL DEFAULT(GETDATE()),--创建时间
UpdateTime	DATETIME NOT NULL DEFAULT(GETDATE()),--更新时间
DataState	TINYINT NOT NULL DEFAULT(0)--逻辑删除标记，0-正常，1-删除	
)
go