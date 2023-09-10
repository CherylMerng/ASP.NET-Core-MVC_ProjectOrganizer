-- Save queries in a new project
-- Merge script

USE [MyProjectsOrganizer_db];

-------- Select --------
SELECT * FROM Projects;

SELECT * FROM Projects
WHERE Project_Id = 3;

-------------------------

-- delete all data from table "Projects"
DELETE FROM Projects;
DELETE FROM Models;

-- DBCC - Database Console Command
-- RESEED - ID starts from 0 again

DBCC CHECKIDENT ('Projects', RESEED, 0);
DBCC CHECKIDENT ('Models', RESEED, 0);