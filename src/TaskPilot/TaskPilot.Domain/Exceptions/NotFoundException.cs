// Exemplo: TaskPilot.Domain/Exceptions/NotFoundException.cs

using System;

namespace TaskPilot.Domain.Exceptions // OU o namespace de sua escolha
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }

        public NotFoundException(string message)
            : base(message)
        {
        }
    }
}