// TaskPilot.Infrastructure/Data/Models/NewIdResult.cs (ou Domain)
public class NewIdResult
{
    // O nome da propriedade deve corresponder ao alias do SELECT na sua SP (SCOP_IDENTITY() AS NewTaskId)
    public int NewTaskId { get; set; }
}