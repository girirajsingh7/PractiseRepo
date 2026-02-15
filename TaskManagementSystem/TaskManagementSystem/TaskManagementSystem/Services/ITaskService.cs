using TaskManagementSystem.Models;

namespace TaskManagementSystem.Services
{
    public interface ITaskService
    {
        Task<TaskItem> CreateTask(TaskItem item);
        Task<bool> UpdateTask(int id, TaskItem task);
        Task<bool> DeleteTask(int id);
    }
}
