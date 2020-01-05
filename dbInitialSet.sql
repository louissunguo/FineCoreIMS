/*创建数据库*/
exec ('create database '+@dbName)
go
exec('USE '+@dbName+
' create CreateBizDb(
        @dbName nvarchar(128),  @path nvarchar(256)=null,
        @dbInitSize int=10,     @dbMaxSize int=500,     @dbGrowth int=5,
        @logInitSize int=5,     @logMaxSize int=2500,         @logGrowth int=5)
	as
	begin
		create database @dbName 
        ON 
        ( NAME = @dbName,
            FILENAME = isnull(@path,select SUBSTRING(filename,0, LEN(filename) - CHARINDEX(''\'', REVERSE(filename)) + 1) from master.dbo.sysdatabases )+''\\''+@dbName+''.mdf'',
            SIZE = @dbInitSize+''MB'',
            MAXSIZE = @dbMaxSize+''MB'',
            FILEGROWTH =  @dbGrowth+''MB'' )
        LOG ON
        ( NAME = @dbName+''_log'',
            FILENAME = isnull(@path,select SUBSTRING(filename,0, LEN(filename) - CHARINDEX(''\'', REVERSE(filename)) + 1) from master.dbo.sysdatabases )+''\\''+@dbName+''_log.ldf'',
            IZE = @dbInitSize+''MB'',
            MAXSIZE = @dbMaxSize+''MB'',
            FILEGROWTH =  @dbGrowth+''MB'' ) ;
	end'
)
go
/*开始建表（名称信息）*/
create table NameInf(
Id int identity(1,1) primary key(Id),
Actived bit default(1) not null,
/*特有字段*/
RefTable nvarchar(128) not null,
RefModelId int not null,
DisplayText nvarchar(128) not null,
LanguageName nvarchar(6) default('zh-cn') not null,
)
go
/*开始建表（公司信息）*/
create table CorpInf(
Id int identity(1,1) primary key(Id),
Actived bit default(1) not null,
/*添加字段*/
Code nvarchar(16) constraint pk_Corp_Code unique(Code) not null,
/*特有字段*/
CEOId int not null,
GMId int not null,
CFOId int not null,
CashierId int not null,/*出纳*/
PhoneNo nvarchar(11) null,
FaxNo nvarchar(11) null,
PostAddress nvarchar(256) null,
)
go
/*开始建表（部门信息）*/
create table DeptInf(
Id int identity(1,1) primary key(Id),
Actived bit default(1) not null,
/*添加字段*/
Code nvarchar(16) constraint pk_Dept_Code unique(Code) not null,
/*特有字段*/
DeptManagerId	int not null,
DeptSecretaryId int		null,
ExtNo nvarchar(4)		null,
)
go
/*开始建表（角色信息）*/
create table RoleInf(
Id int identity(1,1) primary key(Id),
Actived bit default(1) not null,
/*添加字段*/
Code nvarchar(16) constraint pk_Dept_Code unique(Code) not null,
/*特有字段*/
DescriptionText nvarchar(256),
)
go
/*开始建表（功能信息）*/
create table FuncInf(
Id int identity(1,1) primary key(Id),
Actived bit default(1) not null,
/*添加字段*/
Code nvarchar(16) constraint pk_Func_Code unique(Code) not null,
DescriptionText nvarchar(256),
/*特有字段*/
PageUrl nvarchar(256),
FullClassName nvarchar(256),
ParentId int foreign key(ParentId) references FuncInf(Id),
)
go

create table UserInf(
	Id	int	identity(1,1) primary key(Id),
	Actived bit default(1) not null,
    /*添加字段*/
	Code nvarchar(20) constraint pk_User_Code unique(Code) not null,
    /*特有字段*/
	UserName nvarchar(50) constraint pk_User_Name unique(UserName) not null,
	LoginPwd nvarchar(64) not null,
	EmailUrl nvarchar(64) not null,
	MobileNo nchar(11)	not null,
	WechatNo nvarchar(64) not null,
)


