using JKF.DB.DbContexts;
using JKF.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = JKF.DB.Models.Task;

namespace JKF.DB.Operation
{
    public class TaskService : BaseService<Task>
    {
        public TaskService(BaseDbContext baseDbContext) : base(baseDbContext) { }
    }
}
