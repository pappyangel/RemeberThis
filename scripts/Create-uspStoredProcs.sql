/****** Object:  StoredProcedure [dbo].[sp_GetAllItems]    Script Date: 8/23/2024 9:57:01 AM ******/
DROP PROCEDURE [dbo].[sp_GetAllItems]
GO

/****** Object:  StoredProcedure [dbo].[sp_GetAllItems]    Script Date: 8/23/2024 9:57:01 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:      tr
-- Create Date: 1.23.2023
-- Description: select all Items using view
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetAllItems]
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    SELECT 
		  [Id]
		, [UserObjectId]
		, [Description]
		, [Location]
		, [Dt]
		, [ImagePath]
		from [dbo].[vw_Items]

END
GO
---------------------------
/****** Object:  StoredProcedure [dbo].[usp_GetAllItemsbyUser]    Script Date: 8/23/2024 9:58:03 AM ******/
DROP PROCEDURE [dbo].[usp_GetAllItemsbyUser]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetAllItemsbyUser]    Script Date: 8/23/2024 9:58:03 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- Create the stored procedure in the specified schema
CREATE PROCEDURE [dbo].[usp_GetAllItemsbyUser]
    @selecteduser varchar(100) 
    
-- add more stored procedure parameters here
AS
BEGIN

   SELECT 
        [Id]
      ,[UserObjectId]
      ,[Description]
      ,[Location]
      ,[Dt]
      ,[ImagePath]
  FROM [dbo].[Items]
  where (UserObjectId = @selecteduser)
  order by dt desc
 
END
GO
--------------------

/****** Object:  StoredProcedure [dbo].[usp_GetaPageOfItemsbyUser]    Script Date: 8/23/2024 9:59:09 AM ******/
DROP PROCEDURE [dbo].[usp_GetaPageOfItemsbyUser]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetaPageOfItemsbyUser]    Script Date: 8/23/2024 9:59:09 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Create the stored procedure in the specified schema
CREATE PROCEDURE [dbo].[usp_GetaPageOfItemsbyUser]
    @selecteduser varchar(100) , 
    @page  int  = 1 ,
    @ietmsperpage int  = 10
    
-- add more stored procedure parameters here
AS
BEGIN
Declare @offset int = (@page-1)*@ietmsperpage
   SELECT 
        [Id]
      ,[UserObjectId]
      ,[Description]
      ,[Location]
      ,[Dt]
      ,[ImagePath]
  FROM [dbo].[Items]
  where (UserObjectId = @selecteduser)
  order by dt
  OFFSET @offset ROWS 
FETCH NEXT @ietmsperpage ROWS ONLY
END
GO
-------------------

/****** Object:  StoredProcedure [dbo].[usp_GetAllItemsbyLocation]    Script Date: 8/23/2024 9:59:48 AM ******/
DROP PROCEDURE [dbo].[usp_GetAllItemsbyLocation]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetAllItemsbyLocation]    Script Date: 8/23/2024 9:59:48 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_GetAllItemsbyLocation]
    @selectedLocation varchar(100) 
    
-- add more stored procedure parameters here
AS
BEGIN

   SELECT 
        [Id]
      ,[UserObjectId]
      ,[Description]
      ,[Location]
      ,[Dt]
      ,[ImagePath]
  FROM [dbo].[Items]
  where (Location = @selectedLocation)
  --where (Location = @selectedLocation 'backyard' OR 'a'='a')
 -- order by dt desc
 
END
GO
--------------------

/****** Object:  StoredProcedure [dbo].[usp_GetAllItemsbyLocation]    Script Date: 8/23/2024 10:00:51 AM ******/
DROP PROCEDURE [dbo].[usp_GetAllItemsbyLocation]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetAllItemsbyLocation]    Script Date: 8/23/2024 10:00:51 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_GetAllItemsbyLocation]
    @selectedLocation varchar(100) 
    
-- add more stored procedure parameters here
AS
BEGIN

   SELECT 
        [Id]
      ,[UserObjectId]
      ,[Description]
      ,[Location]
      ,[Dt]
      ,[ImagePath]
  FROM [dbo].[Items]
  where (Location = @selectedLocation)
  --where (Location = @selectedLocation 'backyard' OR 'a'='a')
 -- order by dt desc
 
END
GO
-------------------------



