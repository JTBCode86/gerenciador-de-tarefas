SET NOCOUNT ON
GO
CREATE OR ALTER PROCEDURE dbo.SpCreateNewTask
    @UserId INT,
    @Title NVARCHAR(256),
    @Description NVARCHAR(MAX) = NULL,
    @PriorityLevel INT = 1, -- Default: Baixa
    @EstimatedDuration INT = NULL,
    @DueDate DATETIME2 = NULL
AS
BEGIN
    SET NOCOUNT ON; -- Boa prática para evitar conjuntos de resultados extras

    -- A tarefa inicia com o Status 'Pendente' (Assumindo Id=1)
    DECLARE @DefaultStatusId INT = 1;

    INSERT INTO Tasks (UserId, StatusId, Title, Description, PriorityLevel, EstimatedDuration, DueDate, CreatedAt)
    VALUES (@UserId, @DefaultStatusId, @Title, @Description, @PriorityLevel, @EstimatedDuration, @DueDate, GETDATE());

    -- Retorna o ID da Tarefa recém-criada
    SELECT SCOPE_IDENTITY() AS NewTaskId;
END
GO