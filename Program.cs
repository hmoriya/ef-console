using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using(var db = new TodoDbContext())
            {
                
                db.Todos.Add(new ToDo(){ IsDone = false,Text = "First Commit"});
                
                db.SaveChanges();

                foreach (var item in db.Todos)
                {
                    //Console.WriteLine("[{0}] {2} : Done {1}",item.Id.ToString(),item.IsDone,item.Text);
                    Console.WriteLine($"[{item.Id}] {item.Text} : {item.IsDone}");
                }

            }

        }
    }
    public class TodoDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./Todo.db");
        }
        public DbSet<ToDo> Todos { get; set; }
    }
    [Table("Todos")]
    public class ToDo
    {
        public int Id { get; set; }
        public bool IsDone { get; set; }
        public string Text { get; set; }
    }
}

