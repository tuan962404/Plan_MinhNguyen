USE [master]
GO
/****** Object:  Database [Plan]    Script Date: 22/07/2019 14:52:55 ******/
CREATE DATABASE [Plan]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Plan', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Plan.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Plan_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Plan_log.ldf' , SIZE = 8384KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Plan] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Plan].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Plan] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Plan] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Plan] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Plan] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Plan] SET ARITHABORT OFF 
GO
ALTER DATABASE [Plan] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Plan] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Plan] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Plan] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Plan] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Plan] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Plan] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Plan] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Plan] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Plan] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Plan] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Plan] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Plan] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Plan] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Plan] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Plan] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Plan] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Plan] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Plan] SET  MULTI_USER 
GO
ALTER DATABASE [Plan] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Plan] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Plan] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Plan] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Plan] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Plan]
GO
/****** Object:  Table [dbo].[Assy_PSI_Shortage]    Script Date: 22/07/2019 14:52:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assy_PSI_Shortage](
	[STT] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](200) NULL,
	[Date] [date] NULL,
	[Requirement] [int] NULL,
 CONSTRAINT [PK_Assy_PSI_Shortage] PRIMARY KEY CLUSTERED 
(
	[STT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CodeList]    Script Date: 22/07/2019 14:52:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CodeList](
	[No.] [int] IDENTITY(1,1) NOT NULL,
	[Injection-Code] [nvarchar](200) NOT NULL,
	[Machine-No.] [nvarchar](200) NULL,
	[Machine] [nvarchar](200) NULL,
	[Cycle-Time] [nvarchar](200) NULL,
	[Cavity] [nvarchar](200) NULL,
	[Manpower] [nvarchar](200) NULL,
	[Resin] [nvarchar](200) NULL,
	[Resin-Code] [nvarchar](200) NULL,
	[Weight] [nvarchar](200) NULL,
	[Another Code] [nvarchar](200) NULL,
	[ItemName] [nvarchar](200) NULL,
 CONSTRAINT [PK_No_] PRIMARY KEY CLUSTERED 
(
	[No.] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Credential]    Script Date: 22/07/2019 14:52:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Credential](
	[UserGroupID] [varchar](20) NOT NULL,
	[RoleID] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Credential] PRIMARY KEY CLUSTERED 
(
	[UserGroupID] ASC,
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DA_Injection]    Script Date: 22/07/2019 14:52:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DA_Injection](
	[No.] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](200) NULL,
	[So-May] [int] NULL,
	[Trong-Luong-May] [nvarchar](200) NULL,
	[%Good] [float] NULL,
	[Ke-Hoach] [int] NULL,
	[Ke-Hoach-Bo-Sung] [int] NULL,
	[Manpower] [float] NULL,
	[Stock] [int] NULL,
	[Cycle-time] [float] NULL,
	[Cavity] [int] NULL,
	[Capa] [float] NULL,
	[Balance-Plan] [int] NULL,
	[Date] [date] NULL,
	[Requirements] [int] NULL,
	[Plan-Time(D)] [float] NULL,
	[Plan(D)] [int] NULL,
	[Good(D)] [int] NULL,
	[NG(D)] [int] NULL,
	[%NG] [float] NULL,
	[[Plan-Time(N)] [float] NULL,
	[Plan(N)] [int] NULL,
	[Good(N)] [int] NULL,
	[NG(N)] [int] NULL,
	[Date_End] [date] NULL,
	[Week] [nvarchar](200) NULL,
	[ID_PlanTimeDem] [nvarchar](200) NULL,
	[ID_PlanTimeNgay] [nvarchar](200) NULL,
	[PSI] [nvarchar](200) NULL,
	[Statuss] [nvarchar](200) NULL,
	[plandemtemp] [int] NULL,
	[plantimedemtemp] [float] NULL,
	[planngaytemp] [int] NULL,
	[plantimengaytemp] [float] NULL,
	[ID_PlanDem] [nvarchar](50) NULL,
	[ID_PlanNgay] [nvarchar](50) NULL,
	[Html_PlanTimeDem] [nvarchar](50) NULL,
	[Html_PlanTimeNgay] [nvarchar](50) NULL,
	[FlagDem] [bit] NULL,
	[FlagNgay] [bit] NULL,
	[ID_ValidateDem] [nvarchar](50) NULL,
	[ID_ValidateNgay] [nvarchar](50) NULL,
	[ID_Balance] [nvarchar](50) NULL,
	[ID_KHBS] [nvarchar](50) NULL,
	[Html_SoMay] [nvarchar](50) NULL,
	[STT] [int] NULL,
	[ItemName] [nvarchar](200) NULL,
 CONSTRAINT [PK_DA_Injection] PRIMARY KEY CLUSTERED 
(
	[No.] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Domain]    Script Date: 22/07/2019 14:52:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Domain](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Domain] [nvarchar](50) NULL,
 CONSTRAINT [PK_Domain] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[KetQuaSanXuatThieu]    Script Date: 22/07/2019 14:52:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KetQuaSanXuatThieu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NULL,
	[Date] [date] NULL,
	[SoThieuCaDem] [int] NULL,
	[SoThieuCaNgay] [int] NULL,
	[SoMay] [int] NULL,
	[Week] [nvarchar](50) NULL,
	[PSI] [nvarchar](50) NULL,
	[FlagDem] [bit] NULL,
	[FlagNgay] [bit] NULL,
 CONSTRAINT [PK_KetQuaSanXuatThieu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MailServer]    Script Date: 22/07/2019 14:52:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MailServer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
 CONSTRAINT [PK_MailServer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MailVerify]    Script Date: 22/07/2019 14:52:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MailVerify](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[CodeVerify] [nvarchar](50) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_MailVerify] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PicklistVatTu]    Script Date: 22/07/2019 14:52:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PicklistVatTu](
	[STT] [int] IDENTITY(1,1) NOT NULL,
	[Injection_Code] [nvarchar](200) NULL,
	[Machine_No.] [nvarchar](200) NULL,
	[Machine] [nvarchar](200) NULL,
	[Cycle_Time] [nvarchar](200) NULL,
	[Cavity] [nvarchar](200) NULL,
	[Manpower] [nvarchar](200) NULL,
	[Resin] [nvarchar](200) NULL,
	[Resin Code] [nvarchar](200) NULL,
	[Weight] [nvarchar](200) NULL,
	[Vat_Tu_Code] [nvarchar](200) NULL,
	[Ten_Vat_Tu] [nvarchar](200) NULL,
	[Ty_Le] [nvarchar](200) NULL,
	[Another_Code] [nvarchar](200) NULL,
 CONSTRAINT [PK_PicklistVatTu] PRIMARY KEY CLUSTERED 
(
	[STT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Plan_Week]    Script Date: 22/07/2019 14:52:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Plan_Week](
	[STT] [int] IDENTITY(1,1) NOT NULL,
	[DateStart] [date] NULL,
	[DateEnd] [date] NULL,
	[Week] [nvarchar](200) NULL,
 CONSTRAINT [PK_Plan_Week] PRIMARY KEY CLUSTERED 
(
	[STT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Production-Result]    Script Date: 22/07/2019 14:52:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Production-Result](
	[Code] [nvarchar](200) NOT NULL,
	[Date] [date] NULL,
	[Shift1(G)] [int] NULL,
	[Shift2(G)] [int] NULL,
	[Shift1(NG)] [int] NULL,
	[Shift2(NG)] [int] NULL,
	[STT] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_STT] PRIMARY KEY CLUSTERED 
(
	[STT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PSI_V_C]    Script Date: 22/07/2019 14:52:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PSI_V_C](
	[STT] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](200) NULL,
	[Description] [nvarchar](200) NULL,
	[Date] [date] NULL,
	[Requirement] [int] NULL,
 CONSTRAINT [PK_PSI_V_C] PRIMARY KEY CLUSTERED 
(
	[STT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PSI-WM-REF]    Script Date: 22/07/2019 14:52:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PSI-WM-REF](
	[Code] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[Date] [date] NULL,
	[Requirements] [int] NULL,
	[STT] [int] IDENTITY(1,1) NOT NULL,
	[Balace_Stock_] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[STT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ResinStock]    Script Date: 22/07/2019 14:52:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResinStock](
	[STT] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](200) NOT NULL,
	[ResinStock] [int] NULL,
 CONSTRAINT [PK_ResinStock] PRIMARY KEY CLUSTERED 
(
	[STT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role]    Script Date: 22/07/2019 14:52:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Role](
	[ID] [varchar](50) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Stock]    Script Date: 22/07/2019 14:52:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock](
	[Code] [nvarchar](200) NOT NULL,
	[Stock] [int] NULL,
	[STT] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Stock] PRIMARY KEY CLUSTERED 
(
	[STT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SummaryByProduct]    Script Date: 22/07/2019 14:52:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SummaryByProduct](
	[STT] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NULL,
	[Date] [date] NULL,
	[SoLuong] [int] NULL,
	[Mc] [int] NULL,
	[ResinCode] [nvarchar](50) NULL,
	[Resin] [nvarchar](50) NULL,
	[MixRate] [float] NULL,
	[NetWeight] [float] NULL,
	[Week] [nvarchar](50) NULL,
 CONSTRAINT [PK_SummaryByProduct] PRIMARY KEY CLUSTERED 
(
	[STT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 22/07/2019 14:52:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[User] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[Name] [nvarchar](50) NULL,
	[FullName] [nvarchar](50) NULL,
	[BoPhan] [nvarchar](50) NULL,
	[NumberPhone] [int] NULL,
	[Email] [nvarchar](50) NULL,
	[Status] [bit] NULL,
	[GroupID] [varchar](20) NULL CONSTRAINT [DF_User_GroupID]  DEFAULT ('MEMBER'),
	[VIEW_USER] [bit] NULL,
	[LAP_KEHOACH] [bit] NULL,
	[EDIT_KEHOACH] [bit] NULL,
	[XUAT_EXCEL] [bit] NULL,
	[ADD_STOCK] [bit] NULL,
	[DELETE_STOCK] [bit] NULL,
	[ADD_RESINSTOCK] [bit] NULL,
	[DELETE_RESINSTOCK] [bit] NULL,
	[EDIT_STOCK] [bit] NULL,
	[EDIT_RESINSTOCK] [bit] NULL,
	[EDIT_KQSX] [bit] NULL,
	[NHAP_KQSX] [bit] NULL,
	[ADD_CODELIST] [bit] NULL,
	[ADD_PICKLISTVATTU] [bit] NULL,
	[IMPORT_PSI] [bit] NULL,
	[IMPORT_STOCK] [bit] NULL,
	[IMPORT_RESINSTOCK] [bit] NULL,
	[EDIT_CODELIST] [bit] NULL,
	[EDIT_PICKLISTVATTU] [bit] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserGroup]    Script Date: 22/07/2019 14:52:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserGroup](
	[ID] [varchar](20) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserGroup] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[CodeList] ON 

INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (1, N'DJ66-00631B', N'1', N'TB160S', N'39', N'2', N'0,5', N'ABS', N'0103-009610', N'13,5', N'', N'LEVER CYCLONE;SC8800,ABS,HG-0760GP,NEUTR')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (2, N'DC61-02646A', N'8', N'TB240S', N'36', N'4', N'0,5', N'PP', N'0103-007539', N'1,6', N'', N'GUIDE-HOSE')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (3, N'DC62-00477C', N'2', N'TB160S', N'43', N'2', N'1', N'PP+M/B', N'0103-003461
0103-010287', N'8,5', N'', N'CAP')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (4, N'DJ62-00331A', N'38', N'TE110', N'35,1', N'2', N'0,5', N'TPV', N'0103-003320', N'5,25', N'', N'SEAL DUCT')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (5, N'DA63-09042A', N'3', N'TE160', N'39', N'2', N'0', N'HIPS/S834S1, COOL WHITE', N'0103-003252', N'13,2', N'', N'COVER RAIL LEFT')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (6, N'DA63-09044A', N'3', N'TE160', N'39', N'2', N'0,5', N'HIPS/S834S1, COOL WHITE', N'0103-003252', N'13,2', N'', N'COVER RAIL RIGHT')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (7, N'DA63-08639A', N'3', N'TB160S', N'35', N'1', N'0,25', N'HIPS/S834S1', N'0103-003252', N'29', N'', N'COVER RAIL-RIGHT')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (8, N'DA63-08637A', N'3', N'TB160S', N'35', N'1', N'0,25', N'HIPS/S834S1', N'0103-003252', N'29', N'', N'COVER RAIL-LEFT')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (9, N'DA66-01185A', N'3', N'TB160S', N'35', N'4', N'0,5', N'POM, K300', N'0103-002641', N'12,9', N'', N'GEAR CAM;POM,NATURAL,K300')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (10, N'DA66-01186A', N'3', N'TB160S', N'39', N'4', N'0,5', N'POM, K300', N'0103-002641', N'8,9', N'', N'GEAR CAM-MIDDLE;POM,NATURAL,K300')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (11, N'DA61-10771A', N'4', N'TB160S', N'35', N'2', N'1', N'HIPS/''S834S1', N'0103-003252', N'22,6', N'', N'SUPPORT TRAY;HIPS,T3,S834S1,COOL WHITE')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (12, N'DA66-01183A', N'4', N'TB160S', N'35', N'2', N'0,5', N'ABS/780', N'0103-002604', N'10,9', N'', N'LEVER;ABS,HG-0760GP,COOL WHITE')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (13, N'DA66-01184A', N'4', N'TB160S', N'38', N'2', N'1', N'ABS/780', N'0103-002604', N'8,3', N'', N'LEVER HANDLE;ABS,T2.0,HG-0760GP,COOL WHI')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (14, N'DA63-08275A', N'35', N'TB160S', N'38', N'2', N'1', N'HIPS', N'0103-003252', N'23', N'', N'COVER-GEAR;HIPS,HB,COOL WHITE,S834S1')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (15, N'DJ64-01011B', N'35', N'TB160S', N'32', N'2', N'1', N'PP+', N'0103-010425', N'18,2', N'', N'DECORATION CYCLONE;SC8800,PP+TD30%,Airbo')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (16, N'DJ64-00791C', N'1', N'TB160S', N'23,4', N'4', N'0,5', N'ABS', N'0103-010368', N'4,4', N'', N'GRILLE CYCLONE;SC8800,ABS,Airborne,HG-07')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (17, N'DA61-09096A', N'5', N'TB240S', N'28', N'8', N'1', N'HIPS/S834S1', N'0103-003252', N'2,9', N'', N'FIXER SHELF-FRE')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (18, N'DA63-07423B', N'9', N'TB240S', N'43', N'4', N'1', N'PP/BJ750+PPW177710', N'0103-001846
0103-002992', N'12,5', N'', N'COVER DEODORIZER')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (19, N'DA64-04412A', N'10', N'TB240S', N'36', N'8', N'1', N'HIPS/S834S1', N'0103-003252', N'8,4', N'', N'KNOB-DIAL')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (20, N'DJ63-01123A/ASS', N'6', N'TB240S', N'46,1', N'2', N'1', N'ABS', N'0103-009610', N'57,3', N'DJ63-01123A', N'COVER SPONGE')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (21, N'DJ64-01014A', N'7', N'TB240S', N'39,25', N'2', N'0,5', N'ABS', N'0103-003145', N'39', N'', N'GRILLE-SPONGE;SC8830,ABS,ILLUSHION ORANG')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (22, N'DA63-09479A', N'7', N'TB240S', N'35', N'16', N'0,5', N'FRPP', N'0103-004022', N'1,3', N'', N'GROMMET SCREW')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (23, N'DJ64-01008/IN', N'4', N'TB160S', N'40', N'2', N'1', N'ABS/''TX0510', N'0103-009203', N'18,6', N'DJ64-01008A/IN', N'PANEL CONTROL')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (24, N'DJ61-01827A', N'8', N'TB240S', N'43', N'2', N'1', N'PP/ SSPP640', N'0103-007539', N'41,2', N'', N'GUIDE-COVER')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (25, N'DJ63-01287A', N'8', N'TB240S', N'62', N'4', N'1', N'POM/K300', N'0103-001493', N'50,9', N'', N'GUARD CORD')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (26, N'DJ64-01010/IN', N'4', N'TB160S', N'37', N'2', N'1', N'PC,3022R
VIOLET RED', N'0103-008846 ', N'20,8', N'DJ64-01010A/IN', N'PANEL CONTROL-H')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (27, N'DJ64-01012T/IN', N'10', N'TB240S', N'42,1', N'2', N'1', N'ABS', N'0103-010368', N'22', N'DJ64-01012T', N'HANDLE CYCLONE-UP')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (28, N'DJ64-01012V/IN', N'10', N'TB240S', N'42,1', N'2', N'1', N'ABS', N'0103-010365', N'22', N'DJ64-01012V', N'HANDLE CYCLONE-UP')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (29, N'DJ64-01012W/IN', N'10', N'TB240S', N'42,1', N'2', N'1', N'ABS', N'0103-003108', N'22', N'DJ64-01012W', N'HANDLE CYCLONE-UP')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (30, N'DJ64-01012U/IN', N'10', N'TB240S', N'42,1', N'2', N'1', N'ABS', N'0103-010859T', N'22', N'DJ64-01012U', N'HANDLE CYCLONE-UP')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (31, N'DJ64-01006A', N'2', N'TB160S', N'34,2', N'2', N'0,5', N'ABS/GP35', N'0103-003108', N'17,1', N'', N'BUTTON-C/REEL;SC8830,ABS,EBONY BLK')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (32, N'DJ64-01007A', N'2', N'TB160S', N'34,2', N'2', N'0,5', N'ABS', N'0103-003108', N'17,1', N'', N'BUTTON SWITCH;SC8830,ABS,EBONY BLK')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (33, N'DJ66-00398A', N'11', N'TB240S', N'48', N'2', N'0,5', N'PP/ MB(PP).P744J ', N'0103-0001846
0103-0002777', N'67,3', N'', N'WHEEL BLACK ')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (34, N'DJ61-01599A', N'2', N'TB160S', N'38', N'4', N'1', N'ABS/GP35', N'0103-003108', N'19,8', N'', N'HOLDER-CLAMPER')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (35, N'DJ67-00632A/ASS', N'12', N'TB280S', N'48', N'2', N'1', N'ABS', N'0103-003108', N'41,4', N'DJ67-00632A', N'DUCT-INLET')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (36, N'DJ64-01013A', N'13', N'TB280S', N'43,2', N'2', N'1', N'ABS', N'0103-003108', N'29', N'', N'HANDLE-CYCLONE LOW;SC8830,ABS,EBONY BLK')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (37, N'DA63-08670A', N'14', N'TB380S', N'40', N'2', N'0,5', N'PP/P744J, MB', N'0103-001846
0103-002992', N'48', N'', N'TRAY ICE;RR7000M,PP,COOL WHITE')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (38, N'DA61-11391A', N'16', N'TB380S', N'55', N'1', N'1', N'HIPS/''S834S1', N'0103-003252', N'386,4', N'', N'CASE TRAY;RR7000M,HIPS,COOL WHITE')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (39, N'DA63-08671A', N'14', N'TB380S', N'52', N'1', N'1', N'HIPS', N'0103-003252', N'219,8', N'', N'COVER TRAY-FRONT;RR7000M,ABS,T2.5,HB,COO')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (40, N'DA61-11392/IN', N'15', N'TB380S', N'65', N'2', N'1', N'HIPS', N'0103-003252', N'62,1', N'DA61-11392A', N'CASE TRAY-FRE UP')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (41, N'DA63-07349M', N'15', N'TB380S', N'39', N'4', N'1', N'PP/BJ750+MB INOX GRAY', N'0103-001846
0103-009569', N'22,5', N'', N'COVER HINGE UPP')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (42, N'DA63-07349N', N'15', N'TB380S', N'39', N'4', N'1', N'PP/P744J+MB  WHITE', N'0103-001846
0103-002990', N'22,5', N'', N'COVER HINGE UPP')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (43, N'DA61-11356A', N'18', N'DL450S', N'45', N'2', N'0,5', N'HIPS/S834S1', N'0103-009131', N'93,5', N'', N'GUIDE AIR-REF')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (44, N'DA61-10462B', N'18', N'DL450S', N'55', N'2', N'0,5', N'HIPS CNTK H304', N'0103-009131', N'99,4', N'', N'0')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (45, N'DA61-08425A', N'18', N'DL450S', N'44', N'2', N'0,5', N'ABS/CNTK A-204', N'0103-007625', N'101,5', N'', N'CASE MOTOR FRE')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (46, N'DJ63-01119B', N'17', N'TB380S', N'52,9', N'2', N'1', N'ABS', N'0103-003108', N'68', N'', N'COVER FRONT;SC8800,ABS,EBONY BLACK,NON-C')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (47, N'DA61-11164A', N'14', N'TB380S', N'30', N'7', N'0,5', N'PP/SSPP640', N'0103-007539', N'1,7', N'', N'FIXER SUCTION')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (48, N'DA64-04402A', N'18', N'DL450S', N'45', N'2', N'1', N'ABS/HG-0760GP', N'0103-007331', N'112,8', N'', N'HANDLE BASE')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (49, N'DA64-04402B', N'18', N'DL450S', N'45', N'2', N'1', N'Cũ: ABS/HG-0760GP', N'0103-004144', N'112,8', N'', N'HANDLE BASE')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (50, N'DA67-03601A', N'19', N'DL450S', N'31', N'2', N'0,5', N'HIPS/''S834S1', N'0103-004130', N'32', N'', N'DRAIN CONNECTOR')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (51, N'DA61-11385A', N'18', N'DL450S', N'58', N'2', N'1', N'HIPS + MB', N'0103-004130
0103-007638', N'135,4', N'', N'GUIDE ICE')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (52, N'DA61-11393A', N'47', N'DL450A5', N'63', N'2', N'1', N'HIPS', N'0103-003252', N'139,1', N'', N'GUIDE WATER-OUT;RR7000M,ABS,T2.5,HB,COOL')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (53, N'DA61-11262A', N'19', N'DL450S', N'50', N'2', N'1', N'HIPS CNTK H304', N'0103-009131', N'122,2', N'', N'')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (54, N'DJ63-01118A', N'47', N'DL450A5', N'54', N'2', N'1', N'ABS', N'0103-003108', N'106', N'', N'COVER BACK;SC8830,ABS,EBONY BLK,NO-COATI')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (55, N'DJ61-01852A/ASS', N'20', N'DL550A5', N'53,8', N'2', N'1', N'ABS/ GP35', N'0103-003108', N'185,2', N'DJ61-01852A', N'CASE SPONGE')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (56, N'DA63-07368A', N'21', N'DL450S', N'29', N'4', N'1', N'GPPS/168N', N'0103-004921', N'15,2', N'', N'COVER-LAMP;3050,GPPS,T2.0,TP BLUE,G116HV')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (57, N'DJ63-01558/RED', N'22', N'DL650A5', N'60,4', N'2', N'1', N'ABS', N'0103-011218', N'303', N'', N'COVER DUST')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (58, N'DJ63-01055/DEEP', N'22', N'DL650A5', N'60,4', N'2', N'1', N'ABS', N'0103-010365', N'303', N'', N'COVER DUST')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (59, N'DJ63-01558M', N'22', N'DL650A5', N'60,4', N'2', N'1', N'ABS/GP35', N'0103-010366', N'303', N'', N'COVER DUST')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (60, N'DJ63-01558N', N'22', N'DL650A5', N'60,4', N'2', N'1', N'ABS/''HG0760GP', N'0103-010859T', N'303,8', N'', N'COVER DUST;REB,ABS,HB,MERLOT PURPLE,1600')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (61, N'DJ61-01846A', N'24', N'DL850A5', N'75', N'2', N'1', N'PP+MB', N'0103-001846
0103-002777 ', N'401,8', N'', N'BODY MID;SC8830,PP,BLK')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (62, N'DC63-01943A', N'25', N'DL850A5', N'63', N'2', N'1', N'ABS/''HG-0760GP', N'0103-003138', N'101,9', N'', N'COVER-HOLDER')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (63, N'DC63-02022A', N'25', N'DL850A5', N'54', N'1', N'1', N'ABS', N'0103-000990', N'449', N'', N'COVER- GLASS')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (64, N'DA61-11386A', N'26', N'DL650S', N'63', N'2', N'1', N'GPPS/168N', N'0103-004921', N'404', N'', N'CASE ICE')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (65, N'DA63-08635A', N'26', N'DL650S', N'65,5', N'2', N'1', N'GPPS/147F', N'0103-004921', N'336', N'', N'TRAY-UP')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (66, N'DA63-08634A', N'26', N'DL650S', N'65,5', N'2', N'1', N'GPPS/147F', N'0103-004921', N'489', N'', N'TRAY-REF')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (67, N'DJ61-01850A', N'26', N'DL650S', N'67', N'2', N'1', N'ABS/ TX0510', N'0103-007555', N'333,3', N'', N'CASE-CYCLONE;SC8830,ABS,GRY TRP, G0290')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (68, N'DA67-04350A', N'45', N'DL450A5', N'60', N'1', N'1', N'HIPS/S834S1', N'0103-003252', N'311,2', N'', N'SHELF-FRE')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (69, N'DA67-04511B', N'49', N'DL450A5', N'65', N'1', N'1', N'PP', N'0103-001846', N'480', N'', N'SHELF-BOTTOM')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (70, N'DJ64-01005Q', N'31', N'DL850S', N'65', N'2', N'1', N'ABS', N'0103-010368', N'143,8', N'', N'DECORATION MID;SC8800,ABS,Airborne,NON-C')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (71, N'DJ64-01005S', N'31', N'DL850S', N'65', N'2', N'1', N'ABS', N'0103-010859T', N'143,8', N'', N'DECORATION MID;SC8800,ABS,Merlot purple,')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (72, N'DJ64-01005R', N'31', N'DL850S', N'65', N'2', N'1', N'ABS', N'0103-010365', N'143,8', N'', N'DECORATION MID;SC8800,ABS,Deep Blue,NON-')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (73, N'DJ64-01005N', N'31', N'DL850S', N'65', N'2', N'1', N'ABS', N'0103-003108', N'143,8', N'', N'DECORATION MID;SC8830,ABS,-,EBONY BLACK')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (74, N'DJ66-00638D', N'36', N'TE150', N'48', N'2', N'0,5', N'PP,
M/B(PP)', N'0103-001846
0103-002777', N'41,3', N'', N'WHEEL BLACK - I')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (75, N'DJ66-00474D', N'37', N'TE150', N'50', N'2', N'1', N'TPE/ S-700B', N'0103-002884', N'14,1', N'', N'WHEEL BLACK - II')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (76, N'DJ61-01851A', N'48', N'DL450A5', N'57,2', N'2', N'1', N'ABS/ TX0510', N'0103-007555', N'137', N'', N'CASE-CYCLONE I;SC8830,ABS,GRY TRP,UT0515')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (77, N'DC61-02634A', N'63', N'WIS2000X', N'60', N'2', N'3', N'PP', N'0103-003460', N'643', N'', N'')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (78, N'MD19-63959A', N'28', N'DL650S', N'85', N'1', N'1', N'PP RoHS BU510', N'10401017001362', N'354', N'', N'BASE BODY
Thân Máy')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (79, N'MD19-63960A', N'16', N'TB380S', N'60', N'2', N'1', N'PP RoHS BU510', N'10401017001362', N'43,5', N'', N'Babe Boby Upper Cover
Nắp Thân')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (80, N'MD19-17356A', N'16', N'TB380S', N'48', N'2', N'1', N'PP RoHS PP-HG', N'10401017A00101', N'75,5', N'', N'AIR OUTLET PLATE
Bản Thoát Gió')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (81, N'MD19-17359A', N'49', N'DL450A5', N'65', N'1', N'1', N'ABS RoHS HI-121H', N'10401001000778', N'534,7', N'', N'BRUSH COVER
Nắp Trên Quét Nền')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (82, N'MD19-46108A', N'16', N'TB380S', N'55', N'2', N'1', N'PP RoHS Ca10', N'10401017003642', N'88,2', N'', N'Brush Bottom Cover
Nắp DướiQuét Nền')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (83, N'MD19-46101A', N'29', N'DL650S', N'60', N'1', N'1', N'ABS RoHS 920', N'10401001000557', N'342,4', N'', N'DUST CUP
Hộc Chứa Bụi')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (84, N'MD19-63967A', N'16', N'TB380S', N'55', N'1', N'1', N'PP RoHS BU510', N'10401017001362', N'148,3', N'', N'DUST CUP CAP
Nắp Trên Hộc Chứa Bụi')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (85, N'MD19-63165A', N'39', N'TE110', N'34', N'2', N'1', N'ABS RoHS HI-121H', N'10401001000773', N'21', N'', N'ROTATE BUTTON
Nút Chỉnh Cao Thấp')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (86, N'MD19-46102A', N'9', N'TB240S', N'45', N'1', N'1', N'PP RoHS RJ581Z', N'10401017002582', N'97,3', N'', N'DUST CUP CYCLONE
Bộ Lọc')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (87, N'MD19-46095A', N'9', N'TB240S', N'42', N'2', N'1', N'PP RoHS RJ581Z', N'10401017002582', N'60', N'', N'DUST CUP FLANG SEPARATOR PRIMARY
Ống Hút Xoáy Cyclone')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (88, N'MD19-42829A', N'1', N'TB160S', N'42', N'2', N'1', N'PP RoHS Ca10', N'10401017003642', N'38,2', N'', N'CREVICE TOOL
Đầu Hút Dài')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (89, N'MD19-08558A', N'1', N'TB160S', N'35', N'2', N'1', N'PP RoHS BU510 Masterbatch 0.6%', N'10401017001083', N'13,2', N'', N'SMALL WHEEL
Bánh Xe Nhỏ')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (90, N'MD19-63179A', N'9', N'TB240S', N'45', N'2', N'1', N'PP RoHS Ca10', N'10401017003642', N'37,65', N'', N'BIG WHEEL
Bánh Xe Lớn')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (91, N'MD19-04301A', N'1', N'TB160S', N'40', N'2', N'1', N'PP RoHS Ca10', N'10401017003642', N'14,1', N'', N'RIGHT COVER
Giá Đỡ Phụ Kiện Trái')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (92, N'MD19-63962A', N'46', N'DL450A5', N'74', N'2', N'1', N'PP RoHS BU510', N'10401017001362', N'183', N'', N'Motor front cover
Chụp Trước Motor')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (93, N'MD19-17704A', N'14', N'TB380S', N'78', N'2', N'1', N'PP RoHS BU510', N'10401017001362', N'170', N'', N'Motor rear cover
Chụp Sau Motor')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (94, N'MD19-63964A', N'15', N'TB380S', N'49', N'2', N'1', N'PP RoHS BU510', N'10401017001362', N'330', N'', N'Dust cup handle
Tay Cầm Hộc Chứa Bụi')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (95, N'MD19-03810A', N'39', N'TE110', N'60', N'2', N'1', N'TPE RoHS TPE-60(TA60PR)', N'10401025000008', N'155', N'', N'Air duct seal
Ron Dán Kín Ống Đảo Gió')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (96, N'MD19-63162A', N'39', N'TE110', N'50', N'2', N'1', N'ABS RoHS 920', N'10401001000557', N'185', N'', N'Dust cup decorative cover
Nắp Trang Trí Vòng Lọc Bụi')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (97, N'MD19-10885A', N'40', N'TE110', N'34', N'2', N'1', N'PC RoHS IR2200', N'10401011001067', N'100', N'', N'Motor pressure seat
Đế Motor')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (98, N'MD19-17355A', N'1', N'TB160S', N'41', N'2', N'1', N'PP RoHS PP-HG', N'10401017A00101', N'22', N'', N'Dust cup handle cover
Nắp Tay Cầm Hộc Chứa Bụi')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (99, N'MD19-21423A', N'15', N'TB380S', N'43', N'2', N'1', N'PP Ca10', N'10401017003642', N'32', N'', N'Filter cover
Chụp Lọc Bụi')
GO
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (100, N'DJ64-01132A', N'38', N'TE110', N'29,58', N'2', N'', N'PC', N'0103-003360', N'10', N'', N'BUTTON SLIDE')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (101, N'DC61-04346A', N'39', N'TE110', N'31,96', N'2', N'', N'ABS', N'0103-003108', N'0', N'', N'GUIDE LED RIGHT ')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (102, N'DC61-04347A', N'39', N'TE110', N'36,68', N'2', N'', N'ABS', N'0103-003108', N'26', N'', N'GUIDE LED ')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (103, N'DC61-04317A', N'40', N'TE110', N'29,76', N'2', N'', N'ABS -GP-35(LOTTTE)', N'0103-009089 ', N'23,4', N'', N'GUIDE LED')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (104, N'DC61-04333A', N'39', N'TE110', N'34,04', N'2', N'', N'ABS', N'0103-003108', N'34,5', N'', N'GUIDE LED-CENTER')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (105, N'DJ63-01278A', N'1', N'TB160S', N'24,24', N'4', N'', N'ABS', N'0103-003108', N'20,5', N'', N'COVER DUCT')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (106, N'M76A975A01', N'1', N'TB160S', N'38', N'2', N'', N'PP BJ750 NTR', N'0103-001846', N'23,63878', N'', N'LEVER')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (107, N'DC61-03543A', N'1', N'TB160S', N'39', N'4', N'', N'ABS-884X01', N'0103-003218T', N'16,2', N'', N'GUIDE-PCB(H)')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (108, N'DC61-04561A', N'11', N'TB240S', N'33,04', N'2', N'', N'ABS', N'0103-007340', N'60,5', N'', N'GUIDE COVER PCB')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (109, N'DC61-04316A', N'2', N'TB160S', N'32,78', N'2', N'', N'ABS -GP-35(LOTTTE)', N'0103-009089 ', N'46', N'', N'GUIDE LED')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (110, N'DJ63-01289A', N'11', N'TB240S', N'36,2', N'4', N'', N'ABS', N'0103-003108', N'19,05', N'', N'COVER DUCT (280t)')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (111, N'M76A977A01', N'5', N'TB240S', N'33', N'2', N'', N'PP-BJ73SL-NP', N'HD0601-2551A', N'47,6', N'', N'TRAY')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (112, N'M76A395A01', N'10', N'TB240S', N'42', N'2', N'', N'ABS ', N'0103-007625', N'151', N'', N'HOLDER -B')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (113, N'DC61-04358A', N'11', N'TB240S', N'38,86', N'2', N'', N'ABS', N'0103-003108', N'107,8', N'', N'GUIDE PCB(M)')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (114, N'DJ67-00688A', N'14', N'TB380S', N'35', N'2', N'', N'ABS ', N'0103-003108', N'29,26', N'', N'DUCT-INLET')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (115, N'M76A343A01', N'15', N'TB380S', N'31,9', N'2', N'', N'PP-JH370A-NP(MTB NI 01-0306(4%)', N'HD0301-0424A', N'50', N'', N'TRAY')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (116, N'M76A530A01', N'14', N'TB380S', N'53,86', N'1', N'', N'ABS', N'HF 380 (09733)', N'321', N'', N'FRAME')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (117, N'DC61-04323A', N'14', N'TB380S', N'42', N'2', N'', N'ABS', N'0103-003108', N'162', N'', N'GUIDE-PCB(M)')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (118, N'DC61-04301A', N'15', N'TB380S', N'38,8', N'2', N'', N'ABS(VE-0858)', N'0103-003218', N'180', N'', N'GUIDE COVER PCB')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (119, N'DJ61-02045A', N'43', N'DL450A5', N'34', N'2', N'', N'ABS', N'0103-010020', N'82,5', N'', N'HOLDER GRILLE')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (120, N'DJ61-02045B', N'43', N'DL450A5', N'34', N'2', N'', N'ABS', N'0103-001062', N'82,5', N'', N'HOLDER GRILLE')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (121, N'DJ61-02045F', N'43', N'DL450A5', N'34', N'2', N'', N'ABS', N'0103-010076', N'82,5', N'', N'HOLDER GRILLE')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (122, N'DJ61-02045H', N'43', N'DL450A5', N'34', N'2', N'', N'ABS ', N'0103-011452', N'82,5', N'', N'HOLDER GRILLE')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (123, N'DJ64-01130A', N'45', N'DL450A5', N'46,24', N'4', N'', N'PC ABS', N'0103-003284', N'116,5', N'', N'GRILLE BACK')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (124, N'DJ61-02045D', N'45', N'DL450A5', N'45,16', N'2', N'', N'ABS', N'0103-010829', N'82,5', N'', N'HOLDER GRILLE')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (125, N'DJ61-02045C', N'45', N'DL450A5', N'45,38', N'2', N'', N'ABS', N'0103-010830', N'82,5', N'', N'HOLDER GRILLE')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (126, N'DJ61-02045E', N'46', N'DL450A5', N'35,4', N'2', N'', N'ABS', N'0103-010840', N'82,5', N'', N'HOLDER GRILLE')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (127, N'M76A342A01', N'46', N'DL450A5', N'48', N'1', N'', N'ABS(W94066)', N'HD0301-0417A', N'390', N'', N'FRAME')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (128, N'DC63-02029A', N'46', N'DL450A5', N'40', N'2', N'', N'ABS', N'0103-003108', N'132,6', N'', N'COVER PCB-SUB')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (129, N'DJ63-01286A', N'20', N'DL550A5', N'46,08', N'4', N'', N'ABS', N'0103-003108', N'116,5', N'', N'COVER FRONT')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (130, N'M76A929A01', N'20', N'DL550A5', N'43,92', N'1', N'', N'HIPS', N'HD0301-0414A', N'383,1', N'', N'FRAME')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (131, N'DC63-02007A', N'46', N'DL450A5', N'41', N'2', N'', N'ABS 884X01', N'0103-003218T', N'237', N'', N'COVER PCB-MAIN(Có vách)')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (132, N'DC63-02007B', N'27', N'DL650S', N'41', N'2', N'', N'ABS 884X01', N'0103-003218T', N'250', N'', N'COVER PCB-MAIN(ko vách)')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (133, N'DC63-02177A', N'20', N'DL550A5', N'30', N'2', N'', N'ABS', N'0103-007340', N'170', N'', N'COVER PCB-SUB')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (134, N'DC63-02038A', N'45', N'DL450A5', N'55', N'2', N'', N'ABS-884X01', N'0103-003218T', N'151', N'', N'COVER PCB')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (135, N'AQ19-212958A', N'32B', N'150', N'115', N'2', N'1', N'WHITE 130 ABS
COMPONOUND', N'0,003021311', N'66', N'P0030212958', N'FILTER ZIM A')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (136, N'AQ19-212884H', N'3', N'160', N'35', N'4', N'1', N'WHITE 130 ABS
COMPONOUND', N'0,003021217', N'20,4', N'P0030212884A', N'HOSE JOINT-FR100ET')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (137, N'AQ19-212959B', N'5', N'240', N'35', N'2', N'1', N'WHITE 130 ABS
COMPONOUND', N'0,003021311', N'34,4', N'P0030212959', N'FILTER ZIM B')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (138, N'BN63-18034A', N'28', N'DL850S', N'85', N'4', N'3', N'', N'0103-004609', N'145', N'', N'COVER-STAND TOP LEFT')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (139, N'BN63-18037A', N'30', N'DL650S', N'75', N'3', N'3', N'', N'0103-004609', N'39,7', N'', N'COVER-STAND TOP RIGHT')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (140, N'BN63-18040A', N'41', N'DL850S', N'85', N'3', N'1', N'', N'0103-011475', N'108', N'', N'COVER-STAND NECK LEFT')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (141, N'BN63-18022A', N'27', N'DL650S', N'100', N'4', N'2', N'', N'0103-011425', N'33,4', N'', N'')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (142, N'BN63-18043A', N'42', N'DL1050S', N'85', N'4', N'1', N'', N'0103-011475', N'108', N'', N'COVER-STAND NECK RIGHT')
INSERT [dbo].[CodeList] ([No.], [Injection-Code], [Machine-No.], [Machine], [Cycle-Time], [Cavity], [Manpower], [Resin], [Resin-Code], [Weight], [Another Code], [ItemName]) VALUES (143, N'BN63-18028A', N'27', N'DL650S', N'100', N'4', N'1', N'', N'0103-011260', N'158,2', N'', N'')
SET IDENTITY_INSERT [dbo].[CodeList] OFF
SET IDENTITY_INSERT [dbo].[Domain] ON 

INSERT [dbo].[Domain] ([ID], [Domain]) VALUES (1, N'http://localhost:62053')
SET IDENTITY_INSERT [dbo].[Domain] OFF
SET IDENTITY_INSERT [dbo].[MailServer] ON 

INSERT [dbo].[MailServer] ([ID], [Email], [Password]) VALUES (1, N'minhnguyenserver@gmail.com', N'Minhnguyenpm')
SET IDENTITY_INSERT [dbo].[MailServer] OFF
SET IDENTITY_INSERT [dbo].[MailVerify] ON 

INSERT [dbo].[MailVerify] ([ID], [UserName], [Email], [CodeVerify], [Status]) VALUES (1, N'admin', N'hthunguyen1392@gmail.com', N'3SFZZ25UGY', 0)
INSERT [dbo].[MailVerify] ([ID], [UserName], [Email], [CodeVerify], [Status]) VALUES (2, N'admin', N'nlctuan@minhnguyenpm.com.vn', N'BX8RCZRIKD', 1)
INSERT [dbo].[MailVerify] ([ID], [UserName], [Email], [CodeVerify], [Status]) VALUES (3, N'admin', N'nlctuan@gmail.com', N'HLJ7I0GU94', 1)
SET IDENTITY_INSERT [dbo].[MailVerify] OFF
SET IDENTITY_INSERT [dbo].[PicklistVatTu] ON 

INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (1, N'DJ66-00631B', N'1', N'160', N'39', N'2', N'0.5', N'ABS', N'0103-009610', N'13.5', N'BB00001', N'Tấm lót Carton', N'0.009345794', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (2, N'DC61-02646A', N'1', N'160', N'36', N'4', N'0.5', N'PP', N'0103-007539', N'1.6', N'BB00001', N'Tấm lót Carton', N'0.0004', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (3, N'DC62-00477C', N'2', N'240', N'43', N'2', N'1', N'PP+M/B', N'0103-003461
0103-010287', N'8.5', N'BB00001', N'Tấm lót Carton', N'0.003174603', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (4, N'DJ62-00331A', N'2', N'240', N'35.1', N'2', N'0.5', N'TPV', N'0103-003320', N'5.25', N'BB00001', N'Tấm lót Carton', N'0.0005', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (5, N'DA63-09042A', N'34', N'240', N'39', N'2', N'0', N'HIPS/S834S1, COOL WHITE', N'0103-003252', N'13.2', N'BB00001', N'Tấm lót Carton', N'0.000833333', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (6, N'DA63-09044A', N'34', N'240', N'39', N'2', N'0.5', N'HIPS/S834S1, COOL WHITE', N'0103-003252', N'13.2', N'BB00001', N'Tấm lót Carton', N'0.000833333', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (7, N'DA63-08639A', N'3', N'240', N'35', N'1', N'0.25', N'HIPS/S834S1', N'0103-003252', N'29', N'BB00001', N'Tấm lót Carton', N'0.002', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (8, N'DA63-08637A', N'3', N'240', N'35', N'1', N'0.25', N'HIPS/S834S1', N'0103-003252', N'29', N'BB00001', N'Tấm lót Carton', N'0.002', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (9, N'DA66-01185A', N'3', N'240', N'35', N'4', N'0.5', N'POM, K300', N'0103-002641', N'12.9', N'BB00001', N'Tấm lót Carton', N'0.001', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (10, N'DA66-01186A', N'3', N'240', N'39', N'4', N'0.5', N'POM, K300', N'0103-002641', N'8.9', N'BB00001', N'Tấm lót Carton', N'0.001', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (11, N'DA61-10771A', N'4', N'240', N'35', N'2', N'1', N'HIPS/''S834S1', N'0103-003252', N'22.6', N'BB00001', N'Tấm lót Carton', N'0.005128205', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (12, N'DA66-01183A', N'4', N'240', N'35', N'2', N'0.5', N'ABS/780', N'0103-002604', N'10.9', N'BB00001', N'Tấm lót Carton', N'0.001', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (13, N'DA66-01184A', N'4', N'240', N'38', N'2', N'1', N'ABS/780', N'0103-002604', N'8.3', N'BB00001', N'Tấm lót Carton', N'0.004761905', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (14, N'DA63-08275A', N'35', N'240', N'38', N'2', N'1', N'HIPS', N'0103-003252', N'23', N'BB00001', N'Tấm lót Carton', N'0.009433962', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (15, N'DJ64-01011B', N'4', N'240', N'32', N'2', N'1', N'PP+', N'0103-010425', N'18.2', N'BB00001', N'Tấm lót Carton', N'0.009615385', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (16, N'DJ64-00791C', N'4', N'240', N'23.4', N'4', N'0.5', N'ABS', N'0103-010368', N'4.4', N'BB00001', N'Tấm lót Carton', N'0.0005', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (17, N'DA61-09096A', N'5', N'240', N'28', N'8', N'1', N'HIPS/S834S1', N'0103-003252', N'2.9', N'BB00001', N'Tấm lót Carton', N'0.000173611', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (18, N'DA63-07423B', N'9', N'240', N'43', N'4', N'1', N'PP/BJ750+PPW177710', N'0103-001846
0103-002992', N'12.5', N'BB00001', N'Tấm lót Carton', N'0.012987013', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (19, N'DA63-07423B', N'9', N'240', N'43', N'4', N'1', N'PP/BJ750+PPW177710', N'0103-001846
0103-002992', N'12.5', N'BB-00008', N'Bao Lami bag : 0.5*W570*L610', N'0.012987013', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (20, N'DA64-04412A', N'5', N'240', N'36', N'8', N'1', N'HIPS/S834S1', N'0103-003252', N'8.4', N'BB00001', N'Tấm lót Carton', N'0.004807692', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (21, N'DJ63-01123A/ASS', N'6', N'240', N'46.1', N'2', N'1', N'ABS', N'0103-009610', N'57.3', N'BB00001', N'Tấm lót Carton', N'0.027777778', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (22, N'DJ64-01014A', N'7', N'240', N'39.25', N'2', N'0.5', N'ABS', N'0103-003145', N'39', N'BB00001', N'Tấm lót Carton', N'0.008928571', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (23, N'DA63-03634C', N'7', N'240', N'35', N'16', N'0.5', N'FRPP', N'0103-004022', N'1.3', N'BB00001', N'Tấm lót Carton', N'0.0000625', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (24, N'DJ64-01008/IN', N'8', N'160', N'40', N'2', N'1', N'ABS/''TX0510', N'0103-009203', N'18.6', N'BB00001', N'Tấm lót Carton', N'0.010416667', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (25, N'DJ61-01827A', N'8', N'160', N'43', N'2', N'1', N'PP/ SSPP640', N'0103-007539', N'41.2', N'BB00001', N'Tấm lót Carton', N'0.023809524', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (26, N'DJ63-01287A', N'8', N'160', N'62', N'4', N'1', N'POM/K300', N'0103-001493', N'50.9', N'BB00001', N'Tấm lót Carton', N'0.018518519', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (27, N'DJ64-01010/IN', N'8', N'160', N'37', N'2', N'1', N'PC,3022R
VIOLET RED', N'0103-008846 ', N'20.8', N'BB00001', N'Tấm lót Carton', N'0.021276596', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (28, N'DJ64-01012T/IN', N'10', N'240', N'42.1', N'2', N'1', N'ABS', N'0103-010368', N'22', N'BB00001', N'Tấm lót Carton', N'0.009090909', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (29, N'DJ64-01012V/IN', N'10', N'240', N'42.1', N'2', N'1', N'ABS', N'0103-010365', N'22', N'BB00001', N'Tấm lót Carton', N'0.009090909', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (30, N'DJ64-01012W/IN', N'10', N'240', N'42.1', N'2', N'1', N'ABS', N'0103-003108', N'22', N'BB00001', N'Tấm lót Carton', N'0.009090909', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (31, N'DJ64-01012U/IN', N'10', N'240', N'42.1', N'2', N'1', N'ABS', N'0103-010859T', N'22', N'BB00001', N'Tấm lót Carton', N'0.009090909', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (32, N'DJ64-01006A', N'11', N'380', N'34.2', N'2', N'0.5', N'ABS/GP35', N'0103-003108', N'17.1', N'BB00001', N'Tấm lót Carton', N'0.008333333', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (33, N'DJ64-01007A', N'11', N'380', N'34.2', N'2', N'0.5', N'ABS', N'0103-003108', N'17.1', N'BB00001', N'Tấm lót Carton', N'0.008333333', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (34, N'DJ66-00398A', N'11', N'380', N'48', N'2', N'0.5', N'PP/ MB(PP).P744J ', N'0103-0001846H
0103-0002777', N'67.3', N'BB00001', N'Tấm lót Carton', N'0.041666667', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (35, N'DJ61-01599A', N'12', N'280', N'38', N'4', N'1', N'ABS/GP35', N'0103-003108', N'19.8', N'BB00001', N'Tấm lót Carton', N'0.003039514', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (36, N'DJ67-00632A/ASS', N'12', N'280', N'48', N'2', N'1', N'ABS', N'0103-003108', N'41.4', N'BB00001', N'Tấm lót Carton', N'', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (37, N'DJ64-01013A', N'13', N'280', N'43.2', N'2', N'1', N'ABS', N'0103-003108', N'29', N'BB00001', N'Tấm lót Carton', N'0.022222222', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (38, N'DA63-08670A', N'14', N'450', N'40', N'2', N'0.5', N'PP/P744J, MB', N'0103-001846
0103-002992', N'48', N'BB00001', N'Tấm lót Carton', N'0.0625', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (39, N'DA61-11391A', N'16', N'450', N'55', N'1', N'1', N'HIPS/''S834S1', N'0103-003252', N'386.4', N'BB00001', N'Tấm lót Carton', N'0.076923077', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (40, N'DA63-08671A', N'14', N'450', N'52', N'1', N'1', N'HIPS', N'0103-003252', N'219.8', N'BB00001', N'Tấm lót Carton', N'0.25', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (41, N'DA61-11392/IN', N'15', N'450', N'65', N'2', N'1', N'HIPS', N'0103-003252', N'62.1', N'BB00001', N'Tấm lót Carton', N'0.007142857', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (42, N'DA63-07349M', N'15', N'380', N'39', N'4', N'1', N'PP/BJ750+MB INOX GRAY', N'0103-001846
0103-009569', N'22.5', N'BB00001', N'Tấm lót Carton', N'0.016666667', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (43, N'DA63-07349N', N'15', N'380', N'39', N'4', N'1', N'PP/P744J+MB  WHITE', N'0103-001846
0103-002990', N'22.5', N'BB00001', N'Tấm lót Carton', N'0.016666667', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (44, N'DA61-11356A', N'16', N'380', N'45', N'2', N'0.5', N'HIPS/S834S1', N'0103-009131', N'93.5', N'BB00001', N'Tấm lót Carton', N'0.111111111', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (45, N'DA61-10462B', N'16', N'380', N'55', N'2', N'0.5', N'HIPS CNTK H304', N'0103-009131', N'99.4', N'BB00001', N'Tấm lót Carton', N'0.071428571', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (46, N'DA61-08425A', N'16', N'380', N'44', N'2', N'0.5', N'ABS/CNTK A-204', N'0103-007625', N'101.5', N'BB00001', N'Tấm lót Carton', N'0.0227272731', NULL)
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (47, N'DJ63-01119B', N'17', N'380', N'52.9', N'2', N'1', N'ABS', N'0103-003108', N'68', N'BB00001', N'Tấm lót Carton', N'0.076923077', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (48, N'DA61-11164A', N'17', N'380', N'30', N'12', N'0.5', N'PP/SSPP640', N'0103-007539', N'1.7', N'BB00001', N'Tấm lót Carton', N'0.0003125', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (49, N'DA64-04402A', N'18', N'650', N'45', N'2', N'1', N'ABS/HG-0760GP', N'0103-007331', N'0', N'BB00001', N'Tấm lót Carton', N'0.0625', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (50, N'DA64-04402B', N'18', N'650', N'45', N'2', N'1', N'Cũ: ABS/HG-0760GP', N'0103-004144', N'112.8', N'BB00001', N'Tấm lót Carton', N'0.0625', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (51, N'DA67-03601A', N'19', N'450', N'36', N'2', N'0.5', N'HIPS/''S834S1', N'0103-004130', N'32', N'BB00001', N'Tấm lót Carton', N'0.003333333', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (52, N'DA61-11385A', N'19', N'450', N'58', N'2', N'1', N'HIPS + MB', N'0103-004130
0103-007638', N'135.4', N'BB00001', N'Tấm lót Carton', N'0.0625', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (53, N'DA61-11393A', N'47', N'450', N'63', N'2', N'1', N'HIPS', N'0103-003252', N'139.1', N'BB00001', N'Tấm lót Carton', N'0.071428571', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (54, N'DA61-11262A', N'19', N'450', N'50', N'2', N'1', N'HIPS CNTK H304', N'0103-009131', N'122.2', N'', N'', N'', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (55, N'DJ63-01118A', N'20', N'850', N'54', N'2', N'1', N'ABS', N'0103-003108', N'106', N'BB00001', N'Tấm lót Carton', N'0.033333333', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (56, N'DJ61-01852A/ASS', N'20', N'850', N'53.8', N'2', N'1', N'ABS/ GP35', N'0103-003108', N'185.2', N'DJ62-00347A', N'SEAL-CASE SPONGE;SC8830,EVA FOAM,BLK', N'1', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (57, N'DJ61-01852A/ASS', N'20', N'850', N'53.8', N'2', N'1', N'ABS/ GP35', N'0103-003108', N'185.2', N'BB00001', N'Tấm lót Carton', N'0.125', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (58, N'DA63-07368A', N'21', N'450', N'29', N'4', N'1', N'GPPS/168N', N'0103-004921', N'15.2', N'BB00001', N'Tấm lót Carton', N'0.016666667', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (59, N'DA63-07368A', N'21', N'450', N'29', N'4', N'1', N'GPPS/168N', N'0103-004921', N'15.2', N'BB-00008', N'Bao Lami bag : 0.5*W570*L610', N'0.016666667', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (60, N'DJ63-01558/RED', N'22', N'650', N'60.4', N'2', N'1', N'ABS', N'0103-011218', N'303', N'BB00007', N'Bao Lami bag : 0.5*W400*L450', N'1', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (61, N'DJ63-01055/DEEP', N'22', N'650', N'60.4', N'2', N'1', N'ABS', N'0103-010365', N'303', N'BB00007', N'Bao Lami bag : 0.5*W400*L450', N'1', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (62, N'DJ63-01558M', N'22', N'650', N'60.4', N'2', N'1', N'ABS/GP35', N'0103-010366', N'303', N'BB00007', N'Bao Lami bag : 0.5*W400*L450', N'1', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (63, N'DJ63-01558N', N'22', N'650', N'60.4', N'2', N'1', N'ABS/''HG0760GP', N'0103-010859T', N'303.8', N'BB00007', N'Bao Lami bag : 0.5*W400*L450', N'1', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (64, N'DJ61-01846A', N'24', N'650', N'72', N'2', N'1', N'PP+MB', N'0103-001846
0103-002777 ', N'401.8', N'BB00001', N'Tấm lót Carton', N'0.1', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (65, N'DC63-01943A', N'42', N'850', N'63', N'2', N'1', N'ABS/''HG-0760GP', N'0103-003138', N'101.9', N'BB00001', N'Tấm lót Carton', N'0.071428571', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (66, N'DC63-01943A', N'42', N'850', N'63', N'2', N'1', N'ABS/''HG-0760GP', N'0103-003138', N'101.9', N'BB00007', N'Bao Lami bag : 0.5*W400*L450', N'1', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (67, N'DC63-02022A', N'25', N'850', N'54', N'1', N'1', N'ABS', N'0103-000990', N'449', N'', N'', N'', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (68, N'DA61-11386A', N'26', N'650', N'63', N'2', N'1', N'GPPS/168N', N'0103-004921', N'404', N'BB00001', N'Tấm lót Carton', N'0.142857143', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (69, N'DA61-11386A', N'26', N'650', N'63', N'2', N'1', N'', N'', NULL, N'BB-00008', N'Bao Lami bag : 0.5*W570*L610', N'1', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (70, N'DA63-08635A', N'26', N'650', N'65.5', N'2', N'1', N'GPPS/147F', N'0103-004921', N'336', N'BB00001', N'Tấm lót Carton', N'0.2', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (71, N'DA63-08635A', N'26', N'650', N'65.5', N'2', N'1', N'GPPS/147F', N'0103-004921', N'336', N'PE 006', N'Màng PE 450*450', N'30 cm', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (72, N'DA63-08634A', N'26', N'650', N'65.5', N'2', N'1', N'GPPS/147F', N'0103-004921', N'489', N'BB00001', N'Tấm lót Carton', N'0.333333333', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (73, N'DA63-08634A', N'26', N'650', N'65.5', N'2', N'1', N'GPPS/147F', N'0103-004921', N'489', N'BB00007', N'Bao Lami bag : 0.5*W400*L450', N'1', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (74, N'DJ61-01850A', N'26', N'650', N'67', N'2', N'1', N'ABS/ TX0510', N'0103-007555', N'333.3', N'BB00001', N'Tấm lót Carton', N'0.111111111', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (75, N'DJ61-01850A', N'26', N'650', N'67', N'2', N'1', N'ABS/ TX0510', N'0103-007555', N'333.3', N'BB00007', N'Bao Lami bag : 0.5*W400*L450', N'0.111111111', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (76, N'DA67-04350A', N'27', N'550', N'58', N'1', N'1', N'HIPS/S834S1', N'0103-003252', N'311.2', N'BB00001', N'Tấm lót Carton', N'0.05', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (77, N'DA67-04350A', N'27', N'550', N'58', N'1', N'1', N'HIPS/S834S1', N'0103-003252', N'311.2', N'BB-00008', N'Bao Lami bag : 0.5*W570*L610', N'1', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (78, N'DA67-04511B', N'49', N'550', N'65', N'1', N'1', N'PP', N'0103-001846', N'480', N'QA-DA97-17973A
(sử dụng cho code A)', N'QA Code', N'1', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (79, N'DA67-04511B', N'49', N'550', N'65', N'1', N'1', N'PP', N'0103-001846', N'480', N'QA-DA97-17973B
(sử dụng cho code B)', N'QA Code', N'1', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (80, N'DA67-04511B', N'49', N'550', N'65', N'1', N'1', N'PP', N'0103-001846', N'480', N'0203-006515', N'TAPE-POLY;OPP,T0.13,W24,WHITE', N'3 cm', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (81, N'DA67-04511B', N'49', N'550', N'65', N'1', N'1', N'PP', N'0103-001846', N'480', N'6902-002306', N'BAG PE;HDPE/PE FOAM,T0.012/T0.5,W600,L50', N'1', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (82, N'DA67-04511B', N'49', N'550', N'65', N'1', N'1', N'PP', N'0103-001846', N'480', N'BB00001', N'Tấm lót Carton', N'0.1', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (83, N'DJ64-01005Q', N'31', N'850', N'57', N'2', N'1', N'ABS', N'0103-010368', N'143.8', N'BB-00008', N'Bao Lami bag : 0.5*W570*L610', N'1', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (84, N'DJ64-01005Q', N'31', N'850', N'57', N'2', N'1', N'ABS', N'0103-010368', N'143.8', N'BB00001', N'Tấm lót Carton', N'0.071428571', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (85, N'DJ64-01005S', N'31', N'850', N'57', N'2', N'1', N'ABS', N'0103-010859T', N'143.8', N'BB-00008', N'Bao Lami bag : 0.5*W570*L610', N'1', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (86, N'DJ64-01005S', N'31', N'850', N'57', N'2', N'1', N'ABS', N'0103-010859T', N'143.8', N'BB00001', N'Tấm lót Carton', N'0.071428571', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (87, N'DJ64-01005R', N'31', N'850', N'57', N'2', N'1', N'ABS', N'0103-010365', N'143.8', N'BB-00008', N'Bao Lami bag : 0.5*W570*L610', N'1', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (88, N'DJ64-01005R', N'31', N'850', N'57', N'2', N'1', N'ABS', N'0103-010365', N'143.8', N'BB00001', N'Tấm lót Carton', N'0.071428571', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (89, N'DJ64-01005N', N'31', N'850', N'57', N'2', N'1', N'ABS', N'0103-003108', N'143.8', N'BB-00008', N'Bao Lami bag : 0.5*W570*L610', N'1', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (90, N'DJ64-01005N', N'31', N'850', N'57', N'2', N'1', N'ABS', N'0103-003108', N'143.8', N'BB00001', N'Tấm lót Carton', N'0.071428571', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (91, N'DJ66-00638D', N'33', N'150', N'48', N'2', N'0.5', N'PP,
M/B(PP)', N'0103-001846H
0103-002777', N'41.3', N'BB00001', N'Tấm lót Carton', N'0.025', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (92, N'DJ66-00474D', N'37', N'150', N'55', N'2', N'1', N'TPE/ S-700B', N'0103-002884', N'14.1', N'BB00001', N'Tấm lót Carton', N'0.025', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (93, N'DJ66-00474D', N'37', N'150', N'55', N'2', N'1', N'TPE/ S-700B', N'0103-002884', N'14.1', N'BB-00008', N'Bao Lami bag : 0.5*W570*L610', N'0.025', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (94, N'DJ61-01851A', N'48', N'450', N'57.2', N'2', N'1', N'ABS/ TX0510', N'0103-007555', N'137', N'BB00001', N'Tấm lót Carton', N'0.033333333', N'')
INSERT [dbo].[PicklistVatTu] ([STT], [Injection_Code], [Machine_No.], [Machine], [Cycle_Time], [Cavity], [Manpower], [Resin], [Resin Code], [Weight], [Vat_Tu_Code], [Ten_Vat_Tu], [Ty_Le], [Another_Code]) VALUES (95, N'DJ61-01851A', N'48', N'450', N'57.2', N'2', N'1', N'ABS/ TX0510', N'0103-007555', N'137', N'BB00007', N'Bao Lami bag : 0.5*W400*L450', N'1', N'')
SET IDENTITY_INSERT [dbo].[PicklistVatTu] OFF
INSERT [dbo].[Role] ([ID], [Name]) VALUES (N'ADD_CODELIST', N'Thêm code list')
INSERT [dbo].[Role] ([ID], [Name]) VALUES (N'ADD_PICKLISTVATTU', N'Thêm picklist vật tư')
INSERT [dbo].[Role] ([ID], [Name]) VALUES (N'ADD_RESINSTOCK', N'Thêm resin stock')
INSERT [dbo].[Role] ([ID], [Name]) VALUES (N'ADD_STOCK', N'Thêm stock')
INSERT [dbo].[Role] ([ID], [Name]) VALUES (N'ADD_USER', N'Thêm')
INSERT [dbo].[Role] ([ID], [Name]) VALUES (N'DELETE_RESINSTOCK', N'Xóa resin stock')
INSERT [dbo].[Role] ([ID], [Name]) VALUES (N'DELETE_STOCK', N'Xóa stock')
INSERT [dbo].[Role] ([ID], [Name]) VALUES (N'DELETE_USER', N'Xóa')
INSERT [dbo].[Role] ([ID], [Name]) VALUES (N'EDIT_CODELIST', N'Chỉnh sửa code list')
INSERT [dbo].[Role] ([ID], [Name]) VALUES (N'EDIT_KEHOACH', N'Chỉnh sửa kế hoạch')
INSERT [dbo].[Role] ([ID], [Name]) VALUES (N'EDIT_KQSX', N'Chỉnh sửa kết quả sản xuất')
INSERT [dbo].[Role] ([ID], [Name]) VALUES (N'EDIT_PICKLISTVATTU', N'Chỉnh sửa picklist vật tư')
INSERT [dbo].[Role] ([ID], [Name]) VALUES (N'EDIT_RESINSTOCK', N'Chỉnh sửa resin stock')
INSERT [dbo].[Role] ([ID], [Name]) VALUES (N'EDIT_STOCK', N'Chỉnh sửa stock')
INSERT [dbo].[Role] ([ID], [Name]) VALUES (N'EDIT_USER', N'Chỉnh sửa')
INSERT [dbo].[Role] ([ID], [Name]) VALUES (N'IMPORT_PSI', N'Import psi')
INSERT [dbo].[Role] ([ID], [Name]) VALUES (N'IMPORT_RESINSTOCK', N'Import resin stock')
INSERT [dbo].[Role] ([ID], [Name]) VALUES (N'IMPORT_STOCK', N'Import stock')
INSERT [dbo].[Role] ([ID], [Name]) VALUES (N'LAP_KEHOACH', N'Lập kế hoạch')
INSERT [dbo].[Role] ([ID], [Name]) VALUES (N'NHAP_KQSX', N'Nhập kết quả sản xuất')
INSERT [dbo].[Role] ([ID], [Name]) VALUES (N'VIEW_USER', N'Xem danh sách')
INSERT [dbo].[Role] ([ID], [Name]) VALUES (N'XUAT_EXCEL', N'Xuất Excel')
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([ID], [User], [Password], [Name], [FullName], [BoPhan], [NumberPhone], [Email], [Status], [GroupID], [VIEW_USER], [LAP_KEHOACH], [EDIT_KEHOACH], [XUAT_EXCEL], [ADD_STOCK], [DELETE_STOCK], [ADD_RESINSTOCK], [DELETE_RESINSTOCK], [EDIT_STOCK], [EDIT_RESINSTOCK], [EDIT_KQSX], [NHAP_KQSX], [ADD_CODELIST], [ADD_PICKLISTVATTU], [IMPORT_PSI], [IMPORT_STOCK], [IMPORT_RESINSTOCK], [EDIT_CODELIST], [EDIT_PICKLISTVATTU]) VALUES (1, N'admin', N'1410a924bed6b61a9e9f71d8f8dd8ee5', N'ADMIN', N'Nguyễn Lê Cao Tuấn', N'IT', 965940294, N'nlctuan@minhnguyenpm.com.vn', 0, N'MANAGER', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
INSERT [dbo].[UserGroup] ([ID], [Name]) VALUES (N'ADMIN', N'Quản trị')
INSERT [dbo].[UserGroup] ([ID], [Name]) VALUES (N'MEMBER', N'Thành viên')
INSERT [dbo].[UserGroup] ([ID], [Name]) VALUES (N'MOD', N'Moderatior')
/****** Object:  StoredProcedure [dbo].[Delete_ResinStock]    Script Date: 22/07/2019 14:52:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Delete_ResinStock]
as
truncate table ResinStock
GO
/****** Object:  StoredProcedure [dbo].[Delete_Stock]    Script Date: 22/07/2019 14:52:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Delete_Stock]
as
truncate table Stock
GO
/****** Object:  StoredProcedure [dbo].[GetViewPlan]    Script Date: 22/07/2019 14:52:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[GetViewPlan]
@week nvarchar(200),
@status nvarchar(200)
as
select * from DA_Injection where Week = @week and Statuss = @status
order by Code,Date asc
GO
/****** Object:  StoredProcedure [dbo].[Sum_PLD_DA]    Script Date: 22/07/2019 14:52:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Sum_PLD_DA]
@code_assy nvarchar(200),
@week nvarchar(200)
as
select sum([Plan(D)]) from DA_Injection where Code = @code_assy and Week = @week

GO
/****** Object:  StoredProcedure [dbo].[Sum_PLN_DA]    Script Date: 22/07/2019 14:52:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Sum_PLN_DA]
@code_assy nvarchar(200),
@week nvarchar(200)
as
select sum([Plan(N)]) from DA_Injection where Code = @code_assy and Week = @week

GO
/****** Object:  StoredProcedure [dbo].[Sum_Require_Assy]    Script Date: 22/07/2019 14:52:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Sum_Require_Assy]
@code_assy nvarchar(200),
@dateStart date,
@dateEnd date
as
select sum(Requirement) from Assy_PSI_Shortage where Code = @code_assy and Date >= @dateStart and Date <= @dateEnd

GO
/****** Object:  StoredProcedure [dbo].[Sum_Require_Ref]    Script Date: 22/07/2019 14:52:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Sum_Require_Ref]
@code_assy nvarchar(200),
@dateStart date,
@dateEnd date
as
select sum(Requirements) from [PSI-WM-REF] where Code = @code_assy and Date >= @dateStart and Date <= @dateEnd

GO
/****** Object:  StoredProcedure [dbo].[Sum_Require_VC]    Script Date: 22/07/2019 14:52:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Sum_Require_VC]
@code_assy nvarchar(200),
@dateStart date,
@dateEnd date
as
select sum(Requirement) from PSI_V_C where Code = @code_assy and Date >= @dateStart and Date <= @dateEnd

GO
/****** Object:  StoredProcedure [dbo].[SumAssy]    Script Date: 22/07/2019 14:52:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[SumAssy]
@code nvarchar(200)
as
select sum(Requirement) from Assy_PSI_Shortage where Code = @code
GO
USE [master]
GO
ALTER DATABASE [Plan] SET  READ_WRITE 
GO
