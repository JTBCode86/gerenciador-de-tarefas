SET NOCOUNT ON
GO
CREATE OR ALTER PROCEDURE SpMarkTaskAsCompleted
    @TaskId INT,
    @UserId INT -- Parâmetro de segurança: garantir que apenas o dono possa concluir
AS
BEGIN
    -- Assumindo que o Status 'Concluída' tem o Id=3
    DECLARE @CompletedStatusId INT = 3;

    UPDATE Tasks
    SET StatusId = @CompletedStatusId,
        CompletedAt = GETDATE() -- Registra a hora exata da conclusão
    WHERE Id = @TaskId AND UserId = @UserId;
    
    -- Retorna o número de linhas afetadas para a API
    SELECT @@ROWCOUNT AS RowsAffected;
END
GO