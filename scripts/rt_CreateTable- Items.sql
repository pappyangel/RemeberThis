-- new table for RemeberThis
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

if OBJECT_ID('dbo.Items','U') is not null
	drop table dbo.Items;
	
CREATE TABLE [dbo].[Items](
	[Id] [int] identity(1001,1) PRIMARY KEY NOT NULL,
	[UserObjectId] [varchar](100) NOT NULL,
	[Description] [varchar](255) NULL,
	[Location] [varchar](100) NULL,
	[Dt] [datetime] NULL,
	[ImagePath] [varchar](255) NULL
) ON [PRIMARY]
GO


