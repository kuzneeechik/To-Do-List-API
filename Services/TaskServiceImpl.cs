using Microsoft.EntityFrameworkCore;
using ToDoList_API.Data;
using ToDoList_API.Data.Entities;
using ToDoList_API.Models;

namespace ToDoList_API.Services
{
    public class TaskServiceImpl : ITaskService
    {
        private readonly DataContext _context;

        public TaskServiceImpl(DataContext context) 
        {
            _context = context;
        }

        public async Task<IdModel> CreateTask(TaskCreateModel newTask)
        {
            var task = new TaskEntity { 
                Title = newTask.Title
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return new IdModel { Id = task.Id };
        }

        public async Task<List<TaskModel>> GetTasks()
        {
            var tasks = await _context.Tasks
                .AsNoTracking()
                .Select(t => new TaskModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    IsDone = t.IsDone
                })
                .ToListAsync();

            return tasks;
        }

        public async Task DeleteTask(Guid id)
        {
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
            {
                throw new Exception();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task EditTask(Guid id, TaskEditModel task)
        {
            var editedTask = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == id);

            if (editedTask == null)
            {
                throw new Exception("Not found");
            }

            if (editedTask.IsDone)
            {
                throw new Exception("Bad request");
            }

            editedTask.Title = task.Title;

            await _context.SaveChangesAsync();
        }

        public async Task CheckTask(Guid id)
        {
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
            {
                throw new Exception("Not found");
            }

            if (task.IsDone)
            {
                throw new Exception("Bad request");
            }

            task.IsDone = true;

            await _context.SaveChangesAsync();
        }

        public async Task UncheckTask(Guid id)
        {
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
            {
                throw new Exception();
            }

            if (!task.IsDone)
            {
                throw new Exception("Bad request");
            }

            task.IsDone = false;

            await _context.SaveChangesAsync();
        }

        public async Task UploadTasks(List<TaskModel> tasks)
        {
            var currentTasks = await _context.Tasks.ToListAsync();

            _context.Tasks.RemoveRange(currentTasks);

            await _context.Tasks
                .AddRangeAsync(tasks.Select(t => new TaskEntity
                {
                    Id = t.Id,
                    Title = t.Title,
                    IsDone = t.IsDone
                }));

            await _context.SaveChangesAsync();
        }
    }
}
