-- Inserção de dados iniciais para Users
INSERT INTO Users 
(FirstName,LastName,Email,PasswordHash,IsActive,CreatedAt)
VALUES ('FirstUser','Test','teste@taskpilot.com', 'teste',1,getdate())
GO