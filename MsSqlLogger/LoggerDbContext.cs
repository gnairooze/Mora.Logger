using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mora.Logger.MsSqlLogger
{
    internal class LoggerDbContext:DbContext
    {
        public LoggerDbContext() : base("LoggerDB")
        {

        }

        public DbSet<LogDataModel> LogData { get; set; }
    }
}
