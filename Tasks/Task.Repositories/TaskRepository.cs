using Core.Entities;
using Core.Repositories;
using Core.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Task.Repositories
{
    public class TaskRepository : BaseRepository<TaskEntity>, ITaskRepository
    {
        public TaskRepository(DbContext context) : base(context)
        {
        }
    }
}