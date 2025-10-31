# application/use_cases.py (ATUALIZADO)

from domain.entities import ITaskRepository, Task, CreateTaskCommand
from typing import List

# --- Caso de Uso Existente (Busca) ---
# ... (GetAllTasksUseCase permanece o mesmo) ...
class GetAllTasksUseCase:
    """Caso de uso para buscar todas as tarefas de um usuário."""
    
    def __init__(self, repository: ITaskRepository):
        self.repository = repository

    def execute(self, user_id: int) -> List[Task]:
        return self.repository.get_all_tasks(user_id)
# --- Fim Caso de Uso Existente ---


# 🆕 NOVO CASO DE USO: Criação
class CreateTaskUseCase:
    """Caso de uso para criar uma nova tarefa."""
    def __init__(self, repository: ITaskRepository):
        self.repository = repository
        
    def execute(self, command: CreateTaskCommand) -> int:
        # Aqui, a lógica de aplicação poderia incluir validações adicionais no objeto Command
        if not command.title:
            raise ValueError("O título da tarefa é obrigatório.")
            
        return self.repository.create_task(command)

# 🆕 NOVO CASO DE USO: Encerramento
class CompleteTaskUseCase:
    """Caso de uso para encerrar (concluir) uma tarefa."""
    def __init__(self, repository: ITaskRepository):
        self.repository = repository
        
    def execute(self, task_id: int, user_id: int) -> bool:
        if task_id <= 0:
            return False
            
        return self.repository.complete_task(task_id, user_id)