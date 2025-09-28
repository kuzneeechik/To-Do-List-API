using System.ComponentModel.DataAnnotations;

namespace ToDoList_API.Models
{
    public class TaskCreateModel
    {
        [MinLength(1)]
        public required string Title { get; set; }
    }
}
