/****** Object:  View [dbo].[vw_Items]    Script Date: 8/23/2024 9:53:46 AM ******/
DROP VIEW [dbo].[vw_Items]
GO

/****** Object:  View [dbo].[vw_Items]    Script Date: 8/23/2024 9:53:46 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE view [dbo].[vw_Items] as
SELECT [Id]
      ,[UserObjectId]
      ,[Description]
      ,[Location]
      ,[Dt]
      ,[ImagePath]
  FROM [dbo].[Items]
GO
