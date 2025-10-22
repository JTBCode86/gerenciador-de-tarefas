-- 2. Tabela de Status de Tarefas (TaskStatuses) - Tabela de Lookup
CREATE TABLE TaskStatuses (
    Id INT PRIMARY KEY, -- Usar IDs fixos (1, 2, 3...) para Status é comum
    Name NVARCHAR(50) NOT NULL UNIQUE
);
GO;