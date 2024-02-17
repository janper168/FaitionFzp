using JKF.DB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = JKF.DB.Models.Task;

namespace JKF.DB.DbContexts
{
    public partial class BaseDbContext
    {
        public DbSet<Task> Task { get; set; }
    }
}
