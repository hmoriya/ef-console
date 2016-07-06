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
    public class Program
    {

        // TODO create new IServiceCollection and IServiceProvider.  
        private static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection()
                                    .AddDbContext<TodoDbContext>(options =>
                                    {
                                        options.UseInMemoryDatabase();
                                        //options.UseSqlite("Filename=./Todo.db");
                                    });

            serviceCollection.AddTransient<TodoRepository, TodoRepository>();
            return serviceCollection.BuildServiceProvider();
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello EF Core /w with DI container !");

            var provider = ConfigureServices();

            // TODO get DbContext instance from DIContainer.
            var db = provider.GetService<TodoDbContext>();

            db.Todos.Add(new Todo() { IsDone = false, Text = "First Commit" });

            db.SaveChanges();

            foreach (var item in db.Todos)
            {
                Console.WriteLine($"[{item.Id}] {item.Text} : {item.IsDone}");
            }

            Console.WriteLine("Hello EF Core /w with Repository !");

            // TODO repository get by DIContainer.
            using (var repos = provider.GetService<TodoRepository>())
            {
                repos.Add(new Todo() { IsDone = false, Text = "Added by repostitory" });
                repos.Save();

                foreach(var item in repos.ListAll()){
                    
                    Console.WriteLine($"[{item.Id}] {item.Text} : {item.IsDone}");
                }
            }

        }
    }

    

   



    

}

