using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApplication
{
    public class Program
    {

        // TODO create new IServiceCollection and IServiceProvider.  
        private static IServiceProvider ConfigureServices() 
        {
            var serviceCollection = new ServiceCollection()
                                    .AddDbContext<TodoDbContext>(options => {
                                        options.UseInMemoryDatabase();
                                        //options.UseSqlite("Filename=./Todo.db");
                                    });

            return serviceCollection.BuildServiceProvider();
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello EF Core /w with DI container !");

            var provider = ConfigureServices();

            // TODO get DbContext instance from DIContainer.
            using(var db = provider.GetService<TodoDbContext>())
            {
                db.Todos.Add(new ToDo(){ IsDone = false,Text = "First Commit"});
                
                db.SaveChanges();

                foreach (var item in db.Todos)
                {
                    Console.WriteLine($"[{item.Id}] {item.Text} : {item.IsDone}");
                }

            }

        }
    }
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

