USE [MyProjectsOrganizer_db]
IF OBJECT_ID (N'[dbo].[Properties]',N'U') IS NULL
BEGIN
	CREATE TABLE [dbo].[Properties](
		[Property_Id] [int] IDENTITY(1,1) NOT NULL, -- start with 1, increment 1
		[Property_Name] [nvarchar] (50) NOT NULL,
		[Data_Type]	[nvarchar] (10) NOT NULL,
		[Access_Modifier] [nvarchar] (10) NOT NULL,
		[Description] [nvarchar] (255) NULL,
		[Model_Id] [int] NOT NULL,
		PRIMARY KEY (Property_Id),
		FOREIGN KEY (Model_Id) REFERENCES Models(Model_Id)
	)
END
GO