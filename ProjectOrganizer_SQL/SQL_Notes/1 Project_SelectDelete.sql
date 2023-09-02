-- Save queries in a new project
-- Merge script

USE MyProjectsOrganizer_db;

SELECT * FROM Projects;

SELECT * FROM Projects
WHERE Project_Id = 3;

-- delete all data from table "Projects"
DELETE FROM Projects
--Reseed
DBCC CHECKIDENT ('Projects', RESEED, 1);