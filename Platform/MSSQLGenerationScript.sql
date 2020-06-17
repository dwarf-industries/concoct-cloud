USE [master]
GO
/****** Object:  Database [RokonoControl]    Script Date: 6/17/2020 2:40:18 PM ******/
CREATE DATABASE [RokonoControl]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RokonoControl', FILENAME = N'/var/opt/mssql/data/RokonoControl.mdf' , SIZE = 13639680KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RokonoControl_log', FILENAME = N'/var/opt/mssql/data/RokonoControl_log.ldf' , SIZE = 466944KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [RokonoControl] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RokonoControl].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RokonoControl] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RokonoControl] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RokonoControl] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RokonoControl] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RokonoControl] SET ARITHABORT OFF 
GO
ALTER DATABASE [RokonoControl] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RokonoControl] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RokonoControl] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RokonoControl] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RokonoControl] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RokonoControl] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RokonoControl] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RokonoControl] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RokonoControl] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RokonoControl] SET  ENABLE_BROKER 
GO
ALTER DATABASE [RokonoControl] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RokonoControl] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RokonoControl] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RokonoControl] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RokonoControl] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RokonoControl] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RokonoControl] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RokonoControl] SET RECOVERY FULL 
GO
ALTER DATABASE [RokonoControl] SET  MULTI_USER 
GO
ALTER DATABASE [RokonoControl] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RokonoControl] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RokonoControl] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RokonoControl] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RokonoControl] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'RokonoControl', N'ON'
GO
ALTER DATABASE [RokonoControl] SET QUERY_STORE = OFF
GO
USE [RokonoControl]
GO
/****** Object:  User [RGSOC]    Script Date: 6/17/2020 2:40:18 PM ******/
CREATE USER [RGSOC] FOR LOGIN [RGSOC] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [RGSOC]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [RGSOC]
GO
/****** Object:  Table [dbo].[ApiKeys]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApiKeys](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FeatureName] [nvarchar](300) NULL,
	[SecretKey] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssociatedAccountNotes]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssociatedAccountNotes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NoteId] [int] NOT NULL,
	[UserAccountId] [int] NOT NULL,
	[ProjectId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssociatedAccountProjectNotificationRights]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssociatedAccountProjectNotificationRights](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NULL,
	[UserId] [int] NULL,
	[RightId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssociatedBoardWorkItems]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssociatedBoardWorkItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BoardId] [int] NOT NULL,
	[WorkItemId] [int] NOT NULL,
	[ProjectId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssociatedBranchCommits]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssociatedBranchCommits](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CommitId] [int] NOT NULL,
	[BranchId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssociatedChatChannelMessages]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssociatedChatChannelMessages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PublicMessageId] [int] NULL,
	[ChatChannelId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssociatedChatPersonalMessages]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssociatedChatPersonalMessages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NULL,
	[PublicMessageId] [int] NULL,
	[SenderId] [int] NULL,
	[ReciverId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssociatedChatRoomRights]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssociatedChatRoomRights](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ChatRoomId] [int] NOT NULL,
	[RightId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssociatedCommitFiles]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssociatedCommitFiles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FileId] [int] NULL,
	[CommitId] [int] NULL,
	[DateOfCommit] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssociatedDocumentationCategoryPage]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssociatedDocumentationCategoryPage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](max) NULL,
	[CategoryField] [int] NULL,
	[Title] [nvarchar](400) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssociatedProjectApiKeys]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssociatedProjectApiKeys](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NULL,
	[KeyId] [int] NULL,
	[ApiSecret] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssociatedProjectBoards]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssociatedProjectBoards](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BoardId] [int] NULL,
	[ProjectId] [int] NULL,
	[Position] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssociatedProjectBuilds]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssociatedProjectBuilds](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RepositoryId] [int] NULL,
	[BuildId] [int] NULL,
	[ProjectId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssociatedProjectChangelogs]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssociatedProjectChangelogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LogId] [int] NULL,
	[ProjectId] [int] NULL,
	[CurrentMonth] [int] NULL,
	[LogYear] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssociatedProjectFeedback]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssociatedProjectFeedback](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NULL,
	[MessageId] [int] NULL,
	[rating] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssociatedProjectIterations]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssociatedProjectIterations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[IterationId] [int] NOT NULL,
	[ActiveIteration] [int] NULL,
 CONSTRAINT [PK_AssociatedProjectIterations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssociatedProjectMemberRights]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssociatedProjectMemberRights](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RightsId] [int] NULL,
	[UserAccountId] [int] NULL,
	[ProjectId] [int] NULL,
 CONSTRAINT [PK_AssociatedProjectMemberRights] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssociatedProjectMembers]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssociatedProjectMembers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserAccountId] [int] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[RepositoryId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssociatedProjectNotifications]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssociatedProjectNotifications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NotificationId] [int] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[NewNotification] [int] NULL,
	[UserAccountId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssociatedProjectPublicDiscussions]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssociatedProjectPublicDiscussions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NULL,
	[PublicMessageId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssociatedProjectPublicMessages]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssociatedProjectPublicMessages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MessageId] [int] NOT NULL,
	[ProjectId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssociatedRepositoryBranches]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssociatedRepositoryBranches](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RepositoryId] [int] NOT NULL,
	[BranchId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssociatedUserChatNotifications]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssociatedUserChatNotifications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[ProjectId] [int] NULL,
	[ChatChannelId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssociatedUserChatRights]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssociatedUserChatRights](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[RightId] [int] NULL,
	[ProjectId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssociatedUserDashboardItemComponent]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssociatedUserDashboardItemComponent](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Item] [int] NULL,
	[ItemComponent] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssociatedUserNotifications]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssociatedUserNotifications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NotificationId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[NewNotification] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssociatedWorkItemChangelogs]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssociatedWorkItemChangelogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LogId] [int] NULL,
	[workitemId] [int] NULL,
	[ProjectId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssociatedWorkItemFiles]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssociatedWorkItemFiles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WorkItemId] [int] NULL,
	[FileId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssociatedWorkItemMessages]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssociatedWorkItemMessages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WorkItemId] [int] NOT NULL,
	[MessageId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssociatedWrorkItemChildren]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssociatedWrorkItemChildren](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WorkItemId] [int] NULL,
	[WorkItemChildId] [int] NULL,
	[RelationType] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Boards]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Boards](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RepositoryId] [int] NOT NULL,
	[BoardType] [int] NOT NULL,
	[BoardName] [nvarchar](1000) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Branches]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Branches](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[DateOfCreation] [datetime] NULL,
	[BranchName] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Builds]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Builds](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReasonName] [nvarchar](600) NOT NULL,
	[FrameworkVersion] [int] NULL,
	[DateOfBuild] [datetime] NULL,
	[CompleationStatus] [int] NULL,
	[AccountId] [int] NULL,
	[PlatformId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Changelogs]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Changelogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LogDetails] [varchar](max) NOT NULL,
	[DayOfLog] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChatChannels]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChatChannels](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ChannelName] [nvarchar](300) NULL,
	[ChannelType] [int] NULL,
	[ChatRoom] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChatChannelTypes]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChatChannelTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChatRoomRights]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChatRoomRights](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RightName] [nvarchar](300) NOT NULL,
	[PojectId] [int] NULL,
	[Background] [nvarchar](500) NULL,
	[Foreground] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChatRooms]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChatRooms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoomName] [nvarchar](300) NULL,
	[ProjectId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Commits]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Commits](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CommitData] [nvarchar](max) NOT NULL,
	[DateOfCommit] [datetime] NULL,
	[CommitedBy] [nvarchar](max) NULL,
	[CommitKey] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Documentation]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Documentation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocumentationCategory]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentationCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](300) NULL,
	[DocumentationId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocumentationCategoryField]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentationCategoryField](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PageName] [nvarchar](300) NULL,
	[CategoryId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Efforts]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Efforts](
	[Id] [int] NOT NULL,
	[EffortName] [nvarchar](300) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Files]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Files](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FilePath] [nvarchar](max) NULL,
	[CurrentName] [nvarchar](max) NULL,
	[FileData] [nvarchar](max) NULL,
	[DateOfFile] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FileTypes]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FileTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](300) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NotificationRights]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationRights](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PersonalMessageNEnabled] [int] NULL,
	[ChatChannelNEnabled] [int] NULL,
	[UpdateWorkItemNEnabled] [int] NULL,
	[CreateWorkItemNEnabled] [int] NULL,
	[PublicDiscussionMNEnabled] [int] NULL,
	[FeedbackNEnabled] [int] NULL,
	[BugReportNEnabled] [int] NULL,
	[ChanegelogNEnabled] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notifications]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[WorkItemRelationid] [int] NULL,
	[DateOfMessage] [datetime] NULL,
	[NotificationType] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NotificationTypes]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NotificationType] [nvarchar](max) NOT NULL,
	[Icon] [nvarchar](300) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Projects]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RepositoryId] [int] NOT NULL,
	[ProjectName] [nvarchar](max) NULL,
	[ProjectDescription] [nvarchar](max) NULL,
	[ProjectTitle] [nvarchar](max) NULL,
	[CreationDate] [datetime] NULL,
	[BoardId] [int] NULL,
	[PublicBoard] [int] NULL,
	[AllowPublicControl] [int] NULL,
	[AllowPublicFeatures] [int] NULL,
	[AllowPublicBugs] [int] NULL,
	[AllowPublicFeedback] [int] NULL,
	[AllowPublicMessages] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PublicMessage]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PublicMessage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[WorkItemRelationid] [int] NULL,
	[Rating] [int] NULL,
	[Email] [nvarchar](200) NOT NULL,
	[DateOfMessage] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PublicMessages]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PublicMessages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SenderName] [nvarchar](255) NOT NULL,
	[MessageContent] [nvarchar](max) NULL,
	[DateOfMessage] [datetime] NULL,
	[IsNew] [int] NULL,
	[SenderId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Repository]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Repository](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FolderPath] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Risks]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Risks](
	[Id] [int] NOT NULL,
	[RiskName] [nvarchar](300) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemFiles]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemFiles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SenderName] [nvarchar](255) NOT NULL,
	[FileLocation] [nvarchar](max) NULL,
	[DateOfMessage] [datetime] NULL,
	[FileType] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAccounts]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAccounts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[Email] [nvarchar](max) NULL,
	[FirstName] [nvarchar](400) NULL,
	[LastName] [nvarchar](400) NULL,
	[ProjectRights] [int] NULL,
	[GitUsername] [nvarchar](300) NULL,
	[Salt] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserDashboardItem]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDashboardItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](200) NULL,
	[DataRow] [int] NULL,
	[DataCol] [int] NULL,
	[DataSizeY] [int] NULL,
	[DataSizeX] [int] NULL,
	[UserDashboard] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserDashboardItemComponent]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDashboardItemComponent](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ComponentName] [nvarchar](200) NULL,
	[IsRow] [int] NULL,
	[ColumnNumber] [int] NULL,
	[ComponentInternalName] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserDashboards]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDashboards](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DashboardName] [nvarchar](max) NULL,
	[UserId] [int] NULL,
	[DateOfDashboard] [datetime] NULL,
	[ProjectId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserNotes]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserNotes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NoteBackground] [nvarchar](200) NOT NULL,
	[NoteForeground] [nvarchar](200) NOT NULL,
	[Content] [nvarchar](max) NULL,
	[DateOfMessage] [datetime] NULL,
	[TopPos] [nvarchar](max) NULL,
	[LeftPos] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRights]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRights](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WorkItemRule] [smallint] NOT NULL,
	[ChatChannelsRule] [smallint] NOT NULL,
	[UpdateUserRights] [smallint] NOT NULL,
	[ManageIterations] [smallint] NOT NULL,
	[ManageUserdays] [smallint] NOT NULL,
	[ViewOtherPeoplesWork] [smallint] NOT NULL,
	[Documentation] [int] NULL,
 CONSTRAINT [PK_UserRights] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ValueAreas]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ValueAreas](
	[Id] [int] NOT NULL,
	[ValueAreaName] [nvarchar](300) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkItem]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WorkItemTypeId] [int] NULL,
	[Title] [nvarchar](max) NULL,
	[AssignedAccount] [int] NULL,
	[StateId] [int] NULL,
	[AreaId] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[PriorityId] [int] NULL,
	[ClassificationId] [int] NULL,
	[DevelopmentId] [int] NULL,
	[ParentId] [int] NULL,
	[Reason] [int] NULL,
	[Iteration] [int] NULL,
	[RepoSteps] [nvarchar](max) NULL,
	[SystemInfo] [nvarchar](max) NULL,
	[ResolvedReason] [nvarchar](max) NULL,
	[itemPriority] [int] NULL,
	[Severity] [int] NULL,
	[Activity] [int] NULL,
	[OriginEstitame] [nvarchar](300) NULL,
	[Remaining] [nvarchar](300) NULL,
	[Compleated] [nvarchar](300) NULL,
	[BranchId] [int] NULL,
	[FoundInBuild] [int] NULL,
	[IntegratedInBuild] [int] NULL,
	[ReasonId] [int] NULL,
	[RelationId] [int] NULL,
	[RiskId] [int] NULL,
	[BusinessValue] [nvarchar](max) NULL,
	[TimeCapacity] [nvarchar](max) NULL,
	[ValueAreaId] [int] NULL,
	[Effort] [nvarchar](max) NULL,
	[StackRank] [nvarchar](max) NULL,
	[DueDate] [datetime] NULL,
	[StoryPoints] [nvarchar](400) NULL,
	[AcceptanceCriteria] [nvarchar](max) NULL,
	[IsPublic] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkItemActivity]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkItemActivity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ActivityName] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkItemAreas]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkItemAreas](
	[ID] [int] NOT NULL,
	[AreaName] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkItemIterations]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkItemIterations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IterationName] [nvarchar](255) NOT NULL,
	[IsActive] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkItemMessage]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkItemMessage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SenderId] [int] NOT NULL,
	[Content] [nvarchar](max) NULL,
	[DateOfMessage] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkItemPriorities]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkItemPriorities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PriorityName] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkItemRealtionshipType]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkItemRealtionshipType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkItemReasons]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkItemReasons](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReasonName] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkItemRelations]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkItemRelations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RelationName] [nvarchar](300) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkItemSeverities]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkItemSeverities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SeverityName] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkItemStates]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkItemStates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StateName] [nvarchar](300) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkItemTypes]    Script Date: 6/17/2020 2:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkItemTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](300) NULL,
	[Icon] [nvarchar](600) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AssociatedAccountNotes]  WITH CHECK ADD FOREIGN KEY([NoteId])
REFERENCES [dbo].[UserNotes] ([Id])
GO
ALTER TABLE [dbo].[AssociatedAccountNotes]  WITH CHECK ADD FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[AssociatedAccountNotes]  WITH CHECK ADD FOREIGN KEY([UserAccountId])
REFERENCES [dbo].[UserAccounts] ([Id])
GO
ALTER TABLE [dbo].[AssociatedAccountProjectNotificationRights]  WITH CHECK ADD FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[AssociatedAccountProjectNotificationRights]  WITH CHECK ADD FOREIGN KEY([RightId])
REFERENCES [dbo].[NotificationRights] ([Id])
GO
ALTER TABLE [dbo].[AssociatedAccountProjectNotificationRights]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[UserAccounts] ([Id])
GO
ALTER TABLE [dbo].[AssociatedBoardWorkItems]  WITH CHECK ADD FOREIGN KEY([BoardId])
REFERENCES [dbo].[Boards] ([Id])
GO
ALTER TABLE [dbo].[AssociatedBoardWorkItems]  WITH CHECK ADD FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[AssociatedBoardWorkItems]  WITH CHECK ADD FOREIGN KEY([WorkItemId])
REFERENCES [dbo].[WorkItem] ([Id])
GO
ALTER TABLE [dbo].[AssociatedBranchCommits]  WITH CHECK ADD FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branches] ([Id])
GO
ALTER TABLE [dbo].[AssociatedBranchCommits]  WITH CHECK ADD FOREIGN KEY([CommitId])
REFERENCES [dbo].[Commits] ([Id])
GO
ALTER TABLE [dbo].[AssociatedChatChannelMessages]  WITH CHECK ADD FOREIGN KEY([ChatChannelId])
REFERENCES [dbo].[ChatChannels] ([Id])
GO
ALTER TABLE [dbo].[AssociatedChatChannelMessages]  WITH CHECK ADD FOREIGN KEY([PublicMessageId])
REFERENCES [dbo].[PublicMessages] ([Id])
GO
ALTER TABLE [dbo].[AssociatedChatPersonalMessages]  WITH CHECK ADD FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[AssociatedChatPersonalMessages]  WITH CHECK ADD FOREIGN KEY([PublicMessageId])
REFERENCES [dbo].[PublicMessages] ([Id])
GO
ALTER TABLE [dbo].[AssociatedChatPersonalMessages]  WITH CHECK ADD FOREIGN KEY([ReciverId])
REFERENCES [dbo].[UserAccounts] ([Id])
GO
ALTER TABLE [dbo].[AssociatedChatPersonalMessages]  WITH CHECK ADD FOREIGN KEY([SenderId])
REFERENCES [dbo].[UserAccounts] ([Id])
GO
ALTER TABLE [dbo].[AssociatedChatRoomRights]  WITH CHECK ADD FOREIGN KEY([ChatRoomId])
REFERENCES [dbo].[ChatRooms] ([Id])
GO
ALTER TABLE [dbo].[AssociatedChatRoomRights]  WITH CHECK ADD FOREIGN KEY([RightId])
REFERENCES [dbo].[ChatRoomRights] ([Id])
GO
ALTER TABLE [dbo].[AssociatedCommitFiles]  WITH CHECK ADD FOREIGN KEY([CommitId])
REFERENCES [dbo].[Commits] ([Id])
GO
ALTER TABLE [dbo].[AssociatedCommitFiles]  WITH CHECK ADD FOREIGN KEY([FileId])
REFERENCES [dbo].[Files] ([Id])
GO
ALTER TABLE [dbo].[AssociatedDocumentationCategoryPage]  WITH CHECK ADD FOREIGN KEY([CategoryField])
REFERENCES [dbo].[DocumentationCategoryField] ([Id])
GO
ALTER TABLE [dbo].[AssociatedProjectApiKeys]  WITH CHECK ADD FOREIGN KEY([KeyId])
REFERENCES [dbo].[ApiKeys] ([Id])
GO
ALTER TABLE [dbo].[AssociatedProjectApiKeys]  WITH CHECK ADD FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[AssociatedProjectBoards]  WITH CHECK ADD FOREIGN KEY([BoardId])
REFERENCES [dbo].[Boards] ([Id])
GO
ALTER TABLE [dbo].[AssociatedProjectBoards]  WITH CHECK ADD FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[AssociatedProjectBuilds]  WITH CHECK ADD FOREIGN KEY([BuildId])
REFERENCES [dbo].[Builds] ([Id])
GO
ALTER TABLE [dbo].[AssociatedProjectBuilds]  WITH CHECK ADD FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[AssociatedProjectBuilds]  WITH CHECK ADD FOREIGN KEY([RepositoryId])
REFERENCES [dbo].[Repository] ([Id])
GO
ALTER TABLE [dbo].[AssociatedProjectChangelogs]  WITH CHECK ADD FOREIGN KEY([LogId])
REFERENCES [dbo].[Changelogs] ([Id])
GO
ALTER TABLE [dbo].[AssociatedProjectChangelogs]  WITH CHECK ADD FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[AssociatedProjectFeedback]  WITH CHECK ADD FOREIGN KEY([MessageId])
REFERENCES [dbo].[PublicMessages] ([Id])
GO
ALTER TABLE [dbo].[AssociatedProjectFeedback]  WITH CHECK ADD FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[AssociatedProjectIterations]  WITH CHECK ADD  CONSTRAINT [FK_AssociatedProjectIterations_Projects] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[AssociatedProjectIterations] CHECK CONSTRAINT [FK_AssociatedProjectIterations_Projects]
GO
ALTER TABLE [dbo].[AssociatedProjectIterations]  WITH CHECK ADD  CONSTRAINT [FK_AssociatedProjectIterations_WorkItemIterations] FOREIGN KEY([IterationId])
REFERENCES [dbo].[WorkItemIterations] ([Id])
GO
ALTER TABLE [dbo].[AssociatedProjectIterations] CHECK CONSTRAINT [FK_AssociatedProjectIterations_WorkItemIterations]
GO
ALTER TABLE [dbo].[AssociatedProjectMemberRights]  WITH CHECK ADD  CONSTRAINT [FK_AssociatedProjectMemberRights_Projects] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[AssociatedProjectMemberRights] CHECK CONSTRAINT [FK_AssociatedProjectMemberRights_Projects]
GO
ALTER TABLE [dbo].[AssociatedProjectMemberRights]  WITH CHECK ADD  CONSTRAINT [FK_AssociatedProjectMemberRights_UserAccounts] FOREIGN KEY([UserAccountId])
REFERENCES [dbo].[UserAccounts] ([Id])
GO
ALTER TABLE [dbo].[AssociatedProjectMemberRights] CHECK CONSTRAINT [FK_AssociatedProjectMemberRights_UserAccounts]
GO
ALTER TABLE [dbo].[AssociatedProjectMemberRights]  WITH CHECK ADD  CONSTRAINT [FK_AssociatedProjectMemberRights_UserRights] FOREIGN KEY([RightsId])
REFERENCES [dbo].[UserRights] ([Id])
GO
ALTER TABLE [dbo].[AssociatedProjectMemberRights] CHECK CONSTRAINT [FK_AssociatedProjectMemberRights_UserRights]
GO
ALTER TABLE [dbo].[AssociatedProjectMembers]  WITH CHECK ADD FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[AssociatedProjectMembers]  WITH CHECK ADD FOREIGN KEY([RepositoryId])
REFERENCES [dbo].[Repository] ([Id])
GO
ALTER TABLE [dbo].[AssociatedProjectMembers]  WITH CHECK ADD FOREIGN KEY([UserAccountId])
REFERENCES [dbo].[UserAccounts] ([Id])
GO
ALTER TABLE [dbo].[AssociatedProjectNotifications]  WITH CHECK ADD FOREIGN KEY([NotificationId])
REFERENCES [dbo].[Notifications] ([Id])
GO
ALTER TABLE [dbo].[AssociatedProjectNotifications]  WITH CHECK ADD FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[AssociatedProjectNotifications]  WITH CHECK ADD FOREIGN KEY([UserAccountId])
REFERENCES [dbo].[UserAccounts] ([Id])
GO
ALTER TABLE [dbo].[AssociatedProjectPublicDiscussions]  WITH CHECK ADD FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[AssociatedProjectPublicDiscussions]  WITH CHECK ADD FOREIGN KEY([PublicMessageId])
REFERENCES [dbo].[PublicMessages] ([Id])
GO
ALTER TABLE [dbo].[AssociatedProjectPublicMessages]  WITH CHECK ADD FOREIGN KEY([MessageId])
REFERENCES [dbo].[PublicMessage] ([Id])
GO
ALTER TABLE [dbo].[AssociatedProjectPublicMessages]  WITH CHECK ADD FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[AssociatedRepositoryBranches]  WITH CHECK ADD FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branches] ([Id])
GO
ALTER TABLE [dbo].[AssociatedRepositoryBranches]  WITH CHECK ADD FOREIGN KEY([RepositoryId])
REFERENCES [dbo].[Repository] ([Id])
GO
ALTER TABLE [dbo].[AssociatedUserChatNotifications]  WITH CHECK ADD FOREIGN KEY([ChatChannelId])
REFERENCES [dbo].[ChatChannels] ([Id])
GO
ALTER TABLE [dbo].[AssociatedUserChatNotifications]  WITH CHECK ADD FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[AssociatedUserChatNotifications]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[UserAccounts] ([Id])
GO
ALTER TABLE [dbo].[AssociatedUserChatRights]  WITH CHECK ADD FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[AssociatedUserChatRights]  WITH CHECK ADD FOREIGN KEY([RightId])
REFERENCES [dbo].[ChatRoomRights] ([Id])
GO
ALTER TABLE [dbo].[AssociatedUserChatRights]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[UserAccounts] ([Id])
GO
ALTER TABLE [dbo].[AssociatedUserDashboardItemComponent]  WITH CHECK ADD FOREIGN KEY([ItemComponent])
REFERENCES [dbo].[UserDashboardItemComponent] ([Id])
GO
ALTER TABLE [dbo].[AssociatedUserDashboardItemComponent]  WITH CHECK ADD FOREIGN KEY([Item])
REFERENCES [dbo].[UserDashboardItem] ([Id])
GO
ALTER TABLE [dbo].[AssociatedUserNotifications]  WITH CHECK ADD FOREIGN KEY([NotificationId])
REFERENCES [dbo].[Notifications] ([Id])
GO
ALTER TABLE [dbo].[AssociatedUserNotifications]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[UserAccounts] ([Id])
GO
ALTER TABLE [dbo].[AssociatedWorkItemChangelogs]  WITH CHECK ADD FOREIGN KEY([LogId])
REFERENCES [dbo].[Changelogs] ([Id])
GO
ALTER TABLE [dbo].[AssociatedWorkItemChangelogs]  WITH CHECK ADD FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[AssociatedWorkItemChangelogs]  WITH CHECK ADD FOREIGN KEY([workitemId])
REFERENCES [dbo].[WorkItem] ([Id])
GO
ALTER TABLE [dbo].[AssociatedWorkItemFiles]  WITH CHECK ADD FOREIGN KEY([FileId])
REFERENCES [dbo].[SystemFiles] ([Id])
GO
ALTER TABLE [dbo].[AssociatedWorkItemFiles]  WITH CHECK ADD FOREIGN KEY([WorkItemId])
REFERENCES [dbo].[WorkItem] ([Id])
GO
ALTER TABLE [dbo].[AssociatedWorkItemMessages]  WITH CHECK ADD FOREIGN KEY([MessageId])
REFERENCES [dbo].[WorkItemMessage] ([Id])
GO
ALTER TABLE [dbo].[AssociatedWorkItemMessages]  WITH CHECK ADD FOREIGN KEY([WorkItemId])
REFERENCES [dbo].[WorkItem] ([Id])
GO
ALTER TABLE [dbo].[AssociatedWrorkItemChildren]  WITH CHECK ADD FOREIGN KEY([RelationType])
REFERENCES [dbo].[WorkItemTypes] ([Id])
GO
ALTER TABLE [dbo].[AssociatedWrorkItemChildren]  WITH CHECK ADD FOREIGN KEY([WorkItemId])
REFERENCES [dbo].[WorkItem] ([Id])
GO
ALTER TABLE [dbo].[AssociatedWrorkItemChildren]  WITH CHECK ADD FOREIGN KEY([WorkItemChildId])
REFERENCES [dbo].[WorkItem] ([Id])
GO
ALTER TABLE [dbo].[Branches]  WITH CHECK ADD FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[ChatChannels]  WITH CHECK ADD FOREIGN KEY([ChannelType])
REFERENCES [dbo].[ChatChannelTypes] ([Id])
GO
ALTER TABLE [dbo].[ChatChannels]  WITH CHECK ADD FOREIGN KEY([ChatRoom])
REFERENCES [dbo].[ChatRooms] ([Id])
GO
ALTER TABLE [dbo].[ChatRoomRights]  WITH CHECK ADD FOREIGN KEY([PojectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[ChatRooms]  WITH CHECK ADD FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[Documentation]  WITH CHECK ADD FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[DocumentationCategory]  WITH CHECK ADD FOREIGN KEY([DocumentationId])
REFERENCES [dbo].[Documentation] ([Id])
GO
ALTER TABLE [dbo].[DocumentationCategoryField]  WITH CHECK ADD FOREIGN KEY([CategoryId])
REFERENCES [dbo].[DocumentationCategory] ([Id])
GO
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD FOREIGN KEY([NotificationType])
REFERENCES [dbo].[NotificationTypes] ([Id])
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD FOREIGN KEY([RepositoryId])
REFERENCES [dbo].[Repository] ([Id])
GO
ALTER TABLE [dbo].[SystemFiles]  WITH CHECK ADD FOREIGN KEY([FileType])
REFERENCES [dbo].[FileTypes] ([Id])
GO
ALTER TABLE [dbo].[UserDashboardItem]  WITH CHECK ADD FOREIGN KEY([UserDashboard])
REFERENCES [dbo].[UserDashboards] ([Id])
GO
ALTER TABLE [dbo].[UserDashboards]  WITH CHECK ADD FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[WorkItem]  WITH CHECK ADD FOREIGN KEY([Activity])
REFERENCES [dbo].[WorkItemActivity] ([Id])
GO
ALTER TABLE [dbo].[WorkItem]  WITH CHECK ADD FOREIGN KEY([AreaId])
REFERENCES [dbo].[WorkItemAreas] ([ID])
GO
ALTER TABLE [dbo].[WorkItem]  WITH CHECK ADD FOREIGN KEY([AssignedAccount])
REFERENCES [dbo].[UserAccounts] ([Id])
GO
ALTER TABLE [dbo].[WorkItem]  WITH CHECK ADD FOREIGN KEY([Iteration])
REFERENCES [dbo].[WorkItemIterations] ([Id])
GO
ALTER TABLE [dbo].[WorkItem]  WITH CHECK ADD FOREIGN KEY([PriorityId])
REFERENCES [dbo].[WorkItemPriorities] ([Id])
GO
ALTER TABLE [dbo].[WorkItem]  WITH CHECK ADD FOREIGN KEY([ReasonId])
REFERENCES [dbo].[WorkItemReasons] ([Id])
GO
ALTER TABLE [dbo].[WorkItem]  WITH CHECK ADD FOREIGN KEY([RelationId])
REFERENCES [dbo].[WorkItemRelations] ([Id])
GO
ALTER TABLE [dbo].[WorkItem]  WITH CHECK ADD FOREIGN KEY([RiskId])
REFERENCES [dbo].[Risks] ([Id])
GO
ALTER TABLE [dbo].[WorkItem]  WITH CHECK ADD FOREIGN KEY([Severity])
REFERENCES [dbo].[WorkItemSeverities] ([Id])
GO
ALTER TABLE [dbo].[WorkItem]  WITH CHECK ADD FOREIGN KEY([StateId])
REFERENCES [dbo].[WorkItemStates] ([Id])
GO
ALTER TABLE [dbo].[WorkItem]  WITH CHECK ADD FOREIGN KEY([ValueAreaId])
REFERENCES [dbo].[ValueAreas] ([Id])
GO
ALTER TABLE [dbo].[WorkItem]  WITH CHECK ADD FOREIGN KEY([WorkItemTypeId])
REFERENCES [dbo].[WorkItemTypes] ([Id])
GO
ALTER TABLE [dbo].[WorkItemMessage]  WITH CHECK ADD FOREIGN KEY([SenderId])
REFERENCES [dbo].[UserAccounts] ([Id])
GO
USE [master]
GO
ALTER DATABASE [RokonoControl] SET  READ_WRITE 
GO