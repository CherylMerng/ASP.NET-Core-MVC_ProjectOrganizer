USE [MyProjectsOrganizer_db]
IF OBJECT_ID (N'[dbo].[Projects]',N'U') IS NULL
BEGIN
	-- dbo = database's default schema
	-- schema = blueprint
	CREATE TABLE [dbo].[Projects](
		[Project_Id] [int] IDENTITY(1,1) NOT NULL, -- start with 1, increment 1
		[Project_Name] [nvarchar] (50) NOT NULL,	-- unicode string
		[Description] [nvarchar] (255) NULL,
		[Created_Date] [DateTime] NOT NULL,
		[Project_Status] [bit] NOT NULL,	-- boolean
		PRIMARY KEY (Project_Id)
	)
END
GO