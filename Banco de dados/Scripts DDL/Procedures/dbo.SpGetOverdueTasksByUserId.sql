SET NOCOUNT ON
GO
CREATE OR ALTER PROCEDURE SpGetOverdueTasksByUserId
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Assumindo que o Status 'Concluída' tem Id != 3
    DECLARE @CompletedStatusId INT = 3; 

    SELECT T.Id, T.Title, T.DueDate, P.Name AS PriorityName
    FROM Tasks T
    INNER JOIN TaskStatuses S ON T.StatusId = S.Id
    INNER JOIN PriorityLevel P ON T.PriorityLevel = P.Id -- Se tiver tabela de lookup, senão, use CASE/WHEN
    WHERE T.UserId = @UserId
      AND T.DueDate < GETDATE()
      AND T.StatusId != @CompletedStatusId
      AND T.CompletedAt IS NULL -- Garantia extra
    ORDER BY T.DueDate;
END
GO