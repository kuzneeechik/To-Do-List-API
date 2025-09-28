namespace ToDoList_API.Data.Entities
{
    public class TaskEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Title { get; set; }
        public bool IsDone { get; set; } = false;
    }
}
