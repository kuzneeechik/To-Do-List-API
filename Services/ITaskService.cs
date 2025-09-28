using ToDoList_API.Models;

namespace ToDoList_API.Services
{
    public interface ITaskService
    {
        public Task<IdModel> CreateTask(TaskCreateModel newTask);
        public Task<List<TaskModel>> GetTasks();
        public Task DeleteTask(Guid id);
        public Task EditTask(Guid id, TaskEditModel task);
        public Task CheckTask(Guid id);
        public Task UncheckTask(Guid id);
        public Task UploadTasks(List<TaskModel> tasks);
    }
}
