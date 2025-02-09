USE [StudentCourseScoreManagement]
GO
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_User_RoleType]
GO
ALTER TABLE [dbo].[TermCourseTime] DROP CONSTRAINT [FK_TermCourseTime_WeekDay]
GO
ALTER TABLE [dbo].[TermCourseTime] DROP CONSTRAINT [FK_TermCourseTime_TermCourse]
GO
ALTER TABLE [dbo].[TermCourseTime] DROP CONSTRAINT [FK_TermCourseTime_Lession]
GO
ALTER TABLE [dbo].[TermCourseMajor] DROP CONSTRAINT [FK_TermCourseMajor_TermCourse]
GO
ALTER TABLE [dbo].[TermCourseMajor] DROP CONSTRAINT [FK_TermCourseMajor_MajorId]
GO
ALTER TABLE [dbo].[TermCourse] DROP CONSTRAINT [FK_TermCourse_Term]
GO
ALTER TABLE [dbo].[TermCourse] DROP CONSTRAINT [FK_TermCourse_Teacher]
GO
ALTER TABLE [dbo].[TermCourse] DROP CONSTRAINT [FK_TermCourse_InYearId]
GO
ALTER TABLE [dbo].[TermCourse] DROP CONSTRAINT [FK_TermCourse_Credit]
GO
ALTER TABLE [dbo].[TermCourse] DROP CONSTRAINT [FK_TermCourse_CourseType]
GO
ALTER TABLE [dbo].[TermCourse] DROP CONSTRAINT [FK_TermCourse_CourseName]
GO
ALTER TABLE [dbo].[TermCourse] DROP CONSTRAINT [FK_TermCourse_ClassRoom]
GO
ALTER TABLE [dbo].[Term] DROP CONSTRAINT [FK_Term_TermType]
GO
ALTER TABLE [dbo].[Term] DROP CONSTRAINT [FK_Term_AcademicYear]
GO
ALTER TABLE [dbo].[TeachingManagerNameSeed] DROP CONSTRAINT [FK_TeachingManagerNameSeed_TeachingManagerNameSeed]
GO
ALTER TABLE [dbo].[TeacherNameSeed] DROP CONSTRAINT [FK_TeacherNameSeed_Department]
GO
ALTER TABLE [dbo].[Teacher] DROP CONSTRAINT [FK_Teacher_User]
GO
ALTER TABLE [dbo].[StudentNameSeed] DROP CONSTRAINT [FK_StudentNameSeed_Major]
GO
ALTER TABLE [dbo].[Student] DROP CONSTRAINT [FK_Student_User]
GO
ALTER TABLE [dbo].[Student] DROP CONSTRAINT [FK_Student_Major]
GO
ALTER TABLE [dbo].[Student] DROP CONSTRAINT [FK_Student_InYear]
GO
ALTER TABLE [dbo].[Score] DROP CONSTRAINT [FK_Score_TermCourseId]
GO
ALTER TABLE [dbo].[Score] DROP CONSTRAINT [FK_Score_StudentId]
GO
ALTER TABLE [dbo].[RoleRight] DROP CONSTRAINT [FK_RoleRight_Role]
GO
ALTER TABLE [dbo].[RoleRight] DROP CONSTRAINT [FK_RoleRight_Right]
GO
ALTER TABLE [dbo].[Major] DROP CONSTRAINT [FK_Major_Department]
GO
ALTER TABLE [dbo].[Admin] DROP CONSTRAINT [FK_Admin_User]
GO
/****** Object:  Index [IX_TermType]    Script Date: 2023/12/15 5:41:06 ******/
ALTER TABLE [dbo].[TermType] DROP CONSTRAINT [IX_TermType]
GO
/****** Object:  Index [IX_TermCourseTime]    Script Date: 2023/12/15 5:41:06 ******/
ALTER TABLE [dbo].[TermCourseTime] DROP CONSTRAINT [IX_TermCourseTime]
GO
/****** Object:  Index [IX_TermCourseMajor]    Script Date: 2023/12/15 5:41:06 ******/
ALTER TABLE [dbo].[TermCourseMajor] DROP CONSTRAINT [IX_TermCourseMajor]
GO
/****** Object:  Index [IX_Term]    Script Date: 2023/12/15 5:41:06 ******/
ALTER TABLE [dbo].[Term] DROP CONSTRAINT [IX_Term]
GO
/****** Object:  Index [IX_Teacher]    Script Date: 2023/12/15 5:41:06 ******/
ALTER TABLE [dbo].[Teacher] DROP CONSTRAINT [IX_Teacher]
GO
/****** Object:  Index [IX_Student]    Script Date: 2023/12/15 5:41:06 ******/
ALTER TABLE [dbo].[Student] DROP CONSTRAINT [IX_Student]
GO
/****** Object:  Index [IX_Score]    Script Date: 2023/12/15 5:41:06 ******/
ALTER TABLE [dbo].[Score] DROP CONSTRAINT [IX_Score]
GO
/****** Object:  Index [IX_Major]    Script Date: 2023/12/15 5:41:06 ******/
ALTER TABLE [dbo].[Major] DROP CONSTRAINT [IX_Major]
GO
/****** Object:  Index [IX_InYear]    Script Date: 2023/12/15 5:41:06 ******/
ALTER TABLE [dbo].[InYear] DROP CONSTRAINT [IX_InYear]
GO
/****** Object:  Index [IX_Department]    Script Date: 2023/12/15 5:41:06 ******/
ALTER TABLE [dbo].[Department] DROP CONSTRAINT [IX_Department]
GO
/****** Object:  Index [IX_Credit]    Script Date: 2023/12/15 5:41:06 ******/
ALTER TABLE [dbo].[Credit] DROP CONSTRAINT [IX_Credit]
GO
/****** Object:  Index [IX_CourseType]    Script Date: 2023/12/15 5:41:06 ******/
ALTER TABLE [dbo].[CourseType] DROP CONSTRAINT [IX_CourseType]
GO
/****** Object:  Index [IX_Course]    Script Date: 2023/12/15 5:41:06 ******/
ALTER TABLE [dbo].[Course] DROP CONSTRAINT [IX_Course]
GO
/****** Object:  Index [IX_ClassRoom]    Script Date: 2023/12/15 5:41:06 ******/
ALTER TABLE [dbo].[ClassRoom] DROP CONSTRAINT [IX_ClassRoom]
GO
/****** Object:  Index [IX_Admin]    Script Date: 2023/12/15 5:41:06 ******/
ALTER TABLE [dbo].[Admin] DROP CONSTRAINT [IX_Admin]
GO
/****** Object:  Index [IX_AcademicYear]    Script Date: 2023/12/15 5:41:06 ******/
ALTER TABLE [dbo].[AcademicYear] DROP CONSTRAINT [IX_AcademicYear]
GO
/****** Object:  Table [dbo].[WeekDay]    Script Date: 2023/12/15 5:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WeekDay]') AND type in (N'U'))
DROP TABLE [dbo].[WeekDay]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2023/12/15 5:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
DROP TABLE [dbo].[User]
GO
/****** Object:  Table [dbo].[TermType]    Script Date: 2023/12/15 5:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TermType]') AND type in (N'U'))
DROP TABLE [dbo].[TermType]
GO
/****** Object:  Table [dbo].[TermCourseTime]    Script Date: 2023/12/15 5:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TermCourseTime]') AND type in (N'U'))
DROP TABLE [dbo].[TermCourseTime]
GO
/****** Object:  Table [dbo].[TermCourseMajor]    Script Date: 2023/12/15 5:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TermCourseMajor]') AND type in (N'U'))
DROP TABLE [dbo].[TermCourseMajor]
GO
/****** Object:  Table [dbo].[TermCourse]    Script Date: 2023/12/15 5:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TermCourse]') AND type in (N'U'))
DROP TABLE [dbo].[TermCourse]
GO
/****** Object:  Table [dbo].[Term]    Script Date: 2023/12/15 5:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Term]') AND type in (N'U'))
DROP TABLE [dbo].[Term]
GO
/****** Object:  Table [dbo].[TeachingManagerNameSeed]    Script Date: 2023/12/15 5:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TeachingManagerNameSeed]') AND type in (N'U'))
DROP TABLE [dbo].[TeachingManagerNameSeed]
GO
/****** Object:  Table [dbo].[TeacherNameSeed]    Script Date: 2023/12/15 5:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TeacherNameSeed]') AND type in (N'U'))
DROP TABLE [dbo].[TeacherNameSeed]
GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 2023/12/15 5:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Teacher]') AND type in (N'U'))
DROP TABLE [dbo].[Teacher]
GO
/****** Object:  Table [dbo].[SysRight]    Script Date: 2023/12/15 5:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SysRight]') AND type in (N'U'))
DROP TABLE [dbo].[SysRight]
GO
/****** Object:  Table [dbo].[StudentNameSeed]    Script Date: 2023/12/15 5:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StudentNameSeed]') AND type in (N'U'))
DROP TABLE [dbo].[StudentNameSeed]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 2023/12/15 5:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Student]') AND type in (N'U'))
DROP TABLE [dbo].[Student]
GO
/****** Object:  Table [dbo].[Score]    Script Date: 2023/12/15 5:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Score]') AND type in (N'U'))
DROP TABLE [dbo].[Score]
GO
/****** Object:  Table [dbo].[RoleRight]    Script Date: 2023/12/15 5:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleRight]') AND type in (N'U'))
DROP TABLE [dbo].[RoleRight]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 2023/12/15 5:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role]') AND type in (N'U'))
DROP TABLE [dbo].[Role]
GO
/****** Object:  Table [dbo].[Major]    Script Date: 2023/12/15 5:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Major]') AND type in (N'U'))
DROP TABLE [dbo].[Major]
GO
/****** Object:  Table [dbo].[Lession]    Script Date: 2023/12/15 5:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Lession]') AND type in (N'U'))
DROP TABLE [dbo].[Lession]
GO
/****** Object:  Table [dbo].[InYear]    Script Date: 2023/12/15 5:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InYear]') AND type in (N'U'))
DROP TABLE [dbo].[InYear]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 2023/12/15 5:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Department]') AND type in (N'U'))
DROP TABLE [dbo].[Department]
GO
/****** Object:  Table [dbo].[Credit]    Script Date: 2023/12/15 5:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Credit]') AND type in (N'U'))
DROP TABLE [dbo].[Credit]
GO
/****** Object:  Table [dbo].[CourseType]    Script Date: 2023/12/15 5:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CourseType]') AND type in (N'U'))
DROP TABLE [dbo].[CourseType]
GO
/****** Object:  Table [dbo].[CourseScore]    Script Date: 2023/12/15 5:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CourseScore]') AND type in (N'U'))
DROP TABLE [dbo].[CourseScore]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 2023/12/15 5:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Course]') AND type in (N'U'))
DROP TABLE [dbo].[Course]
GO
/****** Object:  Table [dbo].[ClassRoom]    Script Date: 2023/12/15 5:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClassRoom]') AND type in (N'U'))
DROP TABLE [dbo].[ClassRoom]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 2023/12/15 5:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin]') AND type in (N'U'))
DROP TABLE [dbo].[Admin]
GO
/****** Object:  Table [dbo].[AcademicYear]    Script Date: 2023/12/15 5:41:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AcademicYear]') AND type in (N'U'))
DROP TABLE [dbo].[AcademicYear]
GO
USE [master]
GO
/****** Object:  Database [StudentCourseScoreManagement]    Script Date: 2023/12/15 5:41:06 ******/
DROP DATABASE [StudentCourseScoreManagement]
GO
/****** Object:  Database [StudentCourseScoreManagement]    Script Date: 2023/12/15 5:41:06 ******/
CREATE DATABASE [StudentCourseScoreManagement] ON  PRIMARY 
( NAME = N'StudentCourseScoreManagement', FILENAME = N'd:\StudentCourseScoreManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'StudentCourseScoreManagement_log', FILENAME = N'd:\StudentCourseScoreManagement_log.ldf' , SIZE = 4224KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [StudentCourseScoreManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [StudentCourseScoreManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [StudentCourseScoreManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [StudentCourseScoreManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [StudentCourseScoreManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [StudentCourseScoreManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [StudentCourseScoreManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [StudentCourseScoreManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [StudentCourseScoreManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [StudentCourseScoreManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [StudentCourseScoreManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [StudentCourseScoreManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [StudentCourseScoreManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [StudentCourseScoreManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [StudentCourseScoreManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [StudentCourseScoreManagement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [StudentCourseScoreManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [StudentCourseScoreManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [StudentCourseScoreManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [StudentCourseScoreManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [StudentCourseScoreManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [StudentCourseScoreManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [StudentCourseScoreManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [StudentCourseScoreManagement] SET RECOVERY FULL 
GO
ALTER DATABASE [StudentCourseScoreManagement] SET  MULTI_USER 
GO
ALTER DATABASE [StudentCourseScoreManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [StudentCourseScoreManagement] SET DB_CHAINING OFF 
GO
USE [StudentCourseScoreManagement]
GO
/****** Object:  Table [dbo].[AcademicYear]    Script Date: 2023/12/15 5:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AcademicYear](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Year] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_AcademicYear] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 2023/12/15 5:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Name] [nvarchar](5) NOT NULL,
	[Sex] [nchar](1) NOT NULL,
	[BirthDay] [date] NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClassRoom]    Script Date: 2023/12/15 5:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassRoom](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClassRoomNo] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_ClassRoom] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 2023/12/15 5:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CourseName] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Course_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CourseScore]    Script Date: 2023/12/15 5:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseScore](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[Score] [int] NOT NULL,
 CONSTRAINT [PK_CourseScore] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CourseType]    Script Date: 2023/12/15 5:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CourseTypeName] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_CourseType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Credit]    Script Date: 2023/12/15 5:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Credit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreditValue] [float] NOT NULL,
 CONSTRAINT [PK_Credit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 2023/12/15 5:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InYear]    Script Date: 2023/12/15 5:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InYear](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InYearName] [nvarchar](5) NOT NULL,
 CONSTRAINT [PK_InYear] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lession]    Script Date: 2023/12/15 5:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lession](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LessionOrderName] [nvarchar](4) NOT NULL,
	[LessionTime] [nvarchar](14) NOT NULL,
 CONSTRAINT [PK_Lession] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Major]    Script Date: 2023/12/15 5:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Major](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MajorName] [nvarchar](20) NOT NULL,
	[DepartmentId] [int] NOT NULL,
 CONSTRAINT [PK_Major] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 2023/12/15 5:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleType] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](10) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleRight]    Script Date: 2023/12/15 5:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleRight](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleType] [int] NOT NULL,
	[RightId] [int] NOT NULL,
	[IsOpen] [bit] NOT NULL,
	[OpenStartTime] [date] NULL,
	[OpenEndTime] [date] NULL,
 CONSTRAINT [PK_RoleRight] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Score]    Script Date: 2023/12/15 5:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Score](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[TermCourseId] [int] NOT NULL,
	[ScoreValue] [float] NOT NULL,
 CONSTRAINT [PK_Score] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 2023/12/15 5:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Name] [nvarchar](5) NOT NULL,
	[Sex] [nchar](1) NOT NULL,
	[InYearId] [int] NOT NULL,
	[Birthday] [date] NULL,
	[InDate] [date] NULL,
	[MajorId] [int] NOT NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentNameSeed]    Script Date: 2023/12/15 5:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentNameSeed](
	[Year] [nchar](4) NOT NULL,
	[MajorId] [int] NOT NULL,
	[CurrentSeed] [int] NOT NULL,
 CONSTRAINT [PK_StudentNameSeed] PRIMARY KEY CLUSTERED 
(
	[Year] ASC,
	[MajorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SysRight]    Script Date: 2023/12/15 5:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysRight](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RightName] [nvarchar](40) NOT NULL,
	[RightType] [nvarchar](10) NOT NULL,
	[Parent] [int] NULL,
	[Url] [nvarchar](128) NULL,
 CONSTRAINT [PK_Right] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 2023/12/15 5:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teacher](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Name] [nvarchar](5) NOT NULL,
	[Sex] [nchar](1) NOT NULL,
	[Birthday] [date] NULL,
	[InDate] [date] NULL,
	[DepartmentId] [int] NOT NULL,
 CONSTRAINT [PK_Teacher] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TeacherNameSeed]    Script Date: 2023/12/15 5:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeacherNameSeed](
	[Year] [nchar](4) NOT NULL,
	[DepartmentId] [int] NOT NULL,
	[CurrentSeed] [int] NOT NULL,
 CONSTRAINT [PK_TeacherNameSeed] PRIMARY KEY CLUSTERED 
(
	[Year] ASC,
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TeachingManagerNameSeed]    Script Date: 2023/12/15 5:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeachingManagerNameSeed](
	[Year] [nchar](4) NOT NULL,
	[CurrentSeed] [int] NOT NULL,
 CONSTRAINT [PK_TeachingManagerNameSeed] PRIMARY KEY CLUSTERED 
(
	[Year] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Term]    Script Date: 2023/12/15 5:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Term](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[YearId] [int] NOT NULL,
	[TermType] [int] NOT NULL,
	[TermBeginDate] [date] NULL,
	[TermEndDate] [date] NULL,
 CONSTRAINT [PK_Term] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TermCourse]    Script Date: 2023/12/15 5:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TermCourse](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InYearId] [int] NOT NULL,
	[TermId] [int] NOT NULL,
	[CourseNameId] [int] NOT NULL,
	[CourseTypeId] [int] NOT NULL,
	[TeacherId] [int] NOT NULL,
	[CreditId] [int] NOT NULL,
	[ClassRoomId] [int] NOT NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TermCourseMajor]    Script Date: 2023/12/15 5:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TermCourseMajor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TermCourseId] [int] NOT NULL,
	[MajorId] [int] NOT NULL,
 CONSTRAINT [PK_TermCourseMajor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TermCourseTime]    Script Date: 2023/12/15 5:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TermCourseTime](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TermCourseId] [int] NOT NULL,
	[WeekDayId] [int] NOT NULL,
	[LessionId] [int] NOT NULL,
 CONSTRAINT [PK_TermCourseTime] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TermType]    Script Date: 2023/12/15 5:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TermType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TermTypeName] [nvarchar](4) NOT NULL,
 CONSTRAINT [PK_TermType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2023/12/15 5:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LoginName] [nvarchar](15) NOT NULL,
	[LoginPassword] [varchar](50) NOT NULL,
	[RoleType] [int] NOT NULL,
	[Email] [nvarchar](64) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WeekDay]    Script Date: 2023/12/15 5:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WeekDay](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WeekDayName] [nvarchar](3) NULL,
 CONSTRAINT [PK_WeekDay] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AcademicYear] ON 

INSERT [dbo].[AcademicYear] ([Id], [Year]) VALUES (1, N'2021-2022')
INSERT [dbo].[AcademicYear] ([Id], [Year]) VALUES (3, N'2023-2024')
SET IDENTITY_INSERT [dbo].[AcademicYear] OFF
GO
SET IDENTITY_INSERT [dbo].[Admin] ON 

INSERT [dbo].[Admin] ([Id], [UserId], [Name], [Sex], [BirthDay]) VALUES (1002, 2065, N'李二', N'男', CAST(N'1988-04-01' AS Date))
SET IDENTITY_INSERT [dbo].[Admin] OFF
GO
SET IDENTITY_INSERT [dbo].[ClassRoom] ON 

INSERT [dbo].[ClassRoom] ([Id], [ClassRoomNo]) VALUES (8, N'A101')
INSERT [dbo].[ClassRoom] ([Id], [ClassRoomNo]) VALUES (1, N'A-1-01')
INSERT [dbo].[ClassRoom] ([Id], [ClassRoomNo]) VALUES (9, N'A102')
INSERT [dbo].[ClassRoom] ([Id], [ClassRoomNo]) VALUES (10, N'A103')
SET IDENTITY_INSERT [dbo].[ClassRoom] OFF
GO
SET IDENTITY_INSERT [dbo].[Course] ON 

INSERT [dbo].[Course] ([Id], [CourseName]) VALUES (1, N'C语言程序设计')
INSERT [dbo].[Course] ([Id], [CourseName]) VALUES (3, N'邓小平理论')
INSERT [dbo].[Course] ([Id], [CourseName]) VALUES (2, N'毛泽东思想')
INSERT [dbo].[Course] ([Id], [CourseName]) VALUES (4, N'英语（一）')
SET IDENTITY_INSERT [dbo].[Course] OFF
GO
SET IDENTITY_INSERT [dbo].[CourseType] ON 

INSERT [dbo].[CourseType] ([Id], [CourseTypeName]) VALUES (1, N'公共必修课')
INSERT [dbo].[CourseType] ([Id], [CourseTypeName]) VALUES (2, N'公共选修课')
INSERT [dbo].[CourseType] ([Id], [CourseTypeName]) VALUES (3, N'专业必修课')
INSERT [dbo].[CourseType] ([Id], [CourseTypeName]) VALUES (4, N'专业选修课')
SET IDENTITY_INSERT [dbo].[CourseType] OFF
GO
SET IDENTITY_INSERT [dbo].[Credit] ON 

INSERT [dbo].[Credit] ([Id], [CreditValue]) VALUES (1, 0.5)
INSERT [dbo].[Credit] ([Id], [CreditValue]) VALUES (2, 1)
INSERT [dbo].[Credit] ([Id], [CreditValue]) VALUES (3, 1.5)
INSERT [dbo].[Credit] ([Id], [CreditValue]) VALUES (4, 2)
INSERT [dbo].[Credit] ([Id], [CreditValue]) VALUES (5, 2.5)
INSERT [dbo].[Credit] ([Id], [CreditValue]) VALUES (6, 3)
INSERT [dbo].[Credit] ([Id], [CreditValue]) VALUES (7, 3.5)
INSERT [dbo].[Credit] ([Id], [CreditValue]) VALUES (8, 4)
SET IDENTITY_INSERT [dbo].[Credit] OFF
GO
SET IDENTITY_INSERT [dbo].[Department] ON 

INSERT [dbo].[Department] ([Id], [DepartmentName]) VALUES (9, N'航天工程系')
INSERT [dbo].[Department] ([Id], [DepartmentName]) VALUES (1, N'计算机系')
INSERT [dbo].[Department] ([Id], [DepartmentName]) VALUES (3, N'金融系')
INSERT [dbo].[Department] ([Id], [DepartmentName]) VALUES (2, N'土木工程系')
SET IDENTITY_INSERT [dbo].[Department] OFF
GO
SET IDENTITY_INSERT [dbo].[InYear] ON 

INSERT [dbo].[InYear] ([Id], [InYearName]) VALUES (1, N'2021')
INSERT [dbo].[InYear] ([Id], [InYearName]) VALUES (2, N'2023')
SET IDENTITY_INSERT [dbo].[InYear] OFF
GO
SET IDENTITY_INSERT [dbo].[Lession] ON 

INSERT [dbo].[Lession] ([Id], [LessionOrderName], [LessionTime]) VALUES (1, N'第一节', N'08:00 - 08:50')
INSERT [dbo].[Lession] ([Id], [LessionOrderName], [LessionTime]) VALUES (2, N'第二节', N'09:00 - 09:50')
INSERT [dbo].[Lession] ([Id], [LessionOrderName], [LessionTime]) VALUES (3, N'第三节', N'10:00 - 10:50')
INSERT [dbo].[Lession] ([Id], [LessionOrderName], [LessionTime]) VALUES (4, N'第四节', N'11:00 - 11:50')
INSERT [dbo].[Lession] ([Id], [LessionOrderName], [LessionTime]) VALUES (5, N'第五节', N'14:00 - 14:50')
INSERT [dbo].[Lession] ([Id], [LessionOrderName], [LessionTime]) VALUES (6, N'第六节', N'15:00 - 15:50')
INSERT [dbo].[Lession] ([Id], [LessionOrderName], [LessionTime]) VALUES (7, N'第七节', N'16:00 - 16:50')
INSERT [dbo].[Lession] ([Id], [LessionOrderName], [LessionTime]) VALUES (8, N'第八节', N'19:00 - 19:50')
INSERT [dbo].[Lession] ([Id], [LessionOrderName], [LessionTime]) VALUES (9, N'第九节', N'20:00 - 20:50')
SET IDENTITY_INSERT [dbo].[Lession] OFF
GO
SET IDENTITY_INSERT [dbo].[Major] ON 

INSERT [dbo].[Major] ([Id], [MajorName], [DepartmentId]) VALUES (1, N'计算机科学与技术', 1)
INSERT [dbo].[Major] ([Id], [MajorName], [DepartmentId]) VALUES (3, N'软件工程', 1)
INSERT [dbo].[Major] ([Id], [MajorName], [DepartmentId]) VALUES (4, N'大数据', 1)
SET IDENTITY_INSERT [dbo].[Major] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([RoleType], [RoleName]) VALUES (1, N'系统管理员     ')
INSERT [dbo].[Role] ([RoleType], [RoleName]) VALUES (2, N'教学管理员     ')
INSERT [dbo].[Role] ([RoleType], [RoleName]) VALUES (3, N'教师        ')
INSERT [dbo].[Role] ([RoleType], [RoleName]) VALUES (4, N'学生        ')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[RoleRight] ON 

INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (1, 1, 1, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (2, 1, 2, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (3, 1, 3, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (5, 1, 5, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (6, 2, 6, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (9, 2, 9, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (12, 2, 12, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (17, 2, 17, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (25, 4, 25, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (26, 4, 26, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (27, 4, 27, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (28, 3, 28, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (30, 3, 30, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (31, 1, 31, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (34, 1, 32, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (35, 2, 31, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (37, 2, 32, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (38, 3, 31, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (39, 3, 32, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (40, 4, 31, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (41, 4, 32, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (42, 2, 33, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (43, 2, 34, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (44, 2, 37, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (45, 2, 39, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (46, 2, 49, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (47, 2, 50, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (48, 2, 51, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (49, 2, 52, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (51, 4, 54, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (52, 3, 55, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (53, 1, 56, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (54, 2, 56, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (55, 3, 57, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (56, 4, 58, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (57, 2, 60, 1, NULL, NULL)
INSERT [dbo].[RoleRight] ([Id], [RoleType], [RightId], [IsOpen], [OpenStartTime], [OpenEndTime]) VALUES (58, 2, 61, 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[RoleRight] OFF
GO
SET IDENTITY_INSERT [dbo].[Score] ON 

INSERT [dbo].[Score] ([Id], [StudentId], [TermCourseId], [ScoreValue]) VALUES (2, 1, 1, 90)
INSERT [dbo].[Score] ([Id], [StudentId], [TermCourseId], [ScoreValue]) VALUES (4, 5, 2, 90)
SET IDENTITY_INSERT [dbo].[Score] OFF
GO
SET IDENTITY_INSERT [dbo].[Student] ON 

INSERT [dbo].[Student] ([Id], [UserId], [Name], [Sex], [InYearId], [Birthday], [InDate], [MajorId]) VALUES (1, 2052, N'李一', N'男', 1, CAST(N'2021-04-01' AS Date), CAST(N'2021-09-01' AS Date), 1)
INSERT [dbo].[Student] ([Id], [UserId], [Name], [Sex], [InYearId], [Birthday], [InDate], [MajorId]) VALUES (5, 2067, N'李楠', N'男', 2, CAST(N'1988-04-01' AS Date), CAST(N'2023-09-01' AS Date), 1)
SET IDENTITY_INSERT [dbo].[Student] OFF
GO
INSERT [dbo].[StudentNameSeed] ([Year], [MajorId], [CurrentSeed]) VALUES (N'2021', 1, 5)
INSERT [dbo].[StudentNameSeed] ([Year], [MajorId], [CurrentSeed]) VALUES (N'2023', 1, 2)
GO
SET IDENTITY_INSERT [dbo].[SysRight] ON 

INSERT [dbo].[SysRight] ([Id], [RightName], [RightType], [Parent], [Url]) VALUES (1, N'角色权限管理                                  ', N'Folder    ', NULL, NULL)
INSERT [dbo].[SysRight] ([Id], [RightName], [RightType], [Parent], [Url]) VALUES (2, N'角色权限设置                                  ', N'File      ', 1, N'/SystemManagement/RoleRightManagement/RightSetting.aspx')
INSERT [dbo].[SysRight] ([Id], [RightName], [RightType], [Parent], [Url]) VALUES (3, N'教学管理人员管理                                ', N'Folder    ', NULL, NULL)
INSERT [dbo].[SysRight] ([Id], [RightName], [RightType], [Parent], [Url]) VALUES (5, N'教学管理人员管理                     ', N'File      ', 3, N'/SystemManagement/TeachingManagerManagement/TeachingManagerManagement.aspx')
INSERT [dbo].[SysRight] ([Id], [RightName], [RightType], [Parent], [Url]) VALUES (6, N'教师管理                                    ', N'Folder    ', NULL, NULL)
INSERT [dbo].[SysRight] ([Id], [RightName], [RightType], [Parent], [Url]) VALUES (9, N'学生管理                                    ', N'Folder    ', NULL, NULL)
INSERT [dbo].[SysRight] ([Id], [RightName], [RightType], [Parent], [Url]) VALUES (12, N'学期和课程管理                                 ', N'Folder    ', NULL, NULL)
INSERT [dbo].[SysRight] ([Id], [RightName], [RightType], [Parent], [Url]) VALUES (17, N'学系和专业管理                                 ', N'Folder    ', NULL, NULL)
INSERT [dbo].[SysRight] ([Id], [RightName], [RightType], [Parent], [Url]) VALUES (25, N'选课管理                                    ', N'Folder    ', NULL, NULL)
INSERT [dbo].[SysRight] ([Id], [RightName], [RightType], [Parent], [Url]) VALUES (26, N'学生选课                                    ', N'File      ', 25, N'/CourseManagement/SelectCourse.aspx')
INSERT [dbo].[SysRight] ([Id], [RightName], [RightType], [Parent], [Url]) VALUES (27, N'成绩查询                                    ', N'File      ', 25, N'/CourseManagement/ScoreQuery.aspx')
INSERT [dbo].[SysRight] ([Id], [RightName], [RightType], [Parent], [Url]) VALUES (28, N'教课成绩管理                                  ', N'Folder    ', NULL, NULL)
INSERT [dbo].[SysRight] ([Id], [RightName], [RightType], [Parent], [Url]) VALUES (30, N'教课管理           ', N'File      ', 28, N'/ScoreManagement/TeachCourseManagement.aspx')
INSERT [dbo].[SysRight] ([Id], [RightName], [RightType], [Parent], [Url]) VALUES (31, N'用户管理                                    ', N'Folder    ', NULL, NULL)
INSERT [dbo].[SysRight] ([Id], [RightName], [RightType], [Parent], [Url]) VALUES (32, N'修改密码                                    ', N'File      ', 31, N'/UserManager/ChangePassword.aspx')
INSERT [dbo].[SysRight] ([Id], [RightName], [RightType], [Parent], [Url]) VALUES (33, N'年级管理                                    ', N'File      ', 12, N'/TeachingManagement/TermAndCourseManagement/InYearManagement.aspx')
INSERT [dbo].[SysRight] ([Id], [RightName], [RightType], [Parent], [Url]) VALUES (34, N'学年管理                                    ', N'FIle      ', 12, N'/TeachingManagement/TermAndCourseManagement/AcademicYearManagement.aspx')
INSERT [dbo].[SysRight] ([Id], [RightName], [RightType], [Parent], [Url]) VALUES (37, N'学期管理                                    ', N'File      ', 12, N'/TeachingManagement/TermAndCourseManagement/TermManagement.aspx')
INSERT [dbo].[SysRight] ([Id], [RightName], [RightType], [Parent], [Url]) VALUES (39, N'课程管理                                    ', N'File      ', 12, N'/TeachingManagement/TermAndCourseManagement/CourseManagement.aspx')
INSERT [dbo].[SysRight] ([Id], [RightName], [RightType], [Parent], [Url]) VALUES (49, N'学系管理                                    ', N'File      ', 17, N'/TeachingManagement/DepartmentAndMajorManagement/DepartmentManagement.aspx')
INSERT [dbo].[SysRight] ([Id], [RightName], [RightType], [Parent], [Url]) VALUES (50, N'专业管理                                    ', N'File      ', 17, N'/TeachingManagement/DepartmentAndMajorManagement/MajorManagement.aspx')
INSERT [dbo].[SysRight] ([Id], [RightName], [RightType], [Parent], [Url]) VALUES (51, N'教师管理', N'File', 6, N'/TeachingManagement/TeacherManagement/TeacherManagement.aspx')
INSERT [dbo].[SysRight] ([Id], [RightName], [RightType], [Parent], [Url]) VALUES (52, N'学生管理', N'File', 9, N'/TeachingManagement/StudentManagement/StudentManagement.aspx')
INSERT [dbo].[SysRight] ([Id], [RightName], [RightType], [Parent], [Url]) VALUES (53, N'学期课程管理', N'File', 12, N'/TeachingManagement/TermAndCourseManagement/TermCourseManagement.aspx')
INSERT [dbo].[SysRight] ([Id], [RightName], [RightType], [Parent], [Url]) VALUES (54, N'课程表查询', N'File', 25, N'/CourseManagement/CourseTableQuery.aspx')
INSERT [dbo].[SysRight] ([Id], [RightName], [RightType], [Parent], [Url]) VALUES (55, N'课程表查询', N'File', 28, N'/CourseManagement/CourseTableQuery.aspx')
INSERT [dbo].[SysRight] ([Id], [RightName], [RightType], [Parent], [Url]) VALUES (56, N'修改用户信息', N'File', 31, N'/UserManager/ModifyAdmin.aspx')
INSERT [dbo].[SysRight] ([Id], [RightName], [RightType], [Parent], [Url]) VALUES (57, N'修改用户信息', N'File', 31, N'/UserManager/ModifyTeacher.aspx')
INSERT [dbo].[SysRight] ([Id], [RightName], [RightType], [Parent], [Url]) VALUES (58, N'修改用户信息', N'File', 31, N'/UserManager/ModifyStudent.aspx')
INSERT [dbo].[SysRight] ([Id], [RightName], [RightType], [Parent], [Url]) VALUES (60, N'教室管理', N'File', 12, N'/TeachingManagement/TermAndCourseManagement/ClassRoomManagement.aspx')
INSERT [dbo].[SysRight] ([Id], [RightName], [RightType], [Parent], [Url]) VALUES (61, N'学期课程管理', N'File', 12, N'/TeachingManagement/TermAndCourseManagement/TermCourseManagement.aspx')
SET IDENTITY_INSERT [dbo].[SysRight] OFF
GO
SET IDENTITY_INSERT [dbo].[Teacher] ON 

INSERT [dbo].[Teacher] ([Id], [UserId], [Name], [Sex], [Birthday], [InDate], [DepartmentId]) VALUES (14, 2049, N'王一', N'男', CAST(N'1986-10-03' AS Date), CAST(N'2014-01-01' AS Date), 1)
INSERT [dbo].[Teacher] ([Id], [UserId], [Name], [Sex], [Birthday], [InDate], [DepartmentId]) VALUES (26, 2066, N'张北', N'男', CAST(N'1988-04-01' AS Date), CAST(N'2023-12-01' AS Date), 1)
SET IDENTITY_INSERT [dbo].[Teacher] OFF
GO
INSERT [dbo].[TeacherNameSeed] ([Year], [DepartmentId], [CurrentSeed]) VALUES (N'2014', 1, 25)
INSERT [dbo].[TeacherNameSeed] ([Year], [DepartmentId], [CurrentSeed]) VALUES (N'2021', 1, 2)
INSERT [dbo].[TeacherNameSeed] ([Year], [DepartmentId], [CurrentSeed]) VALUES (N'2023', 1, 2)
GO
INSERT [dbo].[TeachingManagerNameSeed] ([Year], [CurrentSeed]) VALUES (N'2021', 5)
INSERT [dbo].[TeachingManagerNameSeed] ([Year], [CurrentSeed]) VALUES (N'2023', 2)
GO
SET IDENTITY_INSERT [dbo].[Term] ON 

INSERT [dbo].[Term] ([Id], [YearId], [TermType], [TermBeginDate], [TermEndDate]) VALUES (1, 1, 1, CAST(N'2021-09-01' AS Date), CAST(N'2022-06-30' AS Date))
INSERT [dbo].[Term] ([Id], [YearId], [TermType], [TermBeginDate], [TermEndDate]) VALUES (2, 3, 1, CAST(N'2023-09-01' AS Date), CAST(N'2024-01-31' AS Date))
SET IDENTITY_INSERT [dbo].[Term] OFF
GO
SET IDENTITY_INSERT [dbo].[TermCourse] ON 

INSERT [dbo].[TermCourse] ([Id], [InYearId], [TermId], [CourseNameId], [CourseTypeId], [TeacherId], [CreditId], [ClassRoomId]) VALUES (1, 1, 1, 1, 3, 14, 6, 1)
INSERT [dbo].[TermCourse] ([Id], [InYearId], [TermId], [CourseNameId], [CourseTypeId], [TeacherId], [CreditId], [ClassRoomId]) VALUES (2, 2, 2, 1, 3, 26, 6, 8)
SET IDENTITY_INSERT [dbo].[TermCourse] OFF
GO
SET IDENTITY_INSERT [dbo].[TermCourseMajor] ON 

INSERT [dbo].[TermCourseMajor] ([Id], [TermCourseId], [MajorId]) VALUES (1, 1, 1)
INSERT [dbo].[TermCourseMajor] ([Id], [TermCourseId], [MajorId]) VALUES (3, 1, 3)
INSERT [dbo].[TermCourseMajor] ([Id], [TermCourseId], [MajorId]) VALUES (4, 1, 4)
INSERT [dbo].[TermCourseMajor] ([Id], [TermCourseId], [MajorId]) VALUES (5, 2, 1)
SET IDENTITY_INSERT [dbo].[TermCourseMajor] OFF
GO
SET IDENTITY_INSERT [dbo].[TermCourseTime] ON 

INSERT [dbo].[TermCourseTime] ([Id], [TermCourseId], [WeekDayId], [LessionId]) VALUES (1, 1, 1, 1)
INSERT [dbo].[TermCourseTime] ([Id], [TermCourseId], [WeekDayId], [LessionId]) VALUES (3, 1, 1, 2)
INSERT [dbo].[TermCourseTime] ([Id], [TermCourseId], [WeekDayId], [LessionId]) VALUES (4, 2, 1, 1)
INSERT [dbo].[TermCourseTime] ([Id], [TermCourseId], [WeekDayId], [LessionId]) VALUES (6, 2, 1, 2)
INSERT [dbo].[TermCourseTime] ([Id], [TermCourseId], [WeekDayId], [LessionId]) VALUES (7, 2, 1, 3)
SET IDENTITY_INSERT [dbo].[TermCourseTime] OFF
GO
SET IDENTITY_INSERT [dbo].[TermType] ON 

INSERT [dbo].[TermType] ([Id], [TermTypeName]) VALUES (1, N'上学期 ')
INSERT [dbo].[TermType] ([Id], [TermTypeName]) VALUES (2, N'下学期 ')
SET IDENTITY_INSERT [dbo].[TermType] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [LoginName], [LoginPassword], [RoleType], [Email]) VALUES (3, N'sysadmin  ', N'96e79218965eb72c92a549dd5a330112', 1, N'fangzhanpeng@qq.com')
INSERT [dbo].[User] ([Id], [LoginName], [LoginPassword], [RoleType], [Email]) VALUES (35, N'JX20140028', N'96e79218965eb72c92a549dd5a330112', 2, N'lixiaolong@qq.com')
INSERT [dbo].[User] ([Id], [LoginName], [LoginPassword], [RoleType], [Email]) VALUES (2049, N'JS2014001013', N'96e79218965eb72c92a549dd5a330112', 3, N'jackyfang888@qq.com')
INSERT [dbo].[User] ([Id], [LoginName], [LoginPassword], [RoleType], [Email]) VALUES (2052, N'XS2021001001', N'96e79218965eb72c92a549dd5a330112', 4, N'jackyfang888@qq.com')
INSERT [dbo].[User] ([Id], [LoginName], [LoginPassword], [RoleType], [Email]) VALUES (2065, N'JX20230001', N'96E79218965EB72C92A549DD5A330112', 2, N'jackyfang888@qq.com')
INSERT [dbo].[User] ([Id], [LoginName], [LoginPassword], [RoleType], [Email]) VALUES (2066, N'JS2023001001', N'96E79218965EB72C92A549DD5A330112', 3, NULL)
INSERT [dbo].[User] ([Id], [LoginName], [LoginPassword], [RoleType], [Email]) VALUES (2067, N'XS2023001001', N'96E79218965EB72C92A549DD5A330112', 4, NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[WeekDay] ON 

INSERT [dbo].[WeekDay] ([Id], [WeekDayName]) VALUES (1, N'周一')
INSERT [dbo].[WeekDay] ([Id], [WeekDayName]) VALUES (2, N'周二')
INSERT [dbo].[WeekDay] ([Id], [WeekDayName]) VALUES (3, N'周三')
INSERT [dbo].[WeekDay] ([Id], [WeekDayName]) VALUES (4, N'周四')
INSERT [dbo].[WeekDay] ([Id], [WeekDayName]) VALUES (5, N'周五')
INSERT [dbo].[WeekDay] ([Id], [WeekDayName]) VALUES (6, N'周六')
INSERT [dbo].[WeekDay] ([Id], [WeekDayName]) VALUES (7, N'周日')
SET IDENTITY_INSERT [dbo].[WeekDay] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AcademicYear]    Script Date: 2023/12/15 5:41:06 ******/
ALTER TABLE [dbo].[AcademicYear] ADD  CONSTRAINT [IX_AcademicYear] UNIQUE NONCLUSTERED 
(
	[Year] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Admin]    Script Date: 2023/12/15 5:41:06 ******/
ALTER TABLE [dbo].[Admin] ADD  CONSTRAINT [IX_Admin] UNIQUE NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ClassRoom]    Script Date: 2023/12/15 5:41:06 ******/
ALTER TABLE [dbo].[ClassRoom] ADD  CONSTRAINT [IX_ClassRoom] UNIQUE NONCLUSTERED 
(
	[ClassRoomNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Course]    Script Date: 2023/12/15 5:41:06 ******/
ALTER TABLE [dbo].[Course] ADD  CONSTRAINT [IX_Course] UNIQUE NONCLUSTERED 
(
	[CourseName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_CourseType]    Script Date: 2023/12/15 5:41:06 ******/
ALTER TABLE [dbo].[CourseType] ADD  CONSTRAINT [IX_CourseType] UNIQUE NONCLUSTERED 
(
	[CourseTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Credit]    Script Date: 2023/12/15 5:41:06 ******/
ALTER TABLE [dbo].[Credit] ADD  CONSTRAINT [IX_Credit] UNIQUE NONCLUSTERED 
(
	[CreditValue] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Department]    Script Date: 2023/12/15 5:41:06 ******/
ALTER TABLE [dbo].[Department] ADD  CONSTRAINT [IX_Department] UNIQUE NONCLUSTERED 
(
	[DepartmentName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_InYear]    Script Date: 2023/12/15 5:41:06 ******/
ALTER TABLE [dbo].[InYear] ADD  CONSTRAINT [IX_InYear] UNIQUE NONCLUSTERED 
(
	[InYearName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Major]    Script Date: 2023/12/15 5:41:06 ******/
ALTER TABLE [dbo].[Major] ADD  CONSTRAINT [IX_Major] UNIQUE NONCLUSTERED 
(
	[MajorName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Score]    Script Date: 2023/12/15 5:41:06 ******/
ALTER TABLE [dbo].[Score] ADD  CONSTRAINT [IX_Score] UNIQUE NONCLUSTERED 
(
	[StudentId] ASC,
	[TermCourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Student]    Script Date: 2023/12/15 5:41:06 ******/
ALTER TABLE [dbo].[Student] ADD  CONSTRAINT [IX_Student] UNIQUE NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Teacher]    Script Date: 2023/12/15 5:41:06 ******/
ALTER TABLE [dbo].[Teacher] ADD  CONSTRAINT [IX_Teacher] UNIQUE NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Term]    Script Date: 2023/12/15 5:41:06 ******/
ALTER TABLE [dbo].[Term] ADD  CONSTRAINT [IX_Term] UNIQUE NONCLUSTERED 
(
	[YearId] ASC,
	[TermType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TermCourseMajor]    Script Date: 2023/12/15 5:41:06 ******/
ALTER TABLE [dbo].[TermCourseMajor] ADD  CONSTRAINT [IX_TermCourseMajor] UNIQUE NONCLUSTERED 
(
	[TermCourseId] ASC,
	[MajorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TermCourseTime]    Script Date: 2023/12/15 5:41:06 ******/
ALTER TABLE [dbo].[TermCourseTime] ADD  CONSTRAINT [IX_TermCourseTime] UNIQUE NONCLUSTERED 
(
	[TermCourseId] ASC,
	[WeekDayId] ASC,
	[LessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_TermType]    Script Date: 2023/12/15 5:41:06 ******/
ALTER TABLE [dbo].[TermType] ADD  CONSTRAINT [IX_TermType] UNIQUE NONCLUSTERED 
(
	[TermTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Admin]  WITH NOCHECK ADD  CONSTRAINT [FK_Admin_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Admin] CHECK CONSTRAINT [FK_Admin_User]
GO
ALTER TABLE [dbo].[Major]  WITH CHECK ADD  CONSTRAINT [FK_Major_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO
ALTER TABLE [dbo].[Major] CHECK CONSTRAINT [FK_Major_Department]
GO
ALTER TABLE [dbo].[RoleRight]  WITH CHECK ADD  CONSTRAINT [FK_RoleRight_Right] FOREIGN KEY([RightId])
REFERENCES [dbo].[SysRight] ([Id])
GO
ALTER TABLE [dbo].[RoleRight] CHECK CONSTRAINT [FK_RoleRight_Right]
GO
ALTER TABLE [dbo].[RoleRight]  WITH CHECK ADD  CONSTRAINT [FK_RoleRight_Role] FOREIGN KEY([RoleType])
REFERENCES [dbo].[Role] ([RoleType])
GO
ALTER TABLE [dbo].[RoleRight] CHECK CONSTRAINT [FK_RoleRight_Role]
GO
ALTER TABLE [dbo].[Score]  WITH CHECK ADD  CONSTRAINT [FK_Score_StudentId] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([Id])
GO
ALTER TABLE [dbo].[Score] CHECK CONSTRAINT [FK_Score_StudentId]
GO
ALTER TABLE [dbo].[Score]  WITH CHECK ADD  CONSTRAINT [FK_Score_TermCourseId] FOREIGN KEY([TermCourseId])
REFERENCES [dbo].[TermCourse] ([Id])
GO
ALTER TABLE [dbo].[Score] CHECK CONSTRAINT [FK_Score_TermCourseId]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_InYear] FOREIGN KEY([InYearId])
REFERENCES [dbo].[InYear] ([Id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_InYear]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_Major] FOREIGN KEY([MajorId])
REFERENCES [dbo].[Major] ([Id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Major]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_User]
GO
ALTER TABLE [dbo].[StudentNameSeed]  WITH CHECK ADD  CONSTRAINT [FK_StudentNameSeed_Major] FOREIGN KEY([MajorId])
REFERENCES [dbo].[Major] ([Id])
GO
ALTER TABLE [dbo].[StudentNameSeed] CHECK CONSTRAINT [FK_StudentNameSeed_Major]
GO
ALTER TABLE [dbo].[Teacher]  WITH CHECK ADD  CONSTRAINT [FK_Teacher_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Teacher] CHECK CONSTRAINT [FK_Teacher_User]
GO
ALTER TABLE [dbo].[TeacherNameSeed]  WITH CHECK ADD  CONSTRAINT [FK_TeacherNameSeed_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO
ALTER TABLE [dbo].[TeacherNameSeed] CHECK CONSTRAINT [FK_TeacherNameSeed_Department]
GO
ALTER TABLE [dbo].[TeachingManagerNameSeed]  WITH CHECK ADD  CONSTRAINT [FK_TeachingManagerNameSeed_TeachingManagerNameSeed] FOREIGN KEY([Year])
REFERENCES [dbo].[TeachingManagerNameSeed] ([Year])
GO
ALTER TABLE [dbo].[TeachingManagerNameSeed] CHECK CONSTRAINT [FK_TeachingManagerNameSeed_TeachingManagerNameSeed]
GO
ALTER TABLE [dbo].[Term]  WITH CHECK ADD  CONSTRAINT [FK_Term_AcademicYear] FOREIGN KEY([YearId])
REFERENCES [dbo].[AcademicYear] ([Id])
GO
ALTER TABLE [dbo].[Term] CHECK CONSTRAINT [FK_Term_AcademicYear]
GO
ALTER TABLE [dbo].[Term]  WITH CHECK ADD  CONSTRAINT [FK_Term_TermType] FOREIGN KEY([TermType])
REFERENCES [dbo].[TermType] ([Id])
GO
ALTER TABLE [dbo].[Term] CHECK CONSTRAINT [FK_Term_TermType]
GO
ALTER TABLE [dbo].[TermCourse]  WITH CHECK ADD  CONSTRAINT [FK_TermCourse_ClassRoom] FOREIGN KEY([ClassRoomId])
REFERENCES [dbo].[ClassRoom] ([Id])
GO
ALTER TABLE [dbo].[TermCourse] CHECK CONSTRAINT [FK_TermCourse_ClassRoom]
GO
ALTER TABLE [dbo].[TermCourse]  WITH CHECK ADD  CONSTRAINT [FK_TermCourse_CourseName] FOREIGN KEY([CourseNameId])
REFERENCES [dbo].[Course] ([Id])
GO
ALTER TABLE [dbo].[TermCourse] CHECK CONSTRAINT [FK_TermCourse_CourseName]
GO
ALTER TABLE [dbo].[TermCourse]  WITH CHECK ADD  CONSTRAINT [FK_TermCourse_CourseType] FOREIGN KEY([CourseTypeId])
REFERENCES [dbo].[CourseType] ([Id])
GO
ALTER TABLE [dbo].[TermCourse] CHECK CONSTRAINT [FK_TermCourse_CourseType]
GO
ALTER TABLE [dbo].[TermCourse]  WITH CHECK ADD  CONSTRAINT [FK_TermCourse_Credit] FOREIGN KEY([CreditId])
REFERENCES [dbo].[Credit] ([Id])
GO
ALTER TABLE [dbo].[TermCourse] CHECK CONSTRAINT [FK_TermCourse_Credit]
GO
ALTER TABLE [dbo].[TermCourse]  WITH CHECK ADD  CONSTRAINT [FK_TermCourse_InYearId] FOREIGN KEY([InYearId])
REFERENCES [dbo].[InYear] ([Id])
GO
ALTER TABLE [dbo].[TermCourse] CHECK CONSTRAINT [FK_TermCourse_InYearId]
GO
ALTER TABLE [dbo].[TermCourse]  WITH CHECK ADD  CONSTRAINT [FK_TermCourse_Teacher] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[Teacher] ([Id])
GO
ALTER TABLE [dbo].[TermCourse] CHECK CONSTRAINT [FK_TermCourse_Teacher]
GO
ALTER TABLE [dbo].[TermCourse]  WITH CHECK ADD  CONSTRAINT [FK_TermCourse_Term] FOREIGN KEY([TermId])
REFERENCES [dbo].[Term] ([Id])
GO
ALTER TABLE [dbo].[TermCourse] CHECK CONSTRAINT [FK_TermCourse_Term]
GO
ALTER TABLE [dbo].[TermCourseMajor]  WITH CHECK ADD  CONSTRAINT [FK_TermCourseMajor_MajorId] FOREIGN KEY([MajorId])
REFERENCES [dbo].[Major] ([Id])
GO
ALTER TABLE [dbo].[TermCourseMajor] CHECK CONSTRAINT [FK_TermCourseMajor_MajorId]
GO
ALTER TABLE [dbo].[TermCourseMajor]  WITH CHECK ADD  CONSTRAINT [FK_TermCourseMajor_TermCourse] FOREIGN KEY([TermCourseId])
REFERENCES [dbo].[TermCourse] ([Id])
GO
ALTER TABLE [dbo].[TermCourseMajor] CHECK CONSTRAINT [FK_TermCourseMajor_TermCourse]
GO
ALTER TABLE [dbo].[TermCourseTime]  WITH CHECK ADD  CONSTRAINT [FK_TermCourseTime_Lession] FOREIGN KEY([LessionId])
REFERENCES [dbo].[Lession] ([Id])
GO
ALTER TABLE [dbo].[TermCourseTime] CHECK CONSTRAINT [FK_TermCourseTime_Lession]
GO
ALTER TABLE [dbo].[TermCourseTime]  WITH CHECK ADD  CONSTRAINT [FK_TermCourseTime_TermCourse] FOREIGN KEY([TermCourseId])
REFERENCES [dbo].[TermCourse] ([Id])
GO
ALTER TABLE [dbo].[TermCourseTime] CHECK CONSTRAINT [FK_TermCourseTime_TermCourse]
GO
ALTER TABLE [dbo].[TermCourseTime]  WITH CHECK ADD  CONSTRAINT [FK_TermCourseTime_WeekDay] FOREIGN KEY([WeekDayId])
REFERENCES [dbo].[WeekDay] ([Id])
GO
ALTER TABLE [dbo].[TermCourseTime] CHECK CONSTRAINT [FK_TermCourseTime_WeekDay]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_RoleType] FOREIGN KEY([RoleType])
REFERENCES [dbo].[Role] ([RoleType])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_RoleType]
GO
USE [master]
GO
ALTER DATABASE [StudentCourseScoreManagement] SET  READ_WRITE 
GO
