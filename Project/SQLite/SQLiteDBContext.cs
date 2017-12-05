using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;



namespace Project.EFClasses
{
    class SQLiteDBContext : HPlusSportsContextSQLite
    {
        //SQLite DB file path: AccessingExistingDatabasesWithEntityFrameworkCore\Project\SQLiteDB\HPlusSports.db
        private const string SQLiteFolder = "\\SQLiteDB\\HPlusSports.db"; 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"Filename={GetProjectPath() + SQLiteFolder}");
            }
        }

        /// <summary>
        /// Get full project path
        /// </summary>
        /// <returns></returns>
        private string GetProjectPath()
        {
            return Directory.GetCurrentDirectory();
        }
    }
}
