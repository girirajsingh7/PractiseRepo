using TaskManagementSystem.Models;

namespace TaskManagementSystem.Services
{
    public class TaskService : ITaskService
    {
        private static List<TaskItem> _task = new();
        private static int _nextId = 1;
        public Task<TaskItem> CreateTask(TaskItem item)
        {
            item.Id = _nextId++;
            _task.Add(item);
            return Task.FromResult(item);
        }

        public Task<bool> DeleteTask(int id)
        {
           var task = _task.FirstOrDefault(x => x.Id == id);
            if (task == null) return Task.FromResult(false);
            _task.Remove(task);
            return Task.FromResult(true);
        }

        public Task<bool> UpdateTask(int id, TaskItem task)
        {
            var existing = _task.FirstOrDefault(x => x.Id == id);
            if (existing == null) return Task.FromResult(false);

            existing.Title = task.Title;
            existing.Description = task.Description;
            existing.IsCompleted = task.IsCompleted;
            return Task.FromResult(true);
        }
    }
}
