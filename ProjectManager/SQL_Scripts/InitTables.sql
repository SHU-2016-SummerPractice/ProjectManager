--create database NEGProjectPortal;

--建表， 假设所有数据都是我们这边生成， 不考虑其他数据源

--人员信息表
create table Staff(
    Id int not null primary key  identity(1,1),
    Name varchar(32) not null,
    Department varchar(32),
    IsOnJob char(1) default 'Y', -- 是否在职 Y/N
);

-- Project表
create table ProjectInfo(
    ID int not null primary key identity(1,1),
    ProjectID int not null,
    GroupId int,
    LO int default -1,  -- -1表示没有冲突， 即代表 # Lanuch Order
    MISStatus varchar(16),
    IsLanuched char(1) default 'N',  --是否已经合并到主线
    CNPMId int,
--各种时间
    StartDate datetime,
    EndDate datetime,           --===这个是不是项目完全完成之后再填写的字段
    ReleaseDate datetime,
    DelayReleaseDate datetime,
    LanuchDate datetime,
    DelayLanuchDate datetime,
);

--PBGroup表
create table PBGroup(
    ID int not null primary key identity(1,1),
    GroupName varchar(255),
)

-- PB表
create table PBInfo(
    ID int not null primary key identity(1,1),
    GroupId int,

    PBNo varchar(32),
    PBLink varchar(1024),
    PBName varchar(1024),
    USPMId int,
-- 工时相关信息
    Maturity float default 1.0 check (Maturity >=0 and Maturity <= 1),
    EstimatedHours float,
    MaturityEstimateHours float,
    ActualHours float,
-- 其他相关信息
    NeedInMobile char(1), --Y/N 
    WillFinish char(1), --Y/N
    BizUnit varchar(16),
    Domain varchar(8),

-- 看起来不太重要的字段
    AppproveDate datetime,
    GQCDate datetime,
    CRLType varchar(32),
    ProjectOwner varchar(32),
    BSA varchar(32),
    USPT varchar(32),
    CDPT varchar(32),
    SCRL float,
    Purpose varchar(32),
    Memo text,
	-- constraint fk_Proect_Staff foreign key (USPM) references Staff(Id),
    -- constraint fk_ProjectID foreign key (ProjectID) references ProjectInfo(ProjectID) on update cascade,
);
-- 工作目录表
create table Domain(
    ID int not null primary key identity(1,1),
    StaffId int not null,
    Percentage int,
    WorkLoadPercentage int,
    Project varchar(32),
    Memo  text,
    -- constraint fk_Domain_Staff foreign key (StaffId) references Staff(Id),
    constraint chk_Percentage check(Percentage >=  0 and Percentage <=120),
    constraint chk_WorkPercentage check (WorkLoadPercentage >= 0 and WorkLoadPercentage < = 100),
);


--WorkHours表
create table WorkHours(
    ID int not null primary key identity(1,1),
    StaffId int,
    StartDate datetime,
    WorkType int default 0,
    WorkContent text,
    -- constraint fk_WH_Staff foreign key (StaffId) references Staff(Id),
);
