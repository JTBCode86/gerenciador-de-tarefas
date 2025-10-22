SET NOCOUNT ON
GO
CREATE OR ALTER VIEW vw_UserTaskSummary
AS
SELECT 
    T.Id AS TaskId,
    T.Title,
    T.Description,
    T.DueDate,
    T.CreatedAt,
    T.CompletedAt,
    T.EstimatedDuration,
    U.FirstName + ' ' + U.LastName AS AssignedUser,
    TS.Name AS TaskStatusName,
    T.PriorityLevel -- Pode ser substituído pelo nome se tiver uma tabela de Priority
    -- Agrega as categorias em uma string separada por vírgula (SQL Server 2017+ ou Azure SQL)
    , STUFF((
        SELECT ', ' + C.Name
        FROM TaskCategoryMap TCM
        INNER JOIN TaskCategories C ON TCM.CategoryId = C.Id
        WHERE TCM.TaskId = T.Id
        FOR XML PATH(''), TYPE
    ).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS Categories
FROM 
    Tasks T
INNER JOIN 
    Users U ON T.UserId = U.Id
INNER JOIN 
    TaskStatuses TS ON T.StatusId = TS.Id;
GO