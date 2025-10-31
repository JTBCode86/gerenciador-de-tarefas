# domain/entities.py (ATUALIZADO)

from dataclasses import dataclass
from datetime import datetime
from typing import Optional

@dataclass
class Task:
    """Entidade de Domínio Task."""
    id: int
    title: str
    description: Optional[str]
    created_at: datetime
    due_date: Optional[datetime]
    is_completed: bool
    user_id: int

@dataclass
class CreateTaskCommand:
    """DTO para a criação de uma nova tarefa (corpo da requisição POST)."""
    title: str
    description: Optional[str]
    due_date: Optional[datetime]
    user_id: int # É importante para o endpoint de criação

# Interface de Repositório (Contrato que a Infrastructure deve seguir)
class ITaskRepository:
    """Interface abstrata para acesso a dados de tarefas."""
    def get_all_tasks(self, user_id: int) -> list[Task]:
        raise NotImplementedError
    
    # 🆕 NOVOS MÉTODOS
    def create_task(self, command: CreateTaskCommand) -> int:
        """Cria uma tarefa e retorna o ID."""
        raise NotImplementedError
        
    def complete_task(self, task_id: int, user_id: int) -> bool:
        """Encerra (conclui) uma tarefa."""
        raise NotImplementedError