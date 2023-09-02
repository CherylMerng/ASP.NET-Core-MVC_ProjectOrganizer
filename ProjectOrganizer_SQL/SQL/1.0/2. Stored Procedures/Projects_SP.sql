USE [MyProjectsOrganizer_db]
GO

IF EXISTS (
	SELECT type_desc, type
	FROM sys.procedures WITH(NOLOCK)
	WHERE NAME = 'Projects_StoredProcedures'
	AND type = 'P'
)
	DROP PROCEDURE dbo.Projects_StoredProcedures
GO

CREATE PROCEDURE Projects_StoredProcedures ( 
	@ProjectId INTEGER,
	@ProjectName NVARCHAR (50),
	@Description NVARCHAR (255),
	@CreatedDate DATETIME,  
	@ProjectStatus BIT,
	@Process NVARCHAR(50) = ''
)

AS 
	
	IF @Process = 'CreateNewProject'
		BEGIN -- start of query
			INSERT INTO Projects(Project_Name, Description, Created_Date, Project_Status)
			VALUES(@ProjectName, @Description, @CreatedDate, @ProjectStatus)
		END -- end of query

	ELSE IF @Process = 'GetAllProject'
		BEGIN
			SELECT * FROM Projects
		END

	ELSE IF @Process = 'GetProjectById'
		BEGIN
			SELECT * FROM Projects
			WHERE Project_Id = @ProjectId
		END

	ELSE IF @Process = 'UpdateProject'
		BEGIN
			UPDATE Projects 
			SET Project_Name = @ProjectName, Description = @Description, Created_Date = @CreatedDate, Project_Status = @ProjectStatus
			WHERE Project_Id = @ProjectId
		END

	ELSE IF @Process = 'DeleteProjectById'
		BEGIN
			DELETE FROM Projects WHERE Project_Id = @ProjectId
		END
GO