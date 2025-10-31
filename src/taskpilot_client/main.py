# main.py (ATUALIZADO)

from infrastructure.api_adapter import TaskApiAdapter
from application.use_cases import GetAllTasksUseCase, CreateTaskUseCase, CompleteTaskUseCase
from domain.entities import Task, CreateTaskCommand
from datetime import datetime, timedelta

# 🚨 Configuração: Altere a URL base da sua API
API_BASE_URL = "https://localhost:5001"  # Verifique a porta!
USER_ID_TO_FETCH = 1  # ID do usuário logado

def print_task_summary(tasks: list[Task]):
    """Imprime um resumo das tarefas."""
    # ... (código de resumo de tarefas permanece o mesmo) ...
    if not tasks:
        print("Nenhuma tarefa encontrada para este usuário.")
        return

    print(f"\n--- Resumo de Tarefas para o Usuário {USER_ID_TO_FETCH} ---")
    
    for task in tasks:
        status = "COMPLETA" if task.is_completed else "PENDENTE"
        due_date_str = task.due_date.strftime("%Y-%m-%d") if task.due_date else "Sem Prazo"
        
        print(f"  [ID: {task.id}] {task.title}")
        print(f"    Status: {status} | Prazo: {due_date_str}")
        print("-" * 20)
    
    print(f"\nTotal de Tarefas: {len(tasks)}")
    completed_count = sum(1 for t in tasks if t.is_completed)
    print(f"Tarefas Concluídas: {completed_count}")
    print(f"Tarefas Pendentes: {len(tasks) - completed_count}")


def main():
    """Configura o sistema e demonstra os casos de uso."""
    
    # 1. Configuração da Infraestrutura (Adaptador)
    task_api_adapter = TaskApiAdapter(base_url=API_BASE_URL)
    
    # 2. Injeção de Dependência da Aplicação
    get_all_tasks_use_case = GetAllTasksUseCase(repository=task_api_adapter)
    create_task_use_case = CreateTaskUseCase(repository=task_api_adapter)
    complete_task_use_case = CompleteTaskUseCase(repository=task_api_adapter)
    
    
    # --- DEMONSTRAÇÃO 1: CRIAR UMA NOVA TAREFA ---
    print("\n--- 1. DEMONSTRAÇÃO: CRIAÇÃO DE TAREFA ---")
    new_task_command = CreateTaskCommand(
        title="Nova Tarefa Criada pelo Python",
        description="Esta tarefa deve ser encerrada no próximo passo.",
        due_date=datetime.now() + timedelta(days=7),
        user_id=USER_ID_TO_FETCH
    )
    
    new_task_id = create_task_use_case.execute(command=new_task_command)
    
    if new_task_id > 0:
        print(f"✅ Tarefa criada com sucesso. ID: {new_task_id}")
    else:
        print("❌ Falha ao criar tarefa. Verifique o console para erros HTTP.")
        return # Interrompe o programa se a criação falhar

    
    # --- DEMONSTRAÇÃO 2: ENCERRAMENTO DA TAREFA CRIADA ---
    print("\n--- 2. DEMONSTRAÇÃO: ENCERRAMENTO DE TAREFA ---")
    
    task_to_complete_id = new_task_id
    
    success = complete_task_use_case.execute(
        task_id=task_to_complete_id, 
        user_id=USER_ID_TO_FETCH
    )
    
    if success:
        print(f"✅ Tarefa ID {task_to_complete_id} encerrada com sucesso.")
    else:
        print(f"❌ Falha ao encerrar tarefa ID {task_to_complete_id}. Verifique a validação de domínio da API.")

    # --- DEMONSTRAÇÃO 3: BUSCAR E EXIBIR TUDO (PARA VERIFICAR MUDANÇAS) ---
    print("\n--- 3. DEMONSTRAÇÃO: BUSCA PÓS-MUDANÇAS ---")
    tasks = get_all_tasks_use_case.execute(user_id=USER_ID_TO_FETCH)
    print_task_summary(tasks)


if __name__ == "__main__":
    main()