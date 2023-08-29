using CommandSims.Entity.Npc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Data
{
    public class SimsContext : DbContext
    {

        public DbSet<Player> Players { get; set; }


        public string DbPath { get; }

        public SimsContext()
        {
            // todo 根据存档更换数据库
            DbPath = Path.Join(Environment.CurrentDirectory, "App_Data", "sims.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
