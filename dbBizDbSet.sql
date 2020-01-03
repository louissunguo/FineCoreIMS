

exec CreateBizDb 
@dbName=@dbNameString,
@path=@dbFilePath,
@dbInitSize=10,
@dbMaxSize =500,
@dbGrowth =5,
@logInitSize =5,
@logMaxSize =2500,
@logGrowth =5

go

create table CustInf(
	Id	int,
)

