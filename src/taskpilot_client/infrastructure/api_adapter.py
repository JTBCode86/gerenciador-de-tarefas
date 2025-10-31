# infrastructure/api_adapter.py (CORRIGIDO)

import requests
from datetime import datetime
from domain.entities import ITaskRepository, Task, CreateTaskCommand
from typing import List
import warnings

class TaskApiAdapter(ITaskRepository):
    # ... (métodos __init__ e outras partes) ...

    # --- Método Existente (Busca) ---
    def get_all_tasks(self, user_id: int) -> list[Task]:
        url = f"{self.base_url}/api/v1/Tasks/summary/{user_id}"
        
        with warnings.catch_warnings():
            warnings.simplefilter("ignore")
            try:
                # ✅ CORREÇÃO AQUI: Adicionar verify=False
                response = requests.get(url, verify=False) 
                response.raise_for_status()
            except requests.exceptions.RequestException as e:
                print(f"Erro ao conectar ou receber resposta da API: {e}")
                return []
        
        # ... (restante do código de mapeamento) ...
        return tasks


    # --- 🆕 NOVO MÉTODO: Criação (Corrigido) ---
    def create_task(self, command: CreateTaskCommand) -> int:
        url = f"{self.base_url}/api/v1/Tasks"
        
        payload = {
            "title": command.title,
            "description": command.description,
            "dueDate": command.due_date.isoformat() if command.due_date else None,
            "userId": command.user_id 
        }

        with warnings.catch_warnings():
            warnings.simplefilter("ignore")
            
            try:
                # ✅ CORREÇÃO AQUI: Adicionar verify=False
                response = requests.post(url, json=payload, verify=False) 
                response.raise_for_status()
                
                if response.status_code == 201:
                    return int(response.text)
                
                return 0 
                
            except requests.exceptions.HTTPError as e:
                print(f"Erro HTTP na criação da tarefa: {e}. Resposta: {response.text}")
                return 0
            except requests.exceptions.RequestException as e:
                print(f"Erro de conexão na criação da tarefa: {e}")
                return 0

    # --- 🆕 NOVO MÉTODO: Encerramento (Corrigido) ---
    def complete_task(self, task_id: int, user_id: int) -> bool:
        url = f"{self.base_url}/api/v1/Tasks/{task_id}/complete"
        
        with warnings.catch_warnings():
            warnings.simplefilter("ignore")
            
            try:
                # ✅ CORREÇÃO AQUI: Adicionar verify=False
                response = requests.post(url, verify=False) 
                response.raise_for_status() 
                
                return response.status_code == 204
                
            except requests.exceptions.HTTPError as e:
                print(f"Erro HTTP ao completar tarefa {task_id}: {e}")
                if e.response.status_code == 404:
                    print("Tarefa não encontrada.")
                elif e.response.status_code == 400:
                    print(f"Regra de negócio violada: {e.response.text}")
                return False
            except requests.exceptions.RequestException as e:
                print(f"Erro de conexão ao completar tarefa: {e}")
                return False