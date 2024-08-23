ALTER TABLE [dbo].[Items] DROP CONSTRAINT [DF__Items__ImagePath__02084FDA]
GO

/****** Object:  Table [dbo].[Items]    Script Date: 8/23/2024 9:33:25 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Items]') AND type in (N'U'))
DROP TABLE [dbo].[Items]
GO

/****** Object:  Table [dbo].[Items]    Script Date: 8/23/2024 9:33:25 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Items](
	[Id] [int] IDENTITY(1001,1) NOT NULL,
	[UserObjectId] [varchar](100) NOT NULL,
	[Description] [varchar](255) NULL,
	[Location] [varchar](100) NULL,
	[Dt] [datetime] NULL,
	[ImagePath] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Items] ADD  DEFAULT ('') FOR [ImagePath]
GO
