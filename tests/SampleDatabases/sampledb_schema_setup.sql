﻿USE [master]
GO
/****** Object:  Database [SampleDB]    Script Date: 06/11/2020 11:10:26 ******/
CREATE DATABASE [SampleDB]
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [SampleDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SampleDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SampleDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SampleDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SampleDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SampleDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SampleDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [SampleDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SampleDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SampleDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SampleDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SampleDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SampleDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SampleDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SampleDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SampleDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SampleDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SampleDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SampleDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SampleDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SampleDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SampleDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SampleDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SampleDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SampleDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SampleDB] SET  MULTI_USER 
GO
ALTER DATABASE [SampleDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SampleDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SampleDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SampleDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SampleDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'SampleDB', N'ON'
GO
ALTER DATABASE [SampleDB] SET QUERY_STORE = OFF
GO
USE [SampleDB]
GO
/****** Object:  Schema [NUIBEQ70IZ4M49JJPRC240BXRG460QSPXA8LNQGZVCKG0S8EMWFEG9L22THP7EOB3N5RWXWT9YFRYGA832FZ5SQ76271KVPSQ1YPT0SD052R6W30GFE7CX6IQYQDISG1]    Script Date: 18/11/2020 11:57:53 ******/
CREATE SCHEMA [NUIBEQ70IZ4M49JJPRC240BXRG460QSPXA8LNQGZVCKG0S8EMWFEG9L22THP7EOB3N5RWXWT9YFRYGA832FZ5SQ76271KVPSQ1YPT0SD052R6W30GFE7CX6IQYQDISG1]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 06/11/2020 11:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[EmailAddress] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[NumberOfHats] [bigint] NOT NULL,
	[HatType] [nvarchar](50) NULL,
	[Cost] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[AllUsers]    Script Date: 06/11/2020 11:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[AllUsers]
AS
SELECT * FROM dbo.Users
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 06/11/2020 11:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[CountryCode] [nvarchar](50) NOT NULL,
	[CountryName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[CountryCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NumbersTable]    Script Date: 06/11/2020 11:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NumbersTable](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IntColumn] [int] NULL,
	[SmallIntColumn] [smallint] NULL,
	[BigIntColumn] [bigint] NULL,
	[DecimalColumn] [decimal](18, 0) NULL,
	[MoneyColumn] [money] NULL,
	[SmallMoneyColumn] [smallmoney] NULL,
	[NumericColumn] [numeric](18, 0) NULL,
	[FloatColumn] [float] NULL,
	[RealColumn] [real] NULL,
	[TinyIntColumn] [tinyint] NULL,
 CONSTRAINT [PK_NumbersTable] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAddresses]    Script Date: 06/11/2020 11:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAddresses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Address1] [nvarchar](50) NOT NULL,
	[Postcode] [nvarchar](10) NOT NULL,
	[Other] [nvarchar](50) NULL,
	[CountryCode] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserAddresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[UserAddresses]  WITH CHECK ADD  CONSTRAINT [FK_UserAddresses_Countries] FOREIGN KEY([CountryCode])
REFERENCES [dbo].[Countries] ([CountryCode])
GO
ALTER TABLE [dbo].[UserAddresses] CHECK CONSTRAINT [FK_UserAddresses_Countries]
GO
ALTER TABLE [dbo].[UserAddresses]  WITH CHECK ADD  CONSTRAINT [FK_UserAddresses_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserAddresses] CHECK CONSTRAINT [FK_UserAddresses_Users]
GO
/****** Object:  StoredProcedure [dbo].[AddUser]    Script Date: 06/11/2020 11:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddUser]
	@FirstName NVARCHAR(50)
	,@LastName NVARCHAR(50)
	,@EmailAddress NVARCHAR(50)
	,@StartDate DATETIME
	,@NumberOfHats INT
	,@Cost DECIMAL(18,2)
AS
BEGIN
	SET NOCOUNT ON;

    INSERT INTO dbo.Users
	(FirstName, LastName, EmailAddress, CreatedDate, StartDate, NumberOfHats, Cost)
	VALUES
	(@FirstName, @LastName, @EmailAddress, GETUTCDATE(), @StartDate, @NumberOfHats, @Cost)
END
GO
/****** Object:  StoredProcedure [dbo].[CountUsers]    Script Date: 06/11/2020 11:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CountUsers]
	@EmailAddress NVARCHAR(50)
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		COUNT(*) AS TotalUsers
	FROM
		dbo.Users
	WHERE
		EmailAddress = @EmailAddress
END
GO
/****** Object:  StoredProcedure [dbo].[GetUser]    Script Date: 06/11/2020 11:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetUser]
	@EmailAddress NVARCHAR(50)
AS
BEGIN

	SET NOCOUNT ON;

    SELECT
		FirstName
		,LastName
		,UserAddresses.Postcode
	FROM
		dbo.Users
	LEFT JOIN
		dbo.UserAddresses ON Users.Id = UserAddresses.UserId
	WHERE
		EmailAddress = @EmailAddress

	SELECT
		COUNT(*) AS TotalUsers
	FROM
		dbo.Users
	WHERE
		EmailAddress = @EmailAddress
END
GO
/****** Object:  Table [dbo].[IdentityOnlyTable]    Script Date: 18/11/2020 11:57:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[IdentityOnlyTable](
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_IdentityOnlyTable] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User's]    Script Date: 18/11/2020 11:57:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User's](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_User's] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [NUIBEQ70IZ4M49JJPRC240BXRG460QSPXA8LNQGZVCKG0S8EMWFEG9L22THP7EOB3N5RWXWT9YFRYGA832FZ5SQ76271KVPSQ1YPT0SD052R6W30GFE7CX6IQYQDISG1].[FXC15NX9HYBS0J8RHT6YHB9JJIPS2TWZQ2MA9C05I70WYG83LG877Q03X1XBGKLI5PRPQ2QZ74CFVMTQUOODFE2MARXI4069OUJTJB1VSAVO4XL32WHFH8HBKFNSQQHQ]    Script Date: 18/11/2020 12:23:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [NUIBEQ70IZ4M49JJPRC240BXRG460QSPXA8LNQGZVCKG0S8EMWFEG9L22THP7EOB3N5RWXWT9YFRYGA832FZ5SQ76271KVPSQ1YPT0SD052R6W30GFE7CX6IQYQDISG1].[FXC15NX9HYBS0J8RHT6YHB9JJIPS2TWZQ2MA9C05I70WYG83LG877Q03X1XBGKLI5PRPQ2QZ74CFVMTQUOODFE2MARXI4069OUJTJB1VSAVO4XL32WHFH8HBKFNSQQHQ](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_FXC15NX9HYBS0J8RHT6YHB9JJIPS2TWZQ2MA9C05I70WYG83LG877Q03X1XBGKLI5PRPQ2QZ74CFVMTQUOODFE2MARXI4069OUJTJB1VSAVO4XL32WHFH8HBKF] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoPrimaryKeyTable]    Script Date: 18/11/2020 12:23:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[NoPrimaryKeyTable](
	[Id] [int] NULL
) ON [PRIMARY]
GO

USE [master]
GO
ALTER DATABASE [SampleDB] SET  READ_WRITE 
GO
