SET NOCOUNT ON
GO
CREATE OR ALTER VIEW vw_ProductivityAnalysisData
AS
SELECT 
    T.Id AS TaskId,
    T.UserId,
    U.Email AS UserEmail,
    T.Title,
    T.CreatedAt,
    T.CompletedAt,
    TS.Name AS TaskStatus,
    T.EstimatedDuration, -- Estimativa em minutos
    
    -- Cálculo da Duração Real em Minutos
    DATEDIFF(MINUTE, T.CreatedAt, T.CompletedAt) AS ActualDurationMinutes,
    
    -- Cálculo do Desvio (Positivo = Atrasado, Negativo = Adiantado)
    DATEDIFF(MINUTE, T.DueDate, T.CompletedAt) AS CompletionDeviationMinutes,
    
    -- Se a tarefa foi concluída ou não
    CASE WHEN T.CompletedAt IS NOT NULL THEN 1 ELSE 0 END AS IsSuccessfullyCompleted
    
FROM 
    Tasks T
INNER JOIN 
    Users U ON T.UserId = U.Id
INNER JOIN 
    TaskStatuses TS ON T.StatusId = TS.Id
WHERE 
    T.CompletedAt IS NOT NULL; -- Apenas tarefas concluídas para análise de produtividade
GO