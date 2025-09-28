namespace ToDoList_API.Models
{
    public class TaskModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Title { get; set; }
        public bool IsDone { get; set; } = false;
    }
}
