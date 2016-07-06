using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApplication
{
    public class TodoDbContext : DbContext
    {

        public TodoDbContext()
        {
        }

        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Filename=./Todo.db");
            }
        }
        public DbSet<Todo> Todos { get; set; }
    }

    [Table("Todos")]
    public class Todo
    {
        public int Id { get; set; }
        public bool IsDone { get; set; }
        public string Text { get; set; }
    }
}